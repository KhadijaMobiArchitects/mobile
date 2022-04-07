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

        public List<Leave> ListLeave { get; set; }
        //public List<Projet> ListProjet { get; set; }
        //public List<SituationProjet> ListSituation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //public Leave SelectedLeave { get; set; }
        public Project SelectedProjet { get; set; }
        public REFTypeLeave SelectedREFTypeLeave { get; set; }
        public SituationProject SelectedSituationProject { get; set; }

        public List<REFTypeLeave> TypeLeaveData;
        public List<REFTypeLeave> TypesLeaveList { get; set; }
        public List<Project> ProjectData;
        public List<Project> ProjectsList { get; set; }

        public List<SituationProject> SituationsProjectList { get; set; }

        private bool ConfirmedBySquad;
        public Color ButtonConfirmedBySquadBackground => ConfirmedBySquad ? Color.FromHex("#126BCD") : Color.White;


        //public bool EnableButtonSendRequest => (SelectedProjet == null && SelectedREFTypeLeave == null && SelectedSituationProject == null);
        public bool EnableButtonSendRequest { get; set; }
        public Color ButtonSendRequestBackground => EnableButtonSendRequest ? Color.FromHex("#126BCD") : Color.FromHex("#B0B6BE");

        public int NumberOfDays => (int) (EndDate - StartDate).TotalDays;

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
            var result = await App.AppServices.GetTypesLeave();
            TypeLeaveData = result.data.ToList();
            TypesLeaveList = TypeLeaveData;

            OnPropertyChanged(nameof(TypesLeaveList));

        }

        public async void getProjects()
        {
            var result = await App.AppServices.GetProjects();
            ProjectData = result.data.ToList();
            ProjectsList = ProjectData;

            OnPropertyChanged(nameof(ProjectsList));

        }

        public async void getSituationsProject()
        {
            var result = await App.AppServices.GetSituationsProject();
            SituationsProjectList = result.data.ToList();

            OnPropertyChanged(nameof(SituationsProjectList));
        }

        private bool CandSendRequest = true;


        public ICommand SendRequest => new Command(async () =>
            {
                try
                {
                    CandSendRequest = false;

                    Leave postParams = new Leave()
                    {
                        StartDate = StartDate,
                        EndDate = EndDate,
                        ConfirmedBySquad = ConfirmedBySquad,
                        CreatedBy = "1",
                        RefStatusLeaveId = 1,
                        RefTypeLeaveId = SelectedREFTypeLeave.Id,
                        RefSituationProjectId = SelectedSituationProject.Id,
                        ProjectId = SelectedProjet.Id


                    };


                    var result = new RESTServiceResponse<object>();
                    result = await App.AppServices.PostLeave(postParams);

                    App.Current.MainPage.Navigation.PopAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    CandSendRequest = true;
                }
            },
            () => CandSendRequest

            );
        public ICommand NavigationBack => new Command(() =>
        {
            //App.Current.MainPage.Navigation.PushAsync(new LeaveRequest());
            App.Current.MainPage.Navigation.PopAsync();


        },
    () => true


    );
        public ICommand NotifySquad => new Command(async () =>
        {
            ConfirmedBySquad = !ConfirmedBySquad;
            OnPropertyChanged(nameof(ButtonConfirmedBySquadBackground));


        },
    () => true
    );


    }
}
