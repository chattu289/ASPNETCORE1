using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private iMailService _mailService;
        private IConfigurationRoot _config;

        public AppController(iMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contact)
        {
            if (contact.Email.Contains("@iol.com"))
            {
                ModelState.AddModelError("Email", "We wont support iol email addresses.");
            }
            if(ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], "ASPNETCORE@microsoft.com", "Subject of the Mail", "Body of the Mail");
                ModelState.Clear();
                ViewBag.Message = "Message Sent";
            }
            return View();
        }

        public JsonResult getAPI()
        {
            var result = new { Name = "API", Version = "1.0.0", Technology = "APSNETCORE" };
            JsonResult json = new JsonResult(result);
            return json;
        } 
    }
}
