using FundaClient.BusinessLogic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FundaClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RichestMakelaar()
        {
            var sellersList = new List<int>();
            var apiCaller = new ApiCaller();

            for (int i = 1; i <= 26; i++)
            {
                var apiResponse = apiCaller.GetApiResponse(i, false);
                var objectsArray = apiResponse["Objects"];
                
                foreach (var item in objectsArray.Children())
                {
                    sellersList.Add((int)item["MakelaarId"]);
                }
            }

            ViewData["richestMakelaars"] = CommonOccurrence(sellersList.ToArray());

            return View();
        }

        public async Task<ActionResult> ObjectsWithTuin()
        {
            var sellersList = new List<int>();
            var apiCaller = new ApiCaller();

            for (int i = 1; i <= 26; i++)
            {
                var apiResponse = await apiCaller.GetApiResponseAsync(i, true);
                var objectsArray = apiResponse["Objects"];

                foreach (var item in objectsArray.Children())
                {
                    sellersList.Add((int)item["MakelaarId"]);
                }
            }

            ViewData["objectsWithTuin"] = CommonOccurrence(sellersList.ToArray());

            return View();
        }

        private static Dictionary<int, int> CommonOccurrence(int[] numbers)
        {
            var groups = numbers.GroupBy(x => x);
            var largest = groups.OrderByDescending(x => x.Count()).Take(10);
            var dict = new Dictionary<int,int>();

            foreach (var item in largest)
            {
                dict.Add(item.Key, item.Count());
            }

            return dict;
        }
    }
}