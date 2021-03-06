﻿using KMS.TwitterClient.API;
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace KMS.TwitterClient.Controllers
{
    public class HomeController : Controller
    {
        private ITwitterServices twitterServices;

        public HomeController(ITwitterServices twitterServices)
        {
            this.twitterServices = twitterServices;
        }

        /// <summary>
        /// Get user tweet when running
        /// </summary>
        /// <returns>User timeline</returns>
        public ActionResult Index()
        {
            var userTweet = twitterServices.GetTweet();
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
            twitterServices.UpdateUserTweet(status);
            return RedirectToAction("Index");
        }
    }
}