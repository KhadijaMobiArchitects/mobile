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
using Xamarin.Essentials;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Threading;

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
                Xamarin.Forms.Application.Current.Resources.TryGetValue(key, out var newColor);
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

            if (exception != null)
            {
                Debug.WriteLine($"-----------------------------");
                Debug.WriteLine($"Exception Message : {exception.Message}");
                Debug.WriteLine($"-----------------------------");
                Debug.WriteLine($"Exception Strack error : {exception.StackTrace}");
                Debug.WriteLine($"-----------------------------");

                Crashes.TrackError(exception);
            }
        }

        public static async Task OkAlert(string tilte, string message, string cancelTitle, string confirmTitle = null)
        {
            if (string.IsNullOrWhiteSpace(tilte))
                return;

            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(tilte, message, cancelTitle);
        }

        public static async Task<bool> AcceptAlert(string tilte, string message, string acceptTitle, string cancelTitle)
        {
            var answer = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(tilte, message, acceptTitle, cancelTitle);

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
                        Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new HomePage());

                    else if (AppPreferences.UserRole.Equals(Roles.Manager)
                             || AppPreferences.UserRole.Equals(Roles.Responsable_RH))
                        Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new HomeAdminPage());

                }
                else
                {
                    Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new SigninPage());
                }
            }
            catch (Exception ex)
            {
                //Microsoft.AppCenter.Crashes.Crashes.TrackError(ex);

                Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new SigninPage());
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
            string action = await Xamarin.Forms.Application.Current.MainPage.DisplayActionSheet("Sélectionner une photo", "Annulé", null, "Caméra", "Galerie");

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

 



        public static bool IsImageVisible { get; set; }

        public static async Task DownloadPDF()
        {
            var webClient = new WebClient();

            var url = new Uri("https://st0rh0profils0dev.blob.core.windows.net/certificatedocs/1486ffa1-30e2-4cba-8706-8910b6c5817d637883047280952836-attestation-travail-intermediaire-146.pdf");
            await Xamarin.Essentials.Browser.OpenAsync(url);

            //webClient.DownloadDataAsync(url);

        }

        public static string Text { get; set; }

        public static ImageSource Image { get; set; }

        public static async Task<FileResult> DoPickPdfAsync()
        {
            var options = new PickOptions
            {
                PickerTitle = "Please select a pdf",
                FileTypes = FilePickerFileType.Pdf,
            };

            var result= await PickAndShow(options);
            return result;
        }
        public static async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    var size = await GetStreamSizeAsync(result);

                    Text = $"File Name: {result.FileName} ({size:0.00} KB)";

                    var ext = Path.GetExtension(result.FileName).ToLowerInvariant();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        var stream = await result.OpenReadAsync();

                        Image = ImageSource.FromStream(() => stream);
                        IsImageVisible = true;
                    }
                    else
                    {
                        IsImageVisible = false;
                    }
                }
                else
                {
                    Text = $"Pick cancelled.";
                }

                return result;
            }
            catch (Exception ex)
            {
                Text = ex.ToString();
                IsImageVisible = false;
                return null;
            }
        }

       public static async Task<double> GetStreamSizeAsync(FileResult result)
        {
            try
            {
                using var stream = await result.OpenReadAsync();
                return stream.Length / 1024.0;
            }
            catch
            {
                return 0.0;
            }
        }
        public static Xamarin.Forms.GoogleMaps.BitmapDescriptor LoadBitmapDescriptors(string fileName, double width =0, double height=0, bool isAnimationPlaying = false)
        {
            try
            {

                var assembly = typeof(App).GetTypeInfo().Assembly;

                Xamarin.Forms.GoogleMaps.BitmapDescriptor descriptor;

                descriptor = Xamarin.Forms.GoogleMaps.BitmapDescriptorFactory.FromStream(GetResourceStream("XForms.Resources.Images." + fileName, assembly), id: fileName);

                return descriptor;
            }
            catch (Exception ex)
            {
                AppHelpers.Alert(exception: ex);

                return default;
            }
        }

        public static System.IO.Stream GetResourceStream(string fileName, Assembly assembly)
        {
            return assembly.GetManifestResourceStream($"{fileName}");
        }

        public async static Task<string> GatGeocoder(double latitude, double longitude)
        {
            try
            {
                var geoCoder = new Xamarin.Forms.GoogleMaps.Geocoder();

                var pinPosition = new Xamarin.Forms.GoogleMaps.Position(latitude, longitude);

                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(pinPosition);

                var address = possibleAddresses.FirstOrDefault();

                return address;
            }
            catch
            {
                return "";
            }
        }

        private static CancellationTokenSource cts;
        public static async Task<Xamarin.Essentials.Location> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {


                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    return location;
                }
                else
                {
                    return null;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                return null;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                var answer = await AppHelpers.AcceptAlert("", "ENABLE_GPS", "YES", "NO");

                if (answer)
                {
                    DependencyService.Get<ILocationSettings>().OpenLocationSettings();
                }

                return null;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                return null;
            }
            catch (Exception ex)
            {
                // Unable to get location
                return null;
            }
        }

        public static async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            return status;
        }

        public static int BusinessDaysUntil(this DateTime firstDay, DateTime lastDay)
        {
            DateTime[] bankHolidays = new DateTime[10];
            bankHolidays.Append(new DateTime(2022, 01, 01));
            bankHolidays.Append(new DateTime(2022, 01, 11));
            bankHolidays.Append(new DateTime(2022, 05, 01));
            bankHolidays.Append(new DateTime(2022, 07, 09));
            bankHolidays.Append(new DateTime(2022, 07, 10));
            bankHolidays.Append(new DateTime(2022, 07, 11));
            bankHolidays.Append(new DateTime(2022, 07, 29));
            bankHolidays.Append(new DateTime(2022, 07, 30));
            bankHolidays.Append(new DateTime(2022, 08, 14));
            bankHolidays.Append(new DateTime(2022, 08, 20));
            bankHolidays.Append(new DateTime(2022, 08, 21));
            bankHolidays.Append(new DateTime(2022, 10, 07));
            bankHolidays.Append(new DateTime(2022, 10, 08));
            bankHolidays.Append(new DateTime(2022, 10, 09));
            bankHolidays.Append(new DateTime(2022, 11, 06));
            bankHolidays.Append(new DateTime(2022, 11, 18));


            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay)
                    --businessDays;
            }

            return businessDays;
        }

    }

}
