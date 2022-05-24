using System;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;
using XForms.views;

namespace XForms.ViewModels
{
    public class ComplaintAdministrationViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ComplaintResponse> ProfilsComplaintsList { get; set; }

        public ObservableRangeCollection<CertaficateResponse> ProfilsConfirmedCertaficateList { get; set; }
        public ObservableRangeCollection<CertaficateResponse> ProfilsInProgressCertaficateList { get; set; }

        public ObservableRangeCollection<CertaficateResponse> ProfilsCertaficateItemsList { get; set; }


        public ComplaintAdministrationViewModel()
        {
            ProfilsComplaintsList = new ObservableRangeCollection<ComplaintResponse>()
            {
                new ComplaintResponse()
                {
                    id = 1,
                    FirstName = "Hassoun",
                    LastName = "Karoum",
                    Title = "Les bruits",
                    Objet = "HHh hhh hhhhhhhh"

               },
                new ComplaintResponse()
                {
                    id = 1,
                    FirstName = "Hamza",
                    LastName = "Karoum",
                    Title = "Les bruits",
                    Objet = "HHh hhh hhhhhhhh"

               },
                new ComplaintResponse()
                {
                    id = 1,
                    FirstName = "Omar",
                    LastName = "Karoum",
                    Title = "Les bruits",
                    Objet = "HHh hhh hhhhhhhh"

               },
            };
        }

        private ProfilComplaintPopup profilComplaintPopup;
        private bool canprofilComplaintPopup = true;

        public ICommand OpenComplaintDetailsPopupCommand => new Command<ComplaintResponse>(async (model) =>
        {
            try
            {
                canprofilComplaintPopup = false;

                if (profilComplaintPopup == null)
                    profilComplaintPopup = new ProfilComplaintPopup() { BindingContext = this };

                //SelectedCertaficate = model;

                await PopupNavigation.Instance.PushSingleAsync(profilComplaintPopup);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canprofilComplaintPopup = true;
            }


        }, (_) => canprofilComplaintPopup);
    }
}
