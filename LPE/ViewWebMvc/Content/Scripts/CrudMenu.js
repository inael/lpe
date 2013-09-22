function goToPage(pageNumber) {
    grid = $("#Grid");
    grid.setGridParam({ page: pageNumber });
    grid.trigger("reloadGrid");
}

function loadGrid(Controler, Action, afterSucess) {
    $.ajax({
        type: "post",
        url: Controler+"/"+Action,
        traditional: true,
        data: {},
        success: function (data, text) {
            var list = eval(data);
            $("#Grid").jqGrid("clearGridData", true);
            $("#Grid").setGridParam({ data: list, rowNum: list.length });
            $("#Grid").trigger("reloadGrid");

            if (afterSucess != null)
                afterSucess();
        }
    });
}

function putInEditMode(doclick) {
    $('#updateButton').unbind('click').click(doclick);
    $('#_div').show();
    $('#Base').hide();
    adjustFieldSets("#addForm");
}

function callCreate(Controller, Action) {
    var page = $('#Grid').getGridParam("page");
    setCurrentPage(page);
    clearForm('#PartialContent', function (element) {
        var remElem = $(element).val();
        if (remElem) {
            for (var i = 0; i < remElem.length; i++)
                $(element).trigger('removeItem', [{ "value": remElem[i]}]);
        }
    });
    putInEditMode(function () { submitForm(Controller,Action); });
}

function callUpdate(Controller, Action, selId, FunctionReturn, ParamReturn) {
    var selected = jQuery("#Grid").jqGrid('getGridParam', 'selrow');
    if (selected != null) {
        var id = $('#Grid').jqGrid('getCell', selected, selId);
        var page = $('#Grid').getGridParam("page");
        clearForm('#PartialContent', function (element) {
            var remElem = $(element).val();
            if (remElem) {
                for (var i = 0; i < remElem.length; i++)
                    $(element).trigger('removeItem', [{ "value": remElem[i]}]);
            }
        });
        setupUpdateForm(Controller, 'Search', id);
        setCurrentPage(page);
        putInEditMode(function () { updateForm(Controller, Action, id, FunctionReturn, ParamReturn); });
    }
    else
        alert("Por favor selecione um registro para ser atualizado.");
}

function callDelete(Controller, Action, selId) {
    var selected = jQuery("#Grid").jqGrid('getGridParam', 'selrow');
    if (selected != null) {
        var confirmation = confirm("Tem certeza que deseja apagar o registro selecionada?");
        if (confirmation == true) {
            var id = $('#Grid').jqGrid('getCell', selected, selId);
            $.ajax({
                type: "post", url: Controller+"/"+Action,
                data: { "id": id },
                success: function (data, text) {
                    if (data == "True") {
                        jQuery("#Grid").jqGrid('delRowData', selected);
                        alert("O registro foi apagado.");
                    }
                    else alert("Ocorreu um erro ao tentar apagar o registro.");
                }
            });
        }
    }
    else
        alert("Por favor selecione um registro para ser deletado!");
}

function createCitiesGrid() {
    $("#CitiesGrid").jqGrid({
        colNames: ['Código', 'Nome do Município', 'Estado'],
        colModel: [
            { name: 'codi_mnr', index: 'codi_mnr', width: 100, sorttype: "int" },
            { name: 'nome_mnr', index: 'nome_mnr', width: 200, sorttype: "text" },
            { name: 'sigl_est', index: 'sigl_est', width: 200, sorttype: "text" }
   	              ],
        grouping: true,
        groupingView: {
            groupField: ['sigl_est'],
            groupColumnShow: [false]
        },
        rowNum: 1,
        datatype: "json",
        height: 'auto',
        caption: "Municípios da Região",
        sortorder: "asc",
        sortable: true,
        viewrecords: true,
        datatype: "local",
        multiselect: true
    });
}

function addCities() {
    var rows = $('#CitiesGrid').jqGrid('getRowData');

    $("#City").find("option").each(function () {
        var htmlContent = $(this).html().toString().split('-');

        var cityName = htmlContent[0];
        var stateName = htmlContent[1].replace(" ", "");
        var id = $(this).val();

        var isDuplicated = false;

        for (i = 0; i < rows.length; ++i)
            if (rows[i].codi_mnr == id) {
                isDuplicated = true;
                break;
            }

        if (!isDuplicated)
            rows[rows.length] = { codi_mnr: id, nome_mnr: cityName, sigl_est: stateName };

        $('#City').trigger('removeItem', [{ "value": id}]);
    });

    $("#CitiesGrid").jqGrid("clearGridData", true);
    $("#CitiesGrid").setGridParam({ data: rows, rowNum: rows.length });
    $("#CitiesGrid").trigger("reloadGrid");
}

function addStates() {
    var states = [];

    $("#selectState").find("option").each(function () {
        var id = $(this).val();
        states.push(id);
        $('#selectState').trigger('removeItem', [{ "value": id}]);
    });

    var rows = $('#CitiesGrid').jqGrid('getRowData');
    var cities = [];
    for (i = 0; i < rows.length; ++i)
        cities.push(rows[i].codi_mnr);

    if (states.length != 0)
        $.ajax({
            type: "post",
            async: false,
            url: "RegionManager/GetCitiesFromStates",
            traditional: true,
            data: { 'state': states, 'city': cities },
            success: function (data, text) {
                var list = eval("(" + data + ")");

                $("#CitiesGrid").jqGrid("clearGridData", true);
                $("#CitiesGrid").setGridParam({ data: list, rowNum: list.length });
                $("#CitiesGrid").trigger("reloadGrid");
            }
        });


}

function removeCities() {
    var checked = $('#CitiesGrid').getGridParam('selarrrow');
    var staticValues = checked;

    if (checked != null) {
        for (i = 0; checked.length != 0; )
            jQuery("#CitiesGrid").jqGrid('delRowData', checked[0]);

        var rows = $('#CitiesGrid').jqGrid('getRowData');
        $("#CitiesGrid").jqGrid("clearGridData", true);
        $("#CitiesGrid").setGridParam({ data: rows, rowNum: rows.length });
        $("#CitiesGrid").trigger("reloadGrid");
    }
    else
        alert("Por favor selecione um registro para ser deletado!");
}

function clientValidation() {
    var valido = $("#addForm").validate().form();
    if (valido) {
        //var city = [];

        //var formdata = generatePostData({});
        var formdata = saveDataForHistory();

        //var gridCities = $('#CitiesGrid').jqGrid('getRowData');

        /*if (gridCities.length > 0) {
            for (i = 0; i < gridCities.length; ++i)
                city.push(gridCities[i].codi_mnr);

            formdata['Cities[]'] = city.join(',');
        }
        else
            formdata['Cities[]'] = null;*/

        return formdata;

    }
    else
        return -1;
}

function callBackButton(Controller, Action) {
    $('#Base').show();
    $('#_div').hide();
    loadGrid(Controller, Action, function () { goToPage(currentPage); });
}

function callBackMenu(Controller, Action) {
    $.ajax({
        type: "post",
        url: Controller + "/" + Action,
        traditional: true,
        data: { },
        success: function (data, text) {
            var entidade = eval("(" + data + ")");

            fillFormWithObject(entidade);

        }
    });
}

function submitForm(Controller,Action) {
    //checkUnadded();
    var formdata = clientValidation();

    if (formdata != -1) {
        AjaxWaitRequest("#updateButton", {
            url: Controller+"/"+Action,
            data: formdata,
            success: function (data, text) {
                if (data == "True") {
                    alert("O registro foi adicionado com sucesso");
                    loadGrid(Controller,'List', function () { goToPage(currentPage); });
                    callBackButton(Controller, 'List');
                } else
                    alert(data);
            }
        });
    }

}

function updateForm(Controller, Action, id, FunctionReturn, ParamReturn) {
    //checkUnadded();
    var formdata = clientValidation();
    formdata['id'] = id;

    if (formdata != -1) {
        AjaxWaitRequest("#updateButton", {
            url: Controller + "/" + Action,
            data: formdata,
            success: function (data, text) {
                alert("O registro foi atualizado com sucesso.");
                //loadGrid(Controller, 'List', function () { goToPage(currentPage); });
                FunctionReturn(ParamReturn);
            }
        });
    }
}

function setCurrentPage(page) {
    currentPage = page;
}

function checkUnadded() {
    var citiesUnadded = 0;
    var statesUnadded = 0;
    var message = "";

    $("#selectState").find("option").each(function () {
        ++statesUnadded;
    });

    $("#City").find("option").each(function () {
        ++citiesUnadded
    });

    if (citiesUnadded == 1) message += "Existe um município não adicionada a grade \n";
    else if (citiesUnadded > 1) message += "Existem municípios não adicionados a grade \n";

    if (statesUnadded == 1) message += "Existe um estado não adicionada a grade \n";
    else if (citiesUnadded > 1) message += "Existem estados não adicionados a grade \n";

    if (message != "") {
        var confirmation = confirm(message + "Deseja adicionar os elementos pendentes?");

        if (confirmation) {
            addCities();
            addStates();
        }

    }
}