using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pension_Management_Portal.Models;

namespace Pension_Management_Portal.Controllers
{
    public class PensionController : Controller
    {
        private readonly ILogger<PensionController> _logger;
        private IConfiguration configuration;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionController));
        
        public PensionController(ILogger<PensionController> logger,IConfiguration _configuration)
        {
            _logger = logger;
            configuration = _configuration;
        }
        /// <summary>
        /// Login form displayed to user
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            _log4net.Info("Pensioner is logging in");
            return View();
        }

        /// <summary>
        /// Taking the login credentials and passing it to authorization api to get the token
        /// </summary>
        /// <param name="cred"></param>
        /// <returns></returns>
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login cred)
        {
            _log4net.Info("Post Login is called");
            Login loginCred = new Login();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44391/api/Auth/", content))
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Info("Login failed");
                        ViewBag.Message = "Please Enter valid credentials";
                        return View("Login");
                    }
                    _log4net.Info("Login Successful and token generated");
                    string token = await response.Content.ReadAsStringAsync();


                    loginCred = JsonConvert.DeserializeObject<Login>(token);
                    string userName = cred.Username;
                    TempData["token"] = token;
                    HttpContext.Session.SetString("token", token);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(cred));
                    HttpContext.Session.SetString("owner", userName);
                }
            }
            return View("PensionPortal");
        }

       /// <summary>
       /// For logging out of the current session
       /// </summary>
       /// <returns></returns>
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            
            return View("Login");
        }

        /// <summary>
        /// Getting the input values from the pensioner
        /// </summary>
        /// <returns></returns>
        public ActionResult PensionPortal()
        {
           
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("Pensioner is not logged in");
                ViewBag.Message = "Please Login First";
                return View("Login");
            }
            _log4net.Info("Pensioner is entering his details");
            return View();
        }

        /// <summary>
        /// processing the Input Values 
        /// </summary>
        /// <param name="input"></param>
        /// <returns> Output View </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PensionPortal(PensionerInput input)
        {
            _log4net.Info("Processing the pension began");
            string processValue = configuration.GetValue<string>("MyLinkValue:processUri");

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(input));

                    client.BaseAddress = new Uri(processValue);
                    client.DefaultRequestHeaders.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer" + token);
                }
            }
            
                return View();
            
        }

        
        

       
    }
}
