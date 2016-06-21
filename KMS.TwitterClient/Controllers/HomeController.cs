using KMS.TwitterClient.API;
using System.Web.Mvc;

namespace KMS.TwitterClient.Controllers
{
    public class HomeController : Controller
    {
        private object userTweet;

        /// <summary>
        /// Get user tweet when running
        /// </summary>
        /// <returns>User and twitter Model</returns>
        public ActionResult Index()
        {
            TwitterAPI helper = new TwitterAPI();
            userTweet = helper.GetTweet();
            return View(userTweet);
        }

        [HttpPost]
        public ActionResult PostNewTweet(string status)
        {
            TwitterAPI helper = new TwitterAPI();
            helper.UpdateUserTweet(status);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// return error view associate with global exception
        /// </summary>
        public ActionResult Error()
        {
            string errorInfo = HttpContext.Application["ErrorInfo"].ToString();
            return View("~/Views/Home/Error.cshtml");
        }
    }
}