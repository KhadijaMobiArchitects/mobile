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
using XForms.views;
using XForms.views.Leave;

namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]           
    public class LeaveRequestViewModel : BaseViewModel
    {
        public List<REFItem> HeadrActionList { get; set; }


        public List<LeaveResponse> LeaveDate { get; set; }
        public ObservableRangeCollection<LeaveResponse> LeavesList { get; set; }


        public ObservableRangeCollection<LeaveResponse> LeaveItemsList { get; set; }

        public List<ChartEntry> entries { get; set; }


        public List<LeaveResponse> InprogessLeavesList { get; set; }
        public List<LeaveResponse> ConfirmedLeavesList { get; set; }
        public List<LeaveResponse> PostponedLeavesList { get; set; }

        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public int numberOfRequests { get; set; }
        public string numberOfRequestsDescription { get; set; }

        public int numberOfRequestsAdmin { get; set; }

        public DonutChart donutChart { get; set; }

        public int ConfirmedDays { get; set; }
        public int InprogessDays { get; set; }
        public int PostponedDays { get; set; }
        public int TotalDays { get; set;}

        public StatistiqueLeaveModel statistiqueLeaveModel { get; set; }
        public LeaveResponse SelectedLeave { get; set; }

        public string StatusName { get; set; }

        public bool IsShowButtonCancelRequest { get; set; }

        public LeaveRequestViewModel()
        {
            ConfirmedLeavesList = new List<LeaveResponse>();
            InprogessLeavesList = new List<LeaveResponse>();
            PostponedLeavesList = new List<LeaveResponse>();

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
                    Name = "Refusée",
                }
            };



            donutChart = new DonutChart()
            {
                MinValue = 0,
                MaxValue = 18,
                HoleRadius = 0.7f,

            };

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var result = await App.AppServices.GetStatistics_ProfilLeaves();
                statistiqueLeaveModel = result.data;

                StatusName = "en cours";
                OnPropertyChanged(nameof(StatusName));
                await getLeavesList();

                //LeaveItemsList = InprogessLeavesList;

                //OnPropertyChanged(nameof(LeavesList));
                OnPropertyChanged(nameof(LeaveItemsList));

            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);

            }

            //StatusName = HeadrActionList[0].IsSelected ? HeadrActionList[0].Name : (HeadrActionList[1].IsSelected ? HeadrActionList[1].Name : HeadrActionList[2].Name);

        }

        public async Task getLeavesList()
        {
            try
            {
                AppHelpers.LoadingShow();

                var result = await App.AppServices.GetLeaves();

                LeaveDate = result.data.ToList();
                LeavesList = new ObservableRangeCollection<LeaveResponse>(LeaveDate);



                FilterLeaves(LeavesList);
                DifferenceOfDays(InprogessLeavesList, ConfirmedLeavesList, PostponedLeavesList);
                LeaveItemsList = new ObservableRangeCollection<LeaveResponse>(InprogessLeavesList);

                numberOfRequests = LeaveItemsList.Count;
                TotalDays = statistiqueLeaveModel.ValidatedDays + statistiqueLeaveModel.InProgresDays + statistiqueLeaveModel.RejectedDays;
                entries = new List<ChartEntry>
            {
                new ChartEntry(statistiqueLeaveModel.ValidatedDays)
                {
                    Color = SKColor.Parse("#95D5A4")
                },
                new ChartEntry(statistiqueLeaveModel.InProgresDays)
                {
                    Color = SKColor.Parse("#FEE07D")
                },
                new ChartEntry(statistiqueLeaveModel.RejectedDays)
                {
                    Color = SKColor.Parse("#D59595")
                },
                //new ChartEntry(TotalDays)
                //{
                //    Color = SKColor.Parse("#F6FFF8")
                //},
            };

                donutChart.Entries = entries;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);

            }
            finally
            {
                AppHelpers.LoadingHide();

            }

        }

        private void DifferenceOfDays(List<LeaveResponse> InprogessLeavesList, List<LeaveResponse> ConfirmedLeavesList, List<LeaveResponse> PostponedLeavesList)
        {

            try
            {
                ConfirmedDays = 0;
                InprogessDays = 0;
                PostponedDays = 0;

                //foreach (var item in ConfirmedLeavesList)
                //{
                //    ConfirmedDays += item.DifferenceOfDays;
                //}
                //foreach (var item in InprogessLeavesList)
                //{
                //    InprogessDays += item.DifferenceOfDays;
                //}
                //foreach (var item in PostponedLeavesList)
                //{
                //    PostponedDays += item.DifferenceOfDays;
                //}

                ConfirmedDays = ConfirmedLeavesList.Count;
                InprogessDays = InprogessLeavesList.Count;
                PostponedDays = PostponedLeavesList.Count;
                //TotalDays = 18;

                OnPropertyChanged(nameof(ConfirmedDays));
                OnPropertyChanged(nameof(InprogessDays));
                OnPropertyChanged(nameof(PostponedDays));
                OnPropertyChanged(nameof(TotalDays));

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);

            }

        }

        private void FilterLeaves(ObservableRangeCollection<LeaveResponse> LeavesList)
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogError(ex);

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
                LeaveItemsList.ReplaceRange(InprogessLeavesList);

            }
            else if (HeadrActionList[1].IsSelected)
            {
                LeaveItemsList.ReplaceRange(ConfirmedLeavesList);

            }
            else if (HeadrActionList[2].IsSelected)
            {
                LeaveItemsList.ReplaceRange(PostponedLeavesList);

            }

            numberOfRequests = LeaveItemsList.Count;
            OnPropertyChanged(nameof(LeaveItemsList));
            OnPropertyChanged(nameof(numberOfRequests));

            StatusName = HeadrActionList[0].IsSelected ? HeadrActionList[0].Name : (HeadrActionList[1].IsSelected ? HeadrActionList[1].Name + "s" : HeadrActionList[2].Name + "s");
            OnPropertyChanged(nameof(StatusName));

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                CanSelectHeaderAction = true;
            }
        },
        (_) => CanSelectHeaderAction);



        private bool canNavigateToNewRequest = true;
        public ICommand NavigationtonewRequest => new Command(() =>
        {
            try
            {
                canNavigateToNewRequest = false;
                App.Current.MainPage.Navigation.PushAsync(new NewLeaveRequestPage());

                HeadrActionList[0].IsSelected = true;
                HeadrActionList[1].IsSelected = false;
                HeadrActionList[2].IsSelected = false;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);
            }
            finally
            {
                canNavigateToNewRequest = true;
            }

        },
    () => canNavigateToNewRequest
    );

        private LeaveDetailsPopup leaveDetailsPopup;

        private bool canOpenLeaveDetailsPopup = true;
        public ICommand OpenLeaveDetailsPopupView => new Command<LeaveResponse>(async (model) =>
        {
            try
            {
                canOpenLeaveDetailsPopup = false;

                if (model == null)
                    return;
                IsShowButtonCancelRequest = HeadrActionList[0].IsSelected;

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
                //await PopupNavigation.Instance.PushSingleAsync(leaveDetailsPopup);
                Logger?.LogError(ex);

            }
            finally
            {
                canOpenLeaveDetailsPopup = true;


            }
        },
        (_) => canOpenLeaveDetailsPopup );

        private ProfilLeaveDetailsPopup profilLeaveDetailsPopup;


        private bool canOpenProfilLeaveDetailsPopup = true;
        public ICommand OpenProfilLeaveDetailsPopupView => new Command<LeaveResponse>(async (model) =>
        {
            try
            {
                canOpenProfilLeaveDetailsPopup = false;

                if (model == null)
                    return;
                //IsShowButtonCancelRequest = HeadrActionList[0].IsSelected;

                SelectedLeave = model;

                if (profilLeaveDetailsPopup == null)
                {
                    profilLeaveDetailsPopup = new ProfilLeaveDetailsPopup() { BindingContext = this };
                }
                OnPropertyChanged(nameof(SelectedLeave));

                await PopupNavigation.Instance.PushSingleAsync(profilLeaveDetailsPopup);

            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushSingleAsync(profilLeaveDetailsPopup);
                Logger?.LogError(ex);


            }
            finally
            {
                canOpenProfilLeaveDetailsPopup = true;


            }
        },
        (_) => canOpenProfilLeaveDetailsPopup);

        private bool CanCancelRequest=true;
        public ICommand CancelRequest => new Command(async () =>
        {
            try
            {
                CanCancelRequest = false;
                var postparamts = new DeleteLeave()
                {
                    LeaveId = SelectedLeave.Id
                };

                AppHelpers.LoadingShow();
                var result = await App.AppServices.DeleteLeave(postparamts);
                AppHelpers.LoadingHide();

                if (result.succeeded)
                {
                    InprogessLeavesList.Remove(SelectedLeave);
                    LeaveItemsList.Remove(SelectedLeave);
                    numberOfRequests--;

                    var re = await App.AppServices.GetStatistics_ProfilLeaves();
                    statistiqueLeaveModel = re.data;

                    //InprogessDays--;
                    //TotalDays = ConfirmedDays + InprogessDays + PostponedDays;

                    //LeaveItemsListAdmin.Remove(SelectedLeave);
                    //numberOfRequestsAdmin--;

                }
                await PopupNavigation.Instance.PopAllAsync();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);


            }
            finally
            {
                CanCancelRequest = true;

            }

        },
() => CanCancelRequest
);

        private bool canNavigationBack = true;
        public ICommand NavigationBack => new Command(() =>
        {
            //App.Current.MainPage.Navigation.PushAsync(new LeaveRequest());
            try
            {
                canNavigationBack = false;
                App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);

            }
            finally
            {
                canNavigationBack = true;
            }


        },
    () => canNavigationBack);


    }
}
