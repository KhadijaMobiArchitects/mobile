using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Administration
{
    public partial class HomePage : BasePage
    {
        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();


        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if (Device.RuntimePlatform == Device.iOS)
        //    {
        //        var isHasNotchScreen = AppHelpers.CheckHasNotchScreen();

        //        MyHeader.Padding = isHasNotchScreen ? new Thickness(30, 40, 30, 0) : new Thickness(30, 30, 20, 0);
        //    }
        //}

    }
}
