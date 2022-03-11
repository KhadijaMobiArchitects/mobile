using System;
using Xamarin.Forms;
using XForms.Enums;

namespace XForms.Controls
{
    public class CornerView : Frame
    {
        private ControlStyleCorner styleCorner = ControlStyleCorner.Circle;
        public ControlStyleCorner StyleCorner
        {
            get { return styleCorner; }
            set { styleCorner = value; }
        }

        public CornerView()
        {
            this.Padding = 0;
            this.HasShadow = false;
            this.IsClippedToBounds = true;

            this.SizeChanged += (object sender, EventArgs e) =>
            {
                switch (StyleCorner)
                {
                    case ControlStyleCorner.Circle:
                        this.WidthRequest = this.Height - this.Padding.HorizontalThickness;
                        this.CornerRadius = (float)this.Height / 2;
                        break;
                    case ControlStyleCorner.RoundCorner:
                        this.CornerRadius = (float)this.Height / 2;
                        break;
                    case ControlStyleCorner.Square:
                        this.HeightRequest = this.Width;
                        break;

                };

            };
        }
    }
}
