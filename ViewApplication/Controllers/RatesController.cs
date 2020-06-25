using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ViewApplication.Models;
using ViewApplication.Utils;
using X.PagedList;

namespace ViewApplication.Controllers
{
    [Route("Rates")]
    public class RatesController: Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RatesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var getAllRates = await httpClient.GetAsync<List<RateDTO>>(string.Format(Constants.Routes.GetAllRates));

            var response = getAllRates.Object;

            return View();
        }

        [Route("Index2")]
        public async Task<ViewResult> Index2(string sortOrder, string currentFilter, string searchString, int? page)
        {
           
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var rates = await httpClient.GetAsync<List<RateDTO>>(string.Format(Constants.Routes.GetAllRates));

            var response = rates.Object.AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                response = response.Where(s => s.CarName.Contains(searchString)
                                       || s.engineNumber.Contains(searchString));
            }
           
            switch (sortOrder)
            {
                case "name_desc":
                    response = response.OrderByDescending(s => s.CarName);
                    break;
                case "Date":
                    response = response.OrderBy(s => s.CreationTime);
                    break;
                case "date_desc":
                    response = response.OrderByDescending(s => s.CreationTime);
                    break;
                default:
                    response = response.OrderBy(s => s.CarName);
                    break;
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(response.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("GetAllRates")]
        public async Task<JsonResult> GetAllRates()
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var getAllRates = await httpClient.GetAsync<List<RateDTO>>(string.Format(Constants.Routes.GetAllRates));

            var response = getAllRates.Object;

            if (getAllRates.ValidationErrors.Count > 0)
            {
                TempData["error"] = "An Error Occurred " + getAllRates.Code + ": " + getAllRates.ShortDescription;
                return null;
            }
            else
            {
                return Json(new
                {
                    data = response
                });
            }         
        }

        [HttpGet]
        [Route("GetRatesbycarId/{carId}")]
        public async Task<RateDTO> GetRatesbyid(int carId)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var response = await client.GetAsync<RateDTO>(string.Format(Constants.Routes.GetRateByCarId, carId));

            var r = response.Object;

            return r;
        }
    }
}
