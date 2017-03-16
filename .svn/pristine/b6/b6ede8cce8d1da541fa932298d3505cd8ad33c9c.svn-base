var pagesToBeDisplayed = 9;
var defaultpageSize = 25;

function searchGridByBtn(tblId, isByHeader, thiss, scsMethodToCall) {
    $("div[divfortbl=" + tblId + "] select").each(function () {
        var ddlId = this.id;
        $("#" + ddlId).multiselect({
            includeSelectAllOption: true
        });
    });
    $("div[divfortbl=" + tblId + "] input[columnType=dates]").each(function () {
        var ddlId = this.id;
        $("#" + ddlId).datetimepicker({ useCurrent: false,
            viewMode: 'days',
            format: 'MM/DD/YYYY'
        });
    });

    if (typeof isByHeader == "undefined") {
        isByHeader = false;
    }
    var newSortColumn = "";
    var newSortDirection = "";
    var prevSortColumn = $("#" + tblId + " th[sorted='y']").attr("column");
    var prevSortDirection = $("#" + tblId).attr("newSortDirection");

    if (isByHeader) {
        var newSortColumn = $(thiss).closest("th").attr("column");
        if (prevSortColumn == newSortColumn) {
            switch (prevSortDirection) {
                case "asc":
                    newSortDirection = "desc";
                    break;
                case "desc":
                    newSortDirection = "asc";
                    break;
                default:
                    newSortDirection = "desc";
                    break;
            }
        }
        else {
            newSortDirection = "desc";
            $("#" + tblId + " th").removeAttr("sorted");
        }
    }
    else {
        $("#" + tblId).attr("calculatepagesAgain", "y");
        $("#" + tblId).attr("currentpage", "1")
        newSortColumn = $("#" + tblId).attr("newSortColumn");
        newSortDirection = $("#" + tblId).attr("newSortDirection");
    }
    $("#" + tblId + " th[column='" + newSortColumn + "']").addClass("currentSort");
    $("#" + tblId + " th[column='" + newSortColumn + "']").attr("sorted", "y");
    $("#" + tblId).attr("newSortDirection", newSortDirection);
    $("#" + tblId).attr("newSortColumn", newSortColumn);
    bindGrid($("#" + tblId).attr("currentPage"), tblId, scsMethodToCall);
}
function clearGridVMRFilter(containerId, tblId, scsMethodToCall) {
    clearAllTextBoxes(containerId);
    $("#" + containerId + " select > option").attr("selected", false);
    $("#" + containerId + " select").multiselect("destroy");
    // $("#" + ddlId).trigger('reset');
    $("#" + containerId + " select ").multiselect({
        includeSelectAllOption: true
    });
    searchGridByBtn(tblId, false, scsMethodToCall)
}

function bindGrid(currentPage, tblId, scsMethodToCall) {
    var areTableHeaderGenerated = $.trim($("#" + tblId).attr("areTableHeaderGenerated"));
    if (areTableHeaderGenerated == "n") {
        var tdFooter1 = tdFooter.replaceAll("[@gridId]", tblId);
        $("#" + tblId + " [tdFooter]").html(tdFooter1);
        var ddlPage = ddlPaging.replaceAll("[@gridId]", tblId);
        $("#" + tblId + " tfoot div[divPagingDDL]").html(ddlPage);
        defaultpageSize = $("#" + tblId).attr("pageSize");
        $("#" + tblId + " tfoot  #ddlPaging_" + tblId).val(defaultpageSize);
        var colsCount = $('#' + tblId + " thead tr th").length;
        $('#' + tblId + " tfoot tr td[tdFooter]").attr("colspan", colsCount);
        $("#" + tblId).attr("areTableHeaderGenerated", "y");
        $("#" + tblId + " thead tr[role='row']  th").each(function () {
            $(this).html("<a href='javascript:void(0)' onclick=searchGridByBtn('" + tblId + "'," + true + ",this)>" + $(this).attr("columnfriendlyname") + "</a> <div class='btn-group pull-right sortGrid'><i class='fa fa-fw fa-arrows-v '></i></div>");
        });
    }
    var pageSize = $("#" + tblId + " .dataTables_length select").val();
    var shallCountpagesAgain = ($("#" + tblId).attr("calculatepagesAgain") == "y") ? true : false;

    var pageSize = $("#" + tblId + " .dataTables_length select").val();
    var _startIndex = (((currentPage - 1) * pageSize) + 1);
    var _lastIndex = (currentPage * pageSize);

    var array = makeSearchCond(tblId);
    var newSortColumn = $.trim($("#" + tblId).attr("newSortColumn"));
    var newSortDirection = $("#" + tblId).attr("newSortDirection");
    var service = $("#" + tblId).attr("service");
    var webMethod = $("#" + tblId).attr("method");
    var datas = "{'searchCond':" + JSON.stringify(array) + ",'startIndex':" + _startIndex + ",'lastIndex':" + _lastIndex + ",'newSortColumn':'" + newSortColumn + "','newSortDirection':'" + newSortDirection + "'}";
    var jsTempleteToCall = $("#" + tblId + " script[type='text/html']").attr("id");
    var jsTempleteToAppendInTbody = $("#" + tblId + " tbody").attr("id");
    $("#" + tblId + " th  i").removeClass("fa-caret-up");
    $("#" + tblId + " th  i").removeClass("fa-caret-up");
    if (newSortDirection == "asc") {
        $("#" + tblId + " th[column='" + newSortColumn + "']   i").addClass("fa-caret-down");
    }
    else {
        $("#" + tblId + " th[column='" + newSortColumn + "']   i").addClass("fa-caret-up");
    }

    callJTemplateServiceMethodURL(datas, service, webMethod, "", jsTempleteToAppendInTbody, jsTempleteToCall, true, bindGrid_Scs, scsMethodToCall, tblId, shallCountpagesAgain);
}

function bindGrid_Scs(scsMethodToCall, tblId, shallCountpagesAgain) {
    hideProcess();
    if (typeof scsMethodToCall != "undefined") {
        scsMethodToCall(tblId);
    }
    
    _formatGridData(tblId)
    if (!shallCountpagesAgain) {
        generatePages(tblId, scsMethodToCall);
    }
    else {
        //if (findRowsGenerated) {
            var sumArray = makeSumCondition(tblId)
            var service = $("#" + tblId).attr("service");
            var webMethod = $("#" + tblId).attr("methodCount");
            callServiceMethod("{'searchCond':" + JSON.stringify(makeSearchCond(tblId)) + ",'sumCond':" + JSON.stringify(sumArray) + "}", service, webMethod, "", "", "",
                     false, "", true, bindGrid_Scs_Scs, tblId, scsMethodToCall, "");
//        }
//        else {
//            generatePages(tblId);
//        }
    }

}

function _formatGridData(tblId) {
    $("#" + tblId + " tbody tr [getValueFromDdl]").each(function () {
        //        var parentDDL = $(this).attr("getValueFromDdl");
        //        var val = $.trim($(this).html())
        //        
        //        $(this).html(getDDLTextByValue(parentDDL, val))

        var parentDDLColumn = $(this).attr("getValueFromDdl");
        //var parentDDL = $("#" + tblId + " thead tr th[column=" + parentDDLColumn + "] select").attr("id");
        var parentDDL = $("#" + tblId + " thead tr th[column=" + parentDDLColumn + "] select").attr("id");
        var val = $.trim($(this).html())
        if (typeof parentDDL == "undefined") {
            parentDDL = parentDDLColumn;
        }
        $(this).html(getDDLTextByValue(parentDDL, val));
    })

    $("#" + tblId + " tbody [formatCurrency]").each(function () {
        var values = $(this).html();
        if (typeof $("#" + tblId + " tbody td[nd]") == "undefined") {
            values = ($.trim(values)).formatMoney('$ ', 2, ',', '.');
        }
        else {
            values = ($.trim(values)).formatMoney('$', 2, ',', '.');
        }
        $(this).html(values);
    });


    $("#" + tblId + " tbody  [formatDateTime]").each(function () {
        var formatDateTime = $(this).attr("formatDateTime");
        var values = $(this).html();
        var values2 = values.formatDateTimeWithParameter(formatDateTime);
        $(this).html(values2);
    });

    $("#" + tblId + " tbody  [formatTimeToHM]").each(function () {
        var values = $.trim($(this).html());
        var values2 = values.formatTimeInMinsToHM();
        $(this).html(values2);
    });

}


function bindGrid_Scs_Scs(tblId, scsMethodToCall, parameter3, data) {
    var datas = data.d.split('***');
    var totalRecords = parseInt(datas[0]);
    if (datas.length > 1) {
        for (i = 1; i < datas.length; i++) {
            var column = datas[i].split(':')[0];
            var value = datas[i].split(':')[1];
            if (datas[i].split(':').length > 2) {
                value = datas[i].split(':')[1] + ":" + datas[i].split(':')[2];
            }
            if (value != "")
                $("#tdFooter_" + tblId + "_" + column + "").html(value);
        }
    }
    var pageSize = $("#" + tblId + " .dataTables_length select").val();

    $("#" + tblId).attr("totalRecords", totalRecords);
    if (totalRecords % pageSize > 0) {
        totalNoOfpages = parseInt(totalRecords / pageSize) + 1;
    }
    else
    { totalNoOfpages = totalRecords / pageSize; }
    $("#" + tblId).attr("totalNoOfpages", totalNoOfpages);
    $("#" + tblId).attr("calculatepagesAgain", "n");
    $("#" + tblId).attr("currentpage", "1")
    generatePages(tblId, scsMethodToCall);

    $("#" + tblId + " tfoot td[formatCurrency]").each(function () {
        var values = $(this).html();
        if ($.trim(values) != "") {
            values = ($.trim(values)).formatMoney('$ ', 2, ',', '.');
            $(this).html(values);
        }
    });


}


function lbtnNextPrev_Command(tblId, command, pageNo, scsMethodToCall) {
    var currentPage = $("#" + tblId).attr("currentPage");
    var totalNoOfpages = $("#" + tblId).attr("totalNoOfpages");
    var calculatePagesAgain = "n";

    switch (command) {
        case "First":
            currentPage = 1;
            break;
        case "Last":
            currentPage = totalNoOfpages;
            break;
        case "Next":
            currentPage = parseInt(currentPage) + 1;
            break;
        case "Prev":
            currentPage = currentPage - 1;
            break;
        case "PageSizeChange":
            currentPage = 1;
            calculatePagesAgain = "y";
            break;
        case "":
            currentPage = pageNo;
            break;
    }
    $("#" + tblId).attr("currentPage", currentPage);
    $("#" + tblId).attr("calculatepagesAgain", calculatePagesAgain);
    bindGrid(currentPage, tblId, scsMethodToCall);
}

 
function makeSumCondition(tblId) {
    var sumArray = new Array();
    $("#" + tblId + " thead tr[role='row']  th").each(function () {

        if ($(this).attr("sum")) {
            var type = $.trim($(this).attr("type"));
            var column = $(this).attr("column");
            var obj = new Object();
            obj.columnName = $(this).attr("column");
            sumArray.push(obj);
        }
    });
    return sumArray;
}

function makeSearchCond(tblId) {
    var searchCondArray = new Array();

    $("div[divForTbl=" + tblId + "] [MakeCondition]").each(function () {
        var shallFormatCurrency = false;
        if (typeof $(this).attr("formatcurrency") != "undefined") {
            shallFormatCurrency = true;
        }
        var columnNameFriendly = $(this).attr("columnfriendlyname");
        var columnName = $(this).attr("column");
        var columnType = $(this).attr("columnType");
        var obj = new Object();
        var shallIncludeFilter = true;
        if (typeof $(this).attr("nofilter") != "undefined") {
            shallIncludeFilter = false;
        }

        var values = "";
        if (columnType == "int" && shallIncludeFilter == true) {
            values = $("#" + this.id + " input[getValue='y']").val();
            var filterType = $("#" + this.id + " ul[type='filter'] a[selected='y']").attr("code");
            if (!filterType)
            { filterType = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code"); }
            if (filterType == "ninl" || filterType == "inl")
            { values = "isNull"; }
            if (values != "") {
                if (filterType == "btwn") {
                    values = values.replaceAll(",", " and ").replaceAll("-", " and ").replaceAll("&", " and ");
                }
                obj.data = values;
                obj.columnName = columnName;
                obj.columnNameFriendly = columnNameFriendly;
                obj.columnType = columnType;
                obj.filterType = filterType;
                obj.formatCurrency = shallFormatCurrency;
                searchCondArray.push(obj);
            }
        }
        else if (columnType == "float" && shallIncludeFilter == true) {
            values = $("#" + this.id + " input[getValue='y']").val();
            var filterType = $("#" + this.id + " ul[type='filter'] a[selected='y']").attr("code");
            if (!filterType)
            { filterType = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code"); }
            if (filterType == "ninl" || filterType == "inl")
            { values = "isNull"; }
            if (values != "") {
                if (filterType == "btwn") {
                    values = values.replaceAll(",", " and ").replaceAll("-", " and ").replaceAll("&", " and ");
                }
                if (typeof $("#" + this.id).attr("time") != "undefined") {
                    var convertTo = ($("#" + this.id).attr("time"));
                    if (convertTo == "m") {

                    }
                }

                obj.data = values;
                obj.columnName = columnName;
                obj.columnNameFriendly = columnNameFriendly;
                obj.columnType = columnType;
                obj.filterType = filterType;
                obj.formatCurrency = shallFormatCurrency;
                searchCondArray.push(obj);
            }
        }
        else if (columnType == "string" && shallIncludeFilter == true) {
            values = $(this).val();
            if (values != "") {
                obj.data = values;
                obj.columnName = columnName;
                obj.columnNameFriendly = columnNameFriendly;
                obj.columnType = columnType;
                obj.formatCurrency = shallFormatCurrency;
                obj.filterType = $(this).attr("filterType")
                searchCondArray.push(obj);
            }
        }
        else if (columnType == "ddl" && shallIncludeFilter == true) {
            values = "";
            //var ddlId = $("#" + this.id + " select").attr("id");
            ddlId = this.id;
            var valSelected = $('#' + ddlId).val();
            if (valSelected != null && valSelected.indexOf("multiselect-all") == -1) {

                var len = valSelected.length;
                values = "";
                if (len > 1) {
                    for (i = 0; i < len; i++) {
                        if (valSelected[i] == parseInt(valSelected[i])) {
                            values = values + "," + valSelected[i];
                        }
                        else {
                            values = values + ",'" + valSelected[i] + "'";
                        }
                    }
                    values = values.substring(1);
                }
                else
                    values = valSelected[0];
                var obj = new Object();
                obj.data = values;
                obj.columnName = columnName;
                obj.columnNameFriendly = columnNameFriendly;
                obj.columnType = columnType;
                obj.formatCurrency = shallFormatCurrency;
                obj.filterType = (valSelected.length == 1) ? "et" : "in";
                searchCondArray.push(obj);
            }
        }
        else if (columnType == "dates" && shallIncludeFilter == true) {
            values = "";

            var startDate = $(this).val();
            var endDate = $("div[divForTbl=" + tblId + "] [column='" + columnName + "'][endDate]").val();
            if (typeof endDate == "undefined") { endDate = ""; }
 
            if (startDate + endDate != "") {
                    var obj = new Object();
                    values = "";
                    if (startDate == "") {
                        values = "ltetDate"
                        obj.data = " <='" + endDate + "'";
                    }
                    else if (endDate == "") {
                        values = "gtetDate"
                        obj.data = " >='" + startDate + "'";
                    }
                    else {
                        values = "btDate"
                        obj.data = "'" + startDate + "' and '" + endDate + "'";
                    }
                    obj.columnName = columnName;
                    obj.columnNameFriendly = columnNameFriendly;
                    obj.filterType = values;
                    obj.columnType = columnType;
                    obj.formatCurrency = shallFormatCurrency;
                    searchCondArray.push(obj);
            }
        }
        else if (columnType == "bool" && shallIncludeFilter == true) {
            var code = $("#" + this.id + " ul[type='filter'] a[selected='y']").attr("code");
            if (!code) { code = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code"); }
            if (code != "nf") {
                var obj = new Object();
                if (typeof $("#" + this.id).attr("makeOpposite") == "undefined") {
                    obj.data = ($("#" + this.id + " input[getValue='y']").is(":checked")) ? 1 : 0;
                }
                else {
                    obj.data = ($("#" + this.id + " input[getValue='y']").is(":checked")) ? 0 : 1;
                }
                obj.columnName = columnName;
                obj.columnNameFriendly = columnNameFriendly;
                obj.columnType = columnType;
                obj.formatCurrency = shallFormatCurrency;
                obj.filterType = $("#" + this.id + " ul[type='filter'] a[selected='y']").html();
                { obj.filterType = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code"); }
                searchCondArray.push(obj);
            }
        }
    })
    return searchCondArray;
}




function generatePages(tblId, scsMethodToCall) {
    if (typeof $("#" + tblId).attr("pagesToBeDisplayed") != "undefined") {
        pagesToBeDisplayed = $("#" + tblId).attr("pagesToBeDisplayed");
    }
    else {
        pagesToBeDisplayed = 9;
    }
    if ($("div[fortbl='" + tblId + "'] #ddlSelectFilter").length > 0) {
    }

    var currentPage = $("#" + tblId).attr("currentPage");
    var pageSize = $("#" + tblId + " .dataTables_length select").val();
    var totalRecords = $("#" + tblId).attr("totalRecords");
    var totalNoOfpages = $("#" + tblId).attr("totalNoOfpages");
    var addOn = parseInt(pagesToBeDisplayed / 2);


    if (totalNoOfpages == 0) {
        if (totalRecords == 0) {
            $("#" + tblId + "_totalRecordsInfo").html("No Records Found");
            if ($("#" + tblId + " tfoot .trFooterSum").length > 0) {
                $("#" + tblId + " tfoot .trFooterSum td").html('');
            }
        }
        else {
            $("#" + tblId + "_totalRecordsInfo").html("Showing 1 to " + totalRecords + " of " + totalRecords + " entries");
        }
        $("#" + tblId + " #pagesGenerated").html('');
    }
    else if (totalNoOfpages == 1) {
        $("#" + tblId + "_totalRecordsInfo").html("Showing 1 to " + totalRecords + " of " + totalRecords + " entries");
        $("#" + tblId + " #pagesGenerated").html('');
    }
    else if (parseInt(totalNoOfpages) < parseInt(pagesToBeDisplayed)) {
        generatePagesNumber(tblId, pageSize, 1, totalNoOfpages, scsMethodToCall);
    }
    else if (parseInt(currentPage) <= parseInt(addOn)) {
        generatePagesNumber(tblId, pageSize, 1, pagesToBeDisplayed, scsMethodToCall);
    }
    else if ((parseInt(currentPage) + parseInt(addOn)) < totalNoOfpages) {
        generatePagesNumber(tblId, pageSize, (currentPage - addOn), (parseInt(currentPage) + parseInt(addOn)), scsMethodToCall);
    }
    else {
        generatePagesNumber(tblId, pageSize, (totalNoOfpages - pagesToBeDisplayed), totalNoOfpages, scsMethodToCall);
    }

}


function generatePagesNumber(tblId, pageSize, startIndex, maxPageToBeDisplayedAtBottom, scsMethodToCall) {
    var totalRecords = $("#" + tblId).attr("totalRecords");
    var currentPage = $("#" + tblId).attr("currentPage");
    var totalNoOfpages = $("#" + tblId).attr("totalNoOfpages");

    var _startIndex = 1;
    var _lastIndex = pageSize;
 

    if (currentPage != 1) {
        _startIndex = (((currentPage - 1) * pageSize) + 1);
    }
    if (currentPage == totalNoOfpages) {
        _lastIndex = totalRecords;
    }
    else {
        _lastIndex = (currentPage * pageSize);
    }

  

    if (typeof scsMethodToCall == "undefined") {
        scsMethodToCall = "";
    }
    else {
        scsMethodToCall = getfunctionNameFromFunction(scsMethodToCall);
    }
    if (scsMethodToCall != "") {
        scsMethodToCall = "," + scsMethodToCall;
    }
   
    $("#" + tblId + "_totalRecordsInfo").html("Showing " + _startIndex + " to " + _lastIndex + " of " + totalRecords + " entries");

    $("#" + tblId + " #pagesGenerated").html('');
    if (currentPage == 1) {
        $("#" + tblId + " #pagesGenerated").append(" <li class='prev disabled'><a nextPrev href='javascript:void(0)' ><i class='fa fa-fw fa-fast-backward'></i></a></li>");
        $("#" + tblId + " #pagesGenerated").append(" <li class='prev disabled'><a nextPrev  href='javascript:void(0)' ><i class='fa fa-fw fa-backward'></i></a></li>");
    }
    else {
        $("#" + tblId + " #pagesGenerated").append("<li><a href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "','',1" + scsMethodToCall + ")  ><i class='fa fa-fw fa-fast-backward'></i></a></li>");
        $("#" + tblId + " #pagesGenerated").append("<li><a href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "','Prev',''" + scsMethodToCall + ")  ><i class='fa fa-fw fa-backward'></i></a></li>");
    }

    for (i = startIndex; i <= maxPageToBeDisplayedAtBottom; i++) {
        if (i == currentPage)
        { $("#" + tblId + " #pagesGenerated").append(" <li class='active'><a href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "',''," + i + scsMethodToCall + ") >" + i + "</a></li>"); }
        else { $("#" + tblId + " #pagesGenerated").append(" <li><a href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "',''," + i + scsMethodToCall + ") >" + i + "</a></li>"); }
    }

    if (currentPage == totalNoOfpages) {
        $("#" + tblId + "#pagesGenerated").append(" <li class='prev disabled'><a nextPrev href='javascript:void(0)' ><i class='fa fa-fw fa-forward'></i></a></li>");
        $("#" + tblId + "#pagesGenerated").append(" <li class='prev disabled'><a nextPrev href='javascript:void(0)' ><i class='fa fa-fw fa-fast-forward'></i></a></li>");
    }
    else {
        $("#" + tblId + " #pagesGenerated").append("<li><a nextPrev href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "','Next',''" + scsMethodToCall + ") ><i class='fa fa-fw fa-forward'></i></a></li>");
        $("#" + tblId + " #pagesGenerated").append("<li><a nextPrev href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "',''," + totalNoOfpages + scsMethodToCall + ") ><i class='fa fa-fw fa-fast-forward'></i></a></li>");
    }
}




  



var intColumn = " <span class='pull-left'><a   href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true) class='headerName'>[@columnFriendlyName]</a></span>" +
                                   "<div class='btn-group pull-right sortGrid' >" +

                                                      "  <i  class='dropdown-toggle fa fa-fw fa-caret-up ' data-toggle='dropdown'></i>" +
                                                       " <ul class='dropdown-menu'  role='menu' type='sort'>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true) sortDirection=''>NoSort</a></li>" +
                                                        "    <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='asc' >SortAsc</a></li>" +
                                                         "   <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='desc' >SortDesc</a></li>" +
                                                       " </ul>" +
                                                   " </div>" +
                               "  <div class='clearfix'></div>" +
                               " <input type='text' getValue='y'   class='txt65' />" +
                               " <div class='btn-group' >" +
                                        "  <button type='button'  class='dropdown-toggle srchBtn' data-toggle='dropdown'>" +
                                                    "     <i class='fa fa-fw fa-filter'  ></i> " +
                                                   " </button>" +
                                                   " <ul class='dropdown-menu' role='menu' type='filter'>" +
                                                      "  <li><a href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='nf'>NotFilter</a></li>" +
                                                      "  <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='c' selected='y'>Contains</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false)  code='dnt' >DoesNotContain</a></li>" +
                                                       "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false)  code='in' >In</a></li>" +
                                                       "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false)  code='btwn' >Between</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='sw' >StartsWith</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ew' >EndsWith</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='et' >EqualTo</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='net' >NotEqualTo</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='gt' >GreaterThan</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='gtet' >GreaterThanOrEqualTo</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='lt' >LessThan</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ltet' >LessThanOrEqualTo</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='inl' >IsNull</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ninl' >NotIsNull</a></li>" +
                                                  "  </ul>" +
                                                "</div>";


var floatColumn = " <span class='pull-left'><a   href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true) class='headerName'>[@columnFriendlyName]</a></span>" +
                                   "<div class='btn-group pull-right sortGrid' >" +

                                                      "  <i  class='dropdown-toggle fa fa-fw fa-caret-up ' data-toggle='dropdown'></i>" +
                                                       " <ul class='dropdown-menu'  role='menu' type='sort'>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true) sortDirection=''>NoSort</a></li>" +
                                                        "    <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='asc' >SortAsc</a></li>" +
                                                         "   <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='desc' >SortDesc</a></li>" +
                                                       " </ul>" +
                                                   " </div>" +
                               "  <div class='clearfix'></div>" +
                               " <input type='text' getValue='y'   class='txt65' />" +
                               " <div class='btn-group' >" +
                                        "  <button type='button'  class='dropdown-toggle srchBtn' data-toggle='dropdown'>" +
                                                    "     <i class='fa fa-fw fa-filter'  ></i> " +
                                                   " </button>" +
                                                   " <ul class='dropdown-menu' role='menu' type='filter'>" +
                                                      "  <li><a href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='nf'>NotFilter</a></li>" +
                                                       "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false)  code='in' >In</a></li>" +
                                                       "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false)  code='btwn' >Between</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='et' >EqualTo</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='net' >NotEqualTo</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='gt' >GreaterThan</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='gtet' >GreaterThanOrEqualTo</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='lt' >LessThan</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ltet' >LessThanOrEqualTo</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='inl' >IsNull</a></li>" +
                                                      "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ninl' >NotIsNull</a></li>" +
                                                  "  </ul>" +
                                                "</div>";

var stringColumn = " <span class='pull-left'><a   href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  class='headerName'>[@columnFriendlyName]</a></span>" +
                                 "  <div class='btn-group pull-right sortGrid'   >" +
                                                   "     <i  class='dropdown-toggle fa fa-fw fa-arrows-v ' data-toggle='dropdown'></i>" +
                                                    "    <ul class='dropdown-menu' role='menu'  type='sort'>" +
                                                    "      <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true) sortDirection=''>NoSort</a></li>" +
                                                       "     <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='asc'>SortAsc</a></li>" +
                                                      "      <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='desc' >SortDesc</a></li>" +
                                                      "  </ul>" +
                                                   " </div>" +
                                " <div class='clearfix'></div>" +
                               " <input type='text' getValue='y' class='txt100' />" +
                              "  <div class='btn-group'  >" +
                                            "  <button type='button'  class='dropdown-toggle srchBtn' data-toggle='dropdown'>" +
                                                            " <i class='fa fa-fw fa-filter'  ></i> " +
                                                      "  </button>" +
                                                       " <ul class='dropdown-menu' role='menu'  type='filter'>" +
                                                        "    <li><a href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='nf'>NotFilter</a></li>" +
                                                        "    <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='c' selected='y'>Contains</a></li>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false)  code='dnt' >DoesNotContain</a></li>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='sw' >StartsWith</a></li>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ew' >EndsWith</a></li>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ets' >EqualTo</a></li>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='net' >NotEqualTo</a></li>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ie' >IsEmpty</a></li>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='nie' >NotIsEmpty</a></li>" +
                                                   "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='inl' >IsNull</a></li>" +
                                                        "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='ninl' >NotIsNull</a></li>" +
                                                        "</ul>" +
                                                    "</div>";


var datesColumn = " <span class='pull-left'><a   href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  class='headerName'>[@columnFriendlyName]</a></span>" +
                                   "<div class='btn-group pull-right sortGrid' >" +
                                    "                    <i  class='dropdown-toggle fa fa-fw fa-arrows-v' data-toggle='dropdown'></i>" +
                                     "                   <ul class='dropdown-menu' role='menu'  type='sort'>" +
                                      "                      <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true) sortDirection=''>NoSort</a></li>" +
                                                          "  <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='asc'>SortAsc</a></li>" +
                                                         "   <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='desc' >SortDesc</a></li>" +
                                                       " </ul>" +
                                                  "  </div>" +
                                " <div class='clearfix'></div>" +
                                 " <input type='text' class='txt65 pull-left startDate'  id='txt[@column]Start1' onclick=displayCalendar(this.id,'mm/dd/yyyy',this) /> " +
                                   "   <button type='button' class='srchBtn' isSearchBtn='y' onclick=sortGrid('[@gridId]',this,false) >  <i class='fa fa-fw fa-search'  ></i>   </button>" +
                                 " <div style='clear:both;height:2px'></div>" +
                               "  <input type='text'  id='txt[@column]End1'  class='txt65  pull-left endDate'  onclick=displayCalendar(this.id,'mm/dd/yyyy',this)  /> ";


var boolColumn = " <span class='pull-left'><a   href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  class='headerName'>[@columnFriendlyName]</a></span>" +
                                  " <div class='btn-group pull-right sortGrid'  >" +
                                                   "     <i  class='dropdown-toggle fa fa-fw fa-arrows-v' data-toggle='dropdown'></i>" +
                                                    "    <ul class='dropdown-menu' role='menu' type='sort'>" +
                                                     "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection=''>NoSort</a></li>" +
                                                      "      <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='asc'>SortAsc</a></li>" +
                                                       "     <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='desc' >SortDesc</a></li>" +
                                                     "   </ul>" +
                                                  "  </div>" +
                              "  <div class='clearfix'></div>" +
                                 "<input type='checkbox'  getValue='y' /> " +
                                  "<div class='btn-group'   >  " +
                                  "  <button type='button'  class='dropdown-toggle srchBtn' data-toggle='dropdown'>" +
                                                           "  <i class='fa fa-fw fa-filter'  ></i> " +
                                                        "</button>" +
                                                       " <ul class='dropdown-menu' role='menu'   type='filter' >" +
                                                          "  <li><a href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='nf' selected='y'>NotFilter</a></li>" +
                                                          "  <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false)  code='et'>EqualTo</a></li>" +
                                                          "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,false) code='net'>NotEqualTo</a></li>" +
                                                        "</ul>" +
                                  " </div>     ";


var ddlColumn = "    <span class='pull-left'><a   href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  class='headerName'>[@columnFriendlyName]</a></span>" +
                                  " <div class='btn-group pull-right sortGrid'>" +
                                             "           <i  class='dropdown-toggle fa fa-fw fa-arrows-v ' data-toggle='dropdown'></i>" +
                                                     "   <ul class='dropdown-menu' role='menu' type='sort' >" +
                                                      "    <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true) sortDirection=''>NoSort</a></li>" +
                                                        "    <li  ><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='asc'>SortAsc</a></li>" +
                                                          "  <li><a  href='javascript:void(0)' onclick=sortGrid('[@gridId]',this,true)  sortDirection='desc' >SortDesc</a></li>" +
                                                       " </ul>" +
                                                   " </div>" +
                                  "<div class='clearfix'></div>" +
                                  " <select id='ddl_[@gridId]_[@column]' multiple='multiple' style='float:left' parentDdl='[@parentDdl]'> </select>" +
                                    "<button type='button' class='srchBtn'  isSearchBtn='y' style='float:left' onclick=sortGrid('[@gridId]',this,false)   >  <i class='fa fa-fw fa-search'  ></i>   </button>";


var ddlPaging = "<div class='dataTables_length'><label style='padding-left: 20px;'><select  id='ddlPaging_[@gridId]' onchange=lbtnNextPrev_Command('[@gridId]','PageSizeChange',1)>" +
                                            " <option value='10'>10</option>" +
                                           "  <option value='20'>20</option>" +
                                           "  <option value='25'>25</option>" +
                                           "  <option value='50'>50</option>" +
                                           "  <option value='100'>100</option>" +
                                        " </select></label></div>";


var tdFooter = "<div class='row'>" +
                        "<div class='col-md-12'>" +
                            "<div class='pull-left'>" +
                               " <div class='dataTables_paginate paging_bootstrap'>" +
                                  "  <ul class='pagination' id='pagesGenerated'>" +
                                    "</ul>" +
                                "</div>" +
                            "</div>" +
                           " <div class='pull-left'  divPagingDDL  style='padding-top: 8px'>" +
                           "</div>" +
                            "<div class='dataTables_info pull-left' id='[@gridId]_totalRecordsInfo'>" +
                                "No records found.</div>" +
                       " </div>" +
                   " </div>";





Date.prototype.getWeekGrdVMR = function (start) {
    //Calcing the starting point
    start = start || 0;
    var today = new Date(this.setHours(0, 0, 0, 0));
    var day = today.getDay() - start;
    var date = today.getDate() - day;

    // Grabbing Start/End Dates
    var StartDate = new Date(today.setDate(date));
    var EndDate = new Date(today.setDate(date + 6));
    return [tsest(StartDate), tsest(EndDate)];
}



function tsest(datesss) {
    var today = new Date(datesss);
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    return mm + '/' + dd + '/' + yyyy;

}





function addDaysInDateInGrdVMR(datePassed, daysToAdd) {
    if (datePassed != "") {
        var dt = new Date(datePassed);
        var now = new Date(datePassed);
        var year = now.getFullYear();
        var month = now.getMonth() + 1;
        var day = now.getDate();
        var hour = now.getHours();
        var minute = now.getMinutes();
        var second = now.getSeconds();
        var amPm = "AM";
        if (hour >= 12) {
            amPm = "PM";
            hour = hour - 12;
        }
        if (month.toString().length == 1) {
            var month = '0' + month;
        }
        if (day.toString().length == 1) {
            var day = '0' + day;
        }
        if (hour.toString().length == 1) {
            var hour = '0' + hour;
        }
        if (minute.toString().length == 1) {
            var minute = '0' + minute;
        }
        if (second.toString().length == 1) {
            var second = '0' + second;
        }
        var dateTime = month + '/' + day + '/' + year;
        return dateTime;
    }
    else
    { return datePassed; }
}