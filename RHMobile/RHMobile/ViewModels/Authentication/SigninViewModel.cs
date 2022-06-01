using System;
using System.Linq;
using System.Windows.Input;
using FluentValidation;
using Xamarin.Forms;
using XForms.Enum;
using XForms.Models;
using XForms.views.Administration;

namespace XForms.ViewModels
{
    public class SigninViewModel : BaseViewModel
    {
        public string SigninEmail { get; set; }
        public string SigninPassword { get; set; }

        public SigninViewModel()
        {
            //Collaborateur

            //SigninEmail = "abdelahasserhihe@gmail.com";
            //SigninPassword = "Password2022!!";

            //Chef_projet

            //SigninEmail = "oussamabouhami@gmail.com";
            //SigninPassword = "Password2022!!";

            //Stagiaire

            //SigninEmail = "hajarharkaoui@gmail.com";
            //SigninPassword = "Password2022!!";

            //Manager

            SigninEmail = "samirghaouissi@gmail.com";
            SigninPassword = "Password2022!!";

            //Responsable RH

            //SigninEmail = "rimrh@gmail.com";
            //SigninPassword = "Password2022!!";



        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            //if (validator == null)
            //    validator = new();
        }

        private bool CanSubmitCommand = true;
        public ICommand SubmitCommand => new Command(async () =>
        {
            try
            {
                CanSubmitCommand = false;

                //validationResult = validator.Validate(this);

                //if (!validationResult.IsValid)
                //    return;

                //Application.Current.MainPage = new NavigationPage(new Views.Home.HomePage());
                //return;

                AppHelpers.LoadingShow();

                var postParams = new SinginRequestModel()
                {
                    userName = SigninEmail,
                    password = SigninPassword
                };


                var result = await App.AppServices.Singin(postParams);

                if (result?.succeeded == true && result?.data != null)
                {
                    AppPreferences.IsAleardyCheckHasNotchScreen = false;
                    AppPreferences.Token = result.data.JwToken;
                    AppPreferences.UserId = result.data.UserId;
                    AppPreferences.IsVerified = result.data.isVerified;
                    AppPreferences.FullName = result.data.LastName + " " + result.data.FirstName;
                    AppPreferences.UserRole = result.data.Roles.FirstOrDefault();
                    AppPreferences.PictureUrl = result.data.PictureUrl;
                    AppPreferences.RefFunctionLabel = result.data.RefFunctionLabel;

                    AppPreferences.IsSignIn = true;

                    //#region Firebase Plugin Callbacks
                    //Plugin.FirebasePushNotification.CrossFirebasePushNotification.Current.Subscribe("general");
                    //Plugin.FirebasePushNotification.CrossFirebasePushNotification.Current.Subscribe("dev");

                    //await App.AppServices.PostSaveFirebaseToken();
                    //#endregion

                    //AppPreferences.IsAleardyCheckHasNotchScreen = false;
                    //App.IsSetDynamicResources = false;

                    //if (result.data.Roles.Any(a => (a.ToUpper() ?? "").Equals(RolesEnums.ADMIN.ToString())))
                    //{
                    //    var r = result.data.Roles.FirstOrDefault(a => (a.ToLower() ?? "").Equals(RolesEnums.ADMIN.ToString()));

                    //    AppPreferences.UserRole = RolesEnums.ADMIN.ToString();

                    //    Application.Current.MainPage = new NavigationPage(new Views.Home.HomePage());

                    //    //Xamarin.Forms.Application.Current.MainPage = new Views.FolderList.FolderList(MemorySettings.UserRole, new ObservableRangeCollection<RequestResult>());
                    //}
                    //else
                    //{
                    //    AppPreferences.UserRole = RolesEnums.CTA_AGENT.ToString();

                    //Application.Current.MainPage = new NavigationPage(new HomePage());
                    //    //Xamarin.Forms.Application.Current.MainPage = new Views.FolderList.FolderList(MemorySettings.UserRole, new ObservableRangeCollection<RequestResult>());
                    //}
                    //string Collaborateur = System.Enum.GetName(typeof(RolesEnum), RolesEnum.Collaborateur);
                    //string Manager = System.Enum.GetName(typeof(RolesEnum), RolesEnum.Manager);

                    if (AppPreferences.UserRole.Equals(Roles.Collaborateur))
                        Application.Current.MainPage = new NavigationPage(new HomePage());

                    if (AppPreferences.UserRole.Equals(Roles.Chef_projet))
                        Application.Current.MainPage = new NavigationPage(new HomePage());

                    if (AppPreferences.UserRole.Equals(Roles.Stagiaire))
                        Application.Current.MainPage = new NavigationPage(new HomePage());

                    else if (AppPreferences.UserRole.Equals(Roles.Manager))
                        Application.Current.MainPage = new NavigationPage(new HomeAdminPage());

                    else if (AppPreferences.UserRole.Equals(Roles.Responsable_RH))
                        Application.Current.MainPage = new NavigationPage(new HomeAdminPage());
                }
                else
                {
                    AppHelpers.Alert(result?.message);
                }
            }
            catch (Exception ex)
            {
                //Logger?.LogError(ex);
            }
            finally
            {
                AppHelpers.LoadingHide();

                CanSubmitCommand = true;
            }
        }, () => CanSubmitCommand);


    }
}