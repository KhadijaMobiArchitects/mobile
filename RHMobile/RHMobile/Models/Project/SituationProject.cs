using System;
namespace XForms.Models
{
    public class SituationProject
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Name => Label;

        public SituationProject()
        {

        }
    }
}
