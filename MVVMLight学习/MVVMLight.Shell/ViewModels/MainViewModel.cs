using PhoneClient.Adapters.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using MVVMLight.Shell.Common.PageUri;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using Microsoft.Phone.Tasks;
using System.IO;
using System.Net.Http;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Info;
using System.Collections.Generic;
using MVVMLight.Shell.Entities;
using Microsoft.Devices;

namespace MVVMLight.Shell.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModel
    {
        #region �ֶ�

        private INavigationService navigationService;

        private Stream imgStream;

        private string fileName;

        private string goodsName;

        private string detail;

        private string locationName;

        private Geolocator geolocator;

        private string latitude;

        private string longtitude;

        private string locationPos;

        private GoodsType selectedType;
        #endregion

        #region ����

        public string Title
        {
            get
            {
                return "MainView";
            }
        }

        public ObservableCollection<string> Names
        {
            get
            {
                return new ObservableCollection<string>()
                {
                    "Tom",
                    "Tony",
                    "Tim"
                };
            }
        }

        public string GoodsName
        {
            get
            {
                return goodsName;
            }
            set
            {
                goodsName = value;
                RaisePropertyChanged(() => GoodsName);
            }
        }

        public string Detail
        {
            get
            {
                return detail;
            }
            set
            {
                detail = value;
                RaisePropertyChanged(() => Detail);
            }
        }

        public string LocationName
        {
            get
            {
                return locationName;
            }
            set
            {
                locationName = value;
                RaisePropertyChanged(() => LocationName);
            }
        }

        public ObservableCollection<GoodsType> GoodsTypes
        {
            get;
            set;
        }

        public GoodsType SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                selectedType = value;
                RaisePropertyChanged(() => SelectedType);
            }
        }
        #endregion

        #region ����

        public ICommand NavigateToPage1Command
        {
            get
            {
                return new RelayCommand(() =>
                {
                    navigationService.NavigateTo("/Views/Page1.xaml");
                });
            }
        }

        /// <summary>
        /// ѡ��ͼƬ
        /// </summary>
        public ICommand OpenPhotoTaskCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    PhotoChooserTask task = new PhotoChooserTask();
                    task.Show();
                    task.Completed += new EventHandler<PhotoResult>((sender, e) =>
                    {
                        imgStream = e.ChosenPhoto;
                        fileName = e.OriginalFileName;
                    });
                });
            }
        }

        public ICommand CameraCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    PhotoCamera cam = new PhotoCamera(CameraType.Primary);
                    cam.Resolution = new Size(800, 800);
                    cam.CaptureImageAvailable += cam_CaptureImageAvailable;
                    //CameraCaptureTask task = new CameraCaptureTask();
                    //task.Show();
                    //task.Completed += ((sender, e) =>
                    //{
                    //    imgStream = e.ChosenPhoto;
                    //    fileName = e.OriginalFileName;
                    //});
                });
            }
        }

        void cam_CaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {

        }

        /// <summary>
        /// �ύ��Ϣ
        /// </summary>
        public ICommand SubmitCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    using (imgStream)
                    {
                        MultipartFormDataContent content = new MultipartFormDataContent("o9qweadc");

                        StringContent stringContent = new StringContent(
                            "{userId:1,goodsTypeId:" + SelectedType.Value + ",goodsName:'" +
                            GoodsName + "',detail:'" + Detail
                            + "',deviceOSId:1,deviceName:'Windows Phone',locationName:'" + LocationName + "',locationPos:'118.92920694397,32.12299389912'}", System.Text.Encoding.UTF8);
                        content.Add(stringContent, "model");
                        StreamContent streamContent = new StreamContent(imgStream);
                        content.Add(streamContent, fileName, fileName);
                        System.Net.Http.HttpClient client = new HttpClient();

                        HttpResponseMessage responseMsg = await client.PostAsync("http://10.21.134.37/api/IndexApi/Insert", content);
                        HttpContent res = responseMsg.Content;
                        string resp = await res.ReadAsStringAsync();

                        MessageBox.Show(resp);
                    }
                });
            }
        }

        #endregion

        #region ���캯��

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
            : base(navigationService, new Uri(PageUriConstant.MAIN_VIEWPAGE, UriKind.Relative))
        {
            this.navigationService = navigationService;
            GoodsTypes = new ObservableCollection<GoodsType>()
                {
                    new GoodsType(){
                        Name="����",
                        Value=1
                    },
                    new GoodsType(){
                        Name="����",
                        Value=2
                    },
                    new GoodsType(){
                        Name="ͼ��",
                        Value=3
                    },
                    new GoodsType(){
                        Name="����",
                        Value=4
                    }
                };
            //geolocator = new Geolocator();

            //// �����ľ��ȼ���PositionAccuracy.Default �� PositionAccuracy.High��
            //geolocator.DesiredAccuracy = PositionAccuracy.High;

            //// ���������ݾ��ȣ��ף�
            //geolocator.DesiredAccuracyInMeters = 50;

            //// �ƶ����볬����ֵ�󣬴��� PositionChanged �¼�
            //geolocator.MovementThreshold = 100;

            //// ������λ�ø��µ�ʱ����м䣬����λ�����ݵ���С��������룩
            //geolocator.ReportInterval = 0;

            //// λ�ø���ʱ�������¼�
            //geolocator.PositionChanged += geolocator_PositionChanged;

            //// λ�÷����״̬�����ı�ʱ�������¼�
            //geolocator.StatusChanged += geolocator_StatusChanged;

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        #endregion

        #region ����

        public async override void OnNavigatedTo()
        {
            // ��ȡλ����Ϣ
            //Geoposition geoposition = await geolocator.GetGeopositionAsync();
        }

        // λ�÷����״̬�仯��
        void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            // ��ȡλ�÷����״̬
            PositionStatus status = geolocator.LocationStatus;

            // ��ȡλ�÷����״̬
            status = args.Status;

            switch (args.Status)
            {
                case PositionStatus.Disabled: // λ���ṩ�����ѽ��ã����û���δ����Ӧ�ó������λ�õ�Ȩ��
                    break;
                case PositionStatus.Initializing: // ��ʼ����
                    break;
                case PositionStatus.NoData: // ����Ч����
                    break;
                case PositionStatus.Ready: // �Ѿ�׼�������������
                    break;
                case PositionStatus.NotAvailable: // λ�÷��񴫸���������
                    break;
                case PositionStatus.NotInitialized: // ��δ��ʼ��
                    break;
            }
        }

        // λ�ñ仯��
        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                Geoposition geoposition = args.Position;

                latitude = geoposition.Coordinate.Latitude.ToString("0.00");

                longtitude = geoposition.Coordinate.Longitude.ToString("0.00");

                locationPos = geoposition.Coordinate.Longitude + "," + geoposition.Coordinate.Latitude;
                if (geoposition.CivicAddress != null)
                {
                    LocationName = geoposition.CivicAddress.City;
                }
            });
        }
        #endregion

    }
}