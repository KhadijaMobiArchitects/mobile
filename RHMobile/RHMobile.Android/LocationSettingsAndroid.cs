using System;
using XForms.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(XForms.Droid.DependencyServices.LocationSettingsAndroid))]
namespace XForms.Droid.DependencyServices
{
    public class LocationSettingsAndroid : ILocationSettings
    {
        public void OpenLocationSettings()
        {
            try
            {
                var ctx = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.ApplicationContext;
                var intent = new Android.Content.Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                intent.AddFlags(Android.Content.ActivityFlags.NewTask);
                ctx.StartActivity(intent);
            }
            catch (Exception ex)
            {
                AppHelpers.Alert(exception: ex);
            }
        }
    }
}
