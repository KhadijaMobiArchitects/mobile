using System;
using Android.Content;
using System.Runtime.Remoting.Contexts;
using Xamarin.Forms.Platform.Android;
using XForms.Controls;
using Xamarin.Forms;
using XFroms.Droid.Renderers;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace XFroms.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Android.Content.Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                Control.Background = null;
        }

    }
}
