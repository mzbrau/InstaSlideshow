using System.Windows;
using System.Windows.Input;

namespace InstaSlideshow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isFullscreen = true;

        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainWindowViewModel(InstaImage);

            DataContext = vm;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && _isFullscreen)
            {
                RemoveFullScreen();
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                ToggleWindowState();
            }
        }

        private void ToggleWindowState()
        {
            if (_isFullscreen)
            {
                RemoveFullScreen();
            }
            else
            {
                MakeFullscreen();
            }
        }

        private void MakeFullscreen()
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            _isFullscreen = true;
        }

        private void RemoveFullScreen()
        {
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            _isFullscreen = false;
        }
    }
}
