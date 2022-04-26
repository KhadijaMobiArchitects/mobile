using System;
using System.Collections.Generic;

namespace XForms.Models
{
    public class SquadResponse
    {

        public int ProjectId { get; set;}

        //  public string ProjectName { get; set; } = null!;

        public ProfilResponse owner { get; set;}

        public List<ProfilResponse> members { get; set;}

    }
}
