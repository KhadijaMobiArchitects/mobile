using System;
namespace XForms.Models
{
    public class REFTypeLeave
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Name => Label;

        public REFTypeLeave()
        {
        }
    }
}
