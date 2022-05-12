using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    public class CertaficateViewModel : BaseViewModel
    {
        public List<REFItem> HeadrActionList { get; set; }

        public CertaficateViewModel()
        {
            HeadrActionList = new List<REFItem>()
            {
                new REFItem()
                {
                    Id = 1,
                    Name = "en cours",
                    IsSelected = true
                },
                new REFItem()
                {
                    Id = 2,
                    Name = "validée",
                }
            };
        }

        private bool CanSelectHeaderAction = true;
        public ICommand SelectHeaderActionCommand => new Command<REFItem>(async (model) =>
        {
            try
            {
                CanSelectHeaderAction = false;

                if (model == null) return;

                foreach (var item in HeadrActionList)
                {
                    item.IsSelected = (item.Id == model.Id);
                    OnPropertyChanged(nameof(item.IsSelected));


                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CanSelectHeaderAction = true;
            }
        },
        (_) => CanSelectHeaderAction);
     }
}
