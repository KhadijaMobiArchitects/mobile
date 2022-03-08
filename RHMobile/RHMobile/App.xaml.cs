using System;
using XForms.views.Authentication;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XForms.views.Conge;

namespace XForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NouvelleDemande();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
