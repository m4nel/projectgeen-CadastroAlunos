using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreinamentoWeb.Enum;

namespace TreinamentoWeb.Models
{
    public class Aluno : Entidade
    {
        public string Matricula { get; set; }
        public SituacaoEnum Situacao { get; set; }

        public string CPFaluno { get; set; }
        public string NomeMae { get; set; }

        public CursoEnum Curso { get; set; }

        public DateTime DataDeNascimento { get; set; }
        public string Observacoes { get; set; }
    }
}