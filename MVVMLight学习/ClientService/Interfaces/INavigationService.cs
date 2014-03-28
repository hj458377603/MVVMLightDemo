using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace PhoneClient.Adapters.Interfaces
{
    public interface INavigationService
    {
        #region 属性

        PhoneApplicationFrame Frame
        {
            get;
        }

        #endregion

        #region 事件

        event NavigatedEventHandler Navigated;
        event NavigatingCancelEventHandler Navigating;

        #endregion

        #region 方法

        void NavigateTo(string url);

        #endregion
    }
}
