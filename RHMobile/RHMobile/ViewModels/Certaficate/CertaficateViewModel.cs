using System;
using System.Collections.Generic;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;
using XForms.Models.Certaficate;
using XForms.views;
using XForms.views.Certaficate;

namespace XForms.ViewModels
{
    public class CertaficateViewModel : BaseViewModel
    {
        public ObservableRangeCollection<CertaficateModel> CertaficatesList { get; set; }

        public CertaficateViewModel()
        {
            CertaficatesList = new ObservableRangeCollection<CertaficateModel>()
            {
                new CertaficateModel()
                {
                    Id = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now ,
                    LabelStatus = "En cours",
                    LabelType = "Attestation du stage",
                },
              new CertaficateModel()
                {
                    Id = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now ,
                    LabelStatus = "En cours",
                    LabelType = "Attestation du travail"
                }
            };

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

        public ICommand OpenCertaficateDetailsPopupCommand => new Command<CertaficateModel>(async (model) =>
        {
            try
            {
                canCertaficateDetailsPopup = false;

                if (certaficateDetailsPopup == null)
                    certaficateDetailsPopup = new CertaficateDetailsPopup() { BindingContext = this };

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
