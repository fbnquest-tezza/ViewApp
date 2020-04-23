using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewApplication.Models;
using ViewApplication.Utils;

namespace ViewApplication.Controllers
{
    public class ReportController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task <IActionResult> SearchFleet()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var getAllLocations = await client.GetAsync(Constants.Routes.GetAllLocations);

            var getAllCarManuacturers = await client.GetAsync(Constants.Routes.GetAllCarManuacturer);

            if (getAllLocations.IsSuccessStatusCode && getAllCarManuacturers.IsSuccessStatusCode)
            {
                var result2 = await getAllCarManuacturers.Content.ReadAsAsync<IList<CarManufacturerDTO>>();

                var result = await getAllLocations.Content.ReadAsAsync<IList<LocationDTO>>();

                ViewBag.CarManufacturer = new SelectList(result2 ?? new List<CarManufacturerDTO>(), "Id", "Name");

                ViewBag.location = new SelectList(result ?? new List<LocationDTO>(), "LocationId", "LocationName");

                return View();
            }
            else
                return View();
        }

        [Route("searchfleetpost")]
        [HttpPost]
        public async Task<IActionResult> searchfleetpost(CarSearchDTO model)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            model.color = model.Colors.ToString();
            model.status = model.Statuses.ToString();
            model.condition = model.Conditions.ToString();
            model.prodYear = model.ProductionDate.GetValueOrDefault().Year;
            var response = await client.PostAsJsonAsync<CarSearchDTO, List<CarsDTO>>(Constants.Routes.CarSearch, model);
            var items = response.Object;
            if (response.ValidationErrors.Count > 0)
            {
                TempData["error"] = "An Error Occurred " + response.Code + ": " + response.ShortDescription;
                return View("SearchFleet", model);
                //return RedirectToAction("AddNewCar");
            }
            else
                return Json(new
                {
                    data = items
                });
        }
        
        public IActionResult SalesHistory()
        {
            return View();
        }

        [HttpGet]
        [Route("GetSalesHistory")]
        public async Task<IActionResult> GetSalesHistory()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var response = await client.GetAsync<List<CarSalesDTO>>(Constants.Routes.GetSalesHistory);

            foreach (var i in response.Object)
            {
                i.totalPaidString = CurrencyConverter.formatAmount(i.TotalPaid);
                i.GenderString = i.Gender.ToString();
                i.DeliveryTypes = i.DeliveryType.ToString();
                i.deliveryFeeString = CurrencyConverter.formatAmount(i.DeliveryFee);
                i.Amount = CurrencyConverter.formatAmount(i.RetailPrice);
                i.vatString = CurrencyConverter.formatAmount(i.VAT);
                i.transactionDate = i.CreationTime.ToString("dd/MM/yyyy HH:mm:ss");
            }
            if (response.ValidationErrors.Count <= 0)
            {
                return Json(new
                {
                    data = response.Object
                });
            }
            else
                return null;
        }
        public IActionResult LeaseHistory()
        {
            return View();
        }

        [HttpGet]
        [Route("GetLeaseHistory")]
        public async Task<IActionResult> GetLeaseHistory()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var response = await client.GetAsync<List<CarLeaseDTO>>(Constants.Routes.GetLeaseHistory);

            foreach (var i in response.Object)
            {
                i.amountString = CurrencyConverter.formatAmount(i.Amount);
                i.totalPaidString = CurrencyConverter.formatAmount(i.TotalPaid);
                i.Genders = i.Gender.ToString();
                i.PaymentTypes = i.PaymentType.ToString();
                i.DeliveryTypes = i.DeliveryType.ToString();
                i.vatString = CurrencyConverter.formatAmount(i.VAT);
                i.DurationTypes = i.DurationType.ToString();
            }

            if (response.ValidationErrors.Count <= 0)
            {
                return Json(new
                {
                    data = response.Object
                });
            }
            else
                return null;
        }
    }
}