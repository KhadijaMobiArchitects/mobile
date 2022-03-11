using System;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using XForms.Enums;

namespace XForms.Controls
{
    public class CornerBox : PancakeView
    {
        private ControlStyleCorner styleCorner = ControlStyleCorner.SemiRounded;
        public ControlStyleCorner StyleCorner
        {
            get { return styleCorner; }
            set { styleCorner = value; }
        }

        public CornerBox()
        {
            this.SizeChanged += (object sender, EventArgs e) =>
            {
                switch (StyleCorner)
                {

                    case ControlStyleCorner.SemiRounded:

                        this.CornerRadius = new CornerRadius(0, 0, this.Width / 2  , this.Width / 2);

                        break;
                };
            };

        }
    }
}
