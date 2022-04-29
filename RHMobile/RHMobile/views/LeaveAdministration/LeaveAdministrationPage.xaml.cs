using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.LeaveAdministration
{
    public partial class LeaveAdministrationPage : BasePage
    {
        public LeaveAdministrationPage()
        {
            InitializeComponent();

            //if (BindingContext == null)
            //    BindingContext = new LeaveRequestViewModel();
        }

        protected async override void OnAppearing()
        {
            try
            {
                if (BindingContext == null)
                {
                    BindingContext = new LeaveRequestViewModel();
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

