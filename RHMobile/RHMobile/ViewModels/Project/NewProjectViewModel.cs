using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NewProjectViewModel : BaseViewModel
    {
        public ObservableCollection<ProfilResponse> ProjectMembersList { get; set; }
        public ObservableCollection<ProfilResponse> ProjectOwnerList { get; set; }
        public ProjectRequest projectRequest { get;set; }


        public NewProjectViewModel()
        {

            projectRequest = new ProjectRequest();

            ProjectMembersList = new ObservableCollection<ProfilResponse>()
            {
                new ProfilResponse()
                {
                    RecId ="11"

                }
                
            };
            ProjectOwnerList = new ObservableCollection<ProfilResponse>()
            {
                new ProfilResponse()
                {
                    RecId ="11"


                }
            };
        }

        #region Chef du projet

        private bool canSelectOwner = true;
        public ICommand SelectOwnerCommand => new Command<ProfilResponse>(async (model) =>
        {
            try
            {
                canSelectOwner = false;

                for (int i = 0; i < ProjectOwnerList.Count; i++)
                    ProjectOwnerList[i].IsSelected = false;

                model.IsSelected = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                canSelectOwner = true;
            }

        },
        (_) => canSelectOwner);

        #endregion

        #region affectation des collaborateurs

        private bool canSelectMember = true;
        public ICommand SelectMemberCommand => new Command<ProfilResponse>(async (model) =>
       {
           try
           {
               canSelectMember = false;
               model.IsSelected = !model.IsSelected;
           }
           catch (Exception ex)
           {

           }
           finally
           {
               canSelectMember = true;
           }

       },
        (_)=> canSelectMember);

        #endregion

        private bool canAddProjectCommand = true;
        public ICommand AddProjectCommand => new Command(async () =>
        {
            try
            {
                canAddProjectCommand = false;

                foreach (ProfilResponse profil in ProjectMembersList)
                {
                    if (profil.IsSelected)
                        projectRequest.members.Add(profil.RecId);
                }
                foreach (ProfilResponse profil in ProjectOwnerList)
                {
                    if (profil.IsSelected)
                    {
                        projectRequest.OwnerBy = profil.RecId;
                        break;
                    }
                }

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


    }
}
