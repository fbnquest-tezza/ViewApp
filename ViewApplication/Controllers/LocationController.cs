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
    [Route("Location")]
    public class LocationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LocationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task <IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var GetAllStates = await httpClient.GetAsync(Constants.Routes.GetAllStates);

            var result = await GetAllStates.Content.ReadAsAsync<IList<LocationDTO>>();

            ViewBag.State = new SelectList(result ?? new List<LocationDTO>(), "StateId", "StateName");

            return View();
        }

        [Route("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var getAllLocations = await httpClient.GetAsync(Constants.Routes.GetAllLocations);

            if (getAllLocations.IsSuccessStatusCode)
            {
                var result = await getAllLocations.Content.ReadAsAsync<IList<LocationDTO>>();

                return Json(new
                {
                    data = result
                });
            }
            else
                return null;
        }

        [HttpPost]
        [Route("AddLocationPost")]
        public async Task<ActionResult> AddLocationPost(LocationDTO model)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var response = await httpClient.PostAsJsonAsync<LocationDTO>(Constants.Routes.AddLocation, model);

            var states = await response.Content.ReadAsAsync<bool>();

            if (states != true)
            {
                TempData["error"] = "An Error Occurred ";

                return View("Index", model);
            }
            else
            {
                TempData["success"] = "Location added successfully";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Route("RemoveLocation/{id}")]
        public async Task<string> RemoveLocation(int id)
        {
            var result = "";
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            //post
            var response = await httpClient.GetAsync<bool>($"{Constants.Routes.Location + "RemoveLocation/" + id}");
            var items = response.Object;
            if (response.ValidationErrors.Count > 0 || items == false)
            {
                TempData["error"] = "An Error Occurred " + response.ShortDescription;
                return result = "An Error Occurred " + response.ShortDescription;
            }
            else
                return result = "Success";
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var result = "";

            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var getAllStates = await httpClient.GetAsync(Constants.Routes.GetAllStates);

            if (getAllStates.IsSuccessStatusCode)
            {
                var states = await getAllStates.Content.ReadAsAsync<IList<LocationDTO>>();

                ViewBag.states = new SelectList(states ?? new List<LocationDTO>(), "StateId", "StateName");
            }

            var response = await httpClient.GetAsync<LocationDTO>($"{Constants.Routes.Location + "GetLocationById/" + id}");

            var items = response.Object;

            if (response.ValidationErrors.Count > 0)
            {
                TempData["error"] = "An Error Occurred " + response.ShortDescription.ToString();
                return View(items);
            }
            else
                return View(items);
        }

        [HttpPost]
        [Route("EditPost")]
        public async Task<IActionResult> EditPost(LocationDTO model)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var url = string.Format(Constants.Routes.UpdateLocation, model.LocationId);
            var response = await httpClient.PutAsync<LocationDTO, bool>(url, model);

            var items = response.Object;
            if (response.ValidationErrors.Count > 0 || items == false)
            {
                TempData["error"] = "An Error Occurred " + response.ShortDescription;

                return RedirectToAction("Update", model.LocationId);
            }
            else
            {
                TempData["success"] = "Update Successful";
                return RedirectToAction("Index");
            }          
        }
    }
}