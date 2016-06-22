using KMS.TwitterClient.API;
using System.Web.Mvc;

namespace KMS.TwitterClient.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// Get user tweet when running
        /// </summary>
        /// <returns>User timeline</returns>
        public ActionResult Index()
        {
            TwitterAPI helper = new TwitterAPI();
            var userTweet = helper.GetTweet();
            return View(userTweet);
        }

        /// <summary>
        /// Post status to user timeline
        /// </summary>
        /// <param name="status">Status user want to post</param>
        /// <returns>Index action</returns>
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
            return View("~/Views/Home/Error.cshtml");
        }
    }
}