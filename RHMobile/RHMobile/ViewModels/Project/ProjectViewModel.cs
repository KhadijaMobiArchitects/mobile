using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;
using XForms.views;
using XForms.views.Project;

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

        private Project AddProjectCell;

        public ProjectViewModel()
        {

        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            AddProjectCell = new Project()
            {
                Id = -1,
                Name = "Ajouter",
                OwnerBy = "Nouveau Projet",
                ShowPercent = false,
                PictureUrl = "https://cdn-icons-png.flaticon.com/512/70/70310.png"
            };

            GetAllProjects();


        }

        public async void GetAllProjects()
        {
            try
            {
                var result = await App.AppServices.GetProjects();

                ProjectsList = new ObservableRangeCollection<Project>(result.data.ToList());
                ProjectsList.Insert(0, AddProjectCell);

                ProjectsList[1].IsSelected = true;

                GetProjectSquad(ProjectsList[1].Id);

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

                 if(model.Id == -1)
                 {
                     App.Current.MainPage.Navigation.PushAsync(new NewProjectPage());
                 }
                 else
                 {
                     GetProjectSquad(model.Id);
                     foreach (Project project in ProjectsList)
                         project.IsSelected = false;

                     model.IsSelected = true;

                 }




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


        private AddMembersPopup addMembersPopup;
        private bool canOpenAddMembersPopup = true;

        public ICommand OpenAddMembersPopupCommand => new Command(async () =>
        {
            try
            {
                canOpenAddMembersPopup = false;

                if (addMembersPopup == null)
                    addMembersPopup = new AddMembersPopup() { BindingContext = this };

                await PopupNavigation.Instance.PushSingleAsync(addMembersPopup);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canOpenAddMembersPopup = true;
            }


        }, () => canOpenAddMembersPopup);


    }
}

