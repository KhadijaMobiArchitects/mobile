using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XForms.Models;

namespace XForms.ViewModels
{
    public class ProjectViewModel :BaseViewModel
    {
        public ObservableRangeCollection<Project> Projects { get; set; }
        public ObservableRangeCollection<Profil> Squad { get; set; }


        public ProjectViewModel()
        {
            Projects = new ObservableRangeCollection<Project>()
            {
                new Project()
                {
                    Id = 1,
                    Name = "Ajouter",
                    Percent = 30,
                    BackgroundColor = Color.FromHex("#ffffff"),
                    Image = AppHelpers.GetImageResource("add.png")


                },

                new Project()
                {
                    Id = 2,
                    Name = "RH",
                    Percent = 40,
                    BackgroundColor = Color.FromHex("#1A26c9"),
                    Image = AppHelpers.GetImageResource("project.png")



                },

                new Project()
                {
                    Id = 3,
                    Name = "Margin",
                    Percent = 80,
                    BackgroundColor = Color.FromHex("#4AeFF9")


                },

                new Project()
                {
                    Id = 3,
                    Name = "Margin",
                    Percent = 80,
                    BackgroundColor = Color.FromHex("#4ACer9")


                },

                new Project()
                {
                    Id = 3,
                    Name = "Margin",
                    Percent = 80,
                    BackgroundColor = Color.FromHex("#33CFF9")


                },

                new Project()
                {
                    Id = 3,
                    Name = "Margin",
                    Percent = 80,
                    BackgroundColor = Color.FromHex("#445CF9")


                }

            };

            Squad = new ObservableRangeCollection<Profil>()
            {
                new Profil()
                {
                    Id=1,
                    Name = "Hassoun Karoum",
                    fonction = "Dev Mobile"
                },
                new Profil()
                {
                    Id = 2,
                    Name = "Salma El Mejjaty",
                    fonction = "Dev Web Back End"
                }
            };

        }
    }
}
