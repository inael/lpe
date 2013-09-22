function initializeOptionMenu(local) {

    UpdateContainer('Home', 'OptionMenu', local,
                function () {
                    setInitIndexPage(indexPage);

                    $('.treeMenuOption').click(function () {
                        var action = $(this).attr('action');
                        var name = $(this).attr('name');
                        var controler = $(this).attr('controler');
                        var conteiner = "conteudo";

                        var hash = [controler, action, name.replace(/[ -]/g, "."), conteiner, ++indexPage];

                        $.history.load(hash.join("-"));
                        document.title = "LPE - " + name

                        $('.selectedMenuItem').removeClass("selectedMenuItem")
                        .css("background-color", "");

                        $(this).addClass("selectedMenuItem");

                        setFocusPage();

                        return false;
                    });
                }
            );
}

function deleteAllCookies() {
    var cookies = document.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}