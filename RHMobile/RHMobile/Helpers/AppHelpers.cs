using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XForms.Interface;
using XForms.Interfaces;
using XForms.Popups;
using XForms.views.Administration;
using XForms.views.Authentication;
using Microsoft.AppCenter.Crashes;
using XForms.Enum;
using System.Net;
using Acr.UserDialogs;
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

        public static void Alert(string message = "", int durationInMs = 5000, Exception exception = default)
        {
            if (exception != default)
            {
                //message = "Une erreur s'est produite";
                message = exception.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                ToastLength toastLength = durationInMs >= 4000 ? ToastLength.LONG : ToastLength.SHORT;

                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(async () =>
                {
                    //    DependencyService.Get<IToast>().Alert(message, toastLength, false);


                    if (Device.RuntimePlatform == Device.Android)
                    {
                        DependencyService.Get<IToast>().Alert(message, toastLength, false);
                    }
                    else
                    {
                        if (PopupNavigation.Instance.PopupStack.Any())
                        {

                            foreach (var item in PopupNavigation.Instance.PopupStack)
                            {
                                if (item.GetType() == typeof(LoadingPopup) && PopupNavigation.Instance.PopupStack.Count > 0)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await Task.Delay(1000);

                                        //DependencyService.Get<IToast>().Alert(message, toastLength, false);

                                        //UserDialogs.Instance.Toast(new ToastConfig(message)
                                        //{
                                        //    Duration = TimeSpan.FromMilliseconds(durationInMs),
                                        //    //BackgroundColor = System.Drawing.Color.FromArgb(90, 0, 0, 0)
                                        //});

                                        DependencyService.Get<IToast>().Alert(message, toastLength, false);
                                    });

                                    return;
                                }
                                else
                                {
                                    DependencyService.Get<IToast>().Alert(message, toastLength, false);
                                }
                            }
                        }
                        else
                        {
                            DependencyService.Get<IToast>().Alert(message, toastLength, false);
                        }
                    }
                });
            }

            if (exception != default)
            {
                Crashes.TrackError(exception);
            }
        }

        public static async Task OkAlert(string tilte, string message, string cancelTitle, string confirmTitle = null)
        {
            if (string.IsNullOrWhiteSpace(tilte))
                return;

            await Application.Current.MainPage.DisplayAlert(tilte, message, cancelTitle);
        }

        public static async Task<bool> AcceptAlert(string tilte, string message, string acceptTitle, string cancelTitle)
        {
            var answer = await Application.Current.MainPage.DisplayAlert(tilte, message, acceptTitle, cancelTitle);

            return answer;
        }


        public static void SetInitialView()
        {
            try
            {
                if (
              (!string.IsNullOrEmpty(AppPreferences.Token)))
                {


                    if (AppPreferences.UserRole.Equals(Roles.Collaborateur)
                        || AppPreferences.UserRole.Equals(Roles.Stagiaire)
                        || AppPreferences.UserRole.Equals(Roles.Chef_projet))
                        Application.Current.MainPage = new NavigationPage(new HomePage());

                    else if (AppPreferences.UserRole.Equals(Roles.Manager)
                             || AppPreferences.UserRole.Equals(Roles.Responsable_RH))
                        Application.Current.MainPage = new NavigationPage(new HomeAdminPage());

                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new SigninPage());
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

        public async static Task<MediaFile> TakePhoto(string fileName = "")
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //Alert("Caméra Introuvable !");
                return null;
            }

            StoreCameraMediaOptions storeCameraMediaOptions;

            if (!string.IsNullOrEmpty(fileName))
            {
                storeCameraMediaOptions = new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    AllowCropping = true,
                    RotateImage = true,
                    PhotoSize = PhotoSize.Medium,
                    DefaultCamera = CameraDevice.Rear,
                    Name = fileName
                };
            }
            else
            {
                storeCameraMediaOptions = new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    AllowCropping = true,
                    RotateImage = true,
                    PhotoSize = PhotoSize.Small,
                    DefaultCamera = CameraDevice.Rear,
                };
            }

            var file = await CrossMedia.Current.TakePhotoAsync(storeCameraMediaOptions);
            return file;
        }

        public async static Task<MediaFile> PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                //Alert("Autorisation non accordée aux photos");
                return null;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                RotateImage = true,
                PhotoSize = PhotoSize.Small
            });

            return file;
        }

        public async static Task<MediaFile> TakeOrPickPhoto()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Sélectionner une photo", "Annulé", null, "Caméra", "Galerie");

            MediaFile file = null;

            if (action == "Caméra")
            {
                file = await TakePhoto();
            }
            else if (action == "Galerie")
            {
                file = await PickPhoto();
            }
            else if (action == "Annulé")
            {
                file = default;
            }

            return file;
        }
        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    stream.Dispose();
                    stream.Close();
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                //Alert(message: ex.Message, exception: ex);
                stream.Dispose();
                stream.Close();
                return null;
            }
            finally
            {

            }
        }

        //public static async Task DownloadFileAndOpenLocalFilePath(Models.File fileModel)
        //{
        //    if (fileModel == null) return;

        //    //var progress = ProgressDonut("Chargement");

        //    try
        //    {
        //        //if (!IsConnected())
        //        //{
        //        //    Alert("Vous n'êtes pas connéctés !");
        //        //    return;
        //        //}

        //        var fileExtension = ".jpg";

        //        var fileUrl = fileModel.URL.Split('?').FirstOrDefault();

        //        if (!string.IsNullOrEmpty(fileUrl))
        //        {
        //            fileExtension = Path.GetExtension(fileUrl);
        //        }

        //        var fileName = Path.GetFileNameWithoutExtension(fileModel.Name).Replace(" ", "");

        //        if (string.IsNullOrWhiteSpace(fileExtension))
        //        {
        //            Alert("N'ont pas d'extension de fichier");
        //            return;
        //        }

        //        var loweredExtension = fileExtension.ToLower().Replace(".", string.Empty);

        //        using (var client = new WebClient())
        //        {
        //            client.Headers.Clear();
        //            client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + AppPreferences.Token);

        //            //client.DownloadProgressChanged += async (s, e) =>
        //            //{
        //            //    progress.PercentComplete = e.ProgressPercentage;
        //            //    if (Device.RuntimePlatform == Device.iOS)
        //            //    {
        //            //        progress.Title = $"{Environment.NewLine}{e.ProgressPercentage}%";
        //            //    }
        //            //    if (e.ProgressPercentage == 100)
        //            //    {
        //            //        progress.Hide();
        //            //        progress.Dispose();
        //            //    }
        //            //};

        //            //progress.Show();

        //            var documentBytes = await client.DownloadDataTaskAsync(fileUrl);

        //            var appdataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), $"{fileName}.{loweredExtension}");

        //            using (Stream stream = new MemoryStream(documentBytes))
        //            {
        //                using (var file = System.IO.File.Create(appdataPath))
        //                {
        //                    await stream.CopyToAsync(file);
        //                }
        //            }

        //            var uri = new Uri(appdataPath);

        //            //Device.BeginInvokeOnMainThread(() =>
        //            //{
        //            //    if (Device.RuntimePlatform == Device.iOS)
        //            //    {
        //            //        DependencyService.Get<IOpenLocalPath>().OpenPath(fileName, uri.AbsolutePath);
        //            //    }
        //            //    else if (Device.RuntimePlatform == Device.Android)
        //            //    {
        //            //        DependencyService.Get<IOpenLocalPath>().OpenPath(fileName, uri.AbsolutePath);
        //            //    }
        //            //});
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //progress.Hide();
        //        //progress.Dispose();
        //        //Alert(ex.Message);
        //        return;
        //    }
        //    finally
        //    {
        //        //progress.Hide();
        //        //progress.Dispose();
        //    }
        //}

       //public async Task DownloadAttachement()
       // {
       //     try
       //     {
       //         AppHelpers.LoadingShow();

       //         var model = new AttachementModel
       //         {
       //             FileId = 1,
       //         };

       //         var result = StaticData.GetAttachement(model);

       //         await Xamarin.Essentials.Browser.OpenAsync(result.FileUrl);

       //         await Close();
       //     }
       //     catch (Exception ex)
       //     {

       //     }
       //     finally
       //     {
       //         AppHelpers.LoadingHide();
       //     }
       // }

        public static async Task DownloadPDF()
        {
            var webClient = new WebClient();

            var url = new Uri("https://st0rh0profils0dev.blob.core.windows.net/certificatedocs/1486ffa1-30e2-4cba-8706-8910b6c5817d637883047280952836-attestation-travail-intermediaire-146.pdf");
            await Xamarin.Essentials.Browser.OpenAsync(url);

            //webClient.DownloadDataAsync(url);

        }

    }
}
