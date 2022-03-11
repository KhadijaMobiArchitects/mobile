using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.Models;
using XForms.Models.Projet;

namespace XForms.ViewModels
{
    public class NouvelleDemandeViewModel : BindableObject
    {
        
        public List<Conge> ListConge { get; set; }
        public List<Projet> ListProjet { get; set; }
        public List<SituationProjet> ListSituation { get; set; }

        public NouvelleDemandeViewModel()
        {
            ListConge = new List<Conge> {
                new Conge(){
                    Type="Annuel"
                }
                ,
                new Conge(){
                    Type="Mensuel"
                }
            };

            ListProjet = new List<Projet> {
                new Projet(){
                    Name="Ta7alil"
                }
                ,
                new Projet(){
                    Name="Khdamat"
                }
            ,
                new Projet(){
                    Name="Kool"
                }
            ,
                new Projet(){
                    Name="ElectroPlanet"
                }
            ,
                new Projet(){
                    Name="Audit"
                }
            };

            ListSituation = new List<SituationProjet>
            {
                new SituationProjet(){
                    Name="Livré partiellement"
                },
                new SituationProjet(){
                    Name="Livré totalement"
                }
            };
        }


    }
}
