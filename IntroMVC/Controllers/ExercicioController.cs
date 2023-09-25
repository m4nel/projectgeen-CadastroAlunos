
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
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
        private static int IncrementaId = 1;

        public ExercicioController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TrocaPagina(string key, string cpf)
        {
            var json = new JsonResponse();

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(cpf))
            {
                json.Sucesso = false;
                json.Mensagens.Add("Aluno Não foi encontrado");
                Response.StatusCode = 400;
                return Json(json, JsonRequestBehavior.AllowGet);
            }

            var alunoAtualId = Alunos.FirstOrDefault(a => a.CPFaluno == cpf).Id;

            if (key == "anterior")
            {
                var alunoExiste = Alunos.Exists(a => a.Id.Equals(alunoAtualId - 1));
                var alunoIdRenderizar = alunoExiste ? alunoAtualId - 1 : Alunos.LastOrDefault().Id;
                var alunoParaRenderizar = Alunos.FirstOrDefault(a => a.Id == alunoIdRenderizar);
                var alunoHtml = RenderRazorViewToString("~/Views/Exercicio/_FormularioAluno.cshtml", alunoParaRenderizar, false);

                json.Objeto = alunoHtml;
                json.Sucesso = true;
                json.Mensagens.Add("Pagina de aluno anterior!");

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            var proximoAlunoExiste = Alunos.Exists(a => a.Id.Equals(alunoAtualId + 1));
            var proximoAlunoIdRenderizar = proximoAlunoExiste ? alunoAtualId + 1 : Alunos[0].Id;
            var proximoAlunoParaRenderizar = Alunos.FirstOrDefault(a => a.Id == proximoAlunoIdRenderizar);
            var proximoAlunoHtml = RenderRazorViewToString("~/Views/Exercicio/_FormularioAluno.cshtml", proximoAlunoParaRenderizar, false);

            json.Objeto = proximoAlunoHtml;
            json.Sucesso = true;
            json.Mensagens.Add("Pagina de aluno posterior!");

            return Json(json, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetAluno(int id)
        {
         
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

            aluno.Id = IncrementaId++;
            Alunos.Add(aluno);
            
            var alunoHtml = RenderRazorViewToString("~/Views/Exercicio/_FormularioAluno.cshtml", aluno, false);

            json.Objeto = alunoHtml;
            json.Sucesso = true;
            json.Mensagens.Add("Aluno Inserido com sucesso!");

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExcluirAluno(string cpf)
        {
            var json = new JsonResponse();
            var alunoParaExcluir = Alunos.FirstOrDefault(a => a.CPFaluno == cpf);
            var alunoParaRenderizar = new Aluno();

            if (alunoParaExcluir is null)
            {
                json.Sucesso = false;
                json.Mensagens.Add("Aluno Não foi encontrado");
                Response.StatusCode = 400;
                return Json(json, JsonRequestBehavior.AllowGet);
            }

            if (Alunos.Count > 1)
            {
                var existeAluno = Alunos.Exists(a => a.Id == alunoParaExcluir.Id + 1);
                var idAlunoParaRenderizar = existeAluno ? alunoParaExcluir.Id + 1 : Alunos[0].Id;
                alunoParaRenderizar = Alunos.FirstOrDefault(a => a.Id == idAlunoParaRenderizar);
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