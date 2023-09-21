using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreinamentoWeb.Models;

namespace TreinamentoWeb.Controllers
{
    public abstract class BaseController : Controller
    {

        protected BaseController()
        {
        }


        public ActionResult JsonResponseWrapper(object result, bool allowGet = true)
        {
            var response = new JsonResponse
            {
                Sucesso = true,
                Objeto = result
            };
            return Json(response, allowGet ? JsonRequestBehavior.DenyGet : JsonRequestBehavior.AllowGet);
        }

        public string RenderRazorViewToString(string viewName, object model, bool detailMode)
        {
            ViewData.Model = model;


            if (!(model == null) && detailMode)
            {
                ViewData.Model = model;
                ViewData.Add("detailMode", true);
            }

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                var aux = sw.GetStringBuilder().ToString();
                return aux;
            }
        }
    }
}