using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Plugin.Media.Abstractions;
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
        public ImageSource UserPictureSource { get; set; } = AppHelpers.GetImageResource("image.png");
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

                if (model.IsSelectedAsOwner)
                {
                    model.IsSelectedAsOwner = false;
                    return;
                }

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
               if (model.IsSelectedAsOwner)
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

                //foreach (ProfilResponse profil in ProjectMembersList)
                //{
                //    if (profil.IsSelected)
                //        projectRequest.members.Add(profil.RecId);
                //}
                //foreach (ProfilResponse profil in ProjectOwnerList)
                //{
                //    if (profil.IsSelected)
                //    {
                //        projectRequest.OwnerBy = profil.RecId;
                //        break;
                //    }
                //}

                projectRequest.members = ProjectMembersList.Where(profil => (profil.IsSelectedAsMember)).Select(x=> x.RecId)?.ToList();
                projectRequest.OwnerBy = ProjectOwnerList.FirstOrDefault(profil => (profil.IsSelectedAsOwner)).RecId;

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

        //public MediaFile file { get; set; }

        private bool canPickPicture = true;
        public ICommand PickPictureCommand => new Command(async () =>
        {
            try
            {
                canPickPicture = false;
                 var pickedFile = await AppHelpers.TakeOrPickPhoto();
                 var fileStream = pickedFile.GetStream();

                byte[] photoBytes;

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    var stream = fileStream;

                    stream.CopyTo(memoryStream);

                    stream.Dispose();
                    stream.Close();

                    photoBytes = memoryStream.ToArray();
                }

                UserPictureSource = ImageSource.FromStream(() => { return fileStream; });
                OnPropertyChanged(nameof(UserPictureSource));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canPickPicture = true;
            }

        }, () =>canPickPicture);


    }
}
