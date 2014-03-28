using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Controls;

namespace MVVMLight.Shell.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            var gt = btn.TransformToVisual(this);
            Point p = gt.Transform(new Point(0, 0));
        }
    }
}