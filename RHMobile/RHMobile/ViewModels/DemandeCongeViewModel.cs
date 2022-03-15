using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using Xamarin.Forms;
using XForms.Constants;
using XForms.Models;
namespace XForms.ViewModels
{
    public class DemandeCongeViewModel : BindableObject
    {
        public List<REFItem> HeadrActionList { get; set; }

        public List<Conge> ListConge { get; set; }

        public List<Conge> ListCongeitems { get; set; }
        public List<ChartEntry> entries { get; set; }


        public List<Conge> ListCongeEncours { get; set; }
        public List<Conge> ListCongeConfirme { get; set; }
        public List<Conge> ListCongeReporte { get; set; }

        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public int nbreDemandes { get; set; }
        public DonutChart donutChart { get; set; }


        //public ObservableRangeCollection<ObservableGroupCollection<string, Conge>> CongeList { get; set; }

        public DemandeCongeViewModel()
        {
            ListCongeConfirme = new List<Conge>();
            ListCongeEncours = new List<Conge>();
            ListCongeReporte = new List<Conge>();

            entries = new List<ChartEntry>
            {
                new ChartEntry(30)
                {
                    Color = SKColor.Parse("#cccccc")
                },
                new ChartEntry(50)
                {
                    Color = SKColor.Parse("#bbbbbb")
                },
                new ChartEntry(20)
                {
                    Color = SKColor.Parse("#aaaaaa")
                },
            };

            donutChart = new DonutChart()
            {
                Entries = entries,
                MinValue = 0,
                MaxValue = 100,
                HoleRadius = 0.6f,


            };

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

            //ListConge = new List<Conge>()
            //{
            //    new Conge()
            //    {
            //        Type="Conge annuel",
            //        Status="En cours",
            //        DateDebut = new DateTime(2022,4,20),
            //        DateFin = new DateTime(2022,9,2)
            //    },
            //    new Conge(){
            //        Type="Conge Mensuel",
            //        Status="Confirmé",
            //        DateDebut = new DateTime(2021,7,20),
            //        DateFin = new DateTime(2021,8,12)
            //    }
            //};

            //Uri uri = new Uri(Uri);

            var client = new HttpClient();
            var resp = client.GetAsync(AppUrls.GesRequestsListConge);

            if (resp.Result.IsSuccessStatusCode)
            {
                var content = resp.Result.Content.ReadAsStringAsync();
                ListConge = JsonConvert.DeserializeObject<List<Conge>>(content.Result.ToString());


            }

            Classerconge(ListConge);

            ListCongeitems = ListCongeEncours;
            nbreDemandes = ListCongeitems.Count;

        }

        private void Classerconge(List<Conge> listConge)
        {
            foreach (var item in listConge)
            {
                if (item.Status == "En cours")
                {
                    ListCongeEncours.Add(item);
                }
                else if (item.Status == "Confirmé")
                {
                    ListCongeConfirme.Add(item);
                }
                else if (item.Status == "Reporté")
                {
                    ListCongeReporte.Add(item);
                }
            }
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

                if (HeadrActionList[0].IsSelected)
                {
                    ListCongeitems = ListCongeEncours;

                }
                else if (HeadrActionList[1].IsSelected)
                {
                    ListCongeitems = ListCongeConfirme;

                }
                else if (HeadrActionList[2].IsSelected)
                {
                    ListCongeitems = ListCongeReporte;

                }

                nbreDemandes = ListCongeitems.Count;
                OnPropertyChanged(nameof(ListCongeitems));
                OnPropertyChanged(nameof(nbreDemandes));

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
