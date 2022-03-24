using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XForms.Constants;
using XForms.Enum;
using XForms.HttpREST;
using XForms.Models;
using XForms.Services.Base;

namespace XForms.Services
{
    public class AppServices : BaseService
    {
        public async Task<RESTServiceResponse<IEnumerable<Conge>>> GetConges()
        {
            return await RESTHelper.GetRequest<IEnumerable<Conge>>(url: $"{AppUrls.GesRequestsListConge}", method: HttpVerbs.GET);
        }
    }
}
