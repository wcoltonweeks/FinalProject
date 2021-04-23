using FinalProject.UI.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Configuration;

using System.Web.Mvc;

namespace FinalProject.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://vaccovid-coronavirus-vaccine-and-treatment-tracker.p.rapidapi.com/api/npm-covid-data/world/"),
                Headers =
                {
                    { "x-rapidapi-key", "eb945d3620msh44e79fda64e466dp1decb7jsned63dea98e3a" },
                    { "x-rapidapi-host", "vaccovid-coronavirus-vaccine-and-treatment-tracker.p.rapidapi.com" },

                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<APIDataModel>>(body);
                ViewBag.Data = data;
     
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            string emailServer = WebConfigurationManager.AppSettings["EmailServer"];            string emailPW = WebConfigurationManager.AppSettings["EmailPW"];            string emailUser = WebConfigurationManager.AppSettings["EmailUser"];            string emailToAddress = WebConfigurationManager.AppSettings["EmailToAddress"];

            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            // 1. build the body of the email
            string body = $"You have revieved an email from {cvm.Name} at the address {cvm.Email} with a subject of {cvm.Subject}<br><strong>Message:</strong>{cvm.Message}";

            // 2.instantiate the MainMessage object (needs System.Net.Mail)
            MailMessage msg = new MailMessage($"{emailUser}", $"{emailToAddress}", cvm.Subject, body);
            // (optional) customize the MainMessage properties
            msg.IsBodyHtml = true;

            //instantiate the SmtpClient
            SmtpClient client = new SmtpClient($"{emailServer}");

            // 4. provide credentials for the client (needs the System.Net namespace)
            client.Credentials = new NetworkCredential($"{emailUser}", $"{emailPW}");

            // 5. attempt to send the email
            try
            {
                client.Send(msg);
            }
            catch (System.Exception ex)
            {
                ViewBag.ErrorMessage = "Sorry, had some trouble with that. see stacktrace or contact an admin." + ex.StackTrace;
            }
            return View("EmailConfirmation", cvm);
        }
        //public async Task<ActionResult> ApiTest()
        //{
        //    var client = new HttpClient();

        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri("https://vaccovid-coronavirus-vaccine-and-treatment-tracker.p.rapidapi.com/api/npm-covid-data/world/"),
        //        Headers =
        //        {
        //            { "x-rapidapi-key", "eb945d3620msh44e79fda64e466dp1decb7jsned63dea98e3a" },
        //            { "x-rapidapi-host", "vaccovid-coronavirus-vaccine-and-treatment-tracker.p.rapidapi.com" },

        //        },
        //    };
        //    using (var response = await client.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var body = await response.Content.ReadAsStringAsync();
        //        var data = JsonConvert.DeserializeObject<List<APIDataModel>>(body);
        //        ViewBag.Data = data;
        //        return View(data);
        //    }

        //}
    }
}
