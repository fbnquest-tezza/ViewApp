using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViewApplication.Models;
using ViewApplication.Utils;

namespace ViewApplication.Controllers
{
    [Route("State")]
    public class StateController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StateController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var GetAllStates = await httpClient.GetAsync(Constants.Routes.GetAllStates);

            if (GetAllStates.IsSuccessStatusCode)
            {
                var result = await GetAllStates.Content.ReadAsAsync<IList<LocationDTO>>();

                return Json(new
                {
                    data = result
                });
            }
            else
                return null;
        }

        [HttpPost]
        [Route("AddStatePost")]
        public async Task<ActionResult> AddStatePost(LocationDTO model)
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var response = await httpClient.PostAsJsonAsync<LocationDTO>(Constants.Routes.AddState, model);

            var states = await response.Content.ReadAsAsync<bool>();

            if (states != true)
            {
                TempData["error"] = "An Error Occurred ";

                return View("Index", model);
            }
            else
            {
                TempData["success"] = "State added successfully";

                return RedirectToAction("Index");
            }
        }
    }
}