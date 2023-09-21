using TreinamentoWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;

namespace TreinamentoWeb.Controllers
{
    public class HomeController : BaseController
    {
        private static List<Entidade> Entidades { get; set; }

        public HomeController()
        {
            Entidades = new List<Entidade>();

            var objeto = new Entidade
            {
                Id = 1,
                Nome = "Fulano",
                Sobrenome = "da Silva"
            };

            Entidades.Add(objeto);
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetEntidade(int id)
        {
            // Obtendo a entidade da lista usando LINQ e Lambda
            var entidade = Entidades.FirstOrDefault(x => x.Id == id);

            var entidadeHtml = RenderRazorViewToString("~/Views/Home/_FormularioEntidade.cshtml", entidade, false);

            var json = new JsonResponse
            {
                Objeto = entidadeHtml,
                Sucesso = true
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}