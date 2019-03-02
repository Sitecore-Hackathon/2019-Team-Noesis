using System;
using System.Web.Mvc;
using Noesis.Feature.PageStructure.Models;
using Noesis.Foundation.SitecoreExtensions.Extensions;
using Sitecore.XA.Foundation.Mvc.Controllers;
using Sitecore.Diagnostics;
using Noesis.Foundation.Contacts;

namespace Noesis.Feature.PageStructure.Controllers
{
    public class PageSectionController : StandardController
    {
        public ActionResult PageSection()
        {
            var dataSourceItem = Rendering.DataSourceItem;

            if (dataSourceItem == null || !dataSourceItem.IsDerived(Templates.PageSection.Id))
            {
                return new EmptyResult();
            }

            var pageSectionModel = new PageSectionViewModel(dataSourceItem);

            return View(pageSectionModel);
        }

        [HttpPost]
        public JsonResult RegisterGoal(Guid goalId)
        {
            try
            {
                ContactManager.AddGoal(goalId);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);

                return Json("Error: " + ex.Message);
            }

            return Json(true);
        }
    }
}