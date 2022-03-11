using System;
using Xamarin.Forms;
using XForms.Enums;

namespace XForms.Controls
{
    public class CustomButton : Button
    {
        public ControlStyleCorner StyleCorner { get; set; } = ControlStyleCorner.Default;

        public static readonly BindableProperty FocsudViewProperty = BindableProperty.Create(nameof(FocsudView), typeof(View), typeof(Entry));
        public View FocsudView
        {
            get => (View)GetValue(FocsudViewProperty);
            set => SetValue(FocsudViewProperty, value);
        }

        public CustomButton()
        {

            this.Clicked += (object sender, EventArgs e) =>
            {
                FocsudView?.Focus();
            };


            this.SizeChanged += (object sender, EventArgs e) =>
            {
                switch (StyleCorner)
                {
                    case ControlStyleCorner.Circle:
                        this.WidthRequest = this.Height;
                        this.CornerRadius = (int)this.Height / 2;
                        break;
                    case ControlStyleCorner.RoundCorner:
                        this.CornerRadius = (int)this.Height / 2;
                        break;
                }
            };

        }
    }
}
