using System;
using System.Windows.Input;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    public class NewComplaintViewModel : BaseViewModel
    {

        public string ComplaintObjectif { get; set; }
        public string ComplaintTitle { get; set; }



        public NewComplaintViewModel()
        {
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

        }


        private bool canSendRequest = true;
        public ICommand SendRequestCommand => new Command(async () =>
        {
            try
            {
                canSendRequest = false;
                AppHelpers.LoadingShow();
                var PostParams = new ComplaintModel()
                {
                    Object = ComplaintObjectif,
                    Title = ComplaintTitle
                };

                var result = await App.AppServices.PostComplaint(PostParams);
                AppHelpers.LoadingHide();
                if (result?.succeeded == true)
                {
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    AppHelpers.Alert(result?.message);
                }

            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);

            }

        }, () => canSendRequest);
    }
}
