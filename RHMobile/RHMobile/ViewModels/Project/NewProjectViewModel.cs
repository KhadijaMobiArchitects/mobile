using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
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
        public ProjectRequest projectRequest { get; set; }
        public ImageSource UserPictureSource { get; set; } = AppHelpers.GetImageResource("image.png");
        public string ProjectName { get; set; }
        public DateTime StartedAt { get;set;}
        public DateTime EndedAt { get; set; }

        public ProfilResponse Owner { get; set; }

        public NewProjectViewModel()
        {

            projectRequest = new ProjectRequest();

            GetProfils();
            GetOwners();
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
                if (result?.succeeded == true)
                {
                    ProjectMembersList = new ObservableCollection<ProfilResponse>(result.data.ToList());
                }
                else
                    AppHelpers.Alert(result?.message);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
        public async void GetOwners()
        {
            try
            {
                var result = await App.AppServices.GetOwners();
                if (result?.succeeded == true)
                {
                    ProjectOwnerList = new ObservableCollection<ProfilResponse>(result.data.ToList()); ;

                }
                else
                    AppHelpers.Alert(result?.message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        #region Chef du projet

        private bool canSelectOwner = true;
        public ICommand SelectOwnerCommand => new Command<ProfilResponse>(async (model) =>
        {
            try
            {
                canSelectOwner = false;

                Owner = model;

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
                Logger.LogError(ex);
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
               if (Owner?.RecId == model?.RecId)
               {
                   model.IsSelectedAsMember = false;
               }
               else
               {
                   model.IsSelectedAsMember = !model.IsSelectedAsMember;
               }
           }
           catch (Exception ex)
           {
               Logger.LogError(ex);
           }
           finally
           {
               canSelectMember = true;
           }

       },
        (_)=> canSelectMember);

        #endregion

        private Models.File projectFile;
        private bool canAddProjectCommand = true;
        public ICommand AddProjectCommand => new Command(async () =>
        {
            try
            {
                canAddProjectCommand = false;

                var memberslist = ProjectMembersList.Where(profil => (profil.IsSelectedAsMember)).Select(x=> x.RecId)?.ToList();
                var OwnerBy = ProjectOwnerList.FirstOrDefault(profil => (profil.IsSelectedAsOwner)).RecId;
                string members ="";

                foreach (string  id in memberslist)
                {
                    members += id+",";
                }
                if (members != "")
                    members = members.Remove(members.Length - 1);

                var projectRequest2 = new ProjectRequest()
                {
                    ProjectName = ProjectName,
                    StartedAt = StartedAt,
                    EndedAt = EndedAt,
                    OwnerBy = OwnerBy,
                    members = members,
                    ProjectFile = projectFile

                };

                AppHelpers.LoadingShow();

                var result = await App.AppServices.PostProject(projectRequest2);

                AppHelpers.LoadingHide();
                AppHelpers.Alert(result?.message);


                await App.Current.MainPage.Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
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
                //UserPictureSource = ImageSource.FromStream(() => { return fileStream; });
                //await CachedImage.InvalidateCache(UserPictureSource, CacheType.All);

                //byte[] photoBytes;
                //var _fileStream = fileStream;
                //using (var memoryStream = new System.IO.MemoryStream())
                //{
                //    var stream = _fileStream;

                //    stream.CopyTo(memoryStream);

                //    stream.Dispose();
                //    stream.Close();

                //    photoBytes = memoryStream.ToArray();
                //}

                //var byteArray = AppHelpers.ConvertStreamToByteArray(fileStream);
                UserPictureSource = ImageSource.FromFile(pickedFile.Path);
                projectFile = new Models.File()
                {
                    Name = System.IO.Path.GetFileName(pickedFile.Path),
                    Path = pickedFile.Path
                };
                
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                canPickPicture = true;
                OnPropertyChanged(nameof(UserPictureSource));

            }

        }, () =>canPickPicture);


    }
}
