using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ViewApplication.Models;
using ViewApplication.Utils;

namespace ViewApplication.Controllers
{
    [Route("Cars")]
    public class CarsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("GetAllCars")]
        public async Task<IActionResult> GetAllCars()
        {
            IEnumerable<CarsDTO> cars = null;
            try
            {
                var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
                var responseTask = await client.GetAsync(Constants.Routes.GetCarsList);

                var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CarsDTO>>();
                    readTask.Wait();

                    cars = readTask.Result;
                }
                else
                {
                    cars = Enumerable.Empty<CarsDTO>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View(cars);
            }
        }

        [Route("GetDataCars")]
        public async Task<IActionResult> GetData()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            var responseTask = await client.GetAsync(Constants.Routes.GetCarsList);
            if (responseTask.IsSuccessStatusCode)
            {
                var result = await responseTask.Content.ReadAsAsync<IList<CarsDTO>>();
                return Json(new
                {
                    data = result
                });
            }
            else
                return null;
        }

        [Route("AddNewCar")]
        public async Task<IActionResult> AddNewCar()
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
        [HttpPost]
        [Route("AddCarPost")]
        public async Task<IActionResult> AddCarPost(CarsDTO model)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            model.Color = model.Colors.ToString();
            model.Status = model.Statuses.ToString();
            model.Condition = model.Conditions.ToString();

            CarColor col = StringToEnumConverter.ParseEnum<CarColor>("Ash");

            var response = await client.PostAsJsonAsync<CarsDTO, bool>(Constants.Routes.AddCar, model);
            var items = response.Object;
            if (response.ValidationErrors.Count > 0)
            {
                TempData["error"] = "An Error Occurred " + response.Code + ": " + response.ShortDescription;
                return View("AddNewCar", model);
                //return RedirectToAction("AddNewCar");
            }
            else
            {
                //return View("AddNewCar", model);
                return RedirectToAction("GetAllCars");
            }
        }
        [Route("EditVehicle/{id}")]
        public async Task<IActionResult> EditVehicle(int id)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            //get vehicle by id
            var response = await client.GetAsync<CarsDTO>(string.Format(Constants.Routes.GetCarById, id));

            var getAllLocations = await client.GetAsync(Constants.Routes.GetAllLocations);

            var getAllCarManuacturers = await client.GetAsync(Constants.Routes.GetAllCarManuacturer);

            var result = await getAllLocations.Content.ReadAsAsync<IList<LocationDTO>>();

            var result2 = await getAllCarManuacturers.Content.ReadAsAsync<IList<CarManufacturerDTO>>();

            ViewBag.CarManufacturer = new SelectList(result2 ?? new List<CarManufacturerDTO>(), "Id", "Name");

            ViewBag.location = new SelectList(result ?? new List<LocationDTO>(), "LocationId", "LocationName");

            var items = response.Object;

            items.Colors = StringToEnumConverter.ParseEnum<CarColor>(items.Color);
            items.Statuses = StringToEnumConverter.ParseEnum<CarStatus>(items.Status);
            items.Conditions = StringToEnumConverter.ParseEnum<CarCondition>(items.Condition);

            if (response.ValidationErrors.Count > 0)
            {
                ViewBag.errors = "An Error Occurred " + response.Code + ": " + response.ShortDescription;
                return RedirectToAction("GetAllCars");
            }
            else
            {
                return View(items);
            }
        }

        [Route("EditVehiclePost")]
        public async Task<IActionResult> EditVehiclePost(CarsDTO model)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            model.Color = model.Colors.ToString();
            model.Status = model.Statuses.ToString();
            model.Condition = model.Conditions.ToString();
            //post

            var url = string.Format(Constants.Routes.UpdateCar, model.Id);
            var response = await httpClient.PutAsync<CarsDTO, bool>(url, model);

            var items = response.Object;
            if (response.ValidationErrors.Count > 0 || items == false)
            {
                TempData["error"] = "An Error Occurred " + response.ShortDescription;
                return RedirectToAction("GetAllCars");
            }
            else
                return RedirectToAction("GetAllCars");
        }

        [Route("RemoveVehicle/{id}")]
        public async Task<string> RemoveVehicle(int id)
        {
            var result = "";
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            //post
            var response = await httpClient.GetAsync<bool>($"{Constants.Routes.Car + "/Remove/" + id}");
            var items = response.Object;
            if (response.ValidationErrors.Count > 0 || items == false)
            {
                TempData["error"] = "An Error Occurred " + response.ShortDescription;
                return result = "An Error Occurred " + response.ShortDescription;
            }
            else
                return result = "Success";
        }
        [Route("GetDataLeased")]
        public async Task<IActionResult> GetDataLeased()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var responseTask = await client.GetAsync(Constants.Routes.GetLeased);

            if (responseTask.IsSuccessStatusCode)
            {
                var result = await responseTask.Content.ReadAsAsync<IList<CarsDTO>>();
                return Json(new
                {
                    data = result
                });
            }
            else
                return null;
        }

        [Route("GetDataSold")]
        public async Task<IActionResult> GetDataSold()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var responseTask = await client.GetAsync(Constants.Routes.GetSold);

            if (responseTask.IsSuccessStatusCode)
            {
                var result = await responseTask.Content.ReadAsAsync<IList<CarsDTO>>();
                return Json(new
                {
                    data = result
                });
            }
            else
                return null;
        }
        [Route("GetDataAvailable")]
        public async Task<IActionResult> GetDataAvailable()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var responseTask = await client.GetAsync(Constants.Routes.GetAvailable);

            if (responseTask.IsSuccessStatusCode)
            {
                var result = await responseTask.Content.ReadAsAsync<IList<CarsDTO>>();
                return Json(new
                {
                    data = result
                });
            }
            else
                return null;
        }
        [HttpGet]
        [Route("GetCarPrice/{id}")]
        public async Task<CarsDTO> GetCarPrice(int id)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            var response = await client.GetAsync<CarsDTO>(string.Format(Constants.Routes.GetCarById, id));
            var price = response.Object.RetailPrice.GetValueOrDefault();

            //get vat
            var response2 = await client.GetAsync<decimal>(string.Format(Constants.Routes.CalculateVAT, price));
            var vat = response2.Object;

            var total = price + vat;

            var totalstring = CurrencyConverter.formatAmount(total);
            var pricestring = CurrencyConverter.formatAmount(price);
            var VatString = CurrencyConverter.formatAmount(vat);

            var model = new CarsDTO
            {
                TotalToPay = totalstring,
                Amount = pricestring,
                VAT = VatString
            };

            return model;
        }

        [HttpGet]
        [Route("GetTotal")]
        public async Task<GetTotalResponseDTO> GetTotal(string amount, DeliveryType deliveryType)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var price = CurrencyConverter.currencyToDecimal(amount);

            var response2 = await client.GetAsync<decimal>(string.Format(Constants.Routes.CalculateVAT, price));
            var vat = response2.Object;

            decimal deliveryFee = 0m;

            if (deliveryType == DeliveryType.HomeDelivery)
                deliveryFee = 50000;

            decimal Total = price + vat + deliveryFee;

            return new GetTotalResponseDTO
            {
                deliveryCharge = CurrencyConverter.formatAmount(deliveryFee),
                amount = CurrencyConverter.formatAmount(price),
                total = CurrencyConverter.formatAmount(Total),
                vat = CurrencyConverter.formatAmount(vat)
            };
        }
        [Route("SellVehicle")]
        public async Task<IActionResult> SellVehicle()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();

            var responseTask = await client.GetAsync(Constants.Routes.GetAvailable);

            if (responseTask.IsSuccessStatusCode)
            {
                var result = await responseTask.Content.ReadAsAsync<IList<CarsDTO>>();

                ViewBag.Cars = new SelectList(result ?? new List<CarsDTO>(), "Id", "Details");
            }
            return View();
        }

        [HttpPost]
        [Route("SellPost")]
        public async Task<IActionResult> SellPost(CarSalesDTO model)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            model.DeliveryFee = CurrencyConverter.currencyToDecimal(model.deliveryFeeString);

            //convert price from view to decimal
            var newPrice = CurrencyConverter.currencyToDecimal(model.Amount);

            model.RetailPrice = newPrice;
            //get old price of vehicle
            var response1 = await client.GetAsync<CarsDTO>(string.Format(Constants.Routes.GetCarById, model.CarId));
            var price = response1.Object.RetailPrice.GetValueOrDefault();

            //if (newPrice != price)
            //{
            //    TempData["error"] = "You changed prices";
            //    return View("Sell", model);
            //}

            var response = await client.PostAsJsonAsync<CarSalesDTO, CarSalesDTO>(Constants.Routes.Sale, model);

            var items = response.Object;
            var code = response.Code;
            if (response.ValidationErrors.Count > 0 || code != HttpStatusCode.OK.ToString())
            {
                TempData["error"] = "An Error Occurred " + response.Code + ": " + response.ShortDescription;
                return View("Sell", model);
            }
            else
            {
                TempData["success"] = "Sale Successful";
                return RedirectToAction("Sell");
            }
        }

        [Route("LeaseVehicle")]
        public async Task <IActionResult> LeaseVehicle()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();

            var responseTask = await client.GetAsync(Constants.Routes.GetAvailable);

            if (responseTask.IsSuccessStatusCode)
            {
                var result = await responseTask.Content.ReadAsAsync<IList<CarsDTO>>();

                ViewBag.Cars = new SelectList(result ?? new List<CarsDTO>(), "Id", "Details");
            }
            return View();
        }
    }
}