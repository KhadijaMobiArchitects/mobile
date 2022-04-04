using System;
using System.Collections.Generic;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using XForms.Models;
using XForms.ViewModels;

namespace XForms.views.Administration
{
    public partial class AdministrationCellView : Grid
    {
        public static readonly BindableProperty TitleProperty =
BindableProperty.Create(nameof(Title), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly BindableProperty IConeProperty =
BindableProperty.Create(nameof(ICone), typeof(SvgImageSource), typeof(View), null, BindingMode.TwoWay);

        public SvgImageSource ICone
        {
            get { return (SvgImageSource)GetValue(IConeProperty); }
            set
            {
                SetValue(IConeProperty, value);
            }
        }


        public AdministrationCellView()
        {
            InitializeComponent();
        }

        void SelectItem_Clicked(System.Object sender, System.EventArgs e)
        {
            if ((sender as View).BindingContext is REFItemAdministration item)
            {
                (this.Parent.BindingContext as HomeViewModel).AdministraionNavigation.Execute(item);
            }

        }
    }
}
