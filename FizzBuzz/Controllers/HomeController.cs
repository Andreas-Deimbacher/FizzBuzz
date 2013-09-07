using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using LinqToTwitter;
using System.Configuration;
namespace FizzBuzz.Controllers
{

    public class HomeController : Controller
    {

        private IOAuthCredentials credentials = new SessionStateCredentials();
        private MvcAuthorizer auth;
        private static TwitterContext twitterCtx;
        

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

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int input)
        {
            
            /***************************** FizzBuzz  *********************************/
            StringBuilder result = new StringBuilder();
            if (input % 3 == 0) result.Append("Fizz");
            if (input % 5 == 0) result.Append("Buzz");
            if (result.Length == 0) result.Append(input);
            
            ViewBag.input = input.ToString();
            ViewBag.output = result.ToString();

            /**************************Twitter post ************************************/

            var tweetStatus = twitterCtx.UpdateStatus(input + " is a " + result);

            ViewBag.tweet = "Tweet succesfull \n" +twitterCtx.UserName + "\n" + tweetStatus.Text;

            return View();
        }

    }
}
