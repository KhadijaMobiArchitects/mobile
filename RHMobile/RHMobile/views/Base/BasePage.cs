using System;
using Xamarin.Forms;
using XForms.ViewModels;

namespace XForms.views.Base
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            if (BindingContext is BaseViewModel bindingContext)
                bindingContext.OnAppearing();
        }
    }
}
