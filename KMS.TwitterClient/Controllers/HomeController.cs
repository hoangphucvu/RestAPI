using KMS.TwitterClient.Helper;
using log4net;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KMS.TwitterClient.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController).Name);
        private object userTweet;

        /// <summary>
        /// Get user tweet when running
        /// </summary>
        /// <returns>User and twitter Model</returns>
        public ActionResult Index()
        {
            try
            {
                TwitterAPI helper = new TwitterAPI();
                userTweet = helper.GetTweet();
            }
            catch (Exception ex)
            {
                Log.Error("Error: " + ex);
                Log.Error(Environment.NewLine);
                ViewBag.Error = "There something went wrong with server <br> Please try again later";
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(userTweet);
        }

        public ActionResult PostNewTweet(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                Log.Error("Status of tweet is empty");
                ViewBag.Error = "Status of tweet is empty";
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                try
                {
                    TwitterAPI helper = new TwitterAPI();
                    helper.UpdateUserTweet(status);
                }
                catch (Exception ex)
                {
                    Log.Error("Error: " + ex);
                    Log.Error(Environment.NewLine);
                    ViewBag.Error = "Error when posting user status.Please try again";
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            return RedirectToAction("Index");
        }
    }
}