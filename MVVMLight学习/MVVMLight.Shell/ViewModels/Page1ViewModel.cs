using GalaSoft.MvvmLight.Command;
using MVVMLight.Shell.Common.PageUri;
using MVVMLight.Shell.Common.StorageHelper;
using PhoneClient.Adapters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMLight.Shell.ViewModels
{
    public class Page1ViewModel : ViewModel
    {
        #region 字段

        private INavigationService navigationService;
        private string text;

        #endregion

        #region 属性

        public string Txt
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged(() => Txt);
            }
        }
        #endregion

        #region 命令

        public ICommand NavigateToPage2Command
        {
            get
            {
                return new RelayCommand(() => navigationService.NavigateTo("/Views/Page2.xaml"));
            }
        }

        #endregion

        #region 构造函数

        public Page1ViewModel(INavigationService navigationService)
            : base(navigationService, new Uri(PageUriConstant.PAGE1_VIEWPAGE, UriKind.Relative))
        {
            this.navigationService = navigationService;
        }

        #endregion

        #region Override

        public override void OnPageDeactivation()
        {
            IsolatedStorageHelper.SettingSave<string>("txt", Txt);
        }

        public override void OnPageResumeFromTombstone()
        {
            Txt = IsolatedStorageHelper.SettingLoad<string>("txt");
            IsolatedStorageHelper.SettingRemove("txt");
        }

        public override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
        }
        #endregion
    }
}
