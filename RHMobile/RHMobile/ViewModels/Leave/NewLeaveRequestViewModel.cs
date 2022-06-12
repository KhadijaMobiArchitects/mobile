using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using FluentValidation.Results;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Constants;
using XForms.HttpREST;
using XForms.Models;
using XForms.Validators.Leave;
using XForms.views.Leave;


namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NewLeaveRequestViewModel : BaseViewModel
    {

        public List<LeaveModel> ListLeave { get; set; }
        //public List<Projet> ListProjet { get; set; }
        //public List<SituationProjet> ListSituation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //public Leave SelectedLeave { get; set; }
        public ProjectModel SelectedProjet { get; set; }
        public REFTypeLeave SelectedREFTypeLeave { get; set; }
        public SituationProject SelectedSituationProject { get; set; }

        public List<REFTypeLeave> TypeLeaveData;
        public List<REFTypeLeave> TypesLeaveList { get; set; }
        public List<ProjectModel> ProjectData;
        public List<ProjectModel> ProjectsList { get; set; }

        public List<SituationProject> SituationsProjectList { get; set; }

        private bool ConfirmedBySquad;
        public Color ButtonConfirmedBySquadBackground => ConfirmedBySquad ? Color.FromHex("#126BCD") : Color.White;


        //public bool EnableButtonSendRequest => (SelectedProjet == null && SelectedREFTypeLeave == null && SelectedSituationProject == null);
        public bool EnableButtonSendRequest { get; set; }
        public Color ButtonSendRequestBackground => EnableButtonSendRequest ? Color.FromHex("#126BCD") : Color.FromHex("#B0B6BE");

        public int NumberOfDays { get; set; }

        private NewLeaveRequestValidator validator;

        public ValidationResult validationResult { get; set; }

        public int SelectedREFTypeLeaveIndex { get; set; }

        public NewLeaveRequestViewModel()
        {


            StartDate = DateTime.Now;
            EndDate = DateTime.Now;



            getTypesLeave();
            getProjects();
            getSituationsProject();

            this.PropertyChanged += (s, e) =>
            {
                if (
                e.PropertyName == nameof(StartDate) ||
                e.PropertyName == nameof(EndDate) ||
                e.PropertyName == nameof(SelectedREFTypeLeave) ||
                e.PropertyName == nameof(SelectedProjet) ||
                e.PropertyName == nameof(SelectedSituationProject)
                )
                {
                    validationResult = validator.Validate(this);
                    EnableButtonSendRequest = validationResult.IsValid;
                    OnPropertyChanged(nameof(ButtonSendRequestBackground));

                }
                if (e.PropertyName == nameof(StartDate) ||
                e.PropertyName == nameof(EndDate)
                )
                {
                    try
                    {
                        NumberOfDays = AppHelpers.BusinessDaysUntil(StartDate, EndDate);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            };

        }


        public override void OnAppearing()
        {
            base.OnAppearing();

            if (validator == null)
                validator = new();

        }

        public async void getTypesLeave()
        {
            try
            {
                var result = await App.AppServices.GetTypesLeave();
                TypeLeaveData = result.data.ToList();
                TypesLeaveList = TypeLeaveData;

                OnPropertyChanged(nameof(TypesLeaveList));
            }
            catch (Exception ex)
            {

            }


        }

        public async void getProjects()
        {
            try
            {
                var result = await App.AppServices.GetProjects();
                ProjectData = result.data.ToList();
                ProjectsList = ProjectData;

                OnPropertyChanged(nameof(ProjectsList));

            }
            catch (Exception ex)
            {

            }
        }

        public async void getSituationsProject()
        {
            try
            {
                var result = await App.AppServices.GetSituationsProject();
                SituationsProjectList = result.data.ToList();

                OnPropertyChanged(nameof(SituationsProjectList));
            }
            catch (Exception ex)
            {

            }
        }

        private bool CandSendRequest = true;


        public ICommand SendRequest => new Command(async () =>
            {
                try
                {
                    AppHelpers.LoadingShow();

                    CandSendRequest = false;

                    LeaveModel postParams = new LeaveModel()
                    {
                        StartDate = StartDate,
                        EndDate = EndDate,
                        ConfirmedBySquad = ConfirmedBySquad,
                        CreatedBy = AppPreferences.UserId,
                        RefStatusLeaveId = 1,
                        RefTypeLeaveId = SelectedREFTypeLeave.Id,
                        RefSituationProjectId = SelectedSituationProject.Id,
                        ProjectId = SelectedProjet.Id,
                        ProjectName = SelectedProjet.Name,
                        SituationProjectName = SelectedSituationProject.Name


                    };

                    //Console.WriteLine(AppPreferences.UserId);


                    var result = new RESTServiceResponse<object>();
                    result = await App.AppServices.PostLeave(postParams);
                    if(result?.succeeded == true)
                    {
                        App.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                    {
                        AppHelpers.Alert(result?.message);
                    }


                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex);
                    AppHelpers.LoadingHide();

                }
                finally
                {
                    CandSendRequest = true;
                    AppHelpers.LoadingHide();
                }
            },
            () => CandSendRequest

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
    () => canNavigationBack );

        public ICommand NotifySquad => new Command(async () =>
        {
            ConfirmedBySquad = !ConfirmedBySquad;
            OnPropertyChanged(nameof(ButtonConfirmedBySquadBackground));


        },
    () => true
    );


    }
}
