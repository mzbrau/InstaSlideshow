using System;
using System.Windows.Media.Imaging;

namespace InstaSlideshow
{
    public class NewImageEvent : EventArgs
    {
        public NewImageEvent(BitmapImage image, string username)
        {
            Image = image;
            Username = username;
        }

        public BitmapImage Image { get; }

        public string Username { get; }
    }
}
