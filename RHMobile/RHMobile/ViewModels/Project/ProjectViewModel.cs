using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;
using XForms.views;

namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ProjectViewModel :BaseViewModel
    {
        public ObservableRangeCollection<Project> Projects { get; set; }
        public ObservableRangeCollection<Project> ProjectsList { get; set; }

        public ObservableRangeCollection<Profil> Squad { get; set; }
        public ObservableRangeCollection<SquadResponse> SquadResponseList { get; set; }
        public ObservableRangeCollection<ProfilResponse> SquadList { get; set; }

        public ProjectViewModel()
        {
            Projects = new ObservableRangeCollection<Project>()
            {
                new Project()
                {
                    Id = 1,
                    Name = "Ajouter",
                    Percent = 30,
                    BackgroundColor = Color.FromHex("#ffffff"),
                    Image = AppHelpers.GetImageResource("add.png")


                },

                new Project()
                {
                    Id = 2,
                    Name = "RH",
                    Percent = 40,
                    BackgroundColor = Color.FromHex("#1A26c9"),
                    Image = AppHelpers.GetImageResource("project.png")



                },

                new Project()
                {
                    Id = 3,
                    Name = "Margin",
                    Percent = 80,
                    BackgroundColor = Color.FromHex("#4AeFF9")


                },

                new Project()
                {
                    Id = 3,
                    Name = "Margin",
                    Percent = 80,
                    BackgroundColor = Color.FromHex("#4ACer9")


                },

                new Project()
                {
                    Id = 3,
                    Name = "Margin",
                    Percent = 80,
                    BackgroundColor = Color.FromHex("#33CFF9")


                },

                new Project()
                {
                    Id = 3,
                    Name = "Margin",
                    Percent = 80,
                    BackgroundColor = Color.FromHex("#445CF9")


                }

            };

            Squad = new ObservableRangeCollection<Profil>()
            {
                new Profil()
                {
                    Id="1",
                    Name = "Hassoun Karoum",
                    fonction = "Dev Mobile"
                },
                new Profil()
                {
                    Id = "2",
                    Name = "Salma El Mejjaty",
                    fonction = "Dev Web Back End"
                }
            };
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            GetAllProjects();


        }

        public async void GetAllProjects()
        {
            try
            {
                var result = await App.AppServices.GetProjects();

                ProjectsList = new ObservableRangeCollection<Project>(result.data.ToList());

                GetProjectSquad(ProjectsList[0].Id);

            }
            catch (Exception ex)
            {

            }
        }

        public async void GetProjectSquad(long ProjectId)
        {
            var result = await App.AppServices.GetProjectSquad(ProjectId);

            SquadList = new ObservableRangeCollection<ProfilResponse>(result.data.ToList());


        }


        private bool canAddProjectCommand = true;
        public ICommand AddProjectCommand => new Command(async () =>
        {
            try
            {
                canAddProjectCommand = false;

                App.Current.MainPage.Navigation.PushAsync(new NewProjectPage());


                //var result = await App.AppServices.PostProject(projectRequest);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                canAddProjectCommand = true;
            }

        },
() => canAddProjectCommand);


        private bool canSelectProject = true;
        public ICommand SelectProjectCommand => new Command<Project>(async (model) =>
         {
             try
             {
                 canSelectProject = false;

                 GetProjectSquad(model.Id);

             }
             catch (Exception ex)
             {

             }
             finally
             {
                 canSelectProject = true;
             }
         }
        ,
        (_) => canSelectProject);
    }
}

