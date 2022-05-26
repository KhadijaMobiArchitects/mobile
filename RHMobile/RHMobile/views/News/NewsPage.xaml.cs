using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class NewsPage : BasePage
    {
        public NewsPage()
        {
            InitializeComponent();
            BindingContext = new NewsViewModel();

        }
        protected async override void OnAppearing()
        {
            try
            {
                if (BindingContext == null)
                {
                    BindingContext = new NewsViewModel();
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
