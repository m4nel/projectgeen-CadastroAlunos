using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreinamentoWeb.Enum;

namespace TreinamentoWeb.Models
{
    public class Aluno
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Matricula { get; set; }
        public SituacaoTipo Situacao { get; set; }

        public string CPFaluno { get; set; }
        public string NomeMae { get; set; }

        public CursoTipo Curso { get; set; }

        public DateTime DataNascimento { get; set; }
        public string Observacoes { get; set; }
        
    }
}