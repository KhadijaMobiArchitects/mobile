using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Constants;
using XForms.HttpREST;
using XForms.Models;
using XForms.views.Leave;

namespace XForms.ViewModels
{
    public class NewLeaveRequestViewModel : BindableObject
    {
        
        public List<Leave> ListLeave { get; set; }
        //public List<Projet> ListProjet { get; set; }
        //public List<SituationProjet> ListSituation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public Leave SelectedLeave { get; set; }
        public Project SelectedProjet { get; set; }
        //public SituationProjet SelectedSituationProjet { get; set; }
        public List<REFTypeLeave> TypeLeaveData;
        public List<REFTypeLeave> TypesLeaveList { get; set; }
        public List<Project> ProjectData;
        public List<Project> ProjectsList { get; set; }

        public List<SituationProject> SituationsProjectList { get; set; }


        public NewLeaveRequestViewModel()
        {

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            getTypesLeave();
            getProjects();
            getSituationsProject();

            //ListSituation = new List<SituationProjet>
            //{
            //    new SituationProjet(){
            //        Name="Livré partiellement"
            //    },
            //    new SituationProjet(){
            //        Name="Livré totalement"
            //    }
            //};


            //API Post

            //Leave item = new Leave()
            //{
            //    Type = "Leave annuel",
            //    Status = "Reporté",
            //    StartDate = StartDate,
            //    EndDate = EndDate
            //};

            //var client = new HttpClient();

            //string json = JsonConvert.SerializeObject(item);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            //var responseMessage = client.PostAsync(AppUrls.GesRequestsListLeave, content);

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

                    Leave item = new Leave()
                    {
                        Type =SelectedLeave.Name,
                        StartDate = StartDate,
                        EndDate = EndDate
                    };

                    //RESTServiceResponse<Leave> response = new RESTServiceResponse<Leave>();
                    //response.data = item;

                    //var client = new HttpClient();

                    //string json = JsonConvert.SerializeObject(item);
                    //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    //var responseMessage = client.PostAsync(AppUrls.GesRequestsListLeave, content);

                    //var result = await App.AppServices.PostLeave(item);

                    App.Current.MainPage.Navigation.PushAsync(new LeaveRequestPage());

                }
                catch(Exception ex)
                {

                }
                finally
                {
                    CandSendRequest = true;
                }
            },
            ()=> CandSendRequest

            );
        public ICommand NavigationBack => new Command(() =>
        {
            //App.Current.MainPage.Navigation.PushAsync(new LeaveRequest());
            App.Current.MainPage.Navigation.PopAsync();


        },
    () => true


    );


    }
}
