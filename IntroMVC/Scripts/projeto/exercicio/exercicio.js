/*
 * Define o namespace para objetos e funções JavaScript da tela Home.
 */
Europa.Controllers.Exercicio = {};

var sucesso = false;
var copiaObj;
Europa.Controllers.Exercicio.GetAluno = function (Id) {
    $.get(Europa.Controllers.Exercicio.UrlGetAluno, { Id }, function (res) {
        $("#form-aluno").html(res.Objeto);
    });
};

function TrocaPagina(key) {

    Europa.Controllers.Exercicio.TrocaPagina(key, $('#cpf').val());

}

Europa.Controllers.Exercicio.TrocaPagina = function (key, cpf) {

    $.get(Europa.Controllers.Exercicio.UrlTrocaPagina, { key, cpf }, function (res) {
        $("#form-aluno").html(res.Objeto);
    });
}

function AbrirModal() {

    $("#meuModal").modal("show");
};
function AlunoExcluido() {

    $("#modalAlunoSalvoExcluido").modal("show");
    $("#AlunoModal").text(` O aluno ${$("#nome").val()} excluido com sucesso`);

}
function ModalAlunoSalvo() {

    $("#modalAlunoSalvoExcluido").modal("show");
    $("#AlunoModal").text(` O aluno ${$("#nome").val()} foi salvo com sucesso.`);
};

function fecharModal() {

    $("#meuModal").modal("hide");

}

function SalvarModal() {
    Europa.Controllers.Exercicio.CadastrarAluno();
};


Europa.Controllers.Exercicio.CadastrarAluno = function () {

    var obj = {
        Nome: $('#nomeModal').val(), 
        Matricula: $('#matriculaModal').val(), 
        Situacao: $('#situacaoModal').val(), 
        CPFaluno: $('#cpfModal').val(), 
        NomeMae: $('#nomeMaeModal').val(), 
        Curso: $('#cursoModal').val(),
        DataNascimento: $('#dataNascimentoModal').val(), 
        Observacoes: $('#obsModal').val(), 
    };

    if (obj.Nome == "" || obj.Matricula == "" || obj.Situacao == "" || obj.CPFaluno == "" || obj.NomeMae == "" || obj.Curso == "" || obj.DataNascimento == "" || obj.Observacoes == "") {
        let messageFaltaAlgo = ""

        if (obj.Nome == "") {
        messageFaltaAlgo += ` Falta adicionar um nome \n `
        }

        if (obj.Matricula == "") {

        messageFaltaAlgo += ` Falta adicionar uma Matricula \n `
        }

        if (obj.Situacao == "") {

        messageFaltaAlgo += ` Falta adicionar uma Situação \n `
        }

        if (obj.CPFaluno == "") {

        messageFaltaAlgo += ` Falta adicionar um CPF \n `
        }

        if (obj.NomeMae == "") {

        messageFaltaAlgo += ` Falta adicionar nome da Mãe \n `
        }

        if (obj.Curso == "") {

        messageFaltaAlgo += ` Falta adicionar um Curso \n `
        }

        if (obj.DataNascimento == "") {

        messageFaltaAlgo += ` Falta adicionar uma Data De nascimento \n `
        }
        if (obj.Observacoes == "") {

        messageFaltaAlgo += ` Falta adicionar uma Observação \n `
        }
        alert(messageFaltaAlgo);

    }
    
    else {

        $.ajax({
         type: 'POST', 
         url: Europa.Controllers.Exercicio.UrlCadastrarAluno, 
         data: JSON.stringify(obj),
         contentType: 'application/json',

         success: function (result) {
             
             sucesso = true;
             copiaObj = obj;
             
             fecharModal();
             $("#form-aluno").html(result.Objeto);
             ModalAlunoSalvo();
         },

         error: function (error) {

             alert(error, "Não foi possivel Cadastrar")
         }
        });
    }
};

function ExcluirAluno() {

    Europa.Controllers.Exercicio.ExcluirAluno($('#cpf').val())
    
};
Europa.Controllers.Exercicio.ExcluirAluno = function (cpf) {

    var stringValue = cpf;
    var URL = Europa.Controllers.Exercicio.UrlExcluirAluno + "?cpf=" + encodeURIComponent(stringValue);

    $.ajax({
        type: 'POST', 
        url: URL,
        
        success: function (result) {

            $("#form-aluno").html(result.Objeto);
            AlunoExcluido()
        },

        error: function (error) {
            alert("Tentou Excluir sem ter aluno");
        }
    });
}

