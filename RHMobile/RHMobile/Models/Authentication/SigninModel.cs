using System;
using System.Collections.Generic;

namespace XForms.Models
{
    public class SinginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SinginResponseModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PrixDiagnostic { get; set; }
        public List<string> Roles { get; set; }
        public bool isVerified { get; set; }
        public string JwToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
