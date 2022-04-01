using System;
using System.Collections.Generic;
using System.Reflection;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public List<REFItemAdministration> AdminstrationList { get; set; }

        public HomeViewModel()
        {

            AdminstrationList = new List<REFItemAdministration>
            {
                new REFItemAdministration()
                {
                    Title = "Demande\nCongé",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(0,0,5,5)
                },
                new REFItemAdministration()
                {
                    Title = "Demande\nAttestation",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(5,0,5,5)

                },
                new REFItemAdministration()
                {
                    Title = "Déplacement\nClient",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(5,0,0,5)

                },
                new REFItemAdministration()
                {
                    Title = "Réclamation\nInterne",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(5,5,0,0)

                }
                  ,
                new REFItemAdministration()
                {
                    Title = "Projets\nClient",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(5,5,5,0)

                }
                ,
                new REFItemAdministration()
                {

                    Title = "Espace\nStagaires",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(0,5,5,5)

                }
                ,
                new REFItemAdministration()
                {
                    Title = "Données\nPersonnelles",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(0,5,5,0)

                }
                ,
                new REFItemAdministration()
                {
                    Title = "Délégations",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(5,5,5,5)

                }
                ,
                new REFItemAdministration()
                {
                    Title = "Bulletins\nDe paie",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(5,5,0,5)

                }
                ,
                new REFItemAdministration()
                {
                    Title = "RCAR",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(0,5,5,5)

                }
                ,
                new REFItemAdministration()
                {
                    Title = "Recore",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(5,5,5,5)

                }
                ,
                new REFItemAdministration()
                {
                    Title = "Recore sur \nprime",
                    ICone =  SvgImageSource.FromResource("XForms.Resources.Images.calendar2.svg", typeof(HomeViewModel).GetTypeInfo().Assembly)
                    ,padding = new Thickness(5,5,0,5)

                }
    };
        }
    }
}
