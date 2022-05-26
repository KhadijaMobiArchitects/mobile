using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XForms.Models;
using XForms.Services;

namespace XForms.ViewModels
{
    public class DisplacementViewModel : BaseViewModel
    {
        public Xamarin.Forms.GoogleMaps.Map map { get; set; }
        public ContentView contentView { get; set; }
        public List<PositionModel> ListPositions { get; set; }

        public string StartPositionAddress { get; set; }
        public string EndPositionAddress { get; set; }

        public PositionModel StartPosition { get; set; }
        public PositionModel EndPosition { get; set; }


        public DisplacementViewModel()
        {
            map = new Xamarin.Forms.GoogleMaps.Map();

            StartPosition = new PositionModel()
            {
                Latitude = "33.54428444238301",
                Longitude = "-7.639737568139186"


            };

            EndPosition = new PositionModel()
            {
                Latitude = "33.54428444208300",
                Longitude = "-7.639737560139185"
            };

            contentView = new ContentView()
            {
                Content = map,
            };
            map.CameraIdled += async (sender, e) =>
            {
                var p = e.Position;

                //var text = $"CameraIdled:Lat={p.Target.Latitude:0.00}, Long={p.Target.Longitude:0.00}, Zoom={p.Zoom:0.00}, Bearing={p.Bearing:0.00}, Tilt={p.Tilt:0.00}";
                ////labelStatus.Text = text;
                //System.Diagnostics.Debug.WriteLine(text);

                EndPositionAddress = await AppHelpers.GatGeocoder(p.Target.Latitude, p.Target.Longitude);
                var endPosition = new PositionModel()
                {
                    Latitude = Convert.ToString(p.Target.Latitude),
                    Longitude = Convert.ToString(p.Target.Longitude)
                };
                EndPosition = endPosition;

                map.Polylines.Clear();

                var pathcontent = await LoadRoute(StartPosition, EndPosition);

                var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
                polyline.StrokeColor = AppHelpers.LookupColor("Primary");
                polyline.StrokeWidth = 6;

                foreach (var po in pathcontent)
                {
                    polyline.Positions.Add(po);

                }
                map.Polylines.Add(polyline);

            };

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            MoveMapToLocalPositionCommand.Execute(true);
 
        }

        private bool CanMoveMapToLocalPosition = true;
        public ICommand MoveMapToLocalPositionCommand => new Command<bool>(async (flag) =>
        {
            try
            {
                CanMoveMapToLocalPosition = false;

                if (map == null)
                    return;

                var status = await AppHelpers.CheckAndRequestLocationPermission();

                Location location = null;

                if (status == PermissionStatus.Granted)
                {

                    var lastKnownLocation = await Geolocation.GetLastKnownLocationAsync();

                    location = lastKnownLocation ?? await AppHelpers.GetCurrentLocation();

                    if (location != null)
                    {
                        AppPreferences.LastKnownLatitude = location.Latitude;
                        AppPreferences.LastKnownLongitude = location.Longitude;

                        map
                        .MoveToRegion
                      (MapSpan
                      .FromCenterAndRadius(
                          new Position(location.Latitude, location.Longitude),
                          Distance.FromKilometers(5)), false);
                    }
                }

                if (location == null)
                {
                    if (flag)
                    {
                        var answer = await AppHelpers.AcceptAlert(" APP_NAME", "Localizable.ENABLE_CURRENT_LOCATION", "Localizable.OK", "Localizable.CANCEL");

                        if (answer)
                        {
                            Xamarin.Essentials.AppInfo.ShowSettingsUI();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex);
            }
            finally
            {
                CanMoveMapToLocalPosition = true;
            }
        }, (_) => CanMoveMapToLocalPosition);

        internal async Task<System.Collections.Generic.List<Xamarin.Forms.GoogleMaps.Position>> LoadRoute(PositionModel StartPosition, PositionModel EndPosition)
        {
            var googleDirection = await ApiServices.ServiceClientInstance.GetDirections(StartPosition.Latitude, StartPosition.Longitude, EndPosition.Latitude, EndPosition.Longitude);
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                return positions;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Add your payment method inside the Google Maps console.", "Ok");
                return null;

            }

        }
    }
}
