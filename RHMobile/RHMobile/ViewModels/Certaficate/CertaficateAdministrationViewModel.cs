using System;
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

        public CertaficateAdministrationViewModel()
        {
            CertaficateProfils = new ObservableRangeCollection<CertaficateResponse>()
            {
                new CertaficateResponse()
                {
                    Id =1,
                    FirstName = "Hassun",
                    LastName = "Karoum",
                    LabelStatus = "En cours",
                    LabelType = "Attestation Scolaire"
                },
                new CertaficateResponse()
                {
                    Id =1,
                    FirstName = "salma",
                    LastName = "Mejjaty",
                    LabelStatus = "En cours",
                    LabelType = "Attestation du travail"
                }
            };
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

    }
}
