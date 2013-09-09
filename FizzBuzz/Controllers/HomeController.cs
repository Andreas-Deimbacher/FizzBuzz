using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using LinqToTwitter;
using System.Configuration;
using FizzBuzz.Models;
namespace FizzBuzz.Controllers
{

    public class HomeController : Controller
    {

        private IOAuthCredentials credentials = new SessionStateCredentials();
        private MvcAuthorizer auth;
        private static TwitterContext twitterCtx;
        private static IndexModels indexModel;


        public ActionResult Index()
        {
            /************** Twitter authorize procedures *********************************/

            if (credentials.ConsumerKey == null || credentials.ConsumerSecret == null)
            {
                credentials.ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
                credentials.ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
            }

            auth = new MvcAuthorizer { Credentials = credentials };
            auth.CompleteAuthorization(Request.Url);

            if (!auth.IsAuthorized)
            {
                Uri specialUri = new Uri(Request.Url.ToString());
                return auth.BeginAuthorization(specialUri);
            }
            twitterCtx = new TwitterContext(auth);

            /***************************************************************************/

            indexModel = new IndexModels();

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? input)
        {
            if (!input.HasValue)
            {
                ModelState.AddModelError("Input", "Please enter a Number");
            }

            if (ModelState.IsValid)
            {

                /***************************** FizzBuzz  *********************************/
                StringBuilder result = new StringBuilder();
                if (input % 3 == 0) result.Append("Fizz");
                if (input % 5 == 0) result.Append("Buzz");
                if (result.Length == 0) result.Append(input);

                indexModel.Input = input;
                indexModel.Output = result.ToString();


                /**************************Twitter post ************************************/

                var tweetStatus = twitterCtx.UpdateStatus(input + " is a " + result);

                indexModel.tweetSend = true;
                indexModel.TweetMessage = "Tweet succesfull \n" + twitterCtx.UserName + "\n" + tweetStatus.Text;
            }
            return View(indexModel);
        }

    }
}
