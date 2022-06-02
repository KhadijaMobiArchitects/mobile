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

        public DisplacementAdministrationViewModel()
        {
        }
        public async override void OnAppearing()
        {
            base.OnAppearing();
            await getProfilDisplacements();

            //ProfilsDispalacementItemsList = new ObservableRangeCollection<DisplacementResponse>();
            ProfilsDispalacementItemsList = ProfilInProgressDispalacementsList;
            IsDispalacementRequestInProgress = true;

        }

        public async Task getProfilDisplacements()
        {
            AppHelpers.LoadingShow();
            var result = await App.AppServices.GetAllDeplacement();
            AppHelpers.LoadingHide();
            if (result?.succeeded == true)
            {
                ProfilDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.ToList());

                ProfilConfirmedDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.Where(x => (x.RefStatusDeplacementId == 2)).ToList());
                ProfilInProgressDispalacementsList = new ObservableRangeCollection<DisplacementResponse>(result.data.Where(x => (x.RefStatusDeplacementId == 1)).ToList());

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
    }
}
