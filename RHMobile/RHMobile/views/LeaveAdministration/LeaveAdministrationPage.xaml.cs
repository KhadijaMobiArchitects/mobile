using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class LeaveAdministrationPage : BasePage
    {
        public LeaveAdministrationPage()
        {
            InitializeComponent();

            //if (BindingContext == null)
            //    BindingContext = new LeaveRequestViewModel();
            BindingContext = new LeaveAdministrationViewModel();

        }

        protected async override void OnAppearing()
        {
            try
            {
                if (BindingContext == null)
                {
                    BindingContext = new LeaveAdministrationViewModel();
                }

                base.OnAppearing();
            }
            catch (Exception ex)
            {
                //AppHelpers.Alert(ex.Message, exception: ex);
            }
            //base.OnAppearing();

            //await (BindingContext as LeaveRequestViewModel).getLeavesList();
        }
    }

}

