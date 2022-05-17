using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Enum;
using XForms.Models;
using XForms.views;
using XForms.views.Certaficate;

namespace XForms.ViewModels
{
    public class CertaficateViewModel : BaseViewModel
    {
        public ObservableRangeCollection<CertaficateResponse> ProfilCertaficatesList { get; set; }
        public ObservableRangeCollection<TypeCertaficate> TypesCertaficateList { get; set; }

        public ObservableRangeCollection<CertaficateResponse> ProfilConfirmedCertaficatesList { get; set; }
        public ObservableRangeCollection<CertaficateResponse> ProfilInProgressCertaficatesList { get; set; }

        public ObservableRangeCollection<CertaficateResponse> ProfilCertaficatesItemsList { get; set; }


        public bool IsCertaficateRequestInProgress { get; set; }
        public bool IsCertaficateRequestConfirmed { get; set; }



        public CertaficateViewModel()
        {
            //CertaficatesList = new ObservableRangeCollection<CertaficateModel>()
            //{
            //    new CertaficateModel()
            //    {
            //        Id = 1,
            //        StartDate = DateTime.Now,
            //        EndDate = DateTime.Now ,
            //        LabelStatus = "En cours",
            //        LabelType = "Attestation du stage",
            //    },
            //  new CertaficateModel()
            //    {
            //        Id = 1,
            //        StartDate = DateTime.Now,
            //        EndDate = DateTime.Now ,
            //        LabelStatus = "En cours",
            //        LabelType = "Attestation du travail"
            //    }
            //};

        }
        public async override void OnAppearing()
        {
            base.OnAppearing();
            await getProfilCertaficates();
            //this.PropertyChanged += (s, e) =>
            //{
            //    if (e.PropertyName == nameof(HeadrActionList))
            //    {

            //    }
            //};
            ProfilCertaficatesItemsList = new ObservableRangeCollection<CertaficateResponse>();
            ProfilCertaficatesItemsList = ProfilInProgressCertaficatesList;

        }

        public async  Task getProfilCertaficates()
        {
            AppHelpers.LoadingShow();
            var result = await App.AppServices.GetProfilCertificates();
            AppHelpers.LoadingHide();
            if (result?.succeeded == true)
            {
                ProfilCertaficatesList = new ObservableRangeCollection<CertaficateResponse>(result.data.ToList());

                ProfilConfirmedCertaficatesList = new ObservableRangeCollection<CertaficateResponse>(result.data.Where(x =>(x.RefStatusCertificateId == 2)).ToList());
                ProfilInProgressCertaficatesList = new ObservableRangeCollection<CertaficateResponse>(result.data.Where(x => (x.RefStatusCertificateId == 1)).ToList());


            }
            else
            {
                AppHelpers.Alert(result?.message);
            }
        }
        public async Task GetTypeCertificates()
        {
            var result = await App.AppServices.GetTypeCertificates();
            if (result?.succeeded == true)
            {
                TypesCertaficateList = new ObservableRangeCollection<TypeCertaficate>(result.data.ToList());

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
                IsCertaficateRequestInProgress = HeadrActionList[0].IsSelected;
                IsCertaficateRequestConfirmed = !IsCertaficateRequestInProgress;

                ProfilCertaficatesItemsList = IsCertaficateRequestInProgress ? ProfilInProgressCertaficatesList : ProfilConfirmedCertaficatesList;
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


        private bool canNavigateToNewRequest = true;
        public ICommand NavigationtonewRequest => new Command(() =>
        {
            try
            {
                canNavigateToNewRequest = false;
                App.Current.MainPage.Navigation.PushAsync(new NewCertaficateRequestPage());

                HeadrActionList[0].IsSelected = true;
                HeadrActionList[1].IsSelected = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canNavigateToNewRequest = true;
            }

        },
    () => canNavigateToNewRequest);

        private CertaficateDetailsPopup certaficateDetailsPopup;
        private bool canCertaficateDetailsPopup = true;

        public ICommand OpenCertaficateDetailsPopupCommand => new Command<CertaficateResponse>(async (model) =>
        {
            try
            {
                canCertaficateDetailsPopup = false;

                if (certaficateDetailsPopup == null)
                    certaficateDetailsPopup = new CertaficateDetailsPopup() { BindingContext = this };

                //IsCertaficateRequestInProgress =

                await PopupNavigation.Instance.PushSingleAsync(certaficateDetailsPopup);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canCertaficateDetailsPopup = true;
            }


        }, (_) => canCertaficateDetailsPopup);
    }
}
