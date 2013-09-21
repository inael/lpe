var GdivContent, GdivChart, GdivPage, Gmetrics, Gheader, Gdata, GheaderRow, GheaderCol, Gfilter, GdataIndex, Goptions, GaggFunc, GaggTotal;

function initPivotTable(divContent, divChart, divPage, metrics, header, data, headerRow, headerCol, filter, dataIndex, options, aggFunc, aggTotal, callbackFunction) {
    GdivContent = divContent;
    GdivChart = divChart;
    GdivPage = divPage;
	Gmetrics = metrics;
    Gheader = header;
    Gdata = data;
    GheaderRow = headerRow;
    GheaderCol = headerCol;
    Gfilter = filter;
    GdataIndex = dataIndex;
    Goptions = options;
    GaggFunc = aggFunc;
    GaggTotal = aggTotal;

    var pivot = new OAT.Pivot(divContent, divChart, divPage, metrics, header, data, headerRow, headerCol, filter, dataIndex, options, aggFunc, aggTotal, callbackFunction);
    
    return pivot;
}

function refreshPivotTable() {
    initPivotTable(GdivContent, GdivChart, GdivPage, Gmetrics, Gheader, Gdata, GheaderRow, GheaderCol, Gfilter, GdataIndex, Goptions, GaggFunc, GaggTotal);
}
