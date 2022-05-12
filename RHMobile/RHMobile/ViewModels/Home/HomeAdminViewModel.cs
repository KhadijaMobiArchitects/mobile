using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using XForms.Enum;
using XForms.Models;
using XForms.views.LeaveAdministration;
using XForms.views.Leave;
using XForms.views;

namespace XForms.ViewModels
{
    public class HomeAdminViewModel : BaseViewModel
    {
        public List<REFItemAdministration> AdminstrationList { get; set; }

        public string FullName { get; set; }
        public string PictureUrl { get; set; }

        public HomeAdminViewModel()
        {
            FullName = AppPreferences.FullName;
            PictureUrl =  AppPreferences.PictureUrl;

            AdminstrationList = new List<REFItemAdministration>
            {
                new REFItemAdministration()
                {
                    Id = AdministrationService.Leave,
                    Title = ResourceHelpers.GetServiceTitle(AdministrationService.Leave),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Leave)
                },
                new REFItemAdministration()
                {
                    Id = AdministrationService.Certaficate,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.Certaficate),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Certaficate),

                },
                new REFItemAdministration()
                {
                    Id = AdministrationService.Move,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.Move),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Move),

                },
                new REFItemAdministration()
                {
                    Id = AdministrationService.Complaint,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.Complaint),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Complaint),

                }
                  ,
                new REFItemAdministration()
                {
                    Id = AdministrationService.Project,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.Project),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Project),

                }
                ,
                new REFItemAdministration()
                {
                    Id = AdministrationService.Intership,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.Intership),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Intership),

                }
                ,
                new REFItemAdministration()
                {
                    Id = AdministrationService.PersonalData,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.PersonalData),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.PersonalData),

                }
                ,
                new REFItemAdministration()
                {
                    Id = AdministrationService.Delegation,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.Delegation),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Delegation),

                }
                ,
                new REFItemAdministration()
                {
                    Id = AdministrationService.Payslips,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.Payslips),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Payslips),

                }
                ,
                new REFItemAdministration()
                {
                    Id = AdministrationService.RCAR,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.RCAR),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.RCAR),

                }
                ,
                new REFItemAdministration()
                {
                    Id = AdministrationService.Recore,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.Recore),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.Recore),

                }
                ,
                new REFItemAdministration()
                {
                    Id = AdministrationService.RecorePrime,
                    Title=ResourceHelpers.GetServiceTitle(AdministrationService.RecorePrime),
                    ICone =(SvgImageSource)ResourceHelpers.GetServiceIcon(AdministrationService.RecorePrime)

                }
    };

        }
        public INavigation navigation { get; set; }
        public bool canAdminisatrionNavigation = true;
        public ICommand AdministraionNavigation => new Command<REFItemAdministration>(async (model) =>
        {
            try
            {
                canAdminisatrionNavigation = false;

                if (model == null)
                    return;

                _ = model.Id switch
                {
                    AdministrationService.Leave => App.Current.MainPage.Navigation.PushAsync(new LeaveAdministrationPage()),
                    AdministrationService.Project => App.Current.MainPage.Navigation.PushAsync(new ProjectPage()),


                };
            }
            catch (Exception ex)
            {


            }
            finally
            {
                canAdminisatrionNavigation = true;
            }
        },
        (_) => canAdminisatrionNavigation);


        public bool canNavigateToUser = true;
        public ICommand NavigateToUser => new Command(async =>
        {
            try
            {
                canNavigateToUser = false;
                App.Current.MainPage.Navigation.PopAsync();


            }
            catch (Exception ex)
            {


            }
            finally
            {
                canNavigateToUser = true;
            }
        },
(_) => canNavigateToUser);

    }
}

