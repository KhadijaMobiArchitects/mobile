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
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class DisplacementAdministrationViewModel : BaseViewModel
    {
        public ObservableRangeCollection<DisplacementResponse> ProfilDispalacementsList { get; set; }

        public ObservableRangeCollection<DisplacementResponse> ProfilConfirmedDispalacementsList { get; set; }
        public ObservableRangeCollection<DisplacementResponse> ProfilInProgressDispalacementsList { get; set; }
        public ObservableRangeCollection<DisplacementResponse> ProfilsDispalacementItemsList { get; set; }


        public bool IsDispalacementRequestInProgress { get; set; }
        public bool IsDispalacementRequestConfirmed { get; set; }
        public DisplacementResponse SelectedDisplacement { get; set; }
        public int numberOfRequestsAdmin { get; set; }

        public DisplacementAdministrationViewModel()
        {
        }
        public async override void OnAppearing()
        {
            base.OnAppearing();
            IsDispalacementRequestInProgress = true;

            await getallProfilsDisplacement();

        }

        public async Task getallProfilsDisplacement()
        {
            AppHelpers.LoadingShow();
            var result = await App.AppServices.GetAllDeplacement();
            AppHelpers.LoadingHide();
            if (result?.succeeded == true)
            {
                ProfilDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.ToList());

                ProfilConfirmedDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.Where(x => (x.RefStatusDeplacementId == 2)).ToList());
                ProfilInProgressDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.Where(x => (x.RefStatusDeplacementId == 1)).ToList());

                ProfilsDispalacementItemsList = IsDispalacementRequestInProgress ? ProfilInProgressDispalacementsList : ProfilConfirmedDispalacementsList;
                numberOfRequestsAdmin = ProfilsDispalacementItemsList.Count;


            }
            else
            {
                AppHelpers.Alert(result?.message);
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

                ProfilsDispalacementItemsList = IsDispalacementRequestInProgress ? ProfilInProgressDispalacementsList : ProfilConfirmedDispalacementsList;
                numberOfRequestsAdmin = ProfilsDispalacementItemsList.Count;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                CanSelectHeaderAction = true;
            }
        },

        (_) => CanSelectHeaderAction);

        private ProfilDisplacementPopup profilDisplacementPopup;
        private bool canOpenProfilDisplacementPopup = true;
        public ICommand OpenProfilDisplacementPopupCommand => new Command<DisplacementResponse>(async (model) =>
        {
            try
            {
                canOpenProfilDisplacementPopup = false;
                SelectedDisplacement = model;

                SelectedDisplacement.StartAddress = await AppHelpers.GatGeocoder(SelectedDisplacement.StartPostion.Latitude, SelectedDisplacement.StartPostion.Longitude);
                SelectedDisplacement.EndAddress = await AppHelpers.GatGeocoder(SelectedDisplacement.EndPostion.Latitude, SelectedDisplacement.EndPostion.Longitude);


                if (profilDisplacementPopup == null)
                    profilDisplacementPopup = new ProfilDisplacementPopup() { BindingContext = this };

                await PopupNavigation.Instance.PushSingleAsync(profilDisplacementPopup);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canOpenProfilDisplacementPopup = true;
            }


        }, (_) => canOpenProfilDisplacementPopup);



        private bool canConfirmDispalacement = true;
        public ICommand ConfirmeDisplacementCommand => new Command(async () =>
        {
            try
            {
                canConfirmDispalacement = false;
                AppHelpers.LoadingShow();

                var postParams = new Models.UpdateDeplacementModel()
                {
                    Id = SelectedDisplacement.Id,
                    RefStatusDeplacementId = 2
                };
                var result = await App.AppServices.PosteUpdateDisplacement(postParams);

                await getallProfilsDisplacement();

                await PopupNavigation.Instance.PopAllAsync();
                AppHelpers.LoadingHide();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canConfirmDispalacement = true;
            }

        }, () => canConfirmDispalacement);

        private bool canRejectDispalacement = true;
        public  ICommand RejectDisplacementCommand => new Command(async () =>
        {
            try
            {
                canRejectDispalacement = false;
                AppHelpers.LoadingShow();

                var postParams = new Models.UpdateDeplacementModel()
                {
                    Id = SelectedDisplacement.Id,
                    RefStatusDeplacementId = 3
                };
                var result = await App.AppServices.PosteUpdateDisplacement(postParams);

                //ProfilsDispalacementItemsList.Remove()

                await getallProfilsDisplacement();

                await PopupNavigation.Instance.PopAllAsync();
                AppHelpers.LoadingHide();

                OnPropertyChanged(nameof(ProfilsDispalacementItemsList));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canRejectDispalacement = true;
            }

        }, () => canRejectDispalacement);
    }
}
