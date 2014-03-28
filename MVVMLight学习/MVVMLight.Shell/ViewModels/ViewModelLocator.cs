/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MVVMLight.Shell"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using PhoneClient.Adapters.Implements;
using PhoneClient.Adapters.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MVVMLight.Shell.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region 构造函数

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            /// 参数是返回值为IServiceLocator的委托
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            //这种写法有问题，App.RootFrame会在Application_Launching方法执行完成之后有值
            //navigationService = new PhoneNavigationService(App.RootFrame);
            //SimpleIoc.Default.Register<MainViewModel>(() =>
            //    new MainViewModel(navigationService), false);

            // 注入导航服务
            SimpleIoc.Default.Register<INavigationService>(() => new NavigationServiceAdapter(App.RootFrame));

            // 方法执行顺序：
            // 1.App构造函数
            // 2.ViewModelLocator构造函数（App.xaml中的资源添加了ViewModelLocator）
            // 3.App的Application_Launching方法
            // 4.Navigate方法(App.RootFrame不为空)
            // 5.取得对应的ViewModel(MainViewModel)，执行对应的依赖注入的委托
            // 正确写法,不立即注入(默认值)
            // 此处每个ViewModel中的navigationService相同（可考虑改进，适合使用单例模式）
            SimpleIoc.Default.Register<MainViewModel>(() =>
                new MainViewModel(NavigationService));

            // 墓碑恢复的时候navigationService为空
            SimpleIoc.Default.Register<Page1ViewModel>(() =>
                new Page1ViewModel(NavigationService), false);

            SimpleIoc.Default.Register<Page2ViewModel>(() =>
            new Page2ViewModel(NavigationService));
        }

        #endregion

        #region 方法

        private INavigationService NavigationService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<INavigationService>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public Page1ViewModel Page1
        {
            get
            {
                return SimpleIoc.Default.GetInstance<Page1ViewModel>();
            }
        }

        public Page2ViewModel Page2
        {
            get
            {
                return SimpleIoc.Default.GetInstance<Page2ViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        #endregion
    }
}