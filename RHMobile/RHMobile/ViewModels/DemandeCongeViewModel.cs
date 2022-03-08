using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    public class DemandeCongeViewModel : BindableObject
    {
        public List<REFItem> HeadrActionList { get; set; }

        public List<Conge> CongeList { get; set; }

        //public ObservableRangeCollection<ObservableGroupCollection<string, Conge>> CongeList { get; set; }

        public DemandeCongeViewModel()
        {

            HeadrActionList = new List<REFItem>()
            {
                new REFItem()
                {
                    Id = 1,
                    Name = "Demande en cours",
                    IsSelected = true
                },
                new REFItem()
                {
                    Id = 2,
                    Name = "Demande validée",
                },
                new REFItem()
                {
                    Id = 3,
                    Name = "Demande reporté",
                }
            };

            CongeList = new List<Conge>()
            {
                new Conge()
                {
                    Type="Conge annuel",
                    Status="En cours",
                    DateDebut = new DateTime(2022,4,20),
                    DateFin = new DateTime(2022,9,2)
                },
                new Conge(){
                    Type="Conge Mensuel",
                    Status="Confirmé",
                    DateDebut = new DateTime(2021,7,20),
                    DateFin = new DateTime(2021,8,12)
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

                    //item.BackgroundColor = ;

                }

            }
            catch (Exception ex)
            {
                //Logger.LogError(ex);
            }
            finally
            {
                CanSelectHeaderAction = true;
            }
        },
        (_) => CanSelectHeaderAction);
    }
}
