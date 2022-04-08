using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XForms.Popups;

namespace XForms
{
    public static class AppHelpers
    {
        public static FFImageLoading.Svg.Forms.SvgImageSource GetSvgResource(string sourceName)
        {
            try
            {
                return FFImageLoading.Svg.Forms.SvgImageSource.FromResource("XForms.Resources.Images." + sourceName, typeof(AppHelpers).GetTypeInfo().Assembly);
            }
            catch
            {
                return default;
            }
        }

        public static Xamarin.Forms.ImageSource GetImageResource(string sourceName)
        {
            try
            {
                return ImageSource.FromResource("XForms.Resources.Images." + sourceName, typeof(AppHelpers).GetTypeInfo().Assembly);
            }
            catch
            {
                return default;
            }
        }

        public async static void LoadingShow()
        {
            try
            {
                var loadingPopup = new LoadingPopup();
                await PopupNavigation.Instance.PushSingleAsync(loadingPopup);
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
            }
        }

        public async static void LoadingHide()
        {
            try
            {
                foreach (var item in PopupNavigation.Instance.PopupStack)
                {
                    if (item.GetType() == typeof(XForms.Popups.LoadingPopup) && PopupNavigation.Instance.PopupStack.Count > 0)
                    {
                        await Task.Delay(250);
                        await Task.WhenAll(PopupNavigation.Instance.RemovePageAsync(item));
                    }
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
            }
        }

        //public static void Alert(string message = "", int durationInMs = 5000, Exception exception = default)
        //{
        //    if (exception != default)
        //    {
        //        //message = "Une erreur s'est produite";
        //        message = exception.Message;
        //    }

        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        ToastLength toastLength = durationInMs >= 4000 ? ToastLength.LONG : ToastLength.SHORT;

        //        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(async () =>
        //        {
        //            //    DependencyService.Get<IToast>().Alert(message, toastLength, false);


        //            if (Device.RuntimePlatform == Device.Android)
        //            {
        //                DependencyService.Get<IToast>().Alert(message, toastLength, false);
        //            }
        //            else
        //            {
        //                if (PopupNavigation.Instance.PopupStack.Any())
        //                {

        //                    foreach (var item in PopupNavigation.Instance.PopupStack)
        //                    {
        //                        if (item.GetType() == typeof(LoadingPopup) && PopupNavigation.Instance.PopupStack.Count > 0)
        //                        {
        //                            Device.BeginInvokeOnMainThread(async () =>
        //                            {
        //                                await Task.Delay(1000);

        //                                //DependencyService.Get<IToast>().Alert(message, toastLength, false);

        //                                //UserDialogs.Instance.Toast(new ToastConfig(message)
        //                                //{
        //                                //    Duration = TimeSpan.FromMilliseconds(durationInMs),
        //                                //    //BackgroundColor = System.Drawing.Color.FromArgb(90, 0, 0, 0)
        //                                //});

        //                                DependencyService.Get<IToast>().Alert(message, toastLength, false);
        //                            });

        //                            return;
        //                        }
        //                        else
        //                        {
        //                            DependencyService.Get<IToast>().Alert(message, toastLength, false);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    DependencyService.Get<IToast>().Alert(message, toastLength, false);
        //                }
        //            }
        //        });
        //    }

        //    if (exception != default)
        //    {
        //        Crashes.TrackError(exception);
        //    }
        //}

        //public static async Task OkAlert(string tilte, string message, string cancelTitle, string confirmTitle = null)
        //{
        //    if (string.IsNullOrWhiteSpace(tilte))
        //        return;

        //    await Application.Current.MainPage.DisplayAlert(tilte, message, cancelTitle);
        //}

        //public static async Task<bool> AcceptAlert(string tilte, string message, string acceptTitle, string cancelTitle)
        //{
        //    var answer = await Application.Current.MainPage.DisplayAlert(tilte, message, acceptTitle, cancelTitle);

        //    return answer;
        //}

    }
}
