using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XForms.Models;

namespace XForms.ViewModels
{
    public class DisplacementViewModel : BaseViewModel
    {
        public Xamarin.Forms.GoogleMaps.Map map { get; set; }
        public ContentView contentView { get; set; }
        public List<PositionModel> ListPositions { get; set; }
        public string EndPosition { get; set; }

        public DisplacementViewModel()
        {
            map = new Xamarin.Forms.GoogleMaps.Map();
            //Pin pintokyo = new Pin()
            //{
            //    Type = PinType.Place,
            //    Label = "Tokyo SKYTREE",
            //    Address = "Sumida-ku, Tokyo, Japan",
            //    Icon = AppHelpers.LoadBitmapDescriptors("profil.png"),
            //    Position = new Position(33.54316339818309, -7.640259716132234),
            //    Rotation = 33.3f,
            //    Tag = "id_tokyo",
            //    IsDraggable = true

            //};

            //map.Pins.Add(pintokyo);
            //map.PinDragStart += Map_PinDragStart;
            //map.PinDragEnd += Map_PinDragEnd;

            ListPositions = new List<PositionModel>()
            {
                new PositionModel(){Latitude = "33.547337102997076",Longitude = "-7.650029853066444"},
                new PositionModel(){Latitude = "33.543741749157554",Longitude = "-7.6531364429836914"},
                new PositionModel(){Latitude = "33.54315494483275",Longitude = "-7.640261390069967"},
            };

            //foreach (PositionModel item in ListPositions)
            //{
            //    Pin PositionPin = new Pin()
            //    {

            //        Type = PinType.Place,
            //        Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),
            //        Label = "Cars",
            //        //Icon = AppHelpers.LoadBitmapDescriptors("profil.png")

            //    };

            //    map.Pins.Add(PositionPin);


            //}
            //map.MoveToRegion(MapSpan.FromCenterAndRadius(pintokyo.Position, Distance.FromMeters(5000)));

            //map.MoveToRegion(MapSpan.FromCenterAndRadius(, Distance.FromMeters(5000)));

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

                EndPosition = await AppHelpers.GatGeocoder(p.Target.Latitude, p.Target.Longitude);

            };

        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            MoveMapToLocalPositionCommand.Execute(true);
        }

        //private void Map_PinDragStart(object sender, PinDragEventArgs e)
        //{

        //}

        //private async void Map_PinDragEnd(object sender, PinDragEventArgs e)
        //{
        //    var positons = new Position(e.Pin.Position.Latitude, e.Pin.Position.Longitude);
        //    map.MoveToRegion(MapSpan.FromCenterAndRadius(positons, Distance.FromMeters(5000)));
        //    await App.Current.MainPage.DisplayAlert("Alert", "Pick up location : Latitude :" + e.Pin.Position.Latitude + " Longitude : " + e.Pin.Position.Longitude, "OK");
        //    string adresse = await AppHelpers.GatGeocoder(e.Pin.Position.Latitude, e.Pin.Position.Longitude);
        //    await App.Current.MainPage.DisplayAlert("Adresse :", adresse, "OK");


        //}
        //
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
    }
}
