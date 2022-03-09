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
    }
}
