using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Shell;
using MVVMLight.Shell.Common.PageUri;
using MVVMLight.Shell.Common.StorageHelper;
using PhoneClient.Adapters.Implements;
using PhoneClient.Adapters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMLight.Shell.ViewModels
{
    public class Page2ViewModel : ViewModel
    {
        #region 字段

        #endregion

        #region 命令

        /// <summary>
        /// 创建磁贴
        /// </summary>
        public ICommand CreateTileCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (ShellTile.ActiveTiles.Where(item => item.NavigationUri.OriginalString == "/Views/Page2.xaml?DeepLink=true").Count() > 0)
                    {
                        MessageBox.Show("已经添加了磁贴");
                        return;
                    }

                    StandardTileData newTile = new StandardTileData
                    {
                        Title = "Page2次要磁贴",
                        //BackgroundImage = new Uri("TileBackgroundBlue.png", UriKind.Relative),
                        Count = 2,
                        BackTitle = "App Back Tile Title",
                        //BackBackgroundImage = new Uri("TileBackgroundGreen.png", UriKind.Relative),
                        BackContent = "App Back Tile Content" + Environment.NewLine + "OK"
                    };

                    //shellTileService.Update(newTile);                    
                    ShellTile.Create(new Uri("/Views/Page2.xaml?DeepLink=true", UriKind.Relative), newTile);
                });
            }
        }

        /// <summary>
        /// 删除磁贴
        /// </summary>
        public ICommand DeleteTileCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var shellTile = ShellTile.ActiveTiles.First(item => item.NavigationUri.OriginalString == "/Views/Page2.xaml?DeepLink=true");
                    shellTile.Delete();
                });
            }
        }

        #endregion

        #region 构造方法

        public Page2ViewModel(INavigationService navigationService)
            : base(navigationService, new Uri(PageUriConstant.PAGE2_VIEWPAGE, UriKind.Relative))
        {
        }

        #endregion

        #region Override

        public override void OnPageDeactivation()
        {
            IsolatedStorageHelper.SettingSave<string>("txt", "");
        }

        public override void OnPageResumeFromTombstone()
        {
            base.OnPageResumeFromTombstone();
        }

        public override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
        }
        #endregion
        
    }
}
