using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;
using XForms.views;

namespace XForms.ViewModels
{
    public class ComplaintViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ComplaintResponse> ProfilComplaintsList { get; set; }

        public ObservableRangeCollection<ComplaintResponse> ProfilConfirmedComplaintsList { get; set; }
        public ObservableRangeCollection<ComplaintResponse> ProfilInProgressComplaintsList { get; set; }
        public ObservableRangeCollection<ComplaintResponse> ProfilComplaintsItemsList { get; set; }


        public bool IsComplaintRequestInProgress { get; set; }
        public bool IsComplaintRequestConfirmed { get; set; }
        public ComplaintResponse SelectedComplaint { get; set; }
        public int numberOfRequests { get; set; }

        public ComplaintViewModel()
        {

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            IsComplaintRequestInProgress = true;
            await getProfilComplaints();
        }
        public async Task getProfilComplaints()
        {
            try
            {
                AppHelpers.LoadingShow();
                var result = await App.AppServices.GetProfilComplaint();
                AppHelpers.LoadingHide();
                if (result?.succeeded == true)
                {
                    ProfilComplaintsList = new ObservableRangeCollection<ComplaintResponse>(result.data.ToList());

                    ProfilConfirmedComplaintsList = new ObservableRangeCollection<ComplaintResponse>(result.data.Where(x => (x.RefStatusClaimId == 2)).ToList());
                    ProfilInProgressComplaintsList = new ObservableRangeCollection<ComplaintResponse>(result.data.Where(x => (x.RefStatusClaimId == 1)).ToList());
                    ProfilComplaintsItemsList = ProfilInProgressComplaintsList;
                    numberOfRequests = ProfilComplaintsItemsList.Count;
                }
                else
                {
                    AppHelpers.Alert(result?.message);
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);

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
                IsComplaintRequestInProgress = HeadrActionList[0].IsSelected;
                IsComplaintRequestConfirmed = !IsComplaintRequestInProgress;

                ProfilComplaintsItemsList = IsComplaintRequestInProgress ? ProfilInProgressComplaintsList : ProfilConfirmedComplaintsList;
                numberOfRequests = ProfilComplaintsItemsList.Count;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);

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
                Logger.LogError(ex);
            }
            finally
            {
                canNavigateToNewRequest = true;
            }

        },
    () => canNavigateToNewRequest);
    }
}
