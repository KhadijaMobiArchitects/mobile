using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XForms.Enum;

namespace XForms.HttpREST
{
    public static class RESTHelper
    {
        public static async Task<RESTServiceResponse<T>> GetRequest<T>(string url, HttpVerbs method = HttpVerbs.GET, System.Collections.Specialized.NameValueCollection getParams = null, object postObject = null, bool isNeedAcces = true, string contentType = "application/json")
        {
            try
            {

                 //Skip Certificate Validation
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });


                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    Uri uri = new Uri(url);
                    //client.BaseAddress = uri;
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    HttpResponseMessage response = new HttpResponseMessage();
                    switch (method)
                    {
                        case HttpVerbs.GET:
                            response = await client.GetAsync(uri);
                            break;
                        //case HttpVerbs.POST:
                        //    //var content = new StringContent(JsonConvert.SerializeObject(postObject), Encoding.UTF8, contentType);

                        //    var json = JsonConvert.SerializeObject(postObject, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }).ToString();
                        //    //var json = JsonConvert.SerializeObject(postObject).ToString();
                        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
                        //    response = await client.PostAsync(uri, content);
                        //    break;
                        //default:
                        //    break;
                    }

                    var stringResponseJson = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<RESTServiceResponse<T>>(stringResponseJson);

                    return result;
                }
            }
            catch(Exception Ex)
            {
                return new RESTServiceResponse<T>(false, Ex.Message);
            }
        }


    }
}
