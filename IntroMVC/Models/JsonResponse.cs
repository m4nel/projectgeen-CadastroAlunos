using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreinamentoWeb.Models
{
    public class JsonResponse
    {
        public object Objeto { get; set; }
        public bool BloquearAvanco { get; set; }
        public Boolean Sucesso { get; set; }
        public List<String> Mensagens { get; set; }
        public List<String> Campos { get; set; }

        public JsonResponse()
        {
            Mensagens = new List<string>();
            Campos = new List<string>();
            Sucesso = false;
        }
    }
}