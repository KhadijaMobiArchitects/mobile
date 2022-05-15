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
        public List<REFItem> HeadrActionListAdmin { get; set; }

        public ObservableRangeCollection<LeaveResponse> LeaveListAdmin { get; set; }
        public ObservableRangeCollection<LeaveResponse> LeaveItemsListAdmin { get; set; }

        public List<LeaveResponse> InprogessLeavesList { get; set; }
        public List<LeaveResponse> ConfirmedLeavesList { get; set; }

        public LeaveResponse SelectedLeave { get; set; }


        public LeaveAdministrationViewModel()
        {
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
                    InprogessLeavesList = LeaveListAdmin.Where(x => (x.refStatusLeaveId == 1)).ToList();
                    ConfirmedLeavesList = LeaveListAdmin.Where(x => (x.refStatusLeaveId == 2)).ToList();
                    LeaveItemsListAdmin.ReplaceRange(InprogessLeavesList);

                }
                else
                {
                    AppHelpers.Alert(result?.message);
                }

                AppHelpers.LoadingHide();

            }
            catch (Exception ex)
            {

            }
        }
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


                //StatusName = HeadrActionListAdmin[0].IsSelected ? HeadrActionListAdmin[0].Name : HeadrActionList[1].Name + "s";
                //OnPropertyChanged(nameof(StatusName));

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
                //OnPropertyChanged(nameof(SelectedLeave));

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
    }
}
