using System;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;
using XForms.views;

namespace XForms.ViewModels
{
    public class ComplaintViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ComplaintModel> ProfilComplaintsList { get; set; }

        public ObservableRangeCollection<ComplaintModel> ProfilComplaintItemsList { get; set; }


        public ComplaintViewModel()
        {
            ProfilComplaintsList = new ObservableRangeCollection<ComplaintModel>()
            {
                new ComplaintModel()
                {
                    Id =1,
                    Subject ="Music exagirée sur le bureau",
                    Status = "En cours",
                    Date = DateTime.Now
                },
                new ComplaintModel()
                {
                    Id =1,
                    Subject ="Mauvais connexion",
                    Status = "En cours",
                    Date = DateTime.Now
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
                //IsCertaficateRequestInProgress = HeadrActionList[0].IsSelected;
                //IsCertaficateRequestConfirmed = !IsCertaficateRequestInProgress;

                //ProfilCertaficatesItemsList = IsCertaficateRequestInProgress ? ProfilInProgressCertaficatesList : ProfilConfirmedCertaficatesList;
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
                App.Current.MainPage.Navigation.PushAsync(new NewComplaintPage());

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
    }
}
