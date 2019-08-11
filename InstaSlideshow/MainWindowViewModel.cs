using NLog;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InstaSlideshow
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _username;
        private Image _instaImage;
        private AppSettings _settings;
        private ILogger _logger;

        public MainWindowViewModel(Image instaImage)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _settings = new AppSettings(_logger);

            if (_settings.Validate())
            {
                var manager = new SlideshowManager(_settings, _logger);
                manager.ImageUpdated += OnImageUpdated;
                _instaImage = instaImage;
                Username = "Loading...";
            }
            else
            {
                MessageBox.Show("Invalid application settings. Modify the app settings file to include valid username, password and hashtag.", 
                    "Invalid Settings", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error, 
                    MessageBoxResult.OK);
                Application.Current.Shutdown();
            }
        }

        public string Username
        {
            get => _username;

            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HeadingText => _settings.HeadingText.Value;

        private void OnImageUpdated(object sender, NewImageEvent e)
        {
            UpdateImage(e);
        }

        private void UpdateImage(NewImageEvent e)
        {
            _instaImage.ChangeSource(e.Image, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            Username = e.Username;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
