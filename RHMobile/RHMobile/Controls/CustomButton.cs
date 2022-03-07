using System;
using Xamarin.Forms;
using XForms.Enums;

namespace XForms.Controls
{
    public class CustomButton : Button
    {
        public ControlStyleCorner StyleCorner { get; set; } = ControlStyleCorner.Default;


        public CustomButton()
        {
            this.TextColor = Color.White;
            this.FontAttributes = FontAttributes.Bold;
            this.FontSize = 14;

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
