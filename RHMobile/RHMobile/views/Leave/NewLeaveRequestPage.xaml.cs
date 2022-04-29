using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Leave
{
    public partial class NewLeaveRequestPage : BasePage
    {
       public Color ButtonConfirmedBySquadBackground { get; set; }

        public NewLeaveRequestPage()
        {
            InitializeComponent();


            BindingContext = new NewLeaveRequestViewModel();
        }

        void CustomButton_Clicked(System.Object sender, System.EventArgs e)
        {
            //Console.WriteLine("Hello worlds ");

        }

        void NotifySquad(System.Object sender, System.EventArgs e)
        {
            //(BindingContext as NewLeaveRequestViewModel).ConfirmedBySquad = !(BindingContext as NewLeaveRequestViewModel).ConfirmedBySquad;

            // ButtonConfirmedBySquadBackground = (BindingContext as NewLeaveRequestViewModel).ConfirmedBySquad ? Color.Blue : Color.White;

            //OnPropertyChanged(nameof(ButtonConfirmedBySquadBackground));
        }
    }
}
