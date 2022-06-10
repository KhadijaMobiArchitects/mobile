using System;
using System.Collections.Generic;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XForms.Models;
using XForms.views.Authentication;

namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]

    public class BaseViewModel : BindableObject
    {
        public List<REFItem> HeadrActionList { get; set; }

        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public string RefFunctionLabel { get; set; }
        public bool EnableFaceID { get; set; }

        public BaseViewModel()
        {
            FullName = AppPreferences.FullName;
            PictureUrl = AppPreferences.PictureUrl;
            RefFunctionLabel = AppPreferences.RefFunctionLabel;

            HeadrActionList = new List<REFItem>()
            {
                new REFItem()
                {
                    Id = 1,
                    Name = "en cours",
                    IsSelected = true
                },
                new REFItem()
                {
                    Id = 2,
                    Name = "validée",
                }
            };
        }

        public virtual void OnAppearing()
        {
            try
            {

            }
            catch (Exception ex)
            {
                //Logger?.LogError(ex);
            }
            finally
            {

            }
        }
        public virtual void OnDisappearing() { }

        private bool CanLogout = true;
        public ICommand LogoutCommand => new Command(() =>
        {
            try
            {
                CanLogout = false;

                //AppPreferences.ClearCache();
                PopupNavigation.Instance.PopAllAsync();

                Application.Current.MainPage = new NavigationPage(new SigninPage());
            }
            catch (Exception ex)
            {
                //Logger?.LogError(ex, showError: true);
            }
            finally
            {
                CanLogout = true;
            }
        }, () => CanLogout);

        private bool CanOpenFlyout = true;
        private views.FlyoutPage flyoutPagePopup;
        public ICommand OpenFlyoutCommand => new Command(async () =>
        {
            try
            {
                CanOpenFlyout = false;
                if (flyoutPagePopup == null)
                    flyoutPagePopup = new views.FlyoutPage() { BindingContext = this};

                await PopupNavigation.Instance.PushSingleAsync(flyoutPagePopup);
            }
            catch (Exception ex)
            {
                //Logger?.LogError(ex, showError: true);
            }
            finally
            {
                CanOpenFlyout = true;
            }
        }, () => CanOpenFlyout);

        

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
                    //OnPropertyChanged(nameof(item.IsSelected));


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


    }

}

