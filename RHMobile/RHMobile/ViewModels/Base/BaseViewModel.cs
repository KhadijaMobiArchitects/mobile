using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XForms.Models;
using XForms.views.Authentication;

namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]

    public class BaseViewModel : BindableObject
    {
        public List<REFItem> HeadrActionList { get; set; }
        public List<REFItem> HeadrActionListAdmin { get; set; }


        public BaseViewModel()
        {
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

                AppPreferences.ClearCache();

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

    }

}

