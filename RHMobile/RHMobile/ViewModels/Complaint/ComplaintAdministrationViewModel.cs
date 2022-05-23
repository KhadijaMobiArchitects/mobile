using System;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XForms.views.Complaint;

namespace XForms.ViewModels
{
    public class ComplaintAdministrationViewModel : BaseViewModel
    {
        public ComplaintAdministrationViewModel()
        {
        }

        private ProfilComplaintPopup profilComplaintPopup;
        private bool canprofilComplaintPopup = true;

        public ICommand OpenComplaintDetailsPopupCommand => new Command<ProfilComplaintPopup>(async (model) =>
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
