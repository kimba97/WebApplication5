using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class HomeController: SurfaceController
    {
        public ActionResult RenderBase()
        {
            return PartialView("~/Views/Partials/Base.cshtml");
        }
    }
}