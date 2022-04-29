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
        //public ObservableRangeCollection<Project> Projects { get; set; }
        public ObservableRangeCollection<Project> ProjectsList { get; set; }

        public ObservableRangeCollection<ProfilResponse> SquadList { get; set; }
        public ObservableRangeCollection<ProfilResponse> AddMembersList { get; set; }
        public ObservableRangeCollection<ProfilResponse> SearchMembersList { get; set; }

        public int NumberOfProjects { get; set; }

        public string  ProjectOwnerName { get; set; }

        public string SearchPartKeyword { get; set; }

        public String SearchWord { get; set; }


        private Project AddProjectCell;

        public ProjectViewModel()
        {

        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            //AddProjectCell = new Project()
            //{
            //    Id = -1,
            //    Name = "Ajouter",
            //    OwnerBy = "Nouveau Projet",
            //    ShowPercent = false,
            //    PictureUrl = "https://cdn-icons-png.flaticon.com/512/70/70310.png"
            //};

            GetAllProjects();

            this.PropertyChanged += (s, e) =>
            {
                if (
                e.PropertyName == nameof(SearchPartKeyword)
                )
                {
                    //Search(SearchWord);
                    SearchCommand.Execute(null);
                }
            };
        }

        public async void GetAllProjects()
        {
            try
            {
                var result = await App.AppServices.GetProjects();

                ProjectsList = new ObservableRangeCollection<Project>(result.data.ToList());
                //ProjectsList.Insert(0, AddProjectCell);

                if(ProjectsList.Any())
                {
                    ProjectsList[0].IsSelected = true;
                    GetProjectSquad(ProjectsList[0].Id);
                }
                NumberOfProjects = ProjectsList.Count;

            }
            catch (Exception ex)
            {

            }
        }

        public async void GetProjectSquad(long ProjectId)
        {
            try
            {
                AppHelpers.LoadingShow();
                var result = await App.AppServices.GetProjectSquad(ProjectId);
                AppHelpers.LoadingHide();
                SquadList = new ObservableRangeCollection<ProfilResponse>(result.data.ToList());
                AddMembersList = new ObservableRangeCollection<ProfilResponse>(result.data.ToList());
                SearchMembersList = AddMembersList;

                foreach (ProfilResponse profil in SquadList)
                {
                    if (profil.IsOwner)
                    {
                        ProjectOwnerName = profil.FirstName + " " + profil.LastName;
                        break;
                    }

                }
                    ProjectsList.FirstOrDefault(x => x.Id == ProjectId).OwnerName = ProjectOwnerName;
            }
            catch (Exception ex)
            {

            }
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
                 foreach (Project project in ProjectsList)
                     project.IsSelected = false;

                 model.IsSelected = true;



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

        private bool canAddProject = true;
        public ICommand AddProjectCommmand => new Command(async () =>
        {
            try
            {
                canAddProject = false;
                App.Current.MainPage.Navigation.PushAsync(new NewProjectPage());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                canAddProject = true;
            }
        },
          () => canAddProject);

        private bool canSelectProfil = true;
        public ICommand SelectProfilCommand => new Command<ProfilResponse>(async (model) =>
        {
            try
            {
                canSelectProfil = false;
                model.IsSelected = ! model.IsSelected;


            }
            catch (Exception ex)
            {

            }
            finally
            {
                canSelectProfil = true;
            }
        }
       ,
       (_) => canSelectProfil);



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

        public void Search(string word)
        {

            try
            {
                if (word.Length > 0)
                {
                    SearchMembersList = new ObservableRangeCollection<ProfilResponse>();
                    foreach (ProfilResponse profil in AddMembersList)
                    {
                        if (profil.FirstName.ToLower().Contains(word.ToLower()) || profil.LastName.ToLower().Contains(word.ToLower()))
                        {
                            SearchMembersList.Add(profil);
                        }
                    }
                }

                else if(word.Length == 0)
                    SearchMembersList = AddMembersList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }

        }

        private bool CanSearch = true;
        public ICommand SearchCommand => new Command(() =>
        {
            try
            {
                CanSearch = false;

 
                SearchMembersList = new ObservableRangeCollection<ProfilResponse>();
                var key = (SearchPartKeyword ?? "").ToLower();
                if(AddMembersList?.Count > 0)
                {
                    var list = AddMembersList.Where(member => (member.LastName + member.FirstName ?? "").ToLower().Contains(key));
                    SearchMembersList.ReplaceRange(list);
                }
            }
            catch (Exception ex)
            {
                //Logger?.LogError(ex, showError: true);
            }
            finally
            {
                CanSearch = true;
            }
        }, () => CanSearch);


    }
}

