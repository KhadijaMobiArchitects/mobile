using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.Controls;
using XForms.Models;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Leave
{
    public partial class LeaveRequest : BasePage
    {
        public LeaveRequest()
        {
            InitializeComponent();

            BindingContext = new LeaveRequestViewModel();
        }

        //private void SelectItem_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    var view = sender as CustomButton;

        //    if (view != null) {

        //        var item = view.BindingContext as REFItem;

        //        if (item != null) {

        //            var vm = this.BindingContext as LeaveRequestViewModel;
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await (BindingContext as LeaveRequestViewModel).getLeavesList();
        }
    }
    

}
//if ((sender as View)?.BindingContext is REFItem item)
//{
//    (this.BindingContext as LeaveRequestViewModel)?.SelectHeaderActionCommand.Execute(item);
//}