using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XForms.ViewModels;

namespace XForms.views.Base
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            //IsTabStop = true;

            if (!App.Current.Resources.ContainsKey("MainPageStyle")
                 || !App.Current.Resources.ContainsKey("MainGridStyle")
                 || !App.IsSetDynamicResources)
            {
                AppHelpers.SetDynamicResources();
            }

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //  FIX: Clean stuck alerts
            //Xamarin.Forms.DependencyService.Get<IAlertCleaner>().CleanUnwantedAlerts();

            if (BindingContext is BaseViewModel bindingContext)
                bindingContext.OnAppearing();
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            //  FIX: Clean stuck alerts
            //Xamarin.Forms.DependencyService.Get<IAlertCleaner>().CleanUnwantedAlerts();

            if (BindingContext is BaseViewModel bindingContext)
                bindingContext.OnDisappearing();
        }
    }
}
