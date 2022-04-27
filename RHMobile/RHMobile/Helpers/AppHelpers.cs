using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XForms.Interfaces;
using XForms.Popups;
using XForms.views.Administration;
using XForms.views.Authentication;

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

        public static Color LookupColor(string key)
        {
            try
            {
                Application.Current.Resources.TryGetValue(key, out var newColor);
                return (Color)newColor;
            }
            catch
            {
                return Color.White;
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

        public static void SetInitialView()
        {
            try
            {
                //    if (!App.Current.Resources.ContainsKey("MainPageStyle")
                //|| !App.Current.Resources.ContainsKey("MainGridStyle"))
                //    {

                //        //Set Top Padding
                //        var mainPageStyle = new Style(typeof(ScrollView))
                //        {
                //            Setters = {
                //        new Setter {
                //            Property = Grid.PaddingProperty,
                //            Value =  new Thickness(0, 10, 0, 0)
                //        }
                //    }
                //        };

                //        var mainGridStyle = new Style(typeof(Grid))
                //        {
                //            Setters = {
                //        new Setter {
                //            Property = Grid.PaddingProperty,
                //            Value =  new Thickness(0, 10, 0, 0)
                //        }
                //    }
                //        };

                //        App.Current.Resources.Add("MainPageStyle", mainPageStyle);
                //        App.Current.Resources.Add("MainGridStyle", mainGridStyle);

                //        App.IsSetDynamicResources = false;
                //    }

                if (!App.Current.Resources.ContainsKey("MainPageStyle")
                || !App.Current.Resources.ContainsKey("MainGridStyle")
                || !App.IsSetDynamicResources)
                {
                    SetDynamicResources();
                }

                if (
              (!string.IsNullOrEmpty(AppPreferences.Token)))
                {
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                }
            }
            catch (Exception ex)
            {
                //Microsoft.AppCenter.Crashes.Crashes.TrackError(ex);

                Application.Current.MainPage = new NavigationPage(new SigninPage());
            }
        }

        public static void SetDynamicResources()
        {
            #region Dynamic Resources
            int topPadding = 10;

            if (Device.RuntimePlatform == Device.iOS)
            {
                var isHasNotchScreen = AppHelpers.CheckHasNotchScreen();

                topPadding = isHasNotchScreen ? 40 : 30;
            }

            //Set Top Padding
            //var mainPageStyle = new Style(typeof(ScrollView))
            //{
            //    Setters = {
            //        new Setter {
            //            Property = Grid.PaddingProperty,
            //            Value =  new Thickness(0, topPadding, 0, 0)
            //        }
            //    }
            //};

            var mainGridStyle = new Style(typeof(Grid))
            {
                Setters = {
                    new Setter {
                        Property = Grid.PaddingProperty,
                        Value =  new Thickness(0, topPadding, 0, 0)
                    }
                }
            };

            if (App.Current.Resources.ContainsKey("MainPageStyle"))
            {
                App.Current.Resources.Remove("MainPageStyle");
            }

            if (App.Current.Resources.ContainsKey("MainGridStyle"))
            {
                App.Current.Resources.Remove("MainGridStyle");
            }

            //App.Current.Resources.Add("MainPageStyle", mainPageStyle);
            App.Current.Resources.Add("MainGridStyle", mainGridStyle);

            App.IsSetDynamicResources = true;
            #endregion
        }
        public static bool CheckHasNotchScreen()
        {
            bool isHasNotchScreen;

            if (!AppPreferences.IsAleardyCheckHasNotchScreen)
            {
                isHasNotchScreen = DependencyService.Get<INotchScreen>().CheckHasNotchScreen();
                AppPreferences.IsAleardyCheckHasNotchScreen = true;
                AppPreferences.IsHasNotchScreen = isHasNotchScreen;
            }
            else
            {
                isHasNotchScreen = AppPreferences.IsHasNotchScreen;
            }

            return isHasNotchScreen;
        }

    }
}
