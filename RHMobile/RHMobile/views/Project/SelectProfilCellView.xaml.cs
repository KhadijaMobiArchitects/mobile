using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XForms.Models;
using XForms.ViewModels;

namespace XForms.views.Project
{
    public partial class SelectProfilCellView : ContentView
    {
        public bool IsChecked { get; set; }

        public static readonly BindableProperty IsSelectedProperty =
BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(View),false, BindingMode.TwoWay);

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

//        public static readonly BindableProperty SelectProfilProperty =
//BindableProperty.Create(nameof(SelectProfil), typeof(ICommand), typeof(View), default, BindingMode.TwoWay);

//        public ICommand SelectProfil
//        {
//            get { return (ICommand)GetValue(SelectProfilProperty); }
//            set
//            {
//                SetValue(SelectProfilProperty, value);
//            }
//        }

        public SelectProfilCellView()
        {
            InitializeComponent();
        }

        //void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        //{
        //    if ((sender as View).BindingContext is Profil item)
        //    {
        //        (this.Parent.BindingContext as NewProjectViewModel).SelectProfil.Execute(item);
        //    }
        //}
    }
}
