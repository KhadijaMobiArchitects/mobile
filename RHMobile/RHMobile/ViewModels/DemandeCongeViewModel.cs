using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Microcharts;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using Xamarin.Forms;
using XForms.Constants;
using XForms.Enum;
using XForms.Models;
using XForms.views.Conge;

namespace XForms.ViewModels
{
    public class DemandeCongeViewModel : BindableObject
    {
        public List<REFItem> HeadrActionList { get; set; }

        public List<Conge> ListConge { get; set; }
        //public ObservableCollection<Conge> ListConge { get; set; }


        public List<Conge> ListCongeitems { get; set; }
        public List<ChartEntry> entries { get; set; }


        public List<Conge> ListCongeEncours { get; set; }
        public List<Conge> ListCongeConfirme { get; set; }
        public List<Conge> ListCongeReporte { get; set; }

        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public int nbreDemandes { get; set; }
        public DonutChart donutChart { get; set; }

        public int ConfirmedDays { get; set; }
        public int InprogessDays { get; set; }
        public int PostponedDays { get; set; }
        public int TotalDays => 26;

        //public INavigation Navigation;


        //public ObservableRangeCollection<ObservableGroupCollection<string, Conge>> CongeList { get; set; }

        public DemandeCongeViewModel()
        {
            ListCongeConfirme = new List<Conge>();
            ListCongeEncours = new List<Conge>();
            ListCongeReporte = new List<Conge>();


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
                    Name = "Demande reportée",
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

            //ListConge = new List<Conge>();

            //getListConge(ListConge);

            donutChart = new DonutChart()
            {
                MinValue = 0,
                MaxValue = 26,
                HoleRadius = 0.7f,


            };

        }


        public async Task getListConge()
        {
            try
            {
                //                var client = new HttpClient();
                //var resp = await client.GetAsync(AppUrls.GesRequestsListConge);

                //    if (resp.IsSuccessStatusCode)
                //    {
                //        var content = resp.Content.ReadAsStringAsync();
                //ListConge = JsonConvert.DeserializeObject<List<Conge>>(content.Result.ToString());

                var result = await App.AppServices.GetConges();

                ListConge = result.data.ToList();
                //ListConge = new ObservableCollection<Conge>(CongesList);



                Classerconge(ListConge);
                DifferenceOfDays(ListCongeEncours, ListCongeConfirme, ListCongeReporte);
                ListCongeitems = ListCongeEncours;
                nbreDemandes = ListCongeitems.Count;

                OnPropertyChanged(nameof(ListCongeitems));
                OnPropertyChanged(nameof(nbreDemandes));

                entries = new List<ChartEntry>
            {
                new ChartEntry(ConfirmedDays)
                {
                    Color = SKColor.Parse("#95D5A4")
                },
                new ChartEntry(InprogessDays)
                {
                    Color = SKColor.Parse("#FEE07D")
                },
                new ChartEntry(PostponedDays)
                {
                    Color = SKColor.Parse("#D59595")
                },
                new ChartEntry(TotalDays)
                {
                    Color = SKColor.Parse("#E4FAE8")
                },
            };

                donutChart.Entries = entries;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }


        }

        private void DifferenceOfDays(List<Conge> listCongeEncours, List<Conge> listCongeConfirme, List<Conge> listCongeReporte)
        {
            ConfirmedDays = 0;
            InprogessDays = 0;
            PostponedDays = 0;

            foreach (var item in listCongeConfirme)
            {
                ConfirmedDays += item.DifferenceOfDays;
            }
            foreach (var item in listCongeEncours)
            {
                InprogessDays += item.DifferenceOfDays;
            }
            foreach (var item in listCongeReporte)
            {
                PostponedDays += item.DifferenceOfDays;
            }

            OnPropertyChanged(nameof(ConfirmedDays));
            OnPropertyChanged(nameof(InprogessDays));
            OnPropertyChanged(nameof(PostponedDays));


        }

        private void Classerconge(List<Conge> listConge)
        {
            ListCongeConfirme.Clear();
            ListCongeEncours.Clear();
            ListCongeReporte.Clear();

            foreach (var item in listConge)
            {
                if (item.StatusID == (int)StatusConge.Inprogress)
                {
                    ListCongeEncours.Add(item);
                }
                else if (item.StatusID == (int)StatusConge.Confirmed)
                {
                    ListCongeConfirme.Add(item);
                }
                else if (item.StatusID == (int)StatusConge.Postponed)
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



        public ICommand NavigationtonewRequest => new Command(() =>
        {
            App.Current.MainPage.Navigation.PushAsync(new NouvelleDemande());


            HeadrActionList[0].IsSelected = true;
            HeadrActionList[1].IsSelected = false;
            HeadrActionList[2].IsSelected = false;

        },
    () => true


    );

        private LeaveDatailsPopup leaveDatailsPopup;
        public ICommand OpenLeaveDtailsPopupView => new Command(async () =>
        {
            try
            {
                if (leaveDatailsPopup == null)
                {
                    leaveDatailsPopup = new LeaveDatailsPopup();
                }

                await PopupNavigation.Instance.PushAsync(leaveDatailsPopup);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {

            }
        },
        () => true );


    }
}
