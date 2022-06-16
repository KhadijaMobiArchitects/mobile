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
    public class LeaveAdministrationViewModel : BaseViewModel
    {
        public ObservableRangeCollection<LeaveResponse> LeaveListAdmin { get; set; }
        public ObservableRangeCollection<LeaveResponse> LeaveItemsListAdmin { get; set; }

        public List<LeaveResponse> InprogessLeavesList { get; set; }
        public List<LeaveResponse> ConfirmedLeavesList { get; set; }

        public LeaveResponse SelectedLeave { get; set; }

        public bool canOpenProfilLeaveDetailsPopup { get; set; }
        public int numberOfRequests { get; set;}

        public LeaveAdministrationViewModel()
        {

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            await getLeavesList();
        }

        public async Task getLeavesList()
        {
            try
            {
                AppHelpers.LoadingShow();
                var result = await App.AppServices.GetProfilsLeave();
                if (result?.succeeded == true)
                {
                    LeaveListAdmin = new ObservableRangeCollection<LeaveResponse>(result.data.ToList());

                    LeaveItemsListAdmin = new ObservableRangeCollection<LeaveResponse>();
                    InprogessLeavesList = LeaveListAdmin.Where(x => (x.RefStatusLeaveId == 1)).ToList();
                    ConfirmedLeavesList = LeaveListAdmin.Where(x => (x.RefStatusLeaveId == 2)).ToList();
                    LeaveItemsListAdmin.ReplaceRange(InprogessLeavesList);
                    canOpenProfilLeaveDetailsPopup = true;
                    numberOfRequests = LeaveItemsListAdmin.Count;
                }
                else
                {
                    AppHelpers.Alert(result?.message);
                }

                AppHelpers.LoadingHide();

            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);
            }
        }
        private bool CanSelectHeaderAction = true;
        public ICommand SelectHeaderActionAdminCommand => new Command<REFItem>(async (model) =>
        {
            try
            {

                AppHelpers.LoadingHide();

                CanSelectHeaderAction = false;

                if (model == null) return;

                foreach (var item in HeadrActionList)
                {
                    item.IsSelected = (item.Id == model.Id);
                    OnPropertyChanged(nameof(item.IsSelected));


                }

                if (HeadrActionList[0].IsSelected)
                {
                    LeaveItemsListAdmin.ReplaceRange(InprogessLeavesList);

                }
                else if (HeadrActionList[1].IsSelected)
                {
                    LeaveItemsListAdmin.ReplaceRange(ConfirmedLeavesList);

                }
                canOpenProfilLeaveDetailsPopup = HeadrActionList[0].IsSelected;
                numberOfRequests = LeaveItemsListAdmin.Count;


                //StatusName = HeadrActionListAdmin[0].IsSelected ? HeadrActionListAdmin[0].Name : HeadrActionList[1].Name + "s";
                //OnPropertyChanged(nameof(StatusName));

            }
            catch (Exception ex)
            {
                //AppHelpers.Alert(ex.Message, exception: ex);

                Logger.LogError(ex);
            }
            finally
            {
                AppHelpers.LoadingHide();

                CanSelectHeaderAction = true;
            }
        },
        (_) => CanSelectHeaderAction);


        private ProfilLeaveDetailsPopup profilLeaveDetailsPopup;
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
                //OnPropertyChanged(nameof(SelectedLeave));

                await PopupNavigation.Instance.PushSingleAsync(profilLeaveDetailsPopup);

            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);

            }
            finally
            {
                canOpenProfilLeaveDetailsPopup = true;


            }
        },
        (_) => canOpenProfilLeaveDetailsPopup);

        private bool canValidateLeave = true;
        public ICommand ValidateLeaveCommand => new Command<LeaveResponse>(async (model) =>
        {
            try
            {
                canValidateLeave = false;

                var postParam = new UpdateLeaveModel()
                {
                    id = SelectedLeave.Id,
                    refStatusLeaveId = 2
                };

                AppHelpers.LoadingShow();
                var result = await App.AppServices.PostUpdateLeave(postParam);
                AppHelpers.Alert(result?.message);
                await PopupNavigation.Instance.PopAllAsync();
                await getLeavesList();

                //if (result?.succeeded == true)
                //{
                //    AppHelpers.Alert(result?.message);
                //}
                //else
                //{
                //    AppHelpers.Alert(result?.message);
                //}
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);
            }
        }, (_) => canValidateLeave);

        private bool canRejectLeave = true;

        public ICommand RejectLeaveCommand => new Command<LeaveResponse>(async (model) =>
        {
            try
            {
                canRejectLeave = false;

                var postParam = new UpdateLeaveModel()
                {
                    id = SelectedLeave.Id,
                    refStatusLeaveId = 3
                };
                AppHelpers.LoadingShow();
                var result = await App.AppServices.PostUpdateLeave(postParam);
                AppHelpers.Alert(result?.message);
                await PopupNavigation.Instance.PopAllAsync();
                await getLeavesList();

            }
            catch (Exception ex)
            {
                Logger?.LogError(ex);
            }
        }, (_) => canRejectLeave);
    }
}
