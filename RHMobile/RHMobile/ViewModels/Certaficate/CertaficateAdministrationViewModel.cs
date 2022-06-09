using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
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

        public CertaficateResponse SelectedCertaficate { get; set; }

        public FileResult PickedFile { get; set; }
        public int numberOfRequestsAdmin { get; set; }

        public CertaficateAdministrationViewModel()
        {

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            IsCertaficateRequestInProgress = true;

            await GetAllCertificates();

            //ProfilsCertaficateItemsList = new ObservableRangeCollection<CertaficateResponse>();
            //ProfilsCertaficateItemsList = ProfilsInProgressCertaficateList;
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

                SelectedCertaficate = model;

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
                ProfilsCertaficateItemsList = HeadrActionList[0].IsSelected ? ProfilsInProgressCertaficateList : ProfilsConfirmedCertaficateList;
                numberOfRequestsAdmin = ProfilsCertaficateItemsList.Count;
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

                IsCertaficateRequestInProgress = HeadrActionList[0].IsSelected;
                IsCertaficateRequestConfirmed = !IsCertaficateRequestInProgress;

                ProfilsCertaficateItemsList = HeadrActionList[0].IsSelected ? ProfilsInProgressCertaficateList : ProfilsConfirmedCertaficateList;
                numberOfRequestsAdmin = ProfilsCertaficateItemsList.Count;

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

        private Models.File certaficateFile;
        private bool canUploadFile = true;
        public ICommand UploadFileCommand => new Command(async () =>
        {
            try
            {
                canUploadFile = false;
                var pickedFile = await AppHelpers.DoPickPdfAsync();
                certaficateFile = new Models.File()
                {
                    //Name = System.IO.Path.GetFileName(pickedFile.FullPath),
                    Name ="test.pdf",
                    Path = pickedFile.FullPath
                };

            }
            catch (Exception ex)
            {

            }
            finally
            {
                canUploadFile = true;
            }

        }, () => canUploadFile);

        private bool canSendCertaficate = true;
        public ICommand SendCertaficateCommand => new Command(async () =>
        {
            try
            {
                canSendCertaficate = false;

                var bytes = System.IO.File.ReadAllBytes(certaficateFile.Path);


                var postParams = new Models.CertaficateTreatementRequest()
                {
                   Id = SelectedCertaficate.Id,
                   Document = bytes

                };
                var result = await App.AppServices.PostCertaficateTreatement(postParams);

                AppHelpers.LoadingShow();
                await GetAllCertificates();

                await PopupNavigation.Instance.PopAllAsync();
                AppHelpers.LoadingHide();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canSendCertaficate = true;
            }

        }, () => canSendCertaficate);

        //private bool canRejectComplaint = true;
        //public ICommand RejectComplaintCommand => new Command(async () =>
        //{
        //    try
        //    {
        //        canRejectComplaint = false;
        //        AppHelpers.LoadingShow();

        //      var postParams = new Models.CertaficateTreatementRequest()
        //        {
        //           Id = SelectedCertaficate.Id,
        //           Document = certaficateFile

        //        };
        //        var result = await App.AppServices.PosteUpdateComplaint(postParams);

        //        //ProfilsDispalacementItemsList.Remove()

        //        await GetAllComplaints();

        //        await PopupNavigation.Instance.PopAllAsync();
        //        AppHelpers.LoadingHide();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        canRejectComplaint = true;
        //    }

        //}, () => canRejectComplaint);
    }

}

