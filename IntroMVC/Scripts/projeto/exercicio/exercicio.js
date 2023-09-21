/*
 * Define o namespace para objetos e funções JavaScript da tela Home.
 */
Europa.Controllers.Exercicio = {};

var sucesso = false;
var copiaObj;
Europa.Controllers.Exercicio.GetAluno = function (Id) {
    $.get(Europa.Controllers.Exercicio.UrlGetAluno, { Id }, function (res) {
        $("#form-aluno").html(res.Objeto);
        console.log(res, "funcionou");
    });
};


function trocarPaginalMais() {
    contadorId++;
};
function trocarPaginalMenos() {
    contadorId = contadorId - 1;
};
function AbrirModal() {
    $("#meuModal").modal("show");
};
function fecharModal() {

    console.log("foi, fechar modal");
    $("#meuModal").modal("hide");
    if (sucesso) {
        //Europa.Controllers.Exercicio.GetAluno(copiaObj.Id);
        console.log(copiaObj.Id);

    }
};

function SalvarModal() {
    Europa.Controllers.Exercicio.CadastrarAluno();
};

var contadorId = 0;

Europa.Controllers.Exercicio.CadastrarAluno = function () {
    contadorId++ 

    var obj = {
        Id: contadorId,
        Nome: $('#nomeModal').val(), 
        Matricula: $('#matriculaModal').val(), 
        Situacao: $('#situacaoModal').val(), 
        CPFaluno: $('#cpfModal').val(), 
        NomeMae: $('#nomeMaeModal').val(), 
        Curso: $('#cursoModal').val(), 
        DataNascimento: $('#dataNascimentoModal').val(), 
        Observacoes: $('#obsModal').val(), 
    };

    //var validacao = false;
    //var valores = Object.values(obj)

    //Object.keys(valores).forEach(o => {
    //    validacao = o != "" ? true : false;
    //    if (validacao) {
    //        return alert(`Falta inserir o campo ${o}`);
    //    }
    //});

     $.ajax({
         type: 'POST', // Método HTTP POST
         url: Europa.Controllers.Exercicio.UrlCadastrarAluno, // Substitua 'NomeDoControlador' pelo nome real do seu controlador
         data: JSON.stringify(obj), // Converte o objeto para JSON
         contentType: 'application/json',

        
         success: function (result) {
             // Manipular a resposta do servidor aqui
             sucesso = true;
             copiaObj = obj;
             fecharModal();
             $("#form-aluno").html(result.Objeto);
         },

         error: function (error) {
            // Manipular erros aqui
             console.log(error);
         }
    });
};

function ExcluirAluno() {

    Europa.Controllers.Exercicio.ExcluirAluno($('#cpf').val())

};
Europa.Controllers.Exercicio.ExcluirAluno = function (cpf) {

    var stringValue = cpf;
    var URL = Europa.Controllers.Exercicio.UrlExcluirAluno + "?cpf=" + encodeURIComponent(stringValue);

    $.ajax({
        type: 'DELETE', // Método HTTP POST
        url: URL, // Substitua 'NomeDoControlador' pelo nome real do seu controlador
        
        success: function (result) {
            // Manipular a resposta do servidor aqui
            $("#form-aluno").html(result.Objeto);
        },

        error: function (error) {
            // Manipular erros aqui
            console.log(error);
        }
    });
    
}