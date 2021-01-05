using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace FundaClient.BusinessLogic
{
    public class ApiCaller
    {
        private string FundaAPI => ConfigurationManager.AppSettings["FundaAPI"];
        private string FundaKey => ConfigurationManager.AppSettings["FundaKey"];
        
        public JObject GetApiResponse(int i, bool hasTuin)
        {
            var _uri = hasTuin ? $"/?type=koop&zo=/amsterdam/tuin/&page={i}&pagesize=25" : $"/?type=koop&zo=/amsterdam/&page={i}&pagesize=25";
            var fundaApi = string.Concat(FundaAPI, FundaKey, _uri);

            var httpClient = new HttpClient();
            Task<string> content = httpClient.GetStringAsync(fundaApi);

            return JObject.Parse(content.Result);
        }
    }
}