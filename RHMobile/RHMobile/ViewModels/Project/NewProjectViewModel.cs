using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NewProjectViewModel : BaseViewModel
    {
        public ObservableCollection<ProfilResponse> MembersList { get; set; }
        public ObservableCollection<ProfilResponse> ProjectMembersList { get; set; }
        public ObservableCollection<ProfilResponse> ProjectOwnerList { get; set; }
        public ProjectRequest projectRequest { get;set; }


        public NewProjectViewModel()
        {

            projectRequest = new ProjectRequest();

            GetProfils();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

        }

        public async void GetProfils()
        {
            try
            {
                var result = await App.AppServices.GetProfils();
                if(result.succeeded)
                {
                    MembersList = new ObservableCollection<ProfilResponse>(result.data.ToList());
                    ProjectOwnerList = MembersList;
                    ProjectMembersList = MembersList;

                }

            }
            catch (Exception ex)
            {

            }
        }

        #region Chef du projet

        private bool canSelectOwner = true;
        public ICommand SelectOwnerCommand => new Command<ProfilResponse>(async (model) =>
        {
            try
            {
                canSelectOwner = false;

                for (int i = 0; i < ProjectOwnerList.Count; i++)
                    ProjectOwnerList[i].IsSelectedAsOwner = false;
                //ProjectOwnerList.Where(x => (x.IsSelectedAsOwner = false));

                model.IsSelectedAsOwner = true;

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
               if (model.IsOwner)
               {
                   model.IsSelectedAsMember = false;
               }
               else
               {
                   model.IsSelectedAsMember = !model.IsSelectedAsMember;
               }


               //var  owner = ProjectOwnerList.Where(owner => (owner.IsSelected));
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
