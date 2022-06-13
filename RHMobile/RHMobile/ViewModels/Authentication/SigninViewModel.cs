using System;
using System.Linq;
using System.Windows.Input;
using FluentValidation;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XForms.Enum;
using XForms.Models;
using XForms.Popups;
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

            SigninEmail = "hassoun.k@mobiarchitects.com";
            SigninPassword = "Password2022!!";

            //Chef_projet

            //SigninEmail = "oussamabouhami@gmail.com";
            //SigninPassword = "Password2022!!";

            //Stagiaire

            //SigninEmail = "hajarharkaoui@gmail.com";
            //SigninPassword = "Password2022!!";

            //Manager

            //SigninEmail = "sr@mobiarchitects.com";
            //SigninPassword = "Password2022!!";

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

                    //bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
                    bool isFingerprintAvailable = true;

                    //AppPreferences.IsAleardyLoggedIn = false;
                    if (!AppPreferences.IsAleardyLoggedIn && isFingerprintAvailable)
                    //if (isFingerprintAvailable)

                    {
                        AppPreferences.IsAleardyLoggedIn = true;

                        var digitalPrintPopup = new Popups.FeedBackPopup(
                        headerGlyph: Resources.FontAwesomeFonts.finger,
                        headerGlyphBackground: AppHelpers.LookupColor("Primary"),
                        title: "Empreinte digitale",
                        description: "L'empreinte digitale vous permet de vous connecter en toute sécurité et rapidité",
                        confirmActionText: "Activer",
                        cancelActionText: "Plus tard",
                        hasCancelAction: true,
                        primaryColor: AppHelpers.LookupColor("ConfirmedColor"),
                        secondaryColor: AppHelpers.LookupColor("InProgressColor")
                        ) ;

                        digitalPrintPopup.OnEventAcquired += async (sender1, args1) =>
                        {
                            if (args1)
                            {
                                await PopupNavigation.Instance.PopSafeAsync();

                                var request = new AuthenticationRequestConfiguration("Empreinte Digitale", "Veuillez appliquer votre empreinte digitale");
                                var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                                if (result.Authenticated)
                                {
                                    var digitalPrintAuthenticatedPopup = new Popups.FeedBackPopup(
                                               headerGlyph: Resources.FontAwesomeFonts.CheckCircle,
                                               headerGlyphBackground: AppHelpers.LookupColor("Primary"),
                                               title: "Empreinte digitale",
                                               description: "Votre empreinte digitale a été bien enregistrée",
                                               confirmActionText: "D’accord",
                                               primaryColor: AppHelpers.LookupColor("GreenStatut"));
                                    digitalPrintAuthenticatedPopup.OnEventAcquired += async (sender4, args4) =>
                                    {
                                        if (args4)
                                        {
                                            await PopupNavigation.Instance.PopSafeAsync();

                                            AppPreferences.IsDigitalPrintActived = true;

                                            AppPreferences.IsLoggedIn = true;
                                            //App.IsAppAleardyOpen = true;

                                            //AppPreferences.ValidUntil = DateTime.UtcNow.AddSeconds(AppConstants.SessionDuration * 60);

                                            //Application.Current.MainPage = new NavigationPage(new Views.MyTabbedPage());

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
                                    };

                                    await PopupNavigation.Instance.PushSingleAsync(digitalPrintAuthenticatedPopup);
                                }
                            }
                            else
                            {
                                //Close Digital Print Popup
                                var digitalPrintCancelPopup = new Popups.FeedBackPopup(
                     headerGlyph: Resources.FontAwesomeFonts.finger,
                     headerGlyphBackground: AppHelpers.LookupColor("Primary"),
                     title: "Empreinte digitale",
                     description: @"Vous pouvez, à tout moment, activer la connexion par touch ID depuis le menu ""Paramètres""",
                     confirmActionText: "D’accord",
                     primaryColor: AppHelpers.LookupColor("Primary")
                     );

                                digitalPrintCancelPopup.OnEventAcquired += async (sender3, args3) =>
                                {
                                    if (args3)
                                    {
                                        await PopupNavigation.Instance.PopSafeAsync();

                                        AppPreferences.IsDigitalPrintActived = false;

                                        AppPreferences.IsLoggedIn = true;

                                        //App.IsAppAleardyOpen = true;


                                        if (AppPreferences.UserRole.Equals(Roles.Collaborateur))
                                            Application.Current.MainPage = new NavigationPage(new HomePage());

                                        if (AppPreferences.UserRole.Equals(Roles.Chef_projet))
                                            Application.Current.MainPage = new NavigationPage(new HomePage());

                                        if (AppPreferences.UserRole.Equals(Roles.Stagiaire))
                                            Application.Current.MainPage = new NavigationPage(new HomePage());

                                        else if (AppPreferences.UserRole.Equals(Roles.Manager))
                                            Application.Current.MainPage = new NavigationPage(new HomeAdminPage());

                                        else if (AppPreferences.UserRole.Equals(Roles.Responsable_RH))
                                            Application.Current.MainPage = new NavigationPage(new HomeAdminPage());                                        //Application.Current.MainPage = new NavigationPage(new Views.MyTabbedPage());
                                    }
                                };

                                await PopupNavigation.Instance.PushSingleAsync(digitalPrintCancelPopup);
                            }
                        };

                        await PopupNavigation.Instance.PushSingleAsync(digitalPrintPopup);
                    }
                    else
                    {

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

                        //Application.Current.MainPage = new NavigationPage(new Views.MyTabbedPage());
                    }


                }
                else
                {
                    AppHelpers.Alert(result?.message);
                }
            }
            catch (Exception ex)
            {
                //Logger?.LogError(ex);
                AppHelpers.Alert(ex.Message, exception: ex);
            }
            finally
            {
                AppHelpers.LoadingHide();

                CanSubmitCommand = true;
            }
        }, () => CanSubmitCommand);

        private bool CanOpenFingerPrintView = true;
        public ICommand OpenFingerPrintViewCommand => new Command(async () =>
        {
            try
            {
                CanOpenFingerPrintView = false;

                var requestFingerprint = new AuthenticationRequestConfiguration($"{XForms.Constants.AppConstants.AppName} verrouillé", "Veuillez appliquer votre empreinte digitale");
                requestFingerprint.CancelTitle = "Annuler";
                var resultFingerprint = await CrossFingerprint.Current.AuthenticateAsync(requestFingerprint);

                if (resultFingerprint.Authenticated)
                {
                    AppHelpers.LoadingShow();

                    //var postParams = new RefreshTokenRequest()
                    //{
                    //    RefreshToken = Settings.RefreshToken,
                    //};

                    //var deviceUID = Xamarin.Forms.DependencyService.Get<IDevice>().GetIdentifier();

                    //var deviceName = DeviceInfo.Name;

                    //var postParams = new LoginRequestModel()
                    //{
                    //    UserName = Settings.HashOne,
                    //    Password = Settings.HashTwo,
                    //    Uid = deviceUID,
                    //    DeviceName = deviceName,
                    //};

                    //var result = await App.AppServices.PostLoginAction(postParams);

                    //var result = await App.AppServices.PostRefreshToken(postParams);

                    //if (!string.IsNullOrEmpty(result?.AccessToken))
                    //{
                    //    Settings.IsLoggedIn = true;

                    //    Settings.AccessToken = result.AccessToken;
                    //    Settings.UserName = result.UserName;
                    //    Settings.RefreshToken = result.RefreshToken;

                    //    Settings.ValidUntil = DateTime.UtcNow.AddSeconds(AppConstants.SessionDuration);

                    //    Settings.IsAleardyCheckHasNotchScreen = false;
                    //    App.IsSetDynamicResources = false;

                    //    #region Firebase Plugin Callbacks
                    //    Plugin.FirebasePushNotification.CrossFirebasePushNotification.Current.Subscribe("general");
                    //    await App.AppServices.PostSaveFirebaseToken();
                    //    #endregion

                    //    App.IsAppAleardyOpen = true;

                    AppPreferences.IsDigitalPrintActived = true;

                    //    Settings.IsLoggedIn = true;

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
                    //Application.Current.MainPage = new NavigationPage(new Views.MyTabbedPage());
                }
                else
                {
                    var digitalPrintFailedPopup = new FeedBackPopup(
                        headerGlyph: Resources.FontAwesomeFonts.close,
                        headerGlyphBackground: AppHelpers.LookupColor("Red"),
                        title: "Erreur",
                        description: "Votre empreinte digitale n'est pas conforme, veuillez réessayer",
                        confirmActionText: "D’accord",
                        primaryColor: AppHelpers.LookupColor("Red"));
                    digitalPrintFailedPopup.OnEventAcquired += async (sender, args) =>
                    {
                        if (args)
                        {
                            await PopupNavigation.Instance.PopSafeAsync();
                        }
                    };

                    await PopupNavigation.Instance.PushSingleAsync(digitalPrintFailedPopup);
                }

            }
            catch (Exception ex)
            {
                //Logger.LogError(ex);
            }
            finally
            {
                AppHelpers.LoadingHide();

                CanOpenFingerPrintView = true;
            }
        }, () => CanOpenFingerPrintView);



    }
}