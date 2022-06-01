using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.Constants;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class DisplacementPage : BasePage
    {
        public DisplacementPage()
        {
            InitializeComponent();
            BindingContext = new DisplacementViewModel();

            MessagingCenter.Subscribe<DisplacementViewModel, int>(this, AppConstants.SendPipUp, async (sender, arg) =>
            {
                _= pin.TranslateTo(0, arg, 500, Easing.Linear);

            });
        }

    }
}
