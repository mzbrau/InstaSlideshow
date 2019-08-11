using NLog;
using System;
using System.ComponentModel;
using System.Configuration;

namespace InstaSlideshow
{
    public class AppSettings
    {
        private ILogger _logger;

        public AppSettings(ILogger logger)
        {
            _logger = logger;

            HeadingText = new Lazy<string>(() => GetSettingValue<string>("HeadingText", "Heading"));
            Hashtag = new Lazy<string>(() => GetSettingValue<string>("Hashtag", "dog"));
            Username = new Lazy<string>(() => GetSettingValue<string>("Username"));
            Password = new Lazy<string>(() => GetSettingValue<string>("Password"));
            TransitionSpeedMs = new Lazy<int>(() => GetSettingValue<int>("TransitionSpeedMs", 5000));
            PagesCount = new Lazy<int>(() => GetSettingValue<int>("PagesCount", 5));
            StartDate = new Lazy<DateTime>(() => GetSettingValue<DateTime>("StartDate", DateTime.MinValue));
        }

        public Lazy<string> HeadingText { get; }

        public Lazy<string> Hashtag { get; }

        public Lazy<string> Username { get; }

        public Lazy<string> Password { get; }

        public Lazy<int> TransitionSpeedMs { get; }

        public Lazy<int> PagesCount { get; }

        public Lazy<DateTime> StartDate { get; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Username.Value) && 
                   !string.IsNullOrEmpty(Password.Value) && 
                   !string.IsNullOrEmpty(Hashtag.Value);
        }

        private T GetSettingValue<T>(string key, T defaultValue = default(T))
        {
            var value = ConfigurationManager.AppSettings[key];

            try
            {
                var result = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
                _logger.Info($"Setting {key} has value {result}");
                return result;
            }
            catch
            {
                _logger.Error($"Unable to parse {key} to type {typeof(T)}. Using default value {defaultValue}");
                return defaultValue;
            }
        }
    }
}
