using System;
using System.Threading.Tasks;
using XForms.Constants;
using XForms.Enum;
using XForms.HttpREST;
using XForms.Models;

namespace XForms.Services.Base
{
    public class BaseService
    {
        public BaseService()
        {
        }

        public async Task<RESTServiceResponse<SinginResponseModel>> Singin(SinginRequestModel postParams)
        {
            return await RESTHelper.GetRequest<SinginResponseModel>(url: AppUrls.Singin, postObject: postParams, method: HttpVerbs.POST, isNeedAcces: false);
        }
    }
}
