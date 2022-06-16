using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    public class NewCertaficateRequestViewModel : BaseViewModel
    {
        public ObservableRangeCollection<TypeCertaficate> TypesCertaficateList { get; set; }

        public string CertaficateObjectif { get; set; }
        public string RefTypeCertificateId { get; set; }
        public TypeCertaficate SelectedType { get; set; }

        public NewCertaficateRequestViewModel()
        {
        }
        public async override void OnAppearing()
        {
            base.OnAppearing();

            await GetTypeCertificates();
        }

        public async Task GetTypeCertificates()
        {
            try
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
            catch (Exception ex)
            {
                Logger?.LogError(ex);
            }
        }

        private bool canSendRequest = true;
        public ICommand SendRequestCommand => new Command(async () =>
        {
            try
            {
                canSendRequest = false;
                var PostParams = new CertaficateModel()
                {
                    Objectif = CertaficateObjectif,
                    RefTypeCertificateId = SelectedType.Id
                };

                var result = await App.AppServices.PostCertificate(PostParams);
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

        },()=>canSendRequest);

    }
}
