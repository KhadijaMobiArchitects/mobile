using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XForms.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)

                Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}
