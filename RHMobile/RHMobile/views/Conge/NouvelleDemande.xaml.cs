using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Conge
{
    public partial class NouvelleDemande : BasePage
    {
        public NouvelleDemande()
        {
            InitializeComponent();


            BindingContext = new NouvelleDemandeViewModel();
        }

        void CustomButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Hello worlds ");

        }
    }
}
