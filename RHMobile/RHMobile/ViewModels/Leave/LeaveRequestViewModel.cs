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
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Constants;
using XForms.Enum;
using XForms.Models;
using XForms.ViewModels;
using XForms.views.Leave;

namespace XForms.ViewModels
{
    public class LeaveRequestViewModel : BaseViewModel
    {
        public List<REFItem> HeadrActionList { get; set; }

        public List<Leave> LeaveDate { get; set; }
        public ObservableRangeCollection<Leave> LeavesList { get; set; }


        public List<Leave> LeaveItemsList { get; set; }
        public List<ChartEntry> entries { get; set; }


        public List<Leave> InprogessLeavesList { get; set; }
        public List<Leave> ConfirmedLeavesList { get; set; }
        public List<Leave> PostponedLeavesList { get; set; }

        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public int numberOfRequests { get; set; }
        public DonutChart donutChart { get; set; }

        public int ConfirmedDays { get; set; }
        public int InprogessDays { get; set; }
        public int PostponedDays { get; set; }
        public int TotalDays => ConfirmedDays + InprogessDays + PostponedDays;

        public Leave SelectedLeave { get; set; }

        public string StatusName { get; set; }

        //public INavigation Navigation;


        //public ObservableRangeCollection<ObservableGroupCollection<string, Leave>> LeaveList { get; set; }

        public LeaveRequestViewModel()
        {
            ConfirmedLeavesList = new List<Leave>();
            InprogessLeavesList = new List<Leave>();
            PostponedLeavesList = new List<Leave>();

            //StatusName = "en cours";

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
                },
                new REFItem()
                {
                    Id = 3,
                    Name = "reportée",
                }
            };

            donutChart = new DonutChart()
            {
                MinValue = 0,
                MaxValue = 26,
                HoleRadius = 0.7f,


            };

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            //if (InterventionsList?.Any() != true || App.CanRefreshHome)
            //{
            //    await GetIntervenetionsData();

            //    App.CanRefreshHome = false;
            //}
            StatusName = "en cours";
            OnPropertyChanged(nameof(StatusName));
            await getLeavesList();


            //StatusName = HeadrActionList[0].IsSelected ? HeadrActionList[0].Name : (HeadrActionList[1].IsSelected ? HeadrActionList[1].Name : HeadrActionList[2].Name);

        }

        public async Task getLeavesList()
        {
            try
            {
                var result = await App.AppServices.GetLeaves();

                LeaveDate = result.data.ToList();
                LeavesList = new ObservableRangeCollection<Leave>(LeaveDate);



                FilterLeaves(LeavesList);
                DifferenceOfDays(InprogessLeavesList, ConfirmedLeavesList, PostponedLeavesList);
                LeaveItemsList = InprogessLeavesList;
                numberOfRequests = LeaveItemsList.Count;

                OnPropertyChanged(nameof(LeaveItemsList));
                OnPropertyChanged(nameof(numberOfRequests));

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

        private void DifferenceOfDays(List<Leave> InprogessLeavesList, List<Leave> ConfirmedLeavesList, List<Leave> PostponedLeavesList)
        {
            ConfirmedDays = 0;
            InprogessDays = 0;
            PostponedDays = 0;

            foreach (var item in ConfirmedLeavesList)
            {
                ConfirmedDays += item.DifferenceOfDays;
            }
            foreach (var item in InprogessLeavesList)
            {
                InprogessDays += item.DifferenceOfDays;
            }
            foreach (var item in PostponedLeavesList)
            {
                PostponedDays += item.DifferenceOfDays;
            }

            OnPropertyChanged(nameof(ConfirmedDays));
            OnPropertyChanged(nameof(InprogessDays));
            OnPropertyChanged(nameof(PostponedDays));
            OnPropertyChanged(nameof(TotalDays));
        }

        private void FilterLeaves(ObservableRangeCollection<Leave> LeavesList)
        {
            ConfirmedLeavesList.Clear();
            InprogessLeavesList.Clear();
            PostponedLeavesList.Clear();

            foreach (var item in LeavesList)
            {
                if (item.RefStatusLeaveId == (int)LeaveStatus.Inprogress)
                {
                    InprogessLeavesList.Add(item);
                }
                else if (item.RefStatusLeaveId == (int)LeaveStatus.Confirmed)
                {
                    ConfirmedLeavesList.Add(item);
                }
                else if (item.RefStatusLeaveId == (int)LeaveStatus.Postponed)
                {
                    PostponedLeavesList.Add(item);
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


                }

                if (HeadrActionList[0].IsSelected)
                {
                    LeaveItemsList = InprogessLeavesList;

                }
                else if (HeadrActionList[1].IsSelected)
                {
                    LeaveItemsList = ConfirmedLeavesList;

                }
                else if (HeadrActionList[2].IsSelected)
                {
                    LeaveItemsList = PostponedLeavesList;

                }

                numberOfRequests = LeaveItemsList.Count;
                OnPropertyChanged(nameof(LeaveItemsList));
                OnPropertyChanged(nameof(numberOfRequests));

                StatusName = HeadrActionList[0].IsSelected ? HeadrActionList[0].Name : (HeadrActionList[1].IsSelected ? HeadrActionList[1].Name + "s" : HeadrActionList[2].Name + "s");
                OnPropertyChanged(nameof(StatusName));

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
            App.Current.MainPage.Navigation.PushAsync(new NewLeaveRequest());

            HeadrActionList[0].IsSelected = true;
            HeadrActionList[1].IsSelected = false;
            HeadrActionList[2].IsSelected = false;

        },
    () => true
    );

        private LeaveDetailsPopup leaveDetailsPopup;

        private bool canOpenLeaveDetailsPopup = true;
        public ICommand OpenLeaveDetailsPopupView => new Command<Leave>(async (model) =>
        {
            try
            {
                canOpenLeaveDetailsPopup = false;

                if (model == null)
                    return;

                SelectedLeave = model;

                if (leaveDetailsPopup == null)
                {
                    leaveDetailsPopup = new LeaveDetailsPopup() { BindingContext = this};
                }
                OnPropertyChanged(nameof(SelectedLeave));

                await PopupNavigation.Instance.PushSingleAsync(leaveDetailsPopup);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                canOpenLeaveDetailsPopup = true;


            }
        },
        (_) => canOpenLeaveDetailsPopup );

       


    }
}
