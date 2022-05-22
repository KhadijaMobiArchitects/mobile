using System;
using System.Collections.Generic;

namespace XForms.Models
{
    public class AddMembersRequest
    {
        public long projectId { get; set; }
        public List<string> members { get; set; }
    }
}
