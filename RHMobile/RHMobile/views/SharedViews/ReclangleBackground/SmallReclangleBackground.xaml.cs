using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.views.Base;

namespace XForms.views.SharedViews
{
    public partial class SmallReclangleBackground : BaseContent
    {
        public static readonly BindableProperty  StartGradientProperty =
    BindableProperty.Create(nameof(StartGradient), typeof(Color), typeof(View),default, BindingMode.TwoWay);

        public Color StartGradient
        {
            get { return (Color)GetValue(StartGradientProperty); }
            set
            {
                SetValue(StartGradientProperty, value);
            }
        }

        public static readonly BindableProperty EndGradientProperty =
BindableProperty.Create(nameof(EndGradient), typeof(Color), typeof(View), default, BindingMode.TwoWay);

        public Color EndGradient
        {
            get { return (Color)GetValue(EndGradientProperty); }
            set
            {
                SetValue(EndGradientProperty, value);
            }
        }

        public SmallReclangleBackground()
        {
            InitializeComponent();
        }
    }
}
