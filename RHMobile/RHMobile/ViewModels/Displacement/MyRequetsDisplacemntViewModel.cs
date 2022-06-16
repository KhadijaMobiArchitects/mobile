using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;
using XForms.views;

namespace XForms.ViewModels
{
    public class MyRequetsDisplacemntViewModel : BaseViewModel
    {

        public ObservableRangeCollection<DisplacementResponse> ProfilDispalacementsList { get; set; }

        public ObservableRangeCollection<DisplacementResponse> ProfilConfirmedDispalacementsList { get; set; }
        public ObservableRangeCollection<DisplacementResponse> ProfilInProgressDispalacementsList { get; set; }
        public ObservableRangeCollection<DisplacementResponse> ProfilDispalacementsItemsList { get; set; }


        public bool IsDispalacementRequestInProgress { get; set; }
        public bool IsDispalacementRequestConfirmed { get; set; }
        public DisplacementResponse SelectedDisplacement { get; set; }
        public int numberOfRequests { get; set; }


        public MyRequetsDisplacemntViewModel()
        {
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            IsDispalacementRequestInProgress = true;
            await getProfilDisplacements();

        }

        public async Task getProfilDisplacements()
        {
            try
            {
                AppHelpers.LoadingShow();
                var result = await App.AppServices.GetProfilDeplacement();
                AppHelpers.LoadingHide();
                if (result?.succeeded == true)
                {
                    ProfilDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.ToList());

                    ProfilConfirmedDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.Where(x => (x.RefStatusDeplacementId == 2)).ToList());
                    ProfilInProgressDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.Where(x => (x.RefStatusDeplacementId == 1)).ToList());
                    ProfilDispalacementsItemsList = ProfilInProgressDispalacementsList;
                    numberOfRequests = ProfilDispalacementsItemsList.Count;

                }
                else
                {
                    AppHelpers.Alert(result?.message);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        private bool CanSelectHeaderAction = true;
        public ICommand SelectHeaderActionCommand => new Command<REFItem>(async (model) =>
        {
            try
            {
                CanSelectHeaderAction = false;

                if (model == null) return;

                foreach (var item in HeadrActionList)
                {
                    item.IsSelected = (item.Id == model.Id);
                    OnPropertyChanged(nameof(item.IsSelected));


                }
                IsDispalacementRequestInProgress = HeadrActionList[0].IsSelected;
                IsDispalacementRequestConfirmed = !IsDispalacementRequestInProgress;


                ProfilDispalacementsItemsList = IsDispalacementRequestInProgress ? ProfilInProgressDispalacementsList : ProfilConfirmedDispalacementsList;
                numberOfRequests = ProfilDispalacementsItemsList.Count;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                CanSelectHeaderAction = true;
            }
        },

        (_) => CanSelectHeaderAction);

        private bool canNavigateToNewRequest = true;
        public ICommand NavigationtonewRequest => new Command(() =>
        {
            try
            {
                canNavigateToNewRequest = false;
                App.Current.MainPage.Navigation.PushAsync(new DisplacementPage());

                HeadrActionList[0].IsSelected = true;
                HeadrActionList[1].IsSelected = false;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                canNavigateToNewRequest = true;
            }

        },
    () => canNavigateToNewRequest);

        private DisplacementDetailsPopup DisplacementDetailsPopup;
        private bool canCertaficateDetailsPopup = true;
        public ICommand OpenDisplacementDetailsPopupCommand => new Command<DisplacementResponse>(async (model) =>
        {
            try
            {
                canCertaficateDetailsPopup = false;
                SelectedDisplacement = model;

                SelectedDisplacement.StartAddress = await AppHelpers.GatGeocoder(SelectedDisplacement.StartPostion.Latitude, SelectedDisplacement.StartPostion.Longitude);
                SelectedDisplacement.EndAddress = await AppHelpers.GatGeocoder(SelectedDisplacement.EndPostion.Latitude, SelectedDisplacement.EndPostion.Longitude);


                if (DisplacementDetailsPopup == null)
                    DisplacementDetailsPopup = new DisplacementDetailsPopup() { BindingContext = this };

                await PopupNavigation.Instance.PushSingleAsync(DisplacementDetailsPopup);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                canCertaficateDetailsPopup = true;
            }


        }, (_) => canCertaficateDetailsPopup);

    }
}
