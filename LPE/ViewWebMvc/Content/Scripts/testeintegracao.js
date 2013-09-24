$(document).ready(function () {
    $("#button").click(function () {

        //primeiro cria uma sessao para o ususario
        //depois chama a home, se tiver sessao ele exibe os menus, caso contrario chama o login.
        $.post('http://localhost:62341/LPE/Home/LoginMPE',
               { Email: "ricardo@gmail.com", PasswordHash: "yYHZtvpkTfazcgfmBshi6057f20f883e", Nome: "ricardo", UrlOrigin: "http://www.google.com.br" },
               function (pagina) {
                            //window.location.href = '/LPE/Home';
                        
                        var script = document.createElement('script');
                        script.type = 'text/javascript';
                        script.src = "/LPE/Content/Scripts/jquery-1.6.1.js";
                        $("head").append(script)
            
                        script.src = "/LPE/Content/Scripts/jquery-ui-1.8.13.custom.min.js";
                        $("head").append(script)
            
                        script.src = "/LPE/Content/Scripts/testeintegracao.js";
                        $("head").append(script)

                        $("body").html(pagina);

                        $.history.init(HistoryContentUpdate, { unescape: true });
                        $.ajaxSetup({
                            error: trataAjaxError
                        });
                        initializeOptionMenu('#logOn');
            }
         );

        });
    });

    
        