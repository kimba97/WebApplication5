using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using WebApplication5.Models;
using Umbraco.Core.Models;

namespace WebApplication5.Controllers
{
    public class PartialBaseController:SurfaceController
    {
        

        public ActionResult RenderFooter()
        {
            return PartialView("~/Views/Partials/Footer.cshtml");
        }

        public ActionResult RenderHeader()
        {
            //return PartialView("~/Views/Partials/Header.cshtml");
            List<NavigationList> nav = GetNavigationModel();
            return PartialView("~/Views/Partials/Header.cshtml",nav);
        }

        // Step1: create function to get the Navigation model from database
        // Step2: write the code to get the sub navigation
        // Step3: UPdate the code for RenderHeader

        public List<NavigationList> GetNavigationModel()
        {
            int pageId = int.Parse(CurrentPage.Path.Split(',')[1]); //posicion de la pagin en el path
            IPublishedContent pageInfo = Umbraco.Content(pageId);
            var nav = new List<NavigationList>
            {
                new NavigationList(new NavigationLinkInfo(pageInfo.Url, pageInfo.Name))
            };
            nav.AddRange(GetSubNavigationList(pageInfo));
           
            return nav;
        }
        public List<NavigationList> GetSubNavigationList(dynamic page)
        {
            List<NavigationList> navList = null;
            var subPages = page.Children.Where("Visible");
            if (subPages !=null && subPages.Any())
            {
                navList = new List<NavigationList>();
                foreach (var subPage in subPages)
                {
                    var listItem = new NavigationList(new NavigationLinkInfo(subPage.Url, subPage.Name))
                    {
                        NavItems = GetSubNavigationList(subPage)
                    };
                navList.Add(listItem);
                }
            }
            return navList;
        }
    }
}