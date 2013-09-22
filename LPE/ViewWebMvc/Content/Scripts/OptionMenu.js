$(document).ready(function () {
    
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

});

function putInEditModeFormPessoaQuest(doclick) {
    $('#updateButton').unbind('click').click(doclick);
    $('#_div').show();
    $('#browser').hide();
    adjustFieldSets("#addForm");
}

function callUpdatePessoaQuest(idQuest, idUser, idPessoa) {
    setupUpdatePessoaQuest(idPessoa);
    putInEditModeFormPessoaQuest(function () { updatePessoaQuest(idQuest, idUser, idPessoa); });
}

function setupUpdatePessoaQuest(id) {
    $.ajax({
        type: "post",
        url: "PessoaManager/Search",
        traditional: true,
        data: { 'id': id },
        success: function (data, text) {
            var entidade = eval("(" + data + ")");
            var data = new Date(parseInt(entidade.DtNascimentoPessoa.substr(6), 10));
            //var formatData = data.getDate() + "/" + data.getMonth() + "/" + data.getFullYear();
            var estadoCivil = entidade.IdEstadoCivil;
            var endereco = entidade.idEndereco;
            var escolaridade = entidade.IdEscolaridade;
            var profissao = entidade.IdProfissao;

            //entidade.DtNascimentoPessoa = formatData;

            fillFormWithObject(entidade);

            if (entidade.SexoPessoa == "M") {
                $('#masculino').attr('checked', true);
            } else {
                $('#feminino').attr('checked', true);
            }

            $('#Dia').attr('value', data.getDate());
            $('#Mes').attr('value', data.getMonth()+1);
            $('#Ano').attr('value', data.getFullYear());

            //for (var i = 0; i < estados.length; i++)
            $('#EstadoCivil').trigger('addItem', [{ 'title': estadoCivil.Descricao, 'value': estadoCivil.IdEstadoCivil}]);
            $('#Endereco').trigger('addItem', [{ 'title': endereco.Logradouro, 'value': endereco.IdEndereco}]);
            $('#Escolaridade').trigger('addItem', [{ 'title': escolaridade.DescricaoEscolaridade, 'value': escolaridade.IdEscolaridade}]);
            $('#Profissao').trigger('addItem', [{ 'title': profissao.Descricao, 'value': profissao.IdCBOSinonimo}]);
        }
    });
}

function updatePessoaQuest(idQuest, idUser, idPessoa) {
    //checkUnadded();
    var formdata = clientValidation();
    formdata['id'] = idPessoa;

    if ($('#masculino').is(':checked')) {
        formdata['SexoPessoa'] = "M";
    } else {
        formdata['SexoPessoa'] = "F";
    };

    if (formdata != -1) {
        AjaxWaitRequest("#updateButton", {
            url: "PessoaManager/Update",
            data: formdata,
            success: function (data, text) {
                if (data == "True") {
                    alert("O registro foi atualizado com sucesso. Aguarde...");
                    $('#conteudo').html('<img src="Content/imgs/ajax-loader.gif" />');
                    abreQuestionario(idQuest, idUser);
                }
            }
        });
    }
}

function abreQuestionario(id,user){
    $.ajax({
            type: "post",
            url: "Resposta/FormResposta",
            data: { 
                    idQuest: id,
                    idUser : user
                  },
            traditional: true,
            success: function (data, text) {
                $("#conteudo").html(data);
            }
    });
}