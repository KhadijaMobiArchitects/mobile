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

        public List<string> members { get; set; }

        public string Picture { get; set; }


    }
}
