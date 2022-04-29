using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Walkthrough
{
    public partial class WalkthroughPage : BasePage
    {
        public WalkthroughPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (BindingContext == null)
            {
                BindingContext = new WalkthroughViewModel();
            }

            base.OnAppearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            //if (width > 0 && height > 0)
            //{
            //    MainCarouselView.HeightRequest = Device.RuntimePlatform == Device.Android ? height / 2 : height / 2;
            //    //MainCarouselView.ScrollTo(0, 0, ScrollToPosition.End, false);
            //}
        }
    }
}
