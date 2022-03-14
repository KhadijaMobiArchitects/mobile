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

            Device.SetFlags(new string[] { "CollectionView_Experimental", "SwipeView_Experimental", "Shapes_Experimental", "FastRenderers_Experimental", "Brush_Experimental", "RadioButton_Experimental" });

            MainPage = new DemandeConge();
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
