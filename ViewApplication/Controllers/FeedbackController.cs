using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ViewApplication.Models;
using ViewApplication.Utils;

namespace ViewApplication.Controllers
{
    [Route("Feedback")]
    public class FeedbackController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public FeedbackController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            return View();
        }


        [Route("Getall")]
        public async Task<JsonResult> Getall()
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var response = await client.GetAsync<List<FeedBackDTO>>(string.Format(Constants.Routes.Feedbacks));

            return Json(new
            {
                data = response.Object
            });
        }

        [HttpPost]
        [Route("Search")]
        public async Task<JsonResult> search(FeedbacksearchDTO model)
        {
            DateTime startdate; DateTime enddate;

            if (model.startdate == null) { startdate = DateTime.Now.Date; } 
            else { startdate = Convert.ToDateTime(model.startdate).Date; }

            if (model.enddate == null) { enddate = DateTime.Now.Date.AddHours(23).AddMinutes(59); }
            else { enddate = Convert.ToDateTime(model.enddate).Date.AddHours(23).AddMinutes(59).AddSeconds(59); }

            var body = new SearchDTO
            {
                StartDate = startdate,
                Status = model.status,
                EndDate = enddate
            };

            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var response = await client.PostAsJsonAsync<SearchDTO, List<FeedBackDTO>>(Constants.Routes.SearchFeedback, body);

            return Json(new
            {
                data = response.Object
            });
        }
        [HttpGet]
        [Route("GetFeedbackbyid/{id}")]
        public async Task<FeedBackDTO> GetFeedbackbyid(int id)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);

            var response = await client.GetAsync<FeedBackDTO>(string.Format(Constants.Routes.GetFeedbackbyId, id));

            var r =  response.Object;

            return r;
        }
        [HttpPost]
        [Route("Replyfeedback")]
        public async Task<string> Replyfeedback(FeedBackDTO model)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientWithToken);
            var response = await client.PostAsJsonAsync<FeedBackDTO, string>(Constants.Routes.ReplyFeedback, model);
            var result = response.Object;
            return result;
        }
    }
}
