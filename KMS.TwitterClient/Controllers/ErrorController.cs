using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMS.TwitterClient.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// return error view associate with global exception
        /// </summary>
        public ActionResult ShowError()
        {
            return View("~/Views/Error/Error.cshtml");
        }
    }
}