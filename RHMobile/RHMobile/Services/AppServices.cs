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
        public async Task<RESTServiceResponse<IEnumerable<Leave>>> GetLeaves()
        {
            return await RESTHelper.GetRequest<IEnumerable<Leave>>(url: $"{AppUrls.GesRequestsListLeave}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<object>> PostLeave(Leave postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.GesRequestsListLeave}",postObject: postParams, method: HttpVerbs.POST);
        }
    }


}
