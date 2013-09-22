$(document).ready(function () {
    $('#btnLogin').click(function () {
        loginClick();
    });
    $('#resetPassword').click(function () {
        var login = $("#selectLogin").val();
        if (login != "") {
            if (!validateEmail(login) && login != "administrador") {
                alert("E-mail de login inválido.");
            }
            else if (login == "administrador") {
                alert("O login de administrador não possui e-mail.");
            }
            else {
                sendEmail();
            }
        }
    });

    //get user from cookie
    var re = new RegExp('[; ]username=([^\\s;]*)');
    var sMatch = (' ' + document.cookie).match(re);
    if (sMatch) $("#selectLogin").attr('value', unescape(sMatch[1]));
    
    //$('#loginForm')
    //adjustFieldSets('#loginForm');
});

function loginClick() {
    var login;
    var senha;

    if ($("#selectLogin").attr('value') != "")
        login = $("#selectLogin").val();

    if ($("#selectSenha").attr('value') != "")
        senha = $("#selectSenha").val();

    if (login == null || login.length == 0) {
        alert("É necessário preencher o login");
    }
    else if (senha == null || senha.length == 0) {
        alert("É necessário preencher a senha");
    }
    else if (!validateEmail(login) && login != "administrador") {
        alert("E-mail de login inválido");
    }
    else {
        //set user in cookie
        var dt = new Date();
        dt.setHours(dt.getHours() + 3);
        document.cookie = "username=" + escape($("#selectLogin").attr('value')) +
                ";expires=" + dt.toGMTString();

        $('#conteudo').html('<img src="/LPE/Content/imgs/ajax-loader.gif" />');

        AjaxWaitRequest('#btnLogin', {
            url: "Home/CheckLogin",
            data: {
                login: login,//$("#selectLogin").attr('value'),
                password: senha//$("#selectSenha").attr('value')
            },
            success: function (data, text) {
                var entidade = eval("(" + data + ")");
                if (entidade.isValid == "false") {
                    alert("Login ou senha inválidos");
                }
                else if (entidade.isValid == "true") {
                    $('#userName').show();
                    $("#userName").prepend('Olá, <b>'+entidade.userName+'</b>.<br/>Seja bem vindo!<br/>');
                }

                initializeOptionMenu('#conteudo');
                //UpdateContainer('Home', 'LogOn', '#UserInfo', null);

                $('#logo').html('<img src="/LPE/Content/imgs/logo.png" width="262px" height="105px" />');
                $('#topLogon').css({ "height": "153px", "background-image": "url(/LPE/Content/imgs/bar-top-content.png)" });
                $('#logOn').hide();
            }
        });
    }
}

$('#btnLogout').click(function () {
    logoutHome();
});

function sendEmail() {
    AjaxWaitRequest('#btnLogin', {
        url: "Home/SendLink",
        data: {
            login: $("#selectLogin").attr('value')
        },
        success: function (data, text) {
            alert("Uma solicitação de redefinição de senha foi enviada ao e-mail " + $("#selectLogin").attr('value'));
        }
    });
}