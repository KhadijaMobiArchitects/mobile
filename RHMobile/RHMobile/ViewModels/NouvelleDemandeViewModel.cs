using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    public class NouvelleDemandeViewModel : BindableObject
    {
        
        public List<Conge> ListConge { get; set; }

        public NouvelleDemandeViewModel()
        {
            ListConge  = new List<Conge> {
                new Conge(){
                    Type="Annuel"
                }
                ,
                new Conge(){
                    Type="Mensuel"
                }
            };

            
        }

        
    }
}
