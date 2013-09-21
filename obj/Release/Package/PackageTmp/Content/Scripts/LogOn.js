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
        dt.setHours(dt.getHours() + 1);
        document.cookie = "username=" + escape($("#selectLogin").attr('value')) +
                ";expires=" + dt.toGMTString();

        $('#conteudo').html('<img src="/Content/imgs/ajax-loader.gif" />');

        AjaxWaitRequest('#btnLogin', {
            url: "Home/CheckLogin",
            data: {
                login: $("#selectLogin").attr('value'),
                password: $("#selectSenha").attr('value')
            },
            success: function (data, text) {
                if (data == "False") {
                    alert("Login ou senha inválidos");
                }
                else if (data = "True") {
                    $("#userName").html($("#selectLogin").attr('value'));
                }
                initializeOptionMenu('#conteudo');
                UpdateContainer('Home', 'LogOn', '#logOn', null)
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