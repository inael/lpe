/*
Toda linha que começa com a declaração "var th" foi alterada
*/
/*
*  $Id: pivot.js,v 1.7 2007/12/04 17:15:05 source Exp $
*
*  This file is part of the OpenLink Software Ajax Toolkit (OAT) project.
*
*  Copyright (C) 2005-2007 OpenLink Software
*
*  See LICENSE file for details.
*/
/*
p = new OAT.Pivot(div, chartDiv, filterDiv, headerRow, dataRows, headerRowIndexes, headerColIndexes, filterIndexes, dataColumnIndex, optObj) 
div, filterDiv - dom element
headerRow - array
dataRows - array of arrays
headerRowIndexes, headerColIndexes, filterIndexes - arrays
dataColumnIndex - number
optObj - options object
	
p.toXML(xslStr,saveCredentials,noCredentials,query)  -- if query == false than data is dumped

var defOpt = {
headingBefore:1,
headingAfter:1,
agg:1,
aggTotals:1,
showChart:0,
showRowChart:0,
showColChart:0,
type:OAT.PivotData.TYPE_BASIC[0],
customType:function(data){return data;},
showEmpty:1,
subtotals:1,
totals:1
}
	
CSS: .pivot_table, .h1, .h2, .odd, .even .subtotal .total .gtotal .pivot_chart .pivot_row_chart .pivot_col_chart
*/

OAT.PivotData = {    
    TYPE_BASIC: [0, "Básico - 1234.56"],
    TYPE_PERCENT: [1, "Percentual - 1234.56%"],
    TYPE_SCI: [2, "Científico - 1234E+02"],
    TYPE_SPACE: [3, "Com espaço - 1 234.56"],
    TYPE_CUSTOM: [4, "Customizado"], /* function in options.customType */
    TYPE_COMMA: [5, "Com vírgula - 1,234.56"],
    TYPE_CURRENCY: [6, "Moeda - $ 1,234.56"], /* currency symbol in options.currencySymbol. $ is default */
    TYPE_BROWSER: [7, "Representação Local"]
}

OAT.Pivot = function (div, chartDiv, filterDiv, metrics, headerRow, dataRows, headerRowIndexes, headerColIndexes, filterIndexes, dataColumnIndex, optObj, aggFunc, aggTotal, callbackFunction) {
    var self = this;
    this.options = {
        headingBefore: 1,
        headingAfter: 1,
        agg: 1, /* index of default statistic function, SUM */
        aggTotals: 1, /* dtto for subtotals & totals */
        showChart: 0,
        showRowChart: 0,
        showColChart: 0,
        type: OAT.PivotData.TYPE_BROWSER[0],
        customType: function (data) { return data; },
        currencySymbol: "R$",
        showEmpty: 0,
        subtotals: 1,
        totals: 1,
        aggMetric: 0,
        callbackFunction: callbackFunction
    }
    if (optObj) for (p in optObj) { this.options[p] = optObj[p]; }

    this.gd = new OAT.GhostDrag();
    this.div = $oat(div);
    this.aggFunc = aggFunc;
    this.aggTotal = aggTotal;
    this.filterDiv = $oat(filterDiv);
    this.chartDiv = $oat(chartDiv);
    this.defCArray = ["rgb(153,153,255)", "rgb(153,51,205)", "rgb(255,255,204)", "rgb(204,255,255)", "rgb(102,0,102)",
					  "rgb(255,128,128)", "rgb(0,102,204)", "rgb(204,204,255)", "rgb(0,0,128)", "rgb(255,0,255)",
					  "rgb(0,255,255)", "rgb(255,255,0)"];

    this.gnumfmt = new google.visualization.TableNumberFormat({ prefix: "", decimalSymbol: getDecimalSeparator(), groupingSymbol: getThousandsSeparator(), negativeColor: 'red', negativeParens: true });



    this.headerRow = headerRow;
    this.allData = dataRows; /* store data */
    this.filteredData = [];
    this.tabularData = []; /* result */

    this.dataColumnIndex = dataColumnIndex; /* store data */
    this.rowConditions = headerRowIndexes; /* indexes of row conditions */
    this.colConditions = headerColIndexes; /* indexes of column conditions */
    this.filterIndexes = filterIndexes; /* indexes of column conditions */
    this.metricLabel = metrics;

    this.progressCount = 0;
    this.progressTotal = 101;
    this.progressTrigger;

    this.threadCount = 0;
    this.threadColCount = 0;
    this.threadRowCount = 0;
    this.threadChartCount = 0;
    this.threadTableCount = 0;

    this.conditions = [];
    this.filterDiv.selects = [];
    this.rowStructure = {};
    this.colStructure = {};
    this.colPointers = [];
    this.rowPointers = [];
    this.rowTotals = [];
    this.colTotals = [];
    this.gTotal = [];

    this.lightOn = function () {
        for (var i = 0; i < self.gd.targets.length; i++) {
            var elm = self.gd.targets[i][0];
            elm.style.color = "#f00";
        }
    }

    this.lightOff = function () {
        for (var i = 0; i < self.gd.targets.length; i++) {
            var elm = self.gd.targets[i][0];
            elm.style.color = "";
        }
    }
    self.gd.onFail = self.lightOff;

    this.process = function (elm) {
        self.lightOn();
        elm.style.backgroundColor = "#888";
        elm.style.padding = "2px";
        elm.style.cursor = "pointer";
        OAT.Dom.attach(elm, "mouseup", function (e) { self.lightOff(); });
    }

    this.filterOK = function (row) { /* does row pass filters? */
        for (var i = 0; i < self.rowConditions.length; i++) { /* row blacklist */
            var value = row[self.rowConditions[i]];
            var cond = self.conditions[self.rowConditions[i]];
            if (cond.blackList.find(value) != -1) { return false; }
        }
        for (var i = 0; i < self.colConditions.length; i++) { /* column blacklist */
            var value = row[self.colConditions[i]];
            var cond = self.conditions[self.colConditions[i]];
            if (cond.blackList.find(value) != -1) { return false; }
        }
        return true;
    }

    this.sort = function (cond) { /* sort distinct values of a condition */
        var months = ["january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"];
        var sortFunc;
        var coef = cond.sort;
        var numSort = function (a, b) {
            if (a == b) { return 0; }
            return coef * (parseInt(a) > parseInt(b) ? 1 : -1);
        }
        var dictSort = function (a, b) {
            if (a == b) { return 0; }
            return coef * (a > b ? 1 : -1);
        }
        var dateSort = function (a, b) {
            var ia = months.find(a.toLowerCase());
            var ib = months.find(b.toLowerCase());
            return numSort(ia, ib);
        }
        /* get data type, trial & error... */
        var testValue = cond.distinctValues[0];
        if (testValue == parseInt(testValue)) { sortFunc = numSort; } else { sortFunc = dictSort; }
        if (months.find(testValue.toString().toLowerCase()) != -1) { sortFunc = dateSort; }

        cond.distinctValues.sort(sortFunc);
    } /* sort */

    /* init routines */
    this.initCondition = function (position) {
        for (var p = 0; p < dataColumnIndex.length; ++p)
            if (position == self.dataColumnIndex[p]) { //Verifica se o indice do vetor de cabeçalhos é igual ao do elemento indicado como métrica
                self.conditions.push(false);    //Seta o elemento métricas, ou seja aquele que não deve ser 
                return;
            }
        var cond = { distinctValues: [], blackList: [], sort: 1, subtotals: self.options.subtotals };   //Constroi um objeto chamado cond
        self.conditions.push(cond);                                                                     //Adiciona objeto cond ao vetor conditions do OAT

        var array = self.allData;
        var work = new threadedLoop(array);

        work.action = function (item, index, total) {
            var value = array[index][position];
            if (cond.distinctValues.find(value) == -1) { //Verifica se o elemento faz parte do vetor distinctValues daquele objeto cond
                cond.distinctValues.push(value);         //Adiciona elemento a coleção distinct values
            } // if new value 
        };

        work.finish = function (thread) {
            --self.threadCount;
            self.sort(cond);        //Ordena o vetor de cond
        };

        ++self.threadCount;
        work.start();

        /*
        for (var i = 0; i < self.allData.length; i++) {
        var value = self.allData[i][index];
        if (cond.distinctValues.find(value) == -1) { //Verifica se o elemento faz parte do vetor distinctValues daquele objeto cond
        cond.distinctValues.push(value);         //Adiciona elemento a coleção distinct values
        } // if new value 
        } // for all rows 
        self.sort(cond);        //Ordena o vetor de cond
        */
    }

    this.initializeDrawBar = function () {
        if (self.chartDiv) {
            OAT.Dom.clear(self.chartDiv);
            var c1 = OAT.Dom.create("div", {}, "pivot_chart");
            var c2 = OAT.Dom.create("div", {}, "pivot_row_chart");
            var c3 = OAT.Dom.create("div", {}, "pivot_col_chart");
            var l1 = OAT.Dom.button("");
            l1.className = "pivot_icon_chart_column";
            var l2 = OAT.Dom.button("");
            l2.className = "pivot_icon_chart";
            var l3 = OAT.Dom.button("");
            l3.className = "pivot_icon_chart";
            var bt = OAT.Dom.create("select");
            var ld = OAT.Dom.button("Carregando...");

            OAT.Dom.append([self.chartDiv, ld, bt, l1, c1, l3, c3, l2, c2]);

            this.charts = {
                main: new google.visualization.ComboChart(c1),
                row: new google.visualization.PieChart(c2),
                col: new google.visualization.PieChart(c3),
                mainLink: l1,
                rowLink: l2,
                colLink: l3,
                mainDiv: c1,
                rowDiv: c2,
                colDiv: c3,
                chartType: bt,
                loadIcon: ld
            }

            google.visualization.events.addListener(self.charts.main, 'ready', function () {
                $(self.charts.loadIcon).hide();
            });
            google.visualization.events.addListener(self.charts.row, 'ready', function () {
                $(self.charts.loadIcon).hide();
            });
            google.visualization.events.addListener(self.charts.col, 'ready', function () {
                $(self.charts.loadIcon).hide();
            });

            OAT.Dom.attach(l1, "click", function () {
                self.options.showChart = (self.options.showChart + 1) % 2;
                if (self.options.showChart) {
                    OAT.Dom.show(self.charts.mainDiv);
                    self._drawChart();
                    self.charts.mainLink.value = "Ocultar gráfico";
                } else {
                    OAT.Dom.hide(self.charts.mainDiv);
                    self.charts.mainLink.value = "Mostrar gráfico";
                }
            });
            OAT.Dom.attach(l2, "click", function () {
                self.options.showRowChart = (self.options.showRowChart + 1) % 2;
                if (self.options.showRowChart && self.options.totals) {
                    OAT.Dom.show(self.charts.rowDiv);
                    self._drawRowChart();
                    self.charts.rowLink.value = "Ocultar gráfico das linhas";
                } else {
                    OAT.Dom.hide(self.charts.rowDiv);
                    self.charts.rowLink.value = "Mostrar gráfico das linhas";
                }
            });
            OAT.Dom.attach(l3, "click", function () {
                self.options.showColChart = (self.options.showColChart + 1) % 2;
                if (self.options.showColChart && self.options.totals) {
                    OAT.Dom.show(self.charts.colDiv);
                    self._drawColChart();
                    self.charts.colLink.value = "Ocultar gráfico das colunas";
                } else {
                    OAT.Dom.hide(self.charts.colDiv);
                    self.charts.colLink.value = "Mostrar gráfico das colunas";
                }
            });
        }

    }

    this.init = function () {
        self.propPage = OAT.Dom.create("div", { position: "absolute", border: "2px solid #000", padding: "2px", backgroundColor: "#fff" });
        document.body.appendChild(self.propPage);
        OAT.Instant.assign(self.propPage);

        var bound = self.headerRow.length + self.dataColumnIndex.length - 1;
        for (var i = 0; i < bound; i++) { //Itera sobre o vetor de cabeçalhos
            self.initCondition(i);    //Método utilizado para construir o vetor de conditions(Vetor que irá guardar os elementos de colunas
            //bem como os valores distintos para cada um
        }
        OAT.Dom.clear(self.div);
        OAT.Dom.clear(self.chartDiv);
        OAT.Dom.clear(self.propPage);
        OAT.Dom.clear(self.filterDiv);
        OAT.Dom.show(document.getElementById("pivot_div_bar"));
    } /* init */

    /* callback routines */
    this.getOrderReference = function (conditionIndex) {
        return function (target, x, y) {
            /* somehow reorder conditions */
            self.lightOff();

            /* filters? */
            if (target == self.filterDiv) {
                self.filterIndexes.push(conditionIndex);
                self.conditions[conditionIndex].blackList = [];
                for (var i = 0; i < self.rowConditions.length; i++) {
                    if (self.rowConditions[i] == conditionIndex) { self.rowConditions.splice(i, 1); }
                }
                for (var i = 0; i < self.colConditions.length; i++) {
                    if (self.colConditions[i] == conditionIndex) { self.colConditions.splice(i, 1); }
                }
                self.go();
                return;
            }

            var sourceCI = conditionIndex; /* global index */
            var targetCI = target.conditionIndex; /* global index */
            if (sourceCI == targetCI) { return; } /* dragged onto the same */
            var sourceType = false; var sourceI = -1; /* local */
            var targetType = false; var targetI = -1; /* local */
            for (var i = 0; i < self.rowConditions.length; i++) {
                if (self.rowConditions[i] == sourceCI) { sourceType = self.rowConditions; sourceI = i; }
                if (self.rowConditions[i] == targetCI) { targetType = self.rowConditions; targetI = i; }
            }
            for (var i = 0; i < self.colConditions.length; i++) {
                if (self.colConditions[i] == sourceCI) { sourceType = self.colConditions; sourceI = i; }
                if (self.colConditions[i] == targetCI) { targetType = self.colConditions; targetI = i; }
            }
            if (targetCI == -1) {
                /* no cols - lets create one */
                self.colConditions.push(sourceCI);
                self.rowConditions.splice(sourceI, 1);
                self.go();
                return;
            }
            if (targetCI == -2) {
                /* no rows - lets create one */
                self.rowConditions.push(sourceCI);
                self.colConditions.splice(sourceI, 1);
                self.go();
                return;
            }
            if (sourceType == targetType) {
                /* same condition type */
                if (sourceI + 1 == targetI) {
                    /* dragged on condition immediately after */
                    targetType.splice(targetI + 1, 0, sourceCI);
                    targetType.splice(sourceI, 1);
                } else {
                    targetType.splice(sourceI, 1);
                    targetType.splice(targetI, 0, sourceCI);
                }
            } else {
                /* different condition type */
                sourceType.splice(sourceI, 1);
                targetType.splice(targetI, 0, sourceCI);
            }
            self.go();
        }
    }

    this.getClickReference = function (cond) {
        var refresh = function () {
            self.propPage._Instant_hide();
            self.go();
        }
        return function (event) {
            var coords = OAT.Dom.eventPos(event);
            self.propPage.style.left = coords[0] + "px";
            self.propPage.style.top = coords[1] + "px";
            OAT.Dom.clear(self.propPage);
            /* contents */
            var close = OAT.Dom.create("div", { position: "absolute", top: "3px", right: "3px", cursor: "pointer" });
            close.innerHTML = '<img src="/CockpitContent/Content/js/OAT/close.png"/>';
            OAT.Dom.attach(close, "click", refresh);

            var asc = OAT.Dom.radio("order");
            asc.id = "pivot_order_asc";
            OAT.Dom.attach(asc, "change", function () { cond.sort = 1; self.sort(cond); self.go(); });
            OAT.Dom.attach(asc, "click", function () { cond.sort = 1; self.sort(cond); self.go(); });
            var alabel = OAT.Dom.create("label");
            alabel.htmlFor = "pivot_order_asc";
            alabel.innerHTML = "Ascendente";

            var desc = OAT.Dom.radio("order");
            desc.id = "pivot_order_desc";
            OAT.Dom.attach(desc, "change", function () { cond.sort = -1; self.sort(cond); self.go(); });
            OAT.Dom.attach(desc, "click", function () { cond.sort = -1; self.sort(cond); self.go(); });
            var dlabel = OAT.Dom.create("label");
            dlabel.htmlFor = "pivot_order_desc";
            dlabel.innerHTML = "Descendente";

            var hr1 = OAT.Dom.create("hr", { width: "95%", margin: "5px" });
            var hr2 = OAT.Dom.create("hr", { width: "95%", margin: "5px" });

            var subtotals = OAT.Dom.create("div");
            var sch = OAT.Dom.create("input");
            sch.id = "pivot_checkbox_subtotals";
            sch.type = "checkbox";
            sch.checked = (cond.subtotals ? true : false);
            sch.__checked = (sch.checked ? "1" : "0");
            OAT.Dom.attach(sch, "change", function () { cond.subtotals = (sch.checked ? true : false); self.go(); });
            OAT.Dom.attach(sch, "click", function () { cond.subtotals = (sch.checked ? true : false); self.go(); });
            var sl = OAT.Dom.create("label");
            sl.innerHTML = "Subtotais";
            sl.htmlFor = "pivot_checkbox_subtotals";
            OAT.Dom.append([subtotals, sch, sl]);

            var distinct = OAT.Dom.create("div");
            OAT.Dom.append([self.propPage, close, asc, alabel, OAT.Dom.create("br"), desc, dlabel, hr1, subtotals, hr2, distinct]);
            self.distinctDivs(cond, distinct);

            self.propPage._Instant_show();

            /* this needs to be here because of IE :/ */
            asc.checked = cond.sort == 1;
            asc.__checked = asc.checked;
            desc.checked = cond.sort == -1;
            desc.__checked = desc.checked;
        }
    }

    this.getDelFilterReference = function (index) {
        return function () {
            var idx = self.filterIndexes.find(index);
            self.filterIndexes.splice(idx, 1);
            self.rowConditions.push(index); /* add to rows */
            self.go();
        }
    }

    this.distinctDivs = function (cond, div) { /* set of distinct values checboxes */
        var getPair = function (text, id) {
            var div = OAT.Dom.create("div");
            var ch = OAT.Dom.create("input");
            ch.type = "checkbox";
            ch.id = id;
            var t = OAT.Dom.create("label");
            t.innerHTML = text;
            t.htmlFor = id;
            div.appendChild(ch);
            div.appendChild(t);
            return [div, ch];
        }

        var getRef = function (ch, value) {
            return function () {
                if (ch.checked) {
                    var index = cond.blackList.find(value);
                    cond.blackList.splice(index, 1);
                } else {
                    cond.blackList.push(value);
                }
                self.go();
            }
        }

        var allRef = function () {
            cond.blackList = [];
            self.go();
            self.distinctDivs(cond, div);
        }

        var noneRef = function () {
            cond.blackList = [];
            for (var i = 0; i < cond.distinctValues.length; i++) { cond.blackList.push(cond.distinctValues[i]); }
            self.go();
            self.distinctDivs(cond, div);
        }

        var reverseRef = function () {
            var newBL = [];
            for (var i = 0; i < cond.distinctValues.length; i++) {
                var val = cond.distinctValues[i];
                if (cond.blackList.find(val) == -1) { newBL.push(val); }
            }
            cond.blackList = newBL;
            self.go();
            self.distinctDivs(cond, div);
        }

        OAT.Dom.clear(div);
        var d = OAT.Dom.create("div");

        var all = OAT.Dom.button("Todos");
        OAT.Dom.attach(all, "click", allRef);

        var none = OAT.Dom.button("Nenhum");
        OAT.Dom.attach(none, "click", noneRef);

        var reverse = OAT.Dom.button("Inverter");
        OAT.Dom.attach(reverse, "click", reverseRef);

        OAT.Dom.append([d, all, none, reverse], [div, d]);
        for (var i = 0; i < cond.distinctValues.length; i++) {
            var value = cond.distinctValues[i];
            var pair = getPair(value, "pivot_distinct_" + i);
            div.appendChild(pair[0]);
            pair[1].checked = (cond.blackList.find(value) == -1);
            pair[1].__checked = (pair[1].checked ? "1" : "0");
            OAT.Dom.attach(pair[1], "change", getRef(pair[1], value));
            OAT.Dom.attach(pair[1], "click", getRef(pair[1], value));
        }
    }

    this.drawFilters = function () {
        var savedValues = [];

        var div = self.filterDiv;
        OAT.Dom.clear(div);
        self.gd.addTarget(div);
        div.selects = [];
        if (!self.filterIndexes.length) {
            div.innerHTML = "[Arraste aqui as colunas]";
        }
        for (var i = 0; i < self.filterIndexes.length; i++) {
            var index = self.filterIndexes[i];
            var close = OAT.Dom.create("input", {}, "btadd");
            close.style.cursor = "pointer";
            close.title = "Inserir dimensão na grade";
            close.type = "button";
            close.value = self.headerRow[index];
            var ref = self.getDelFilterReference(index);
            OAT.Dom.attach(close, "click", ref);
            OAT.Dom.append([self.filterDiv, close], [close]);
        }
    }

    /*this.drawProgressBar = function () {
    var divPivot_div_bar = OAT.Dom.create("div", {}, "pivot_div_bar");
    var divPivot_count_bar = OAT.Dom.create("div", {}, "pivot_count_bar", "pivot_count_bar");
    var divPivot_progress_bar = OAT.Dom.create("div", {}, "pivot_progress_bar", "pivot_progress_bar");

    var content = document.getElementById("pivot");
    OAT.Dom.show(content);
    OAT.Dom.show(divPivot_div_bar);
    OAT.Dom.show(divPivot_count_bar);
    OAT.Dom.show(divPivot_progress_bar);
    OAT.Dom.append([divPivot_div_bar, divPivot_count_bar, divPivot_progress_bar], [content, divPivot_div_bar]);
        
    }*/

    this.countTotals = function () { /* totals */
        self.rowTotals = [];
        self.colTotals = [];
        self.gTotal = [];

        for (var n = 0; n < self.filteredData.length; n++) {
            self.colTotals[n] = [];
            for (var i = 0; i < self.w; i++) { self.colTotals[n].push([]); }
        }

        for (var n = 0; n < self.filteredData.length; n++) {
            self.rowTotals[n] = [];
            for (var i = 0; i < self.h; i++) { self.rowTotals[n].push([]); }
        }

        for (var n = 0; n < self.filteredData.length; n++) {
            self.gTotal[n] = [];
        }

        for (var n = 0; n < self.filteredData.length; n++) {
            for (var i = 0; i < self.w; i++) {
                for (var j = 0; j < self.h; j++) {
                    var val = self.tabularData[n][i][j];
                    self.colTotals[n][i].push(val);
                    self.rowTotals[n][j].push(val);
                    self.gTotal[n].push(val);
                }
            }
        }

        var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func]; /* statistics */

        for (var n = 0; n < self.filteredData.length; n++) {
            for (var i = 0; i < self.rowTotals[n].length; i++) { self.rowTotals[n][i] = func(self.rowTotals[n][i]); }
        }

        for (var n = 0; n < self.filteredData.length; n++) {
            for (var i = 0; i < self.colTotals[n].length; i++) { self.colTotals[n][i] = func(self.colTotals[n][i]); }
        }

        for (var n = 0; n < self.filteredData.length; n++) {
            self.gTotal[n] = func(self.gTotal[n]);
        }
    }

    this.countSubTotals = function () { /* sub-totals */
        function clean(ptrArray, count) {       //Método uilizado para constuir um vetor totals dentro do objeto, inicialmente vázio
            for (var i = 0; i < ptrArray.length - 1; i++) {     //Deve ter pelo menos  
                var stack = ptrArray[i];
                for (var j = 0; j < stack.length; j++) {
                    stack[j].totals = [];
                    for (var k = 0; k < count; k++) { stack[j].totals.push([]); }
                }
            }
        }

        function addTotal(arr, arrIndex, totalIndex, value) {    //Adiciona um valor ao  totals do respectivo elemento indicado
            if (!arr.length) { return; }
            var item = arr[arr.length - 1][arrIndex].parent;
            while (item.parent) {
                item.totals[totalIndex].push(value);
                item = item.parent;
            }
        }

        function apply(ptrArray, func) {                    //Aplica se existir uma função do aggTotals ao respectivo vetor
            for (var i = 0; i < ptrArray.length - 1; i++) {
                var stack = ptrArray[i];
                for (var j = 0; j < stack.length; j++) {
                    var totals = stack[j].totals;
                    for (var k = 0; k < totals.length; k++) {
                        totals[k] = { array: totals[k], value: func(totals[k]) };
                    }
                }
            }
        }

        clean(self.colPointers, self.h);
        clean(self.rowPointers, self.w);

        for (var n = 0; n < self.filteredData.length; n++) {      //Percorre o vetor tridimensional tabularData adicionando os valores ao subtotals de cada linha 
            for (var i = 0; i < self.w; i++) {
                for (var j = 0; j < self.h; j++) {
                    var val = self.tabularData[n][i][j];
                    addTotal(self.colPointers, i, j, val);
                    addTotal(self.rowPointers, j, i, val);
                }
            }
        }

        var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func]; /* statistics */
        apply(self.colPointers, func);
        apply(self.rowPointers, func);
    }

    this.countPointers = function () { /* create arrays of pointers to levels of agg structures */
        function count(struct, arr, propName) {
            self[propName] = [];
            var stack = [struct];
            for (var i = 0; i < arr.length; i++) {
                var newstack = [];
                for (var j = 0; j < stack.length; j++) {
                    var item = stack[j];
                    for (var k = 0; k < item.items.length; k++) {
                        newstack.push(item.items[k]);
                    }
                }
                stack = newstack;
                self[propName].push(stack.copy());
            }
        }

        count(self.rowStructure, self.rowConditions, "rowPointers");
        count(self.colStructure, self.colConditions, "colPointers");
    }

    this.countOffsets = function () { /* starting offsets for aggregate structures */
        function count(ptrArray) {
            for (var i = 0; i < ptrArray.length; i++) {
                var stack = ptrArray[i];
                var counter = 0;
                for (var j = 0; j < stack.length; j++) {
                    var item = stack[j];
                    item.offset = counter;
                    counter += item.spanData;
                }
            }
        }

        count(self.rowPointers);
        count(self.colPointers);
    }

    this.count = function () { /* create tabularData from filteredData */
        /* compute spans = table dimensions */
        function spans(ptr, arr) { /* return span for a given aggregate pointer */
            var s = 0;
            var sD = 0;
            if (!ptr.items) {
                ptr.span = 1;
                ptr.spanData = 1;
                return [ptr.span, ptr.spanData];
            }
            for (var i = 0; i < ptr.items.length; i++) {
                var tmp = spans(ptr.items[i], arr);
                s += tmp[0];
                sD += tmp[1];
            }
            ptr.span = s;
            ptr.spanData = sD;
            if (ptr.items.length && ptr.items[0].items) {
                var cond = self.conditions[arr[ptr.items[0].depth]];
                if (cond.subtotals) { ptr.span += ptr.items.length; }
            }
            return [ptr.span, ptr.spanData];
        }


        spans(self.rowStructure, self.rowConditions);
        spans(self.colStructure, self.colConditions);

        self.countPointers();
        self.countOffsets();

        /* create blank table */
        self.tabularData = [];
        self.w = 1;
        self.h = 1;
        if (self.colConditions.length) { self.w = self.colPointers[self.colPointers.length - 1].length; }   //Seta a largura da tabela
        if (self.rowConditions.length) { self.h = self.rowPointers[self.rowPointers.length - 1].length; }   //Seta a altura da tabela

        for (var n = 0; n < self.filteredData.length; n++) {    //Bloco de código  utilizado para construir um vetor tridimensional vazio tabularData
            self.tabularData[n] = [];
            for (var i = 0; i < self.w; i++) {
                var col = new Array(self.h);
                for (var j = 0; j < self.h; j++)
                { col[j] = []; }
                self.tabularData[n].push(col);
            }
        }

        //TODO Transformar esse for em multithread e refatorar métodos subsequentes
        for (var b = 0; b < self.filteredData.length; b++) {                //Bloco que seta os valores  do tabular data
            self.builTabularDataLine(b);
        }

    } /* Pivot::count() */

    this.builTabularDataLine = function (position) {
        function coords(struct, arr, row) {
            var pos = 0;
            var ptr = struct;
            for (var i = 0; i < arr.length; i++) {
                var rindex = arr[i];
                var value = row[rindex];
                var o = false;
                for (var j = 0; j < ptr.items.length; j++) {
                    if (ptr.items[j].value != value) {
                        pos += ptr.items[j].spanData;
                    } else {
                        o = ptr.items[j];
                        break;
                    }
                }
                if (!o) { alert("Value not found in distinct?!?!? PANIC!!!"); }
                ptr = o;
            } /* for all conditions */
            return pos;
        }


        var array = self.filteredData[position];
        var work = new threadedLoop(array);

        work.action = function (item, index, total) {
            var row = array[index];
            var x = coords(self.colStructure, self.colConditions, row);     //Seta a coordenada X do elemento
            var y = coords(self.rowStructure, self.rowConditions, row);     //Seta a coordenada Y do elemento
            var val = row[self.dataColumnIndex[0]];
            val = val.toString();
            val = val.replace(/,/g, '.');
            val = val.replace(/%/g, '');
            val = val.replace(/ /g, '');
            val = parseFloat(val);
            if (isNaN(val)) { val = 0; }
            self.tabularData[position][x][y].push(val);
        };

        work.finish = function (thread) {
            --self.threadCount;
        };

        work.start();
        ++self.threadCount;
    }

    this.convertToFloat = function () {
        var func = OAT.Statistics[OAT.Statistics.list[self.options.agg].func]; /* statistics */
        for (var n = 0; n < self.filteredData.length; n++) {                    //Transforma todos os elementos do tabularData em Floats
            for (var i = 0; i < self.w; i++) {
                for (var j = 0; j < self.h; j++) {
                    var result = parseFloat(func(self.tabularData[n][i][j]));
                    self.tabularData[n][i][j] = result;
                }
            }
        }
    }

    this.isSubtotals = function () {
        self.options.subtotals = 0;                                 //Valor do subtotals por defaulf será  (false)
        for (var i = 0; i < self.conditions.length; i++) { //Percorre o vetor de conditions do self, basta que um possua um subtotals  para que o  options.subtotals seja true
            var cond = self.conditions[i];
            if (cond.subtotals) { self.options.subtotals = true; }
        }
        if (self.options.subtotals) { self.countSubTotals(); } //Se o options.subtotals for true chama o método countSubtotals
        if (self.options.totals) { self.countTotals(); }
    }

    this.numericalType = function (event) {
        var coords = OAT.Dom.eventPos(event);
        self.propPage.style.left = coords[0] + "px";
        self.propPage.style.top = coords[1] + "px";
        OAT.Dom.clear(self.propPage);
        var refresh = function () {
            self.propPage._Instant_hide();
            self.go();
        }
        /* contents */
        var type = OAT.Dom.text("Formatação numérica: ");
        self.propPage.appendChild(type);
        var select = OAT.Dom.create("select");
        for (var p in OAT.PivotData) {
            var t = OAT.PivotData[p];
            var o = OAT.Dom.option(t[1], t[0], select);
            if (self.options.type == t[0]) { o.selected = true; }
        }
        OAT.Dom.attach(select, "change", function () { self.options.type = parseInt($v(select)); refresh(); });

        var showNulls = OAT.Dom.create("div");
        var ch = OAT.Dom.create("input");
        ch.id = "pivot_checkbox_empty";
        ch.setAttribute("type", "checkbox");
        ch.checked = (self.options.showEmpty ? true : false);
        ch.__checked = (ch.checked ? "1" : "0");
        showNulls.appendChild(ch);
        var l = OAT.Dom.create("label");
        l.htmlFor = "pivot_checkbox_empty";
        l.innerHTML = "Mostrar itens vazios";
        showNulls.appendChild(l);
        OAT.Dom.attach(ch, "change", function () { self.options.showEmpty = ch.checked; refresh(); });
        OAT.Dom.attach(ch, "click", function () { self.options.showEmpty = ch.checked; refresh(); });

        OAT.Dom.append([self.propPage, select, showNulls]);
        self.propPage._Instant_show();
    }

    this._drawGTotal = function (tr) {
        for (var n = 0; n < self.filteredData.length; n++) {
            var td = OAT.Dom.create("td", {}, "gtotal");
            td.innerHTML = self.formatValue(self.gTotal[n]);
            tr.appendChild(td);
        }
    }

    this._drawRowTotals = function (tr) {
        if (self.options.headingBefore && self.colConditions.length) {
            var th = OAT.Dom.create("td", { border: "none" });
            tr.appendChild(th);
        }
        var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func]; /* statistics */
        if (self.colConditions.length) {
            for (var i = 0; i < self.w; i++)
                for (var n = 0; n < self.filteredData.length; n++) {      // Inicio do For IHGHJKJH
                    var td = OAT.Dom.create("td", {}, "total");
                    td.innerHTML = self.formatValue(self.colTotals[n][i]);
                    tr.appendChild(td);
                    if (!self.colPointers.length) { continue; }
                    var item = self.colPointers[self.colPointers.length - 1][i].parent;

                    while (item.parent) {      //Inicio While   GJVBNHGYGF
                        var cond = self.conditions[self.colConditions[item.depth]];
                        if (item.spanData - 1 == i - item.offset && (n == self.filteredData.length - 1) && cond.subtotals) { // Inicio IF FCHIIJ
                            for (var r = 0; r < self.filteredData.length; r++) {
                                var td = OAT.Dom.create("td", {}, "total");
                                var tmp = [];
                                for (var l = 0; l < item.totals.length; l++) {
                                    //tmp.append(item.totals[l].array);
                                    var stepIndex = item.totals[l].array.length / self.filteredData.length;
                                    for (var y = r * stepIndex; y < (r + 1) * stepIndex; ++y)
                                    { tmp.append(item.totals[l].array[y]); }
                                }
                                td.innerHTML = self.formatValue(func(tmp));
                                tr.appendChild(td);
                            }
                        } /* irregular subtotal */          //Fim IF FCHIIJ
                        item = item.parent;
                    }       // Fim While GJVBNHGYGF
                }           // Fim do For IHGHJKJH
        }
        self._drawGTotal(tr);
    }

    this._drawRowSubtotals = function (tr, i, ptr) { /* subtotals for i-th row */
        var func = OAT.Statistics[OAT.Statistics.list[self.options.aggTotals].func];  // statistics 
        for (var k = 0; k < self.w; k++) {
            for (var n = 0; n < self.filteredData.length; ++n) {   //Inicio For YHYKOK
                var td = OAT.Dom.create("td", {}, "subtotal");
                var sum = [];
                var index = ptr.totals[k].array.length / self.filteredData.length;
                for (var x = index * n; x < index * (n + 1); ++x)
                    sum.append(ptr.totals[k].array[x]);
                td.innerHTML = self.formatValue(func(sum));
                tr.appendChild(td);

                if (!self.colPointers.length) { continue; }
                var item = self.colPointers[self.colPointers.length - 1][k].parent;
                while (item.parent) {
                    var cond = self.conditions[self.colConditions[item.depth]];
                    if (cond.subtotals && item.spanData - 1 == k - item.offset && (n == self.filteredData.length - 1)) {
                        for (var r = 0; r < self.filteredData.length; r++) {
                            var td = OAT.Dom.create("td", {}, "subtotal");
                            tr.appendChild(td);
                            var tmp = [];
                            for (var l = 0; l < ptr.totals.length; l++) {
                                if (l >= item.offset && l < item.spanData + item.offset) {
                                    //tmp.append(ptr.totals[l].array);
                                    var stepIndex = ptr.totals[l].array.length / self.filteredData.length;
                                    for (var y = r * stepIndex; y < (r + 1) * stepIndex; ++y)
                                    { tmp.append(ptr.totals[l].array[y]); }
                                }
                            } // for all possible totals of this row 
                            td.innerHTML = self.formatValue(func(tmp));
                        }
                    } // irregular subtotal 
                    item = item.parent;
                }
            }   //Fim For YHYKOK
        } // for all regular subtotals 

        //Somatorio dos subtotais
        if (self.options.totals && self.colConditions.length) {
            var sumAgg;
            for (var n = 0; n < self.filteredData.length; ++n) {
                sumAgg = [];
                for (var l = 0; l < ptr.totals.length; l++) {
                    var index = ptr.totals[l].array.length / self.filteredData.length;
                    for (var x = index * n; x < index * (n + 1); ++x)
                        sumAgg.append(ptr.totals[l].array[x]);
                }

                var td = OAT.Dom.create("td", {}, "total");
                td.innerHTML = self.formatValue(func(sumAgg));
                tr.appendChild(td);
            }
        }
    }

    this._drawCorner = function (th, target) {
        th.innerHTML = self.headerRow[self.dataColumnIndex[0]];
        th.style.cursor = "pointer";
        th.className = "h1";
        if (target) { self.gd.addTarget(th); }
        OAT.Dom.attach(th, "click", self.numericalType);
    }

    this._drawRowConditionsHeadings = function (tbody, type) {
        /* rowConditions headings */
        var tr = OAT.Dom.create("tr");
        for (var j = 0; j < self.rowConditions.length; j++) {
            var cond = self.conditions[self.rowConditions[j]];
            var th = OAT.Dom.create("td", { cursor: "pointer" }, "h1");
            var div = OAT.Dom.create("div");
            div.innerHTML = self.headerRow[self.rowConditions[j]];
            var ref = self.getClickReference(cond);
            OAT.Dom.attach(th, "click", ref);
            var callback = self.getOrderReference(self.rowConditions[j]);
            self.gd.addSource(div, self.process, callback);
            self.gd.addTarget(th);
            th.conditionIndex = self.rowConditions[j];
            OAT.Dom.append([th, div], [tr, th]);
        }

        if (type) {
            var count = 0;

            if (self.options.totals && self.colConditions.length) { //totals 
                if (self.rowConditions.length) {
                    count += self.metricLabel.length;
                }
            }

            for (var i = 0; i < self.w; ++i) {
                for (var n = 0; n < self.filteredData.length; ++n) {
                    ++count;

                    // column subtotals
                    if (self.options.subtotals && self.colPointers.length) {
                        var item = self.colPointers[self.colPointers.length - 1][i].parent;

                        while (item.parent) {
                            var cond = self.conditions[self.colConditions[item.depth]];
                            if (item.spanData - 1 == i - item.offset && (n == self.filteredData.length - 1) && cond.subtotals) {
                                count += self.metricLabel.length;
                            }
                            item = item.parent;
                        }
                    } // if subtotals      

                }

            }


            if (count > self.metricLabel.length || self.w == 0) {
                var thFiller = OAT.Dom.create("td", { border: "none" });
                tr.appendChild(thFiller);
            }

            for (var r = 0; r < count; r++) {
                var th = OAT.Dom.create("td");
                th.innerHTML = self.metricLabel[r % self.metricLabel.length];
                tr.appendChild(th);
            }

        } else {
            var th = OAT.Dom.create("td");  //blank space above 
            if (!self.colConditions.length) {
                self._drawCorner(th, true);
                th.conditionIndex = -1;
            } else { th.style.border = "none"; }
            th.colSpan = self.filteredData.length * (self.colStructure.span + (self.options.headingBefore ? 1 : 0)) + (self.options.totals ? 1 : 0);
            tr.appendChild(th);
        }

        if (self.colConditions.length) { /* blank space after */
            var th = OAT.Dom.create("td", { border: "none" });
            tr.appendChild(th);
        }
        tbody.appendChild(tr);
    }

    this._drawColConditionsHeadings = function (tr, i) {
        var cond = self.conditions[self.colConditions[i]];
        var th = OAT.Dom.create("td", { cursor: "pointer" }, "h1");
        var div = OAT.Dom.create("div");
        div.innerHTML = self.headerRow[self.colConditions[i]];
        var ref = self.getClickReference(cond);
        OAT.Dom.attach(th, "click", ref);
        var callback = self.getOrderReference(self.colConditions[i]);
        self.gd.addSource(div, self.process, callback);
        self.gd.addTarget(th);
        th.conditionIndex = self.colConditions[i];
        th.appendChild(div);
        tr.appendChild(th);
    }

    this.getClassName = function (i, j) { /* decide odd/even class */
        var xCounter = 1;
        var yCounter = 1;
        if (self.colConditions.length > 1) {
            var colItem = self.colPointers[self.colConditions.length - 1][j].parent;
            var index = colItem.parent.items.find(colItem);
            xCounter = (index % 2 ? 1 : -1);
        }
        if (self.rowConditions.length > 1) {
            var rowItem = self.rowPointers[self.rowConditions.length - 1][i].parent;
            var index = rowItem.parent.items.find(rowItem);
            yCounter = (index % 2 ? 1 : -1);
        }
        if (xCounter * yCounter == 1) { return "odd"; } else { return "even"; }
    }

    this.formatValue = function (value) {
        var result = "";
        switch (self.options.type) { /* numeric type */
            case OAT.PivotData.TYPE_BROWSER[0]:
                var num = new Number(parseFloat(value.toFixed(2)));
                result = num.toLocaleString();
                break;
            case OAT.PivotData.TYPE_BASIC[0]: result = value.toFixed(2); break;
            case OAT.PivotData.TYPE_PERCENT[0]: result = value.toFixed(2) + "%"; break;
            case OAT.PivotData.TYPE_SCI[0]: result = value.toExponential(2); break;
            case OAT.PivotData.TYPE_SPACE[0]:
                result = value.toFixed(2);
                result = result.toString();
                var parts = result.split('.');
                var decPart = (parts.length > 1) ? ('.' + parts[1]) : '';
                var len = parts[0].length;
                var mod = len % 3;
                var wholePart = '';
                var delimiter = '&nbsp;';
                if (mod > 0)
                    wholePart = parts[0].substring(0, mod);
                for (i = mod; i < len; i += 3)
                    wholePart += (i == 0 ? '' : delimiter) + parts[0].substr(i, 3);
                result = wholePart + decPart;
                break;
            case OAT.PivotData.TYPE_COMMA[0]:
                var signal = "";

                if (value < 0) {
                    signal = "-";
                    value *= -1;
                }
                result = value.toFixed(2);
                result = result.toString();
                var parts = result.split('.');
                var decPart = (parts.length > 1) ? ('.' + parts[1]) : '';
                var len = parts[0].length;
                var mod = len % 3;
                var wholePart = '';
                var delimiter = ',';
                if (mod > 0)
                    wholePart = parts[0].substring(0, mod);
                for (i = mod; i < len; i += 3)
                    wholePart += (i == 0 ? '' : delimiter) + parts[0].substr(i, 3);
                result = signal + wholePart + decPart;
                break;
            case OAT.PivotData.TYPE_CURRENCY[0]:
                var signal = "";

                if (value < 0) {
                    signal = "-";
                    value *= -1;
                }
                result = value.toFixed(2);
                result = result.toString();
                var parts = result.split('.');
                var decPart = (parts.length > 1) ? ('.' + parts[1]) : '';
                var len = parts[0].length;
                var mod = len % 3;
                var wholePart = '';
                var delimiter = ',';
                if (mod > 0)
                    wholePart = parts[0].substring(0, mod);
                for (i = mod; i < len; i += 3)
                    wholePart += (i == 0 ? '' : delimiter) + parts[0].substr(i, 3);
                result = self.options.currencySymbol + "&nbsp;" + signal + wholePart + decPart;
                break;
            case OAT.PivotData.TYPE_CUSTOM[0]: result = self.options.customType(value); break;
        } /* switch */
        return result;
    }

    this.drawTable = function () { /* this is the crucial part */
        OAT.Dom.clear(self.div);
        var table = OAT.Dom.create("table", {}, "pivot_table");
        var tbody = OAT.Dom.create("tbody");

        /* upper part */
        for (var i = 0; i < self.colConditions.length; i++) {
            var tr = OAT.Dom.create("tr");
            if (i == 0 && self.rowConditions.length) { //Verifica  se estamos tratando da primeira coluna da primeira linha (Titulo do cubo)
                var th = OAT.Dom.create("td");
                self._drawCorner(th);
                th.colSpan = self.rowConditions.length;
                th.rowSpan = self.colConditions.length;
                tr.appendChild(th);
            }
            if (self.options.headingBefore) { /* column headings before */
                self._drawColConditionsHeadings(tr, i);
            }
            var stack = self.colPointers[i];
            for (var j = 0; j < stack.length; j++) { //column values 
                var item = stack[j];
                var th = OAT.Dom.create("td", {}, "h2");
                if (self.options.callbackFunction) {
                    th.innerHTML = self.options.callbackFunction(self.colConditions[i], item);
                }
                else {
                    th.innerHTML = item.value;
                }
                th.colSpan = item.span * self.filteredData.length;
                tr.appendChild(th);
                var cond = self.conditions[self.colConditions[item.depth]];
                if (cond.subtotals && i + 1 < self.colConditions.length) { //subtotal columns 
                    var th = OAT.Dom.create("td", {}, "h2");
                    th.innerHTML = "Total para " + item.value;
                    th.rowSpan = self.colConditions.length - i /*self.filteredData.length*/;
                    th.colSpan = self.filteredData.length;
                    tr.appendChild(th);
                }
            }
            if (self.options.totals && i == 0) {
                var th = OAT.Dom.create("td", {}, "h2");
                th.innerHTML = "TOTAL";
                th.colSpan = self.filteredData.length;
                th.rowSpan = self.colConditions.length;
                tr.appendChild(th);
            }
            if (self.options.headingAfter) { /* column headings after */
                self._drawColConditionsHeadings(tr, i);
            }
            tbody.appendChild(tr);
        }

        /* first connector */
        if (self.rowConditions.length && self.options.headingBefore) {
            self._drawRowConditionsHeadings(tbody, true);
        }

        // main part 
        var array = new Array(self.h);
        var work = new threadedLoop(array);

        work.action = function (item, index, total) {
            var tr = OAT.Dom.create("tr");
            if (self.rowConditions.length) {        //Inicio IF RTGFVBJHU
                var item = self.rowPointers[self.rowConditions.length - 1][index]; /* stack has number of values equal to height of table */
                var ptrArray = [];
                var ptr = item;
                while (ptr.parent) {   //Inicio While RTGHJ
                    ptrArray.unshift(ptr);
                    ptr = ptr.parent;
                }                     //Fim While RTGHJ
            }                         //Fim IF RTGFVBJHU

            for (var j = 0; j < self.rowConditions.length; j++) {  //row header values   Inicio For 56FHYHGVHJ
                var item = ptrArray[j];
                if (item.offset == index) {
                    var th = OAT.Dom.create("td", {}, "h2");
                    th.rowSpan = ptrArray[j].span;
                    if (self.options.callbackFunction) {
                        th.innerHTML = self.options.callbackFunction(self.rowConditions[j], item);
                    }
                    else {
                        th.innerHTML = item.value;
                    }
                    tr.appendChild(th);
                }
            }                                                       //Fim For 56FHYHGVHJ

            if (self.colConditions.length && index == 0 && self.options.headingBefore) { /* blank space before */
                var th = OAT.Dom.create("td");
                if (!self.rowConditions.length) {
                    self._drawCorner(th, true);
                    th.conditionIndex = -2;
                } else { th.style.border = "none"; }
                th.rowSpan = self.rowStructure.span;
                tr.appendChild(th);
            }

            for (var j = 0; j < self.w; j++) {
                for (var n = 0; n < self.filteredData.length; n++) { // data 
                    var td = OAT.Dom.create("td", {}, self.getClassName(index, j));
                    var result = self.tabularData[n][j][index];
                    td.innerHTML = self.formatValue(result);
                    tr.appendChild(td);

                    // column subtotals
                    if (self.options.subtotals && self.colPointers.length) {
                        var item = self.colPointers[self.colPointers.length - 1][j].parent;

                        while (item.parent) {
                            var cond = self.conditions[self.colConditions[item.depth]];
                            if (item.spanData - 1 == j - item.offset && (n == self.filteredData.length - 1) && cond.subtotals) {
                                for (var r = 0; r < self.filteredData.length; r++) {
                                    var td = OAT.Dom.create("td", {}, "subtotal");
                                    var subTotalVal = 0;
                                    var stepIndex = item.totals[index].array.length / self.filteredData.length;
                                    for (var y = r * stepIndex; y < (r + 1) * stepIndex; ++y)
                                    { subTotalVal += item.totals[index].array[y]; }
                                    td.innerHTML = self.formatValue(subTotalVal);
                                    tr.appendChild(td);
                                }
                            }
                            item = item.parent;
                        }

                    } // if subtotals      

                } // for all rows 
            }

            if (self.options.totals && self.colConditions.length) { //totals 
                if (self.rowConditions.length) {
                    for (var n = 0; n < self.filteredData.length; n++) {
                        var td = OAT.Dom.create("td", {}, "total");
                        td.innerHTML = self.formatValue(self.rowTotals[n][index]);
                        tr.appendChild(td);
                    }
                } else
                { self._drawGTotal(tr); }
            }

            if (self.colConditions.length && index == 0 && self.options.headingAfter) { /* blank space after */
                var th = OAT.Dom.create("td");
                if (!self.rowConditions.length) {
                    self._drawCorner(th, true);
                    th.conditionIndex = -2;
                } else { th.style.border = "none"; }
                th.rowSpan = self.rowStructure.span + (self.options.totals && self.rowConditions.length ? 1 : 0);
                tr.appendChild(th);
            }
            tbody.appendChild(tr);

            for (var j = self.rowConditions.length - 2; j >= 0; j--) { // subtotal rows  Inicio For IUHJUHGBHHYT
                var item = ptrArray[j];
                var cond = self.conditions[self.rowConditions[item.depth]];
                if (cond.subtotals && item.offset + item.spanData - 1 == index) {
                    var tr = OAT.Dom.create("tr");
                    var th = OAT.Dom.create("td", {}, "h2");
                    th.colSpan = self.rowConditions.length - j;
                    th.innerHTML = "Total para " + item.value;
                    tr.appendChild(th);
                    self._drawRowSubtotals(tr, index, item);
                    tbody.appendChild(tr);
                }
            }                                                           //Inicio For IUHJUHGBHHYT
        };

        work.finish = function (thread) {
            //totals row 
            if (self.options.totals && self.rowConditions.length) {
                var tr = OAT.Dom.create("tr");
                var th = OAT.Dom.create("td", {}, "h2");
                th.innerHTML = "TOTAL";
                th.colSpan = self.rowConditions.length;
                tr.appendChild(th);
                self._drawRowTotals(tr);
                tbody.appendChild(tr);
            }

            /* second connector */
            if (self.rowConditions.length && self.options.headingAfter) {
                self._drawRowConditionsHeadings(tbody, false);
            }
            self.drawFilters();
            OAT.Dom.append([table, tbody], [self.div, table]);
                        
            --self.threadTableCount;
        };

        ++self.threadTableCount;
        work.start();

    } /* drawTable */

    this.progressBar = function (value, bound) {
        if (value) self.progressCount = value;

        if (self.progressCount <= 100 && value) {
            document.getElementById("pivot_progress_bar").style.width = self.progressCount * 5 + "px"
            document.getElementById("pivot_count_bar").innerHTML = self.progressCount + "%"
        }
        else if (self.progressCount <= 100 && self.progressCount <= bound) {
            self.progressCount += 0.5;
            document.getElementById("pivot_progress_bar").style.width = (self.progressCount.toFixed(1)) * 5 + "px"
            document.getElementById("pivot_count_bar").innerHTML = (self.progressCount.toFixed(1)) + "%"
        }

    }

    this.applyFilters = function () { // create filteredData from allData 
        self.filteredData = [];
        for (var n = 0; n < self.metricLabel.length; n++) {
            self.filteredData[n] = [];
            for (var i = 0; i < self.allData.length; i++) {
                var subIndex = self.allData[i].length - self.metricLabel.length;
                var subArray = self.allData[i].slice(0, subIndex);
                subArray.push(self.allData[i][n + subIndex])
                if (self.filterOK(subArray)) {
                    self.filteredData[n].push(subArray);
                }
            }
        }
    }

    this.createAggStructure = function () { /* create a multidimensional aggregation structure */
        function createPart(struct, arr) {
            struct.items = false;
            struct.depth = -1;
            var stack = [struct];
            for (var i = 0; i < arr.length; i++) { /* for all conditions */
                var cond = self.conditions[arr[i]];
                var newstack = [];
                for (var j = 0; j < stack.length; j++) { /* for all items to be filled */
                    var items = [];
                    for (var k = 0; k < cond.distinctValues.length; k++) { /* for all currently distinct values */
                        var value = cond.distinctValues[k];
                        if (cond.blackList.find(value) == -1) {
                            var o = { value: cond.distinctValues[k], parent: stack[j], used: false, items: false, depth: i };
                            items.push(o);
                            newstack.push(o);
                        } /* if not blacklisted */
                    } /* distinct values */
                    stack[j].items = items;
                } /* items in stack */
                stack = newstack;
            } /* conditions */
        }

        createPart(self.rowStructure, self.rowConditions);
        createPart(self.colStructure, self.colConditions);
    }

    this.fillAggMetric = function () {
        var m = self.charts.chartType;
        var icon = self.charts.loadIcon;

        var aggMFunc = function () {
            self.options.aggMetric = parseInt($v(m));

            if (self.options.showChart || self.options.showRowChart || self.options.showColChart) {
                $(icon).show("fast", function () {
                    self._drawCharts();
                });
            }
        }

        OAT.Dom.clear(m);                                   //Realiza um clean no elemento select
        for (var i = 0; i < self.metricLabel.length; i++) {
            OAT.Dom.option(self.metricLabel[i], i, m);      //Insere  a opçãop no elemento select
            if (self.options.aggMetric == i) { $oat(m).selectedIndex = i; }        //Seta o elemento selecionado inicial
        }
        OAT.Dom.attach(m, "change", aggMFunc);          //Define a  função que será executada toda vez que o seletor mudar de valor
        OAT.Dom.addClass(icon, "pivotLoadingButton");
        $(icon).hide();
    }

    this.fillAggStructure = function () { /* mark used branches of aggregation structure */


        function fillAllPart(struct) {
            var ptr = struct;
            if (!ptr.items) {
                ptr.used = true;
                return;
            }
            for (var i = 0; i < ptr.items.length; i++) { fillAllPart(ptr.items[i]); }
        }

        if (self.options.showEmpty) {
            fillAllPart(self.rowStructure);
            fillAllPart(self.colStructure);
        } else {
            for (var n = 0; n < self.filteredData.length; n++) {
                self.fillAggStructurePartial(n);
            }
        }
    }

    this.fillAggStructurePartial = function (position) {
        function fillPart(struct, arr, row) {
            var ptr = struct;
            for (var i = 0; i < arr.length; i++) {
                var rindex = arr[i];
                var value = row[rindex];
                var o = false;
                for (var j = 0; j < ptr.items.length; j++) {
                    if (ptr.items[j].value == value) {
                        o = ptr.items[j];
                        break;
                    }
                }
                if (!o) { alert("Value not found in distinct?!?!? PANIC!!!"); }
                ptr = o;
            } /* for all conditions */
            ptr.used = true;
        }

        var array = self.filteredData[position];
        var work = new threadedLoop(array);

        work.action = function (item, index, total) {
            var row = array[index];
            fillPart(self.rowStructure, self.rowConditions, row);
            fillPart(self.colStructure, self.colConditions, row);
        };

        work.finish = function (thread) {
            --self.threadCount;
        };

        ++self.threadCount;
        work.start();
    }

    this.checkAggStructure = function () { /* check structure for empty parts and delete them */
        function check(ptr) { /* recursive function */
            if (!ptr.items) { return ptr.used; } /* for leaves, return their usage state */
            for (var i = ptr.items.length - 1; i >= 0; i--) { /* if node, decide based on children count */
                if (!check(ptr.items[i])) { ptr.items.splice(i, 1); }
            }
            return (ptr.items.length > 0); /* return children state */
        }

        check(self.rowStructure);
        check(self.colStructure);
    }



    this.getLabels = function (arr, direction, glue) {
        if (!arr) { return []; }
        var result = [];
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            var ptr = item;
            var value = [];
            while (ptr && ptr.parent) {
                value.unshift(ptr.value);
                ptr = ptr.parent;
            }
            result.push(value.join(glue));
        }
        return result;
    }

    this.unitLabel = function () {
        var index = self.options.aggMetric;

        var metricLab = self.metricLabel[index];
        var initM = metricLab.indexOf('(');
        var endM = metricLab.indexOf(')');

        if (initM != -1 && endM != -1)
            self.gnumfmt.ta = metricLab.substr(initM+1, endM-initM-1) + ' ';
        else
            self.gnumfmt.ta = '';
    }

    this._drawChart = function () {
        var bc = self.charts.main;
        var data = [];
        var aux = ['row'];
        var index = self.options.aggMetric;

        self.unitLabel();

        data.push(aux.concat(self.getLabels(self.rowPointers[self.rowConditions.length - 1], -1, ":").reverse()));
        var textY = self.getLabels(self.colPointers[self.colConditions.length - 1], 1, ":");
        var aux = self.tabularData[index][0].length - 1;
        for (var i = 0; i < self.tabularData[index].length; i++) {
            var col = [textY[i]];
            self._drawChartPartial(index, i, col, data, aux);
        }

        self.threadCount++;
        var endPointer = setInterval(function () {
            if (self.threadChartCount == 0) {
                var table = google.visualization.arrayToDataTable(data);
                for (var i = 1; i < table.getNumberOfColumns(); i++)
                    self.gnumfmt.format(table, i);
                bc.draw(table, { seriesType: 'bars' });
                self.threadCount--;
                clearInterval(endPointer);
            }
        }, 300);


        /*
        data.push(aux.concat(self.getLabels(self.rowPointers[self.rowConditions.length - 1], -1, ":").reverse()));
        var textY = self.getLabels(self.colPointers[self.colConditions.length - 1], 1, ":");
        for (var i = 0; i < self.tabularData[index].length; i++) {
        var col = [textY[i]];
        var aux = self.tabularData[index][0].length - 1;
        for (var j = 0; j <=  aux; j++) {
        col.push(Math.abs(self.tabularData[index][i][aux-j]));
        }
        data.push(col);
        }

        var table = google.visualization.arrayToDataTable(data);
        for (var i = 1; i < table.getNumberOfColumns(); i++)
        self.gnumfmt.format(table, i);
        bc.draw(table, { seriesType: 'bars' });*/
    }

    this._drawChartPartial = function (metric, position, col, data, aux) {

        var array = self.tabularData[metric][position];
        var work = new threadedLoop(array);

        work.action = function (item, index, total) {
            if (index <= aux)
                col.push(Math.abs(self.tabularData[metric][position][aux - index]));
        };

        work.finish = function (thread) {
            data.push(col);
            --self.threadChartCount;
        };

        ++self.threadChartCount;
        work.start();
    }

    this._drawRowChart = function () {
        var bc = self.charts.row;
        var index = self.options.aggMetric;
        var data = self.rowTotals[index];
        var aux = [["row", "number"]];
        var textX = self.getLabels(self.rowPointers[self.rowConditions.length - 1], 1, ":");

        self.unitLabel();

        self._drawRowPartial(textX, data, aux);

        self.threadCount++;
        var endPointer = setInterval(function () {
            if (self.threadRowCount == 0) {
                var table = google.visualization.arrayToDataTable(aux);
                self.gnumfmt.format(table, 1);
                table.sort({ column: 1, desc: true });
                bc.draw(table, {});
                self.threadCount--;
                clearInterval(endPointer);
            }
        }, 300);
        /*for (var i = 0; i < textX.length; i++) {
        aux.push([textX[i], Math.abs(data[i])]);
        }
        var table = google.visualization.arrayToDataTable(aux);
        self.gnumfmt.format(table, 1);
        table.sort({ column: 1, desc: true });
        bc.draw(table, {}); */

    }

    this._drawRowPartial = function (textX, data, aux) {
        var array = textX;
        var work = new threadedLoop(array);

        work.action = function (item, index, total) {
            aux.push([textX[index], Math.abs(data[index])]);
        };

        work.finish = function (thread) {
            --self.threadRowCount;
        };

        ++self.threadRowCount;
        work.start();
    }

    this._drawColChart = function () {
        var bc = self.charts.col;
        var index = self.options.aggMetric;
        var data = self.colTotals[index];
        var aux = [["col", "number"]];
        var textX = self.getLabels(self.colPointers[self.colConditions.length - 1], 1, ":");

        self.unitLabel();

        self._drawColPartial(textX, data, aux);

        self.threadCount++;
        var endPointer = setInterval(function () {
            if (self.threadColCount == 0) {
                var table = google.visualization.arrayToDataTable(aux);
                self.gnumfmt.format(table, 1);
                table.sort({ column: 1, desc: true });
                bc.draw(table, {});
                self.threadCount--;
                clearInterval(endPointer);
            }
        }, 300);

        /*for (var i = 0; i < textX.length; i++) {
        aux.push([textX[i], Math.abs(data[i])]);
        }

        var table = google.visualization.arrayToDataTable(aux);
        self.gnumfmt.format(table, 1);
        table.sort({ column: 1, desc: true });
        bc.draw(table, {});*/
    }

    this._drawColPartial = function (textX, data, aux) {
        var array = textX;
        var work = new threadedLoop(array);

        work.action = function (item, index, total) {
            aux.push([textX[index], Math.abs(data[index])]);
        };

        work.finish = function (thread) {
            --self.threadColCount;
        };

        ++self.threadColCount;
        work.start();
    }

    this._drawCharts = function () {
        if (self.options.showChart) {
            OAT.Dom.show(self.charts.mainDiv);
            self._drawChart();
            self.charts.mainLink.value = "Ocultar gráfico";
        } else {
            OAT.Dom.hide(self.charts.mainDiv);
            self.charts.mainLink.value = "Mostrar gráfico";
        }
        if (self.options.showRowChart && self.options.totals) {
            OAT.Dom.show(self.charts.rowDiv);
            self._drawRowChart();
            self.charts.rowLink.value = "Ocultar gráfico das linhas";
        } else {
            OAT.Dom.hide(self.charts.rowDiv);
            self.charts.rowLink.value = "Mostrar gráfico das linhas";
        }
        if (self.options.showColChart && self.options.totals) {
            OAT.Dom.show(self.charts.colDiv);
            self._drawColChart();
            self.charts.colLink.value = "Ocultar gráfico das colunas";
        } else {
            OAT.Dom.hide(self.charts.colDiv);
            self.charts.colLink.value = "Mostrar gráfico das colunas";
        }
        if (self.options.totals) {
            OAT.Dom.show(self.charts.rowLink);
            OAT.Dom.show(self.charts.colLink);
        } else {
            OAT.Dom.hide(self.charts.rowLink);
            OAT.Dom.hide(self.charts.colLink);
        }

    }

    this.asyncDrawCharts = function () {
        if (self.chartDiv) { self._drawCharts(); }

        var drawCharts = setInterval(function () {
            self.progressBar(null, 99);

            if (self.threadColCount == 0 && self.threadChartCount == 0 && self.threadRowCount == 0 && self.threadCount == 0) {
                clearInterval(drawCharts);
                self.progressBar(100);

                self.progressCount = 0;
                OAT.Dom.hide(document.getElementById("pivot_div_bar"));
            }
        }, 100);

    }

    this.asyncDrawTable = function () {
        self.drawTable();

        var drawTable = setInterval(function () {
            self.progressBar(null, 85);

            if (self.threadTableCount == 0) {
                clearInterval(drawTable);
                self.progressBar(87);
                self.asyncDrawCharts();
            }
        }, 100);
    }

    this.asyncCount = function () {

        self.count(); // fill tabularData with values 

        var drawPointer = setInterval(function () {
            self.progressBar(null, 75);
            if (self.threadCount == 0) {
                self.progressBar(78);
                clearInterval(drawPointer);

                self.convertToFloat();
                self.isSubtotals();
                self.initializeDrawBar();
                self.fillAggMetric();

                self.asyncDrawTable();

            }
        }, 100);

    }

    this.asyncfillAggStructure = function () {

        self.fillAggStructure();

        var drawPointer = setInterval(function () {
            self.progressBar(null, 60);
            if (self.threadCount == 0) {
                self.progressBar(60);
                clearInterval(drawPointer);
                self.checkAggStructure();
                self.asyncCount();
            }
        }, 100);

    }

    this.attachEventToAggFunc = function () {
        var aggRef = function () {
            self.options.agg = parseInt($v(aggFunc));
            self.go();
        }
        var aggRefTotals = function () {
            self.options.aggTotals = parseInt($v(aggTotal));
            self.go();
        }
        //create agg function list 
        OAT.Dom.clear(self.aggFunc);
        OAT.Dom.clear(self.aggTotal);
        for (var i = 0; i < OAT.Statistics.list.length; i++) {
            var item = OAT.Statistics.list[i];
            OAT.Dom.option(item.longDesc, i, aggFunc);
            OAT.Dom.option(item.longDesc, i, aggTotal);
            if (self.options.agg == i) { $oat(aggFunc).selectedIndex = i; }
            if (self.options.aggTotals == i) { $oat(aggTotal).selectedIndex = i; }
        }
        OAT.Dom.attach(aggFunc, "change", aggRef);
        OAT.Dom.attach(aggTotal, "change", aggRefTotals);
    }

    this.go = function () {
        OAT.Dom.clear(self.div);
        OAT.Dom.clear(self.chartDiv);
        OAT.Dom.clear(self.propPage);
        OAT.Dom.clear(self.filterDiv);
        OAT.Dom.show(document.getElementById("pivot_div_bar"));
        self.gd.clearSources();
        self.gd.clearTargets();

        self.applyFilters();

        self.createAggStructure();

        self.asyncfillAggStructure();
    } // Fim da função go()

    //self.drawProgressBar();
    self.progressBar(0);
    self.init();

    var pointer = setInterval(function () {
        self.progressBar(null, 35);
        if (self.threadCount == 0) {
            self.progressBar(35);
            clearInterval(pointer);
            self.go();
            self.attachEventToAggFunc();
        }
    }, 100);
}
OAT.Loader.featureLoaded("pivot");
