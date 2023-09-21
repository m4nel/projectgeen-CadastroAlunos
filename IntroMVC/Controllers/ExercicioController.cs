
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreinamentoWeb.Models;

namespace TreinamentoWeb.Controllers
{
    public class ExercicioController : BaseController
    {
        private static List<Aluno> Alunos = new List<Aluno>();

        public ExercicioController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAluno(int id)
        {
            // Obtendo a entidade da lista usando LINQ e Lambda
            var aluno = Alunos.FirstOrDefault(x => x.Id == id);

            var alunoHtml = RenderRazorViewToString("~/Views/Exercicio/_FormularioAluno.cshtml", aluno, false);

            var json = new JsonResponse
            {
                Objeto = alunoHtml,
                Sucesso = true
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CadastrarAluno(Aluno aluno)
        {
            var json = new JsonResponse();

            if (aluno is null)
            {
                json.Sucesso = false;
                json.Mensagens.Add("Aluno Invalido não cadastrado");
                return Json(json, JsonRequestBehavior.AllowGet);
            }

            var alunoHtml = RenderRazorViewToString("~/Views/Exercicio/_FormularioAluno.cshtml", aluno, false);

            Alunos.Add(aluno);

            json.Objeto = alunoHtml;
            json.Sucesso = true;
            json.Mensagens.Add("Aluno Inserido com sucesso!");

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult ExcluirAluno(string cpf)
        {
            var json = new JsonResponse();
            var alunoParaExcluir = Alunos.FirstOrDefault(a => a.CPFaluno == cpf);
            var existeAluno = Alunos.Exists(a => a.Id == alunoParaExcluir.Id + 1);
            var idAlunoParaRenderizar = existeAluno ? alunoParaExcluir.Id + 1 : Alunos[0].Id;
            var alunoParaRenderizar = Alunos.FirstOrDefault(a => a.Id == idAlunoParaRenderizar);


            if (alunoParaExcluir is null)
            {
                json.Sucesso = false;
                json.Mensagens.Add("Aluno Não foi encontrado");
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            
            var alunoHtml = RenderRazorViewToString("~/Views/Exercicio/_FormularioAluno.cshtml", alunoParaRenderizar, false);
            
            Alunos.Remove(alunoParaExcluir);
            

            json.Objeto = alunoHtml;
            json.Sucesso = true;
            json.Mensagens.Add("Aluno excluído com sucesso!");


            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}