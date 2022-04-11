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
        public List<REFItem> HeadrActionListAdmin { get; set; }


        public List<Leave> LeaveDate { get; set; }
        public ObservableRangeCollection<Leave> LeavesList { get; set; }


        public ObservableRangeCollection<Leave> LeaveItemsList { get; set; }
        public ObservableRangeCollection<Leave> LeaveItemsListAdmin { get; set; }

        public List<ChartEntry> entries { get; set; }


        public List<Leave> InprogessLeavesList { get; set; }
        public List<Leave> ConfirmedLeavesList { get; set; }
        public List<Leave> PostponedLeavesList { get; set; }

        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public int numberOfRequests { get; set; }
        public int numberOfRequestsAdmin { get; set; }

        public DonutChart donutChart { get; set; }

        public int ConfirmedDays { get; set; }
        public int InprogessDays { get; set; }
        public int PostponedDays { get; set; }
        public int TotalDays { get; set;}
        
        public Leave SelectedLeave { get; set; }

        public string StatusName { get; set; }

        public bool IsShowButtonCancelRequest { get; set; }

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
                    Name = "Annulée",
                }
            };

            HeadrActionListAdmin = new List<REFItem>()
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

            //LeaveItemsList = InprogessLeavesList;

            //OnPropertyChanged(nameof(LeavesList));
            OnPropertyChanged(nameof(LeaveItemsList));


            //StatusName = HeadrActionList[0].IsSelected ? HeadrActionList[0].Name : (HeadrActionList[1].IsSelected ? HeadrActionList[1].Name : HeadrActionList[2].Name);

        }

        public async Task getLeavesList()
        {
            try
            {
                AppHelpers.LoadingShow();

                var result = await App.AppServices.GetLeaves();

                LeaveDate = result.data.ToList();
                LeavesList = new ObservableRangeCollection<Leave>(LeaveDate);



                FilterLeaves(LeavesList);
                DifferenceOfDays(InprogessLeavesList, ConfirmedLeavesList, PostponedLeavesList);
                LeaveItemsList = new ObservableRangeCollection<Leave>(InprogessLeavesList);
                LeaveItemsListAdmin = new ObservableRangeCollection<Leave>(InprogessLeavesList);

                numberOfRequests = LeaveItemsList.Count;
                numberOfRequestsAdmin = LeaveItemsListAdmin.Count;




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
            finally
            {
                AppHelpers.LoadingHide();

            }

        }

        private void DifferenceOfDays(List<Leave> InprogessLeavesList, List<Leave> ConfirmedLeavesList, List<Leave> PostponedLeavesList)
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
            TotalDays = ConfirmedDays + InprogessDays + PostponedDays;

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
                //Logger.LogError(ex);
            }
            finally
            {
                CanSelectHeaderAction = true;
            }
        },
        (_) => CanSelectHeaderAction);

        private bool CanSelectHeaderActionAdmin = true;
        public ICommand SelectHeaderActionAdminCommand => new Command<REFItem>(async (model) =>
        {
            try
            {

                AppHelpers.LoadingHide();

                CanSelectHeaderActionAdmin = false;

                if (model == null) return;

                foreach (var item in HeadrActionListAdmin)
                {
                    item.IsSelected = (item.Id == model.Id);
                    OnPropertyChanged(nameof(item.IsSelected));


                }

                if (HeadrActionListAdmin[0].IsSelected)
                {
                    LeaveItemsListAdmin.ReplaceRange(InprogessLeavesList);

                }
                else if (HeadrActionListAdmin[1].IsSelected)
                {
                    LeaveItemsListAdmin.ReplaceRange(ConfirmedLeavesList);

                }
                //else if (HeadrActionList[2].IsSelected)
                //{
                //    LeaveItemsList.ReplaceRange(PostponedLeavesList);

                //}

                numberOfRequestsAdmin = LeaveItemsListAdmin.Count;
                OnPropertyChanged(nameof(LeaveItemsListAdmin));
                OnPropertyChanged(nameof(numberOfRequestsAdmin));

                StatusName = HeadrActionListAdmin[0].IsSelected ? HeadrActionListAdmin[0].Name : HeadrActionList[1].Name + "s";
                OnPropertyChanged(nameof(StatusName));

            }
            catch (Exception ex)
            {
                AppHelpers.LoadingHide();

                //Logger.LogError(ex);
            }
            finally
            {
                AppHelpers.LoadingHide();

                CanSelectHeaderActionAdmin = true;
            }
        },
        (_) => CanSelectHeaderActionAdmin);

        public ICommand NavigationtonewRequest => new Command(() =>
        {
            App.Current.MainPage.Navigation.PushAsync(new NewLeaveRequestPage());

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
                await PopupNavigation.Instance.PushSingleAsync(leaveDetailsPopup);

            }
            finally
            {
                canOpenLeaveDetailsPopup = true;


            }
        },
        (_) => canOpenLeaveDetailsPopup );

        private ProfilLeaveDetailsPopup profilLeaveDetailsPopup;


        private bool canOpenProfilLeaveDetailsPopup = true;
        public ICommand OpenProfilLeaveDetailsPopupView => new Command<Leave>(async (model) =>
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
                AppHelpers.LoadingShow();
                CanCancelRequest = false;
                var result = await App.AppServices.DeleteLeave(SelectedLeave.Id);
                if (result.succeeded)
                {
                    InprogessLeavesList.Remove(SelectedLeave);
                    LeaveItemsList.Remove(SelectedLeave);
                    numberOfRequests--;
                    InprogessDays--;
                    TotalDays = ConfirmedDays + InprogessDays + PostponedDays;

                    LeaveItemsListAdmin.Remove(SelectedLeave);
                    numberOfRequestsAdmin--;

                }
                await PopupNavigation.Instance.PopAllAsync();
            }
            catch (Exception ex)
            {
                AppHelpers.LoadingHide();

            }
            finally
            {
                CanCancelRequest = true;
                AppHelpers.LoadingHide();


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

            }
            finally
            {
                canNavigationBack = true;
            }


        },
    () => canNavigationBack);


    }
}
