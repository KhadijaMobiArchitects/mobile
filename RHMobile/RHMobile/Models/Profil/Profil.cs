using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XForms.Models
{
    public partial class Profil
    {
        public string Id { get; set; }
        public String Name { get; set; }
        public String fonction { get; set; }
        public bool IsSelected { get; set; }
        public bool IsOwner { get; set; }



    }
}
