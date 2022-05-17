using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;
using XForms.views.Certaficate;

namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CertaficateAdministrationViewModel : BaseViewModel
    {
        public ObservableRangeCollection<CertaficateResponse> CertaficateProfils { get; set; }

        public ObservableRangeCollection<CertaficateResponse> ProfilsConfirmedCertaficateList { get; set; }
        public ObservableRangeCollection<CertaficateResponse> ProfilsInProgressCertaficateList { get; set; }

        public bool IsCertaficateRequestInProgress { get; set; }
        public bool IsCertaficateRequestConfirmed { get; set; }

        public ObservableRangeCollection<CertaficateResponse> ProfilsCertaficateItemsList { get; set; }


        public CertaficateAdministrationViewModel()
        {

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            await GetAllCertificates();

            ProfilsCertaficateItemsList = new ObservableRangeCollection<CertaficateResponse>();
            ProfilsCertaficateItemsList = ProfilsInProgressCertaficateList;
        }

        private ProfilCertaficatePopup profilCertaficatePopup;
        private bool canProfilCertaficatePopup = true;

        public ICommand OpenCertaficateDetailsPopupCommand => new Command<CertaficateResponse>(async (model) =>
        {
            try
            {
                canProfilCertaficatePopup = false;

                if (profilCertaficatePopup == null)
                    profilCertaficatePopup = new ProfilCertaficatePopup() { BindingContext = this };

                await PopupNavigation.Instance.PushSingleAsync(profilCertaficatePopup);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canProfilCertaficatePopup = true;
            }


        }, (_) => canProfilCertaficatePopup);

        public async Task GetAllCertificates()
        {
            AppHelpers.LoadingShow();
            var result = await App.AppServices.GetAllCertificates();
            if(result?.succeeded == true)
            {
                CertaficateProfils = new ObservableRangeCollection<CertaficateResponse>(result.data.ToList());

                ProfilsConfirmedCertaficateList = new ObservableRangeCollection<CertaficateResponse>(result.data.Where(x => (x.RefStatusCertificateId == 2)).ToList());
                ProfilsInProgressCertaficateList = new ObservableRangeCollection<CertaficateResponse>(result.data.Where(x => (x.RefStatusCertificateId == 1)).ToList());

            }
            else
            {
                AppHelpers.Alert(result?.message);
            }
            AppHelpers.LoadingHide();
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
                IsCertaficateRequestInProgress = !HeadrActionList[0].IsSelected;
                IsCertaficateRequestConfirmed = IsCertaficateRequestInProgress;

                ProfilsCertaficateItemsList = HeadrActionList[0].IsSelected ? ProfilsInProgressCertaficateList : ProfilsConfirmedCertaficateList;
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

    }
}
