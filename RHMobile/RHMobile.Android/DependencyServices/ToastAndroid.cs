using Android.Content;
using Android.Net;
using XForms.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(XForms.Droid.DependencyServices.ToastAndroid))]
namespace XForms.Droid.DependencyServices
{
    public class ToastAndroid : IToast
    {
        public void Alert(string message, ToastLength toastLength, bool showActionButtons)
        {
            var context = Plugin.CurrentActivity.CrossCurrentActivity.Current.AppContext;

            Android
                .Widget
                .Toast
                .MakeText(context, message, (Android.Widget.ToastLength)System.Enum.ToObject(typeof(Android.Widget.ToastLength), (int)toastLength))
                .Show();

            //Intent intent = new Intent("");
            //Uri uri = Uri.FromParts("package", "", null);
            //intent.SetData(uri);
            //context.StartActivity(intent);
        }
    }
}