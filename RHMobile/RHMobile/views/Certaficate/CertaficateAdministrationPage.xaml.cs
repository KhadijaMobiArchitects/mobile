using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class CertaficateAdministrationPage : BasePage
    {
        public CertaficateAdministrationPage()
        {
            InitializeComponent();
            BindingContext = new CertaficateAdministrationViewModel();

        }
        protected async override void OnAppearing()
        {
            try
            {
                if (BindingContext == null)
                {
                    BindingContext = new CertaficateAdministrationViewModel();
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
