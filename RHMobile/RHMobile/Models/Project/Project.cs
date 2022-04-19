using System;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace XForms.Models
{
    public class Project
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public string OwnerBy { get; set; }
        public string CreatedBy { get; set; }
        public  Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public ImageSource Image { get; set; }

        public Project()
        {
        }
    }
}
