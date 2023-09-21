/*
 * Define o namespace para objetos e funções JavaScript da tela Home.
 */
Europa.Controllers.Home = {};


$(function () {
    // As funções abaixo são executadas ao carregar a página.
    Europa.Controllers.Home.GetEntidade();
});


Europa.Controllers.Home.GetEntidade = function () {
    $.get(Europa.Controllers.Home.UrlGetEntidade, { id: 1 }, function (res) {
        $("#form-entidade").html(res.Objeto);
    });
};