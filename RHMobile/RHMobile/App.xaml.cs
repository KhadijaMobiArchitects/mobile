using System;
using XForms.views.Authentication;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XForms.views.Leave;
using XForms.views.DashBoard;
using XForms.Services;

namespace XForms
{
    public partial class App : Application
    {

        //Design patterns : Singleton

        private static AppServices _appServices;
        public static AppServices AppServices
        {
            get
            {
                
                    if (_appServices != null)
                    {
                        return _appServices;
                    }
                    else
                    {
                        _appServices = new AppServices();
                        return _appServices;
                    }
                
            }
            set
            {               
                    _appServices = value;
                
            }
        }

        public App()
        {
            InitializeComponent();

            Device.SetFlags(new string[] { "CollectionView_Experimental", "SwipeView_Experimental", "Shapes_Experimental", "FastRenderers_Experimental", "Brush_Experimental", "RadioButton_Experimental" });

            MainPage = new NavigationPage(new LeaveRequest());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
