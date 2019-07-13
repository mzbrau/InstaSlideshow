using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;

namespace InstaSlideshow
{
    public class SlideshowManager
    {
        private ILogger _logger;
        private AppSettings _settings;
        private Timer _imageUpdateTimer;
        private InstaWrapper _insta;
        private List<InstaImage> _images;
        private int _currentIndex;

        public SlideshowManager(AppSettings settings, ILogger logger)
        {
            _logger = logger;
            _settings = settings;
            _insta = new InstaWrapper(_settings, _logger);

            _imageUpdateTimer = new Timer(_settings.TransitionSpeedMs.Value);
            _imageUpdateTimer.Elapsed += OnImageUpdateTimerElapsed;

            Login();
        }

        public event EventHandler<NewImageEvent> ImageUpdated;

        private void OnImageUpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            NextAction();
        }

        private void Login()
        {
            var loggedIn = Task.Run(() => _insta.Login()).GetAwaiter().GetResult();

            if (loggedIn)
            {
                _imageUpdateTimer.Start();
            }
            else
            {
                _logger.Error("Login failed. Aborting slideshow.");
            }
        }

        private void NextAction()
        {
            try
            {
                _imageUpdateTimer.Stop();

                if (_images == null || _images.Count == _currentIndex)
                {
                    _logger.Info("Loading Images...");
                    _images = Task.Run(() => _insta.GetMedia()).GetAwaiter().GetResult();
                    _logger.Info($"Loaded {_images.Count} images");
                    _currentIndex = 0;
                }
                else
                {
                    var image = _images[_currentIndex++];
                    _logger.Debug($"Displaying image {_currentIndex} of {_images.Count} (user: {image.User})");

                    Application.Current.Dispatcher.Invoke(
                    () =>
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(image.Url, UriKind.Absolute);
                        bitmap.EndInit();

                        ImageUpdated?.Invoke(this, new NewImageEvent(bitmap, image.User));
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occured while trying to get or display images. {ex}");
            }
            finally
            {
                _imageUpdateTimer.Start();
            }
        }
    }
}
