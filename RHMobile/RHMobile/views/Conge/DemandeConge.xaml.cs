using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.Controls;
using XForms.Models;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Conge
{
    public partial class DemandeConge : BasePage
    {
        public DemandeConge()
        {
            InitializeComponent();

            BindingContext = new DemandeCongeViewModel();
        }

        //private void SelectItem_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    var view = sender as CustomButton;

        //    if (view != null) {

        //        var item = view.BindingContext as REFItem;

        //        if (item != null) {

        //            var vm = this.BindingContext as DemandeCongeViewModel;
        //            if (vm != null) {

        //                //var demande = new REFItem
        //                //{
        //                //    Id = item.Id,
        //                //    Name = item.Name


        //                //};


        //                vm.SelectHeaderActionCommand.Execute(item);
        //            }
        //        }
        //    }


        //}
    }
}
//if ((sender as View)?.BindingContext is REFItem item)
//{
//    (this.BindingContext as DemandeCongeViewModel)?.SelectHeaderActionCommand.Execute(item);
//}