using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XForms.Constants;
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
        public GoogleMapsApiService googleMapsApiService = new GoogleMapsApiService();

        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places { get; set; }
        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces { get; set; } = new ObservableCollection<GooglePlaceAutoCompletePrediction>();

        public bool ShowRecentPlaces { get; set; }

        public string StartPlaceText { get; set; }
        public string EndPlaceText { get; set; }
        public int PinUp { get; set; }

        public GooglePlaceAutoCompletePrediction SelectedPlace { get; set; }

        public bool IsStartCollectionVisible { get; set; }
        public bool IsEndCollectionVisible {get; set;}

        public bool IsPickPointVisibile { get; set; }

        public bool IsStartPointClicked { get; set; }
        public bool IsEndPointClicked { get; set; }

        public bool IsPickIconVisible { get; set; }

        public DateTime DisplacementDate { get; set; }

        public string Client { get; set; }
        public string Motif { get; set; }

        public bool DownUp { get; set; } = true;
        public string DownUpGlyph { get; set; }

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

            map.CameraMoveStarted += Map_CameraMoveStarted;
            map.CameraIdled += Map_CameraIdled;

            DownUpGlyph = Resources.FontAwesomeFonts.Angledown;

        }


        private async void Map_CameraIdled(object sender, CameraIdledEventArgs e)
        {
            PinUp = 0;
            IsPickPointVisibile = false;
            MessagingCenter.Send<DisplacementViewModel, int>(this, AppConstants.SendPipUp, PinUp);

            var p = e.Position;

            var address = await AppHelpers.GatGeocoder(p.Target.Latitude, p.Target.Longitude);
            var position = new PositionModel()
            {
                Latitude = Convert.ToString(p.Target.Latitude),
                Longitude = Convert.ToString(p.Target.Longitude)
            };

            if (IsStartPointClicked)
            {
                StartPlaceText = address;
                StartPosition = position;
                IsStartCollectionVisible = false;
                if (IsEndPointClicked)
                    IsEndPointClicked = false;

            }

            if (IsEndPointClicked)
            {
                EndPlaceText = address;
                EndPosition = position;
                IsEndCollectionVisible = false;

                if (IsStartPointClicked)
                    IsStartPointClicked = false;

                //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(), Distance.FromKilometers(5)), false);

                map.Polylines.Clear();
                map.Pins.Clear();

                var pathcontent = await LoadRoute(StartPosition, EndPosition);

                var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
                polyline.StrokeColor = AppHelpers.LookupColor("Primary");
                polyline.StrokeWidth = 4;

                foreach (var po in pathcontent)
                {
                    polyline.Positions.Add(po);

                }
                map.Polylines.Add(polyline);


                Pin StartPin = new Pin()
                {
                    //Icon =BitmapDescriptorFactory.FromStream(stream),
                    
                    Label = "Départ",
                    Type = PinType.Place,
                    Position = new Position(Convert.ToDouble(StartPosition.Latitude),Convert.ToDouble(StartPosition.Longitude))

                };
                Pin EndPin = new Pin()
                {
                    Label = "Arrivé",
                    Type = PinType.Place,
                    Position = new Position(Convert.ToDouble(EndPosition.Latitude), Convert.ToDouble(EndPosition.Longitude))

                };
                map.Pins.Add(StartPin);
                map.Pins.Add(EndPin);
            }
        }

        private void Map_CameraMoveStarted(object sender, CameraMoveStartedEventArgs e)
        {
            PinUp = -10;
            IsPickPointVisibile = true;
            MessagingCenter.Send<DisplacementViewModel, int>(this, AppConstants.SendPipUp, PinUp);

        }

        public SKBitmap bmp { get; set; }
        public Stream stream { get; set; }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            MoveMapToLocalPositionCommand.Execute(true);

            this.PropertyChanged += DisplacementViewModel_PropertyChanged;

            using (HttpClient client = new HttpClient())
            {
                stream = await client.GetStreamAsync(AppPreferences.PictureUrl);
            }

        }

        private async void DisplacementViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StartPlaceText))
            {
                IsStartCollectionVisible = !(StartPlaceText == "");
                await GetPlacesByName(StartPlaceText + ", Morocco");
            }

            if (e.PropertyName == nameof(EndPlaceText))
            {
                IsEndCollectionVisible = !(EndPlaceText == "");
                await GetPlacesByName(EndPlaceText + ", Morocco");
            }

            if (e.PropertyName == nameof(IsStartPointClicked))
            {

                IsPickIconVisible = (IsStartPointClicked || IsEndPointClicked);

            }
            if (e.PropertyName == nameof(IsEndPointClicked))
            {

                IsPickIconVisible = (IsStartPointClicked || IsEndPointClicked);


            }
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

                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude),Distance.FromKilometers(5)), false);
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

        public async Task GetPlacesByName(string placeText)
        {
            var places = await googleMapsApiService.GetPlaces(placeText);
            var placeResult = places.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }

            ShowRecentPlaces = (placeResult == null || placeResult.Count == 0);
        }

        private bool canSelectStartAddress = true;
        public ICommand SelectStartAddressCommand => new Command<GooglePlaceAutoCompletePrediction>(async (model) =>
        {
            try
            {
                canSelectStartAddress = false;
                StartPlaceText = model.Description;
                IsStartCollectionVisible = false;
                GooglePlace  googlePlace = await googleMapsApiService.GetPlacePositon(model.PlaceId);
                StartPosition.Latitude = Convert.ToString(googlePlace.Latitude);
                StartPosition.Longitude = Convert.ToString(googlePlace.Longitude);

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(googlePlace.Latitude, googlePlace.Longitude),Distance.FromKilometers(5)), false);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canSelectStartAddress = true;
            }
        }, (_) => canSelectStartAddress);

        private bool canSelectEndAddress = true;
        public ICommand SelectEndAddressCommand => new Command<GooglePlaceAutoCompletePrediction>(async (model) =>
        {
            try
            {
                canSelectEndAddress = false;
                EndPlaceText = model.Description;
                IsEndCollectionVisible = false;
                GooglePlace googlePlace = await googleMapsApiService.GetPlacePositon(model.PlaceId);
                EndPosition.Latitude = Convert.ToString(googlePlace.Latitude);
                EndPosition.Longitude = Convert.ToString(googlePlace.Longitude);

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(googlePlace.Latitude, googlePlace.Longitude), Distance.FromKilometers(5)), false);

                map.Polylines.Clear();
                map.Pins.Clear();

                var pathcontent = await LoadRoute(StartPosition, EndPosition);

                var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
                polyline.StrokeColor = AppHelpers.LookupColor("Primary");
                polyline.StrokeWidth = 4;

                foreach (var po in pathcontent)
                {
                    polyline.Positions.Add(po);

                }
                map.Polylines.Add(polyline);

                Pin StartPin = new Pin()
                {
                    Label = "Départ",
                    Type = PinType.Place,
                    Position = new Position(Convert.ToDouble(StartPosition.Latitude), Convert.ToDouble(StartPosition.Longitude))

                };
                Pin EndPin = new Pin()
                {
                    Label = "Arrivé",
                    Type = PinType.Place,
                    Position = new Position(Convert.ToDouble(EndPosition.Latitude), Convert.ToDouble(EndPosition.Longitude))

                };
                map.Pins.Add(StartPin);
                map.Pins.Add(EndPin);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                canSelectEndAddress = true;
            }
        }, (_) => canSelectEndAddress);


        private bool canSenddisplacementRequest = true;
        public ICommand SendRequestCommand => new Command(async () =>
        {
            try
            {
                canSenddisplacementRequest = false;

                var postParams = new DisplacementModel()
                {
                    LatitudeDepart = Convert.ToDouble(StartPosition.Latitude),
                    LongitudeDepart = Convert.ToDouble(StartPosition.Longitude),
                    LatitudeArrivée = Convert.ToDouble(EndPosition.Latitude),
                    LongitudeArrivée = Convert.ToDouble(EndPosition.Longitude),
                    Date = DisplacementDate,
                    Client = Client,
                    Motif = Motif 
                };

                var result =await App.AppServices.PostDisplacement(postParams);
                if (result.succeeded)
                {
                    await App.Current.MainPage.Navigation.PopAsync();

                }
                else
                {
                    AppHelpers.Alert(result.message);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                canSenddisplacementRequest = true;
            }

        }, () => canSenddisplacementRequest);

        private bool canDownUp = true;
        public ICommand DownUpCommand => new Command(async () =>
        {
            try
            {
                canDownUp = false;
                DownUp = !DownUp;

                if (DownUp)
                    DownUpGlyph = Resources.FontAwesomeFonts.Angledown;
                else
                    DownUpGlyph = Resources.FontAwesomeFonts.AngleUp;


            }
            catch (Exception ex)
            {

            }
            finally
            {
                canDownUp = true;
            }
        }
        ,()=> canDownUp);
    }
}
