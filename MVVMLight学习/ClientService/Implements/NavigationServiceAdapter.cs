using System;
using System.Windows.Navigation;

using Microsoft.Phone.Controls;
using PhoneClient.Adapters.Interfaces;

namespace PhoneClient.Adapters.Implements
{
    public class NavigationServiceAdapter : INavigationService
    {
        #region 字段

        private PhoneApplicationFrame frame;

        #endregion

        #region 事件

        public event NavigatedEventHandler Navigated;
        public event NavigatingCancelEventHandler Navigating;

        #endregion

        #region 构造函数

        public NavigationServiceAdapter(PhoneApplicationFrame frame)
        {
            this.frame = frame;
            this.frame.Navigating += frame_Navigating;
            this.frame.Navigated += frame_Navigated;
        }

        #endregion

        #region 方法

        void INavigationService.NavigateTo(string url)
        {
            if (this.frame != null)
            {
                this.frame.Navigate(new Uri(url, UriKind.Relative));
            }
        }

        void frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (Navigating != null)
            {
                Navigating(sender, e);
            }
        }

        void frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (Navigated != null)
            {
                Navigated(sender, e);
            }
        }

        #endregion

        PhoneApplicationFrame INavigationService.Frame
        {
            get
            {
                return frame;
            }
        }
    }
}
