var origContent = "";
var ourAlertTid = null;
var origalert = window.alert;
var contentForm = [];
var oldURL = "";
var initIndexPage = 0;

function setInitIndexPage(value) {
    initIndexPage = value;
}

var defaultalert = function (msg) {
    origalert(unescape(msg));
};

var jqGridMoney = { decimalSeparator: ",", thousandsSeparator: ".", decimalPlaces: 2, prefix: '' };
var jqGridPerc = { decimalSeparator: ",", thousandsSeparator: ".", decimalPlaces: 2, prefix: '', suffix: '%' };

$(document).ready(function () {
    window.alert = ouralert;
    /*
    jQuery.validator.addMethod('cnpj', function (value, element) {
        if (ValidarCNPJ(element) == -1)
            return false;
        return true;
    }); */
});

Date.prototype.toMSJSON = function () {
    function pad(n) {
        return n < 10 ? '0' + n : n
    }
    return this.getUTCFullYear() + '-'
    + pad(this.getUTCMonth() + 1) + '-'
    + pad(this.getUTCDate()) + 'T'
    + pad(this.getUTCHours()) + ':'
    + pad(this.getUTCMinutes()) + ':'
    + pad(this.getUTCSeconds()) + 'Z';
};

Date.prototype.dtformat = function (format) //author: meizz
{
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }

    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
      RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

function setFocusPage() {
    $('html, body').animate({ scrollTop: 0 }, 'fast');        
}


function permissionServiceReplyMessage(result) {
    var list = result.Result;
    var notAllowedCities = result.CitiesDenied;
    var notAllowedDistributors = result.DistributorsDenied;

    var message = ""
    if (notAllowedCities.length != 0) {
        message += "Você não tem acesso as respectivas cidades: ";
        message += notAllowedCities[0].nome_mnr;
        for (var i = 1; i < notAllowedCities.length; ++i)
            message += ", " + notAllowedCities[i].nome_mnr;
        message += "</br>"
    }

    if (notAllowedDistributors.length != 0) {
        message += "Você não tem acesso as respectivas revendas: ";
        message += notAllowedDistributors[0].SocialReason;
        for (var i = 1; i < notAllowedDistributors.length; ++i)
            message += ", " + notAllowedDistributors[i].SocialReason;
        message += "</br>"
    }

    if (list.length == 0) {
        message += 'Os filtros não retornaram dados.\n';
    }

    return message;
}

function strfmt() {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}

function UpdateContainer(controler, action, container, afterSucess) {
    
    if (controler == "") { 
        alert('Função não implementada.'); 
        return;
    }

    $.ajax({ 
        type: "post", url: controler + "/" + action, 
        data: {}, 
        traditional: true, 
        success: function (data, text) { 
            $(container).html(data); 
            if (afterSucess != null) 
                afterSucess(); 
        }        
    }); 
}

function HistoryContentUpdate(hash) {
    var currentIndexPage = ( hash != "" ? hash.split('-')[4] : -1 );

    if (currentIndexPage > initIndexPage) {
        var parameters = '---#conteudo'.split('-'); //Pagina default
        if (hash != "") {

            parameters = hash.split('-');

            if (origContent == "") {
                origContent = $("#" + parameters[3]).html();
            }

            if (oldURL != "")
                contentForm[oldURL] = saveDataForHistory();

            if (parseInt(oldURL.replace(/A /g, "")) + 1 < parseInt(parameters[4])) {
                //TODO deleta elementos entre oldURL e parameters[4] intervalo aberto
                var upper = parseInt(parameters[4]);
                var lower = parseInt(oldURL.replace(/A /g, "")) + 1;
                for (var i = lower; i < upper; ++i)
                    delete contentForm[i + "A"];
            }

            UpdateContainer(parameters[0], parameters[1], "#" + parameters[3],
            function () {
                oldURL = parameters[4] + 'A';

                if (contentForm[oldURL]) {
                    fillFormWithHistoryObject(contentForm[oldURL]);
                }
            }
        );
        }
        else if (origContent != "") {  //Topo do histórico
            contentForm[oldURL] = saveDataForHistory();
            oldURL = "";
            $(parameters[3]).html(origContent);
        }
    }
}

function clearHistory() {
    $('#map_canvas').remove();
    setInitIndexPage(indexPage);
    $("#conteudo").html(origContent);
    origContent = "";   
    contentForm = [];
    oldURL = "";
}

function AjaxWaitRequest(container, request) {
    $(container).css('background-image', 'url(/LPE/Content/imgs/ajax-loader.gif)');
    $(container).css('background-repeat', 'no-repeat');
    $(container).css('background-position', '3px center');
    $(container).attr('disabled', 'disabled');
    $.ajax({
        type: "post", url: request.url,
        data: request.data,
        traditional: true,
        success: function (data, text) {
            request.success(data, text);
            $(container).css('background-image', '');
            $(container).removeAttr('disabled');
        },
        error: function (response, status, error) {
            $(container).css('background-image', '');
            $(container).removeAttr('disabled');

            var msg = JSON.parse(response.responseText);           
            var errorMsg = "Erro do servidor: " + msg.Message +
                " <br/><br/> Erro interno: " + msg.InnerException +
                " <br/><br/> Caminho do erro: " + msg.StackTrace;
            errorMsg = errorMsg.replace(/'/g, "");
            errorMsg = errorMsg.replace(/"/g, "");
            alert(msg.FriendlyMessage + '<br/><span style="cursor:pointer" onclick="defaultalert(\'' + escape(errorMsg) + '\');">[Mostrar detalhes]</span>');
        }
    });
}

/*
function openErrorPopup(errorMsg) {

    myWindow = window.open('', '', 'width=320,height=210,scrollbars=yes');
    myWindow.document.write(errorMsg);
    myWindow.focus();
}*/

function fillFormWithObject(data) {
    $.each(data, function (i, item) {
        var field = $('input[name][name="' + i + '"]');
        field.val(item);
    });

}

function fillFormWithHistoryObject(data) {
    var aux = [];

    $.each($('#conteudo :input'), function (index, value) {
        var teste = this.type;
        switch (this.type) {
            case 'password':
                //case 'select-one':
            case 'text':
            case 'textarea':
                if (data && value.name)
                    $(this).val(data[value.name]);
                break;
//            case 'select-multiple': 
//                if (data[value.name] != "" && value.name && value.name != "") { 
//                    aux = data[value.name].split(','); 
//                    for (var i = 0; i < aux.length; ++i) { 
//                        var auxItens = aux[i].split('**&& '); 
//                        $(this).trigger('addItem', [{ 'title': auxItens[0], 'value': auxItens[1]}]); 
//                    } 
//                } 
//                break; 
            case 'checkbox':
            case 'radio':
                if (data[value.id])
                    this.checked = true;
                break;
        }
    });
}


function saveDataForHistory() {
    var result = new Object();

    $.each($('#conteudo :input'), function (index, value) {

        if ($(this).attr("class") || $(this).attr("name")) {
            switch (this.type) {
                case 'password':
                    //case 'select-one':
                case 'text':
                case 'textarea':
                    if (value.name != "")
                        result[value.name] = $(value).val();
                    break;
                case 'select-multiple':
                    var optionVal = "";
                    $(this).find("option").each(function (index, value) {
                        if (index == 0)
                            //teste += $(this).html() + ' **&& ' + $(this).val();
                            optionVal += $(this).val();
                        else
                            //teste += ', ' + $(this).html() + ' **&& ' + $(this).val();
                            optionVal +=', ' + $(this).val();
                    });
                    result[value.name] = optionVal;
                    break;
                case 'checkbox':
                case 'radio':
                    if ($(this).is(':checked'))
                        result[value.id] = $(this).val();
                    break;
            }
        }
    });
    return result;
}

function generatePostData(especial) {
    var result = new Object();
   
    $.each($('input'), function (index, value) {
        if (especial[value.name])
            result[value.name] = especial[value.name];
        else
            result[value.name] = $(value).val();
    });
    $.each($('textarea'), function () {
//        if (especial[value.name])
//            result[value.name] = especial[value.name];
//        else
//            result[value.name] = $(value).val();
        if (value.name != "")
            result[value.name] = $(value).val();
    });

    $.each($('select'), function (index, value) {
        if (especial[value.name])
            result[value.name] = especial[value.name];
        else
            result[value.name] = $(value).val();
    });
    return result;
}

function clearForm(form, clearSelect) {
    $(form).find(':input').each(function () {
        if ($(this).hasClass('ComboBox')) {
            return true;
        }
        switch (this.type) {
            case 'password':
            case 'select-one':
            case 'text':
            case 'textarea':
                $(this).val('');
                break;
            case 'select-multiple':
                if (clearSelect)
                    clearSelect($(this));
                else {
                    var remElem = $(this).val();
                    if (remElem) {
                        for (var i = 0; i < remElem.length; i++)
                            $(this).trigger('removeItem', [{ "value": remElem[i]}]);
                    }
                }
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
                break;
        }
    });
}

function adjustFieldSets(form) {
    var max = 0;
    $(form).find("fieldset").each(function () {

        $(this).children("label").each(function () {
            if ($(this).width() > max) max = $(this).width();
        });
        if (max > 0) $(this).find("label").width(max + 5);
        $(this).find("input").each(function () { if (this.id == null) this.id = this.name; });
    });
}

function ouralert(message) {
    var ouralert = $('#ouralert');
    window.clearTimeout(ourAlertTid);
    ouralert.html(message);
    ouralert.show(1);
    ourAlertTid = window.setTimeout(function () { ouralert.hide(1); }, 10000);
}

function getMarkerForText(text)
{
    var url = "http:\/\/chart.apis.google.com/chart?chst=d_bubble_icon_text_small&chld=home|bb|{0}|a6d0ba|222";
    //var url = "http:\/\/chart.apis.google.com/chart?chst=d_map_pin_letter&chld={0}|a6d0ba|222";
    return strfmt(url, text);
}

var getThousandsSeparator = function () {
    var number = 111111111.111111111;
    var numberString = number.toLocaleString();
    var groupSeparator;
    for (var i = 0; i < numberString.length; i++) {
        var char = numberString.charAt(i);
        if (char != "1") {
            groupSeparator = char;
            break;
        }
    }
    return groupSeparator;
};

var getDecimalSeparator = function () {
    var number = 111111111.111111111;
    var numberString = number.toLocaleString();
    var decimalSeparator;
    for (var i = numberString.length - 1; i >= 0; i--) {
        var char = numberString.charAt(i);
        if (char != "1") {
            decimalSeparator = char;
            //decimalDigits = numberString.length - i - 1;
            break;
        }
    }
    return decimalSeparator;
}

function errorCSS(error, element) {
    var offset = element.offset(); 
    error.insertBefore(element); 
    error.addClass('message'); // add a class to the wrapper 
    error.css('position', 'absolute'); 
    error.css('left', offset.left + element.outerWidth());
    error.css('top', offset.top);
    /*
    $("div.error, div.message").fadeOut(7000, function () {
        $("div.error, div.message").remove();                   
    }); */
}

function validateEmail(email)    
{    
    er = /^[a-zA-Z0-9][a-zA-Z0-9\._-]+@([a-zA-Z0-9\._-]+\.)[a-zA-Z-0-9]{2}/;
    if(er.exec(email))
        return true;
    else
        return false;    
};

function formatNumber(aux) {
    aux = Number(aux);
    if (!isNaN(aux) && typeof aux == "number") {
        aux = aux.toLocaleString();
    }
    return aux;
}

function callMirrorDistributor(distname) {
        var action = "DistributorMirrors?distributorCode='" + distname.replace(/-/, ";") + "'";
        var name = "Espelho da Revenda";
        var controler = "mirror";
        var conteiner = "conteudo";

        var hash = [controler, action, name.replace(/[ -]/g, "."), conteiner, ++indexPage];

        $.history.load(hash.join("-"));
        setFocusPage();
        return false;
}

function callMirrorClient(cliname) {
        var action = "ClientMirrors?clientCode='" + cliname.replace(/-/, ";") + "'";
        var name = "Espelho do Cliente";
        var controler = "mirror";
        var conteiner = "conteudo";

        name = name.replace(/[ -]/g, ".");
        var hash = [controler, action, name, conteiner, ++indexPage];

        $.history.load(hash.join("-"));        
        setFocusPage();
        return false;
}

function pushBlank(data, regionCount) {
    var result = new Array();
    for (var i = 0; i < data.length; i++) {
        result.push(data[i]);
        if (i == 0 || i == regionCount-1)
            result.push(["", 0.0]);
    }
    return result;
}

function drawFeedback(list, feedbackDiv, feedbackLabel, feedbackTitle) {    
    var Result = list.Result;
    for (var i = 0; i < Result.length; i++) {
        Result[i][1] = parseFloat(Result[i][1].replace(",", "."));        
    }
    Result = pushBlank(Result, Result.length - list.Distributors.length);

    var table = new google.visualization.DataTable();
    table.addColumn('string', '');
    table.addColumn('number', '');
    table.addRows(Result);

    var formatter = new google.visualization.NumberFormat({ decimalSymbol: getDecimalSeparator(), groupingSymbol: getThousandsSeparator(), prefix: 'R$ ', negativeColor: 'red', negativeParens: true });
    formatter.format(table, 1);
        
    var chart = new google.visualization.ColumnChart(document.getElementById(feedbackDiv));
    chart.draw(table, {
        title: feedbackTitle,
        legend: "none"
    });
}
/*
function drawFeedback(list, feedbackDiv, feedbackLabel, feedbackTitle) {
    var aux = [];
    var Result = list.Result[0];
    var Distributors = list.Distributors;
    var Regions = list.Regions;

    // Generating labels
    var Labels = [feedbackLabel, 'Nacional', ''];
    for (var i = 0; i < Regions.length; i++) {
        Labels.push(Regions[i].nome_mnr);
    }
    Labels.push('');
    for (var i = 0; i < Distributors.length; i++) {
        Labels.push(Distributors[i].SocialReason);
    }

    // Generating values
    var Values = [''];
    var value = Result[0];

    Values.push(parseFloat(value.replace(",", ".")));
    Values.push(0);
    for (var i = 0; i < Regions.length; i++) {
        var value = Result[i + 1];
        Values.push(parseFloat(value.replace(",", ".")));
    }
    Values.push(0);
    for (var i = 0; i < Distributors.length; i++) {
        var value = Result[i + 1 + Regions.length];
        Values.push(parseFloat(value.replace(",", ".")));
    }
    aux[0] = Labels;
    aux[1] = Values;
    var table = google.visualization.arrayToDataTable(aux);
        
    var formatter = new google.visualization.NumberFormat({ decimalSymbol: getDecimalSeparator(), groupingSymbol: getThousandsSeparator(), prefix: '$', negativeColor: 'red', negativeParens: true });
    for (var i = 1; i < (Values.length); i++) {
        formatter.format(table, i);
    }

    var colors = ['#3567d2', '#FFFFFF', '#e33a14', '#ff9b00', '#109818', '#009ace', '#e34579', '#6bae00', '#bd2c29', '#9c459c', '#25aca1', '#b1ac14', '#6732d2', '#900604', '#671067', '#339465', '#5a75ad', '#bd7521', '#16d921', '#bd1288', '#fb36a1', '#afc712', '#2b7890', '#773410', '#0c5b25'];
    colors[Regions.length + 2] = '#FFFFFF';
        
    var comboChart = new google.visualization.ComboChart(document.getElementById(feedbackDiv));
    comboChart.draw(table, {
        title: feedbackTitle,
        vAxis: { title: feedbackLabel },
        colors: colors,
        seriesType: "bars"
    });
}
*/
function validateDate(campo) {
    var date = campo;
    var ardt = new Array;
    var ExpReg = new RegExp("(0?[1-9]|[12][0-9]|3[01])/(0?[1-9]|1[012])/[12][0-9]{3}");
    ardt = date.split("/");
    erro = false;
    if (date.search(ExpReg) == -1) {
        erro = true;
    }
    else if (((ardt[1] == 4) || (ardt[1] == 6) || (ardt[1] == 9) || (ardt[1] == 11)) && (ardt[0] > 30))
        erro = true;
    else if (ardt[1] == 2) {
        if ((ardt[0] > 28) && ((ardt[2] % 4) != 0))
            erro = true;
        if ((ardt[0] > 29) && ((ardt[2] % 4) == 0))
            erro = true;
    }
    if (erro) {
        //alert("\"" + valor + "\" não é uma data válida!!!");
        //campo.focus();
        //campo.value = "";
        return false;
    }
    return true;
}

function gotoTop() {
    window.scrollTo(0, 0);
    $('html, body').animate({ scrollTop: 0 }, 'slow');
}

function logoutPage() {
    UpdateContainer('Home', 'OptionMenu', '#optionsmenu', function () {
        clearHistory();
        $("#conteudo").load("Home/Overview");
        $("#userName").html("Visitante");
    });
}

function openPopupHelp(pagina, largura, altura) {
    return openPopup("Help/IndexHelp?Topic='" + pagina + "'", largura, altura);
}

function openPopup(pagina, largura, altura) {

    //pega a resolução do visitante
    w = screen.width;
    h = screen.height;

    //divide a resolução por 2, obtendo o centro do monitor
    meio_w = w / 2;
    meio_h = h / 2;

    //diminui o valor da metade da resolução pelo tamanho da janela, fazendo com q ela fique centralizada
    altura2 = altura / 2;
    largura2 = largura / 2;
    meio1 = meio_h - altura2;
    meio2 = meio_w - largura2;

    //abre a nova janela, já com a sua devida posição    
    window.open(pagina, 'help', 'height=' + altura + ', width=' + largura + ', top=' + meio1 + ', left=' + meio2 + ', scrollbars=yes ');
}

function parseRSS(url, callback) {
    $.ajax({
        url: document.location.protocol + '//ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=' + encodeURIComponent(url),
        dataType: 'json',
        success: function (data) {
            callback(data.responseData.feed);
        }
    });
}

function showMessageNoData(divParam) {
    $(divParam).html("<center><strong>Não foram encontrados dados.</strong></center>");
    $(divParam).height(10);
}

function logoutHome() {
    $.ajax({
        type: "post",
        url: "Home/Logout",
        traditional: true,
        success: function (data, text) {
            clearHistory();
            UpdateContainer('Home', 'OptionMenu', '#logOn', null);
            $("#conteudo").load("Home/Overview");
            $("#userName").html("Visitante");
        }
    });
}

function trataAjaxError(x, e) {
    if (x.status == 0) {
        alert('Erro na requisição (status 0)');
    } else if (x.status == 404) {
        alert('Página requisitada não encontrada (404)');
    } else if (e == 'parsererror') {
        alert('Erro na requisição JSON. (parsererror)');
    } else if (e == 'timeout') {
        alert('Tempo limite esgotado (timeout)');
    } else {
        try {
            var msg = eval("(" + x.responseText + ")");
            if (msg.Message) {
                var errorMsg = "Erro do servidor: " + msg.Message +
                                " <br/><br/> Erro interno: " + msg.InnerException +
                                " <br/><br/> Caminho do erro: " + msg.StackTrace;
               errorMsg = errorMsg.replace(/'/g, "");
               errorMsg = errorMsg.replace(/"/g, "");
               alert(msg.FriendlyMessage + '<br/><span style="cursor:pointer" onclick="defaultalert(\'' + escape(errorMsg) + '\');">[Mostrar detalhes]</span>');
            }
        }
        catch(e) {
            alert('Erro desconhecido.\n' + x.responseText);
        }
    }
}
