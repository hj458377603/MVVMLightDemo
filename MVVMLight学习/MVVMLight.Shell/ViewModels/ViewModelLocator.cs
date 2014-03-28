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
        #region ���캯��

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            /// �����Ƿ���ֵΪIServiceLocator��ί��
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

            //����д�������⣬App.RootFrame����Application_Launching����ִ�����֮����ֵ
            //navigationService = new PhoneNavigationService(App.RootFrame);
            //SimpleIoc.Default.Register<MainViewModel>(() =>
            //    new MainViewModel(navigationService), false);

            // ע�뵼������
            SimpleIoc.Default.Register<INavigationService>(() => new NavigationServiceAdapter(App.RootFrame));

            // ����ִ��˳��
            // 1.App���캯��
            // 2.ViewModelLocator���캯����App.xaml�е���Դ�����ViewModelLocator��
            // 3.App��Application_Launching����
            // 4.Navigate����(App.RootFrame��Ϊ��)
            // 5.ȡ�ö�Ӧ��ViewModel(MainViewModel)��ִ�ж�Ӧ������ע���ί��
            // ��ȷд��,������ע��(Ĭ��ֵ)
            // �˴�ÿ��ViewModel�е�navigationService��ͬ���ɿ��ǸĽ����ʺ�ʹ�õ���ģʽ��
            SimpleIoc.Default.Register<MainViewModel>(() =>
                new MainViewModel(NavigationService));

            // Ĺ���ָ���ʱ��navigationServiceΪ��
            SimpleIoc.Default.Register<Page1ViewModel>(() =>
                new Page1ViewModel(NavigationService), false);

            SimpleIoc.Default.Register<Page2ViewModel>(() =>
            new Page2ViewModel(NavigationService));
        }

        #endregion

        #region ����

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