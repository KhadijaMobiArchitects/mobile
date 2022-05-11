using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using XForms.Enum;
using XForms.Models;
using XForms.views;
using XForms.views.Administration;
using XForms.views.Leave;

namespace XForms.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public List<REFItemAdministration> AdminstrationList { get; set; }
        public string UserName { get; set; }
        public HomeViewModel()
        {
            UserName = AppPreferences.FullName;

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
                    AdministrationService.Leave => App.Current.MainPage.Navigation.PushAsync(new LeaveRequestPage()),
                    AdministrationService.Move => App.Current.MainPage.Navigation.PushAsync(new DisplacementPage()),
                    AdministrationService.Project =>  App.Current.MainPage.Navigation.PushAsync(new MyProjectsPage()),

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

        public bool canNavigateToAdmin = true;
        public ICommand NavigateToAdmin => new Command(async =>
        {
            try
            {
                canNavigateToAdmin = false;
                App.Current.MainPage.Navigation.PushAsync(new HomeAdminPage());


            }
            catch (Exception ex)
            {


            }
            finally
            {
                canNavigateToAdmin = true;
            }
        },
        (_) => canNavigateToAdmin);
    }
}
