using System;
using Xamarin.Forms;

namespace XForms.Models
{
    public class WalkthroughModel
    {
        public Guid Id => Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public ImageSource Image { get; set; }
        public bool IsLastStep { get; set; }
    }
}