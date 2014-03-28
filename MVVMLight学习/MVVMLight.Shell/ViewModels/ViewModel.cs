using System.Linq;

using GalaSoft.MvvmLight;
using MVVMLight.Shell.Common.StorageHelper;
using PhoneClient.Adapters.Interfaces;
using System;

namespace MVVMLight.Shell.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        #region 字段

        private INavigationService navigationService;

        private Uri pageUri;

        #endregion

        #region 构造方法

        public ViewModel(INavigationService navigationService, Uri pageUri)
        {
            if (navigationService != null)
            {
                this.navigationService = navigationService;
                this.pageUri = pageUri;
                this.navigationService.Navigating += navigationService_Navigating;
                this.navigationService.Navigated += navigationService_Navigated;
            }
        }

        #endregion

        #region 私有方法

        private void navigationService_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            // 保存程序状态
            if (e.Uri.OriginalString == "app://external/")
            {
                OnPageDeactivation();
                IsolatedStorageHelper.SettingSave<bool>("tombstone", true);
            }
        }

        private void navigationService_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // 墓碑恢复
            if (e.Uri.OriginalString != "app://external/" && IsolatedStorageHelper.SettingLoad<bool>("tombstone") == true)
            {
                OnPageResumeFromTombstone();

                // 所有页面都从墓碑状态恢复之后删除独立存储中的墓碑标识
                if (this.navigationService.Frame.BackStack.Count() == 0)
                {
                    IsolatedStorageHelper.SettingRemove("tombstone");
                }
            }

            // 判断当前导航的页面与ViewModel是否对应
            if (this.pageUri.OriginalString == e.Uri.OriginalString)
            {
                OnNavigatedTo();
            }
        }

        #endregion

        #region 方法

        public virtual void OnPageDeactivation()
        {

        }

        public virtual void OnPageResumeFromTombstone()
        {

        }

        public virtual void OnNavigatedTo()
        {

        }

        #endregion

    }
}
