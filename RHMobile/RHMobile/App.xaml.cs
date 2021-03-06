using System;
using XForms.views.Authentication;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XForms.views.Leave;
using XForms.views.Administration;
using XForms.Services;
using XForms.views.Walkthrough;
using XForms.views;
using Microsoft.AppCenter;
using XForms.Constants;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;

namespace XForms
{
    public partial class App : Application
    {
        public static bool CanRefreshHome = false;

        public static bool IsSetDynamicResources;

        public App()
        {
            InitializeComponent();
            if (Xamarin.Essentials.VersionTracking.IsFirstLaunchEver)
            {
                AppPreferences.ClearCache();

                MainPage = new NavigationPage(new SigninPage());
            }
            else
            {
                //AppHelpers.SetInitialView();
                MainPage = new NavigationPage(new SigninPage());



            }

        }


        protected override void OnStart()
        {
            try
            {
                #region AppCenter
                AppCenter.Start($"android={AppConstants.AndroidAppCenterKey};" +
                                 $"ios={AppConstants.iOSAppCenterKey}",
                                 typeof(Analytics), typeof(Crashes));
                #endregion

            }
            catch (Exception ex)
            {

            }
        }
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

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
