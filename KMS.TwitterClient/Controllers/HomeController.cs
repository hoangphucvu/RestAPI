using KMS.TwitterClient.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMS.TwitterClient.Controllers
{

    /// <summary>
    /// Get user tweet when running
    /// </summary>
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
        /// 
        /// </summary>
        /// <returns></returns>
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