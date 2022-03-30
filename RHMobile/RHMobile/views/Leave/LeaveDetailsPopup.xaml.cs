using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.views.Base;

namespace XForms.views.Leave
{
    public partial class LeaveDetailsPopup : BasePopupView
    {
        public string HeaderGlyph { get; set; }
        public Color HeaderGlyphBackground { get; set; }


        public LeaveDetailsPopup(
            string headerGlyph = "",
            Color headerGlyphBackground = default
            )
        {
            InitializeComponent();

            BindingContext = this;

            HeaderGlyph = headerGlyph;
            HeaderGlyphBackground = headerGlyphBackground;


        }
    }
}
