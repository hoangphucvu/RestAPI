using KMS.TwitterClient.Helper;
using System;
using System.Web.Mvc;

namespace KMS.TwitterClient.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Get user tweet when running
        /// </summary>
        /// <returns>User and tweet Model</returns>
        public ActionResult Index()
        {
            TwitterAPI helper = new TwitterAPI();
            var userTweet = helper.GetTweet();
            return this.View(userTweet);
        }

        /// <summary>
        /// Post new Tweet to user timeline
        /// </summary>
        /// <returns>To index action</returns>
        public ActionResult PostNewTweet(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentNullException("Content of tweet is empty");
            }
            else
            {
                TwitterAPI helper = new TwitterAPI();
                helper.UpdateUserTweet(status);
            }
            return RedirectToAction("Index");
        }

    }
}