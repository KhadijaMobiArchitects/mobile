using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.Models
{

    public class ProjectRequest : BindableObject
    {

        public int Id { get; set; }

        public string ProjectName { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }

        public int Percent { get; set; }

        public string OwnerBy { get; set; }

        public string members { get; set; }

        public string Picture { get; set; }

        public Models.File ProjectFile { get; set; }


    }

    public class DeleteMemeberProjectModel
    {
        public int ProjectId { get; set; }
        public string ProfilId { get; set; }
    } 
}
