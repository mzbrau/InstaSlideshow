using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace InstaSlideshow
{
    /// <summary>
    /// Enables the fade in and out transitions for the images.
    /// </summary>
    public static class ImageExtensions
    {
        public static void ChangeSource(this Image image, BitmapImage source, TimeSpan fadeOutTime, TimeSpan fadeInTime)
        {
            var fadeInAnimation = new DoubleAnimation(1d, fadeInTime);

            if (image.Source != null)
            {
                var fadeOutAnimation = new DoubleAnimation(0d, fadeOutTime);

                fadeOutAnimation.Completed += (o, e) =>
                {
                    image.Source = source;
                    image.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
                };

                image.BeginAnimation(Image.OpacityProperty, fadeOutAnimation);
            }
            else
            {
                image.Opacity = 0d;
                image.Source = source;
                image.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
            }
        }
    }
}
