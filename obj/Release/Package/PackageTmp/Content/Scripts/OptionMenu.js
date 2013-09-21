$(document).ready(function () {
    
    /*$('.browser .menufolder').click(function () {
    $(this).next().toggle();
    return false;
    }).next().hide();*/

    $('.treeMenuOption').mouseout(function () {
        if (!$(this).hasClass("selectedMenuItem")) {
            $(this).css("background-color", "white");
            $(this).css("cursor", "default");
        }
    }).mouseover(function () {
        if (!$(this).hasClass("selectedMenuItem")) {
            $(this).css("background-color", "#EFEDE8");
            $(this).css("cursor", "pointer");
        }
    });

    /*$('#lnkFrmResposta').click(function () {
        var id = $("#idQuest").attr('value');
        $.ajax({
            type: "post",
            url: "Resposta/FormResposta",
            data: { idQuest: id },
            traditional: true,
            success: function (data, text) {
                $("#conteudo").html(data);
            }
        });
    });*/
});

function abreQuestionario(id){
    $.ajax({
            type: "post",
            url: "Resposta/FormResposta",
            data: { idQuest: id },
            traditional: true,
            success: function (data, text) {
                $("#conteudo").html(data);
            }
    });
}