$(document).ready(function () {
    $("#button").click(function () {

        //primeiro cria uma sessao para o ususario
        //depois chama a home, se tiver sessao ele exibe os menus, caso contrario chama o login.
        $.post('http://localhost:62341/LPE/Home/LoginMPE', { Email: "ricardo@gmail.com", PasswordHash: "yYHZtvpkTfazcgfmBshi6057f20f883e", Nome: "ricardo", UrlOrigin:"http://www.google.com.br"  }, function () {
            window.location.href = '/LPE/Home';
        });
        
    });
});
        