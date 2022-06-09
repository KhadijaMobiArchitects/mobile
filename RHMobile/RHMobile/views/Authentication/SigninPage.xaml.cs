using System;
using System.Collections.Generic;
using XForms.views.Base;
using Xamarin.Forms;
using XForms.ViewModels;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;

namespace XForms.views.Authentication
{
    public partial class SigninPage : BasePage
    {
        public SigninPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext == null)
            {
                              var vm   = new SigninViewModel();
                BindingContext = vm;

                await Task.Delay(500);
                //vm.OpenFingerPrintViewCommand.Execute(null);

            }

        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var availability = await CrossFingerprint.Current.IsAvailableAsync();

            if (!availability)
            {
                await DisplayAlert("Warning!", "No biometrics available", "OK");

                return;
            }

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Heads up!", "I would like to use your biometrics, please!"));

            if (authResult.Authenticated)
            {
                await DisplayAlert("Yaay!", "Here is the secrets", "Thanks!");
            }
        }
    }
}
