using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InstaSlideshow
{
    class InstaWrapper
    {
        private ILogger _logger;
        private AppSettings _settings;
        private const string stateFile = "state.bin";
        private IInstaApi _instaApi;

        public InstaWrapper(AppSettings settings, ILogger logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public async Task<bool> Login()
        {
            var userSession = new UserSessionData
            {
                UserName = _settings.Username.Value,
                Password = _settings.Password.Value
            };

            var delay = RequestDelay.FromSeconds(2, 2);
            // create new InstaApi instance using Builder
            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(InstaSharper.Logger.LogLevel.Exceptions)) // use logger for requests and debug messages
                .SetRequestDelay(delay)
                .Build();

            LoadState();

            if (!_instaApi.IsUserAuthenticated)
            {
                // login
                _logger.Info($"Logging in as {userSession.UserName}");
                delay.Disable();
                var logInResult = await _instaApi.LoginAsync();
                delay.Enable();
                if (!logInResult.Succeeded)
                {
                    _logger.Info($"Unable to login: {logInResult.Info.Message}");
                    return false;
                }
            }

            SaveState();
            _logger.Info($"Login successful.");
            return true;
        }

        public async Task<List<InstaImage>> GetMedia()
        {
            _logger.Info($"Getting images for hashtag {_settings.Hashtag.Value} (requesting {_settings.PagesCount.Value} pages)");
            var tagFeed = await _instaApi.GetTagFeedAsync(_settings.Hashtag.Value,
                                                          PaginationParameters.MaxPagesToLoad(_settings.PagesCount.Value));
            if (tagFeed.Succeeded)
            {
                var images = tagFeed.Value.Medias.Where(x => x.Images != null)
                    .Where(x => x.Images.Any())
                    .Where(x => x.TakenAt > _settings.StartDate.Value)
                    .Select(s => new InstaImage()
                        {
                            Url = s.Images.First().URI,
                            User = s.User?.FullName ?? "Unknown"
                        }).ToList();

                var carouselMedias = tagFeed.Value.Medias.Where(x => x.Carousel != null && x.Carousel.Any())
                    .Where(s => s.TakenAt > _settings.StartDate.Value);

                foreach (var media in carouselMedias)
                {
                    if (media.TakenAt > _settings.StartDate.Value)
                    {
                        foreach (var item in media.Carousel)
                        {
                            if (item.Images.Any())
                            {
                                images.Add(new InstaImage()
                                {
                                    Url = item.Images.First().URI,
                                    User = media.User?.FullName ?? "Unknown"
                                });
                            }
                        }
                    }
                }

                return images;
            }

            _logger.Error($"Error when getting pictures for hashtag {tagFeed.Info.Message}");
            return new List<InstaImage>();
        }

        public bool IsLoggedIn()
        {
            if (_instaApi == null)
            {
                return false;
            }

            return _instaApi.IsUserAuthenticated;
        }

        private void LoadState()
        {
            const string stateFile = "state.bin";
            try
            {
                if (File.Exists(stateFile))
                {
                    Console.WriteLine("Loading state from file");
                    using (var fs = File.OpenRead(stateFile))
                    {
                        _instaApi.LoadStateDataFromStream(fs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SaveState()
        {
            var state = _instaApi.GetStateDataAsStream();
            using (var fileStream = File.Create(stateFile))
            {
                state.Seek(0, SeekOrigin.Begin);
                state.CopyTo(fileStream);
            }
        }
    }             
}


