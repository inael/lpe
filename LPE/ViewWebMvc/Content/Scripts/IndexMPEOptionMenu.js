var indexPage = 0;
var sessionUser = '@Session["userKey"]';

$(document).ready(function () {
    $.history.init(HistoryContentUpdate, { unescape: true });
    $.ajaxSetup({
        error: trataAjaxError
    });

    //set user in cookie
    var dt = new Date();
    dt.setHours(dt.getHours() + 3);
    document.cookie = "username=" + escape($("#selectLogin").attr('value')) +
                    ";expires=" + dt.toGMTString();

    $('#conteudo').html('<img src="/LPE/Content/imgs/ajax-loader.gif" />');

    $.ajax({
        url: "Home/OptionMenu",
        type: "Post",
        data: {},
        success: function (data) {
            $("#conteudo").html(data);
            var entidade = eval("(" + data + ")");
            if (entidade.isValid == "false") {
                alert("Login ou senha inválidos");
            }
            else if (entidade.isValid == "true") {
                $('#userName').show();
                $("#userName").prepend('Olá, <b>' + entidade.userName + '</b>.<br/>Seja bem vindo!<br/>');
            }

            initializeOptionMenu('#conteudo');
            //UpdateContainer('Home', 'LogOn', '#UserInfo', null);

            $('#logo').html('<img src="/LPE/Content/imgs/logo.png" width="262px" height="105px" />');
            $('#topLogon').css({ "height": "153px", "background-image": "url(/LPE/Content/imgs/bar-top-content.png)" });
            $('#logOn').hide();

            //initializeOptionMenu('#Conteudo');
        }
    });
});