using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Leave
{
    public partial class NewLeaveRequestPage : BasePage
    {
        public NewLeaveRequestPage()
        {
            InitializeComponent();


            BindingContext = new NewLeaveRequestViewModel();
        }

        void CustomButton_Clicked(System.Object sender, System.EventArgs e)
        {
            //Console.WriteLine("Hello worlds ");

        }
    }
}
