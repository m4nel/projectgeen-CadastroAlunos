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
function ModalAlunoSalvo() {

    $("#modalAlunoSalvo").modal("show");
    $("#AlunoModal").text(` O aluno ${$("#nome").val()} foi salvo com sucesso.`);
};

// modal de aluno salvo receberia uma mensagem ne e agente passaria ela por parametro e rendeenzaria junto com esse nome.val e desce só pra eu te mostrar
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

    //var validacao = false;
    //var valores = Object.values(obj)

    //Object.keys(valores).forEach(o => {
    //    validacao = o != "" ? true : false;
    //    if (validacao) {
    //        return alert(`Falta inserir o campo ${o}`);
    //    }
    //});

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

             //Tem q ter algo aq
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
        type: 'POST', 
        url: URL,
        
        success: function (result) {
            // Manipular a resposta do servidor aqui
            $("#form-aluno").html(result.Objeto);
        },

        error: function (error) {
            alert("Tentou Excluir sem ter aluno");
        }
    });
}