var pagesToBeDisplayed = 9;
var defaultpageSize = 25;


 
 
function loadDataForGridView(path, tblId) {
    if (typeof tblId == "undefined") {
        tblId = 'tblGrid';
    }
    $("[content]").html("");
    $("[content]").css({ "min-height": "0", "display": "none" });
 
    if (window === top) { // no Parent
         $.get(path, function (data) {
            $("#divGridView").html(data);
            loadFilterSettings(1, tblId);
        });
    }
    else {
        var tabToHighlight = $(".treeview-menu > li > a.active", window.parent.document).attr("id");
        if ($("hide[for=" + tabToHighlight + "]", window.parent.document).length == 0) {
            $("#leftMenusLiPersonal", window.parent.document).append(" <hide for='" + tabToHighlight + "'></hide>")
         }

        var data = $.trim($("hide[for=" + tabToHighlight + "]", window.parent.document).html());
        if (data.length > 10) {
            $("#divGridView").html(data);
            loadFilterSettings(1, tblId);
        }
        else {
            $.get(path, function (data) {
                $("#divGridView").html(data);
                $("hide[for=" + tabToHighlight + "]", window.parent.document).html(data)
                loadFilterSettings(1, tblId);
            });
        }
    }

   
}

function loadFilterSettings(currentPage, tblId) {
    if ($("[fortbl='" + tblId + "']").length > 0) {
        var serviePage = $.trim($("[fortbl=" + tblId + "]").attr("service"));
        var gridId = $("#" + tblId).attr("gridId");
        callServiceMethod("{'pageName':'" + gridId + "'}", serviePage, "getFilterPrest", "", "", "", false, "", true, loadFilterSettings_Scs, tblId, "", "");
    }
    else {
        bindGrid(1, tblId);
    }
}

function loadFilterSettings_Scs(tblId, p2, p3, data) {
    bindDdlWithData("ddlSelectFilter", data, "id", "name", false, "", "");
    var arr = filterArray(data, { status: true });
    var valSelected = getControlValue("ddlSelectFilter");

    $("#" + tblId).attr("currentColumnOrder", data[0].descr);
    $("#" + tblId).attr("defaultColumnOrder", data[0].descr);
    $("#" + tblId).attr("originalColumnOrder", "");

    if (arr.length > 0) {
        $("#" + tblId).attr("currentcolumnorder", arr[0].descr);
        setControlValue("ddlSelectFilter", arr[0].id);
        $("div[fortbl=" + tblId + "] [btnDelete]").removeClass("hide");
        $("div[fortbl=" + tblId + "] span[filterSetting]").html(arr[0].descr2);
        $("div[fortbl=" + tblId + "] input[txtfiltername]").val(arr[0].name);
        $("div[fortbl=" + tblId + "] input[type='checkbox']").prop('checked', arr[0].status);
        checkUncheckModifedChecRadiokBoxesGrd();
    }
    bindGrid(1, tblId);

}


function bindGrid(currentPage, tblId) {
    var areTableHeaderGenerated = $.trim($("#" + tblId).attr("areTableHeaderGenerated"));
    if (areTableHeaderGenerated == "n") {
        var tdFooter1 = tdFooter.replaceAll("[@gridId]", tblId);
        $("#" + tblId + " [tdFooter]").html(tdFooter1);

        // var pageSize = $.trim($("#"+tblId).attr("pageSize"));
        var ddlPage = ddlPaging.replaceAll("[@gridId]", tblId);
        $("#" + tblId + " tfoot div[divPagingDDL]").html(ddlPage);
        defaultpageSize = $("#" + tblId).attr("pageSize");
        if ($("div [fortbl='" + tblId + "']").length == 0) {
            $("#" + tblId).attr("currentColumnOrder", "");
            $("#" + tblId).attr("defaultColumnOrder", "");
            $("#" + tblId).attr("originalColumnOrder", "");
        }

        $("#" + tblId + " tfoot  #ddlPaging_" + tblId).val(defaultpageSize);

        $("#" + tblId + " thead tr[role='row']  th").each(function () {

            var type = $.trim($(this).attr("type"));
            var shallIncludeFilter = true;
            if (typeof $(this).attr("nofilter") != "undefined") {
                shallIncludeFilter = false;
            }

            var innerHtml = "";
            if (type == "int") {
                innerHtml = intColumn;
            }
            if (type == "float") {
                innerHtml = floatColumn;
            }
            else if (type == "string") {
                innerHtml = stringColumn;
            }
            else if (type == "dates") {
                innerHtml = datesColumn;
            }
            else if (type == "bool") {
                innerHtml = boolColumn;
            }
            else if (type == "ddl") {
                innerHtml = ddlColumn;
            }

            var column = $(this).attr("column");
            $(this).attr("id", tblId + "_" + column);

            if (shallIncludeFilter) {
                innerHtml = innerHtml.replaceAll("[@columnFriendlyName]", $(this).attr("columnFriendlyName"));
                innerHtml = innerHtml.replaceAll("[@gridId]", tblId);
                innerHtml = innerHtml.replaceAll("[@column]", column);
            }
            else {
                innerHtml = $(this).attr("columnFriendlyName");
            }

            $(this).html(innerHtml);

            var attrs = "";
            if (typeof $(this).attr("formatCurrency") != 'undefined') {
                attrs = attrs + " formatCurrency ";
            }
            if (typeof $(this).attr("tdFooterClass") != 'undefined') {
                attrs = attrs + " class='" + $(this).attr("tdFooterClass") + "' ";
            }
            if (typeof $(this).attr("parentddlid") != 'undefined') {
                var parentDDL = $(this).attr("parentddlid");
                innerHtml = innerHtml.replaceAll("[@parentDdl]", parentDDL);
            }
            if (typeof $(this).attr("loadDefaultDate") != 'undefined') {
                var startDateTxtId = $(this).attr("startDateTxtId");
                var endDateTxtId = $(this).attr("endDateTxtId");
                $("#" + this.id + " .startDate").val($("#" + startDateTxtId).val());
                $("#" + this.id + " .endDate").val($("#" + endDateTxtId).val());
            }

            //Load default values
            if (type == "dates") {
                if (typeof $(this).attr("defaults") != 'undefined') {
                    var defaults = $(this).attr("defaults").toLowerCase();
                    switch (defaults) {
                        case "currentweek":
                            var dates = new Date().getWeekGrdVMR();

                            //                            $("#" + this.id + " .startDate").val(dates[0].toLocaleDateString());
                            //                            $("#" + this.id + " .endDate").val(dates[1].toLocaleDateString());
                            $("#" + this.id + " .startDate").val(dates[0]);
                            $("#" + this.id + " .endDate").val(dates[1]);
                            break;
                        case "currentmonth":
                            var date = new Date();
                            var firstDay = tsest(new Date(date.getFullYear(), date.getMonth(), 1));
                            var lastDay = tsest(new Date(date.getFullYear(), date.getMonth() + 1, 0));
                            $("#" + this.id + " .startDate").val(firstDay);
                            $("#" + this.id + " .endDate").val(lastDay);
                            break;
                    }
                }
            }

            if (type == "string" || type == "int" || type == "float") {
                if (typeof $(this).attr("value") != 'undefined') {
                    $("#" + this.id + " input[getvalue='y']").val($(this).attr("value"));
                }
            }


            $("#" + tblId + " .trFooterSum").append("<td " + attrs + " id='tdFooter_" + tblId + "_" + column + "'></td>");
            $("#" + tblId + " thead th[column='" + column + "']").attr("data-header", column);




            //            if ($(this).attr("formatCurrency")) {
            //                $("#" + tblId + " .trFooterSum").append("<td id='tdFooter_" + tblId + "_" + column + "'></td>");
            //            }
            //            else {
            //                $("#" + tblId + " .trFooterSum").append("<td  formatCurrency id='tdFooter_" + tblId + "_" + column + "'></td>");
            //            }
            if (type == "ddl") {
                var ddlId = "ddl_" + tblId + "_" + column;
                if (typeof $(this).attr("parentDdlId") != "undefined") {
                    var parentDdlId = $(this).attr("parentDdlId");
                    parentDdlId = $("[" + parentDdlId + "]").attr("id");
                    createDDLFromAnotherDDLShowSelected(ddlId, parentDdlId, false);
                }
                else if (typeof $(this).attr("xmlPath") != "undefined") {
                    var xmlPath = $(this).attr("xmlPath");
                    var mainNode = $(this).attr("mainNode");
                    var DataValueField = $(this).attr("valueField");
                    var DataTextField = $(this).attr("textField");
                    loadDDLFromXMLDocument(xmlPath, ddlId, mainNode, DataValueField, DataTextField, false, '0', '', true, true);
                }
                else if (typeof $(this).attr("data") != "undefined") {
                    var datas = $(this).attr("data");
                    datas = JSON.parse(datas);
                    var dataValueField = $(this).attr("valueField");
                    var dataTextField = $(this).attr("textField");
                    bindDdlWithData(ddlId, datas, dataValueField, dataTextField, false, "", "");
                }
                $("#" + ddlId).multiselect({
                    includeSelectAllOption: true
                });
            }

            if (typeof $(this).attr("txtWidth") != 'undefined') {
                var wdth = $(this).attr("txtWidth").trim();
               // alert(this.id)
                 $("#"+this.id + " [getvalue=y]").css({ "width": wdth + "px" })
            }


            var allowDrag = $("#" + tblId).attr("allowDrag");
            if (allowDrag == 'y') {
                $(this).append("<div class='dragtable-drag-handle'><i class='fa fa-th'></i></div>");
            }
        });
        $("#" + tblId).attr("areTableHeaderGenerated", "y");
        loadFilterSetting(tblId);

        //Load Default Values
        $("#" + tblId + " thead th").each(function () {
            var type = $(this).attr("type");
            if (type == "string" || type == "int" || type == "float") {
                if (typeof $(this).attr("value") != 'undefined') {
                    $("#" + this.id + " input[getvalue='y']").val($(this).attr("value"));
                    if (typeof $(this).attr("operator") != 'undefined') {
                        //   $("#" + this.id + " input[getvalue='y']").val($(this).attr("operator"));
                        $("#" + this.id + " ul[type='filter'] a").removeAttr("selected");
                        $("#" + this.id + " ul[type='filter'] a[code='" + $(this).attr("operator") + "']").attr("selected", "y");
                    }
                }
            }
        });

 
        var ddlLen = $("div[fortbl='" + tblId + "'] [ddlallfilters]");
        if (ddlLen.length > 0) {
            if (ddlLen[0].selectedIndex == 0) {
                $("div[fortbl=" + tblId + "] [btnDelete]").addClass("hide");
            }
        }

        var canChangePaging = $("#" + tblId).attr("canChangePaging");
        if (typeof canChangePaging != "undefined" && canChangePaging == "n") {
            $("#" + tblId + " #ddlPaging_" + tblId).parent().css("display", "none");
        }
        if (typeof $("#" + tblId).attr("canChangePageSize") != "undefined") {
            if ($("#" + tblId).attr("canChangePageSize") == "n") {
                $("#" + tblId + " .divpagingddl").addClass("hide");
            }
        }


        var colsCount = $('#' + tblId + " thead tr th").length;
        $('#' + tblId + " tfoot tr td[tdFooter]").attr("colspan", colsCount);
        if ($('#' + tblId + " thead th[sum='y']").length == 0) {
           // $('#' + tblId + " [divpagingddl]").addClass("hide");
        }

        var allowDrag = $("#" + tblId).attr("allowDrag");
        if (allowDrag == 'y') {
            $('#' + tblId).dragtable();
            $('#' + tblId).attr("originalcolumnorder", $('#' + tblId).dragtable('order'));
            if ($("#" + tblId).attr("currentColumnOrder") == "" || $("div [fortbl='" + tblId + "']").length == 0) {
                $("#" + tblId).attr("currentColumnOrder", $('#' + tblId).dragtable('order'));
                $("#" + tblId).attr("defaultColumnOrder", $('#' + tblId).dragtable('order'));
            }
        }
    }
    else {
     var allowDrag = $("#" + tblId).attr("allowDrag");
     if (allowDrag == 'y') {
         $('#' + tblId).attr("currentcolumnorder", $('#' + tblId).dragtable('order'));
     }
    }
     var allowDrag = $("#" + tblId).attr("allowDrag");
     if (allowDrag == 'y') {
         var originalColumnOrder = $('#' + tblId).attr("originalColumnOrder");
         var order = originalColumnOrder.split(',');
         originalColumnOrder = '"' + originalColumnOrder.replaceAll(",", '","') + '"';
         var originalColumnOrderSplit = originalColumnOrder.split(",");
         $('#' + tblId).dragtable('order', originalColumnOrderSplit);
     }

    var pageSize = $("#" + tblId + " .dataTables_length select").val();
    var shallCountpagesAgain = ($("#" + tblId).attr("calculatepagesAgain") == "y") ? true : false;

    //Starts:fetchdataFromDatabase
    var _startIndex = (((currentPage - 1) * pageSize) + 1);
    var _lastIndex = (currentPage * pageSize);


//    var accountNumber = "";
//    var contactNumber = "";
//    if (contactNumber == "" || contactNumber == "0") {
//        contactNumber = $.trim($("[content] [contactNumber]").html());
//    }
//    if (contactNumber == "" || contactNumber == "0") {
//        accountNumber = $.trim($("#divGridView .trSelected [accountNumber]").html());
//        if (accountNumber == "") {
//            //accountNumber = $.trim($("[content] [accountNumber]").html());
//            accountNumber = getControlValueByAttr("", "[content] [accountNumber]");
//        }
//        if (accountNumber == "") {
//            //accountNumber = $.trim($(".topLeft > [accountNumber]").html());
//            accountNumber = getControlValueByAttr("", ".topLeft > [accountNumber]");
//        }
//        if (accountNumber == "" || accountNumber == "0")
//        {var i = 10;
//         }
//        else {
//            var j = 20;  // account number
//           // $("#" + tblId + " [column=AccountNumber] [getvalue=y]").val(accountNumber)
//            $("#" + tblId + " [column=AccountNumber] [getvalue=y]").val(accountNumber)
//            $("#" + tblId + " ul[type=filter] a").removeAttr("selected");
//            $("#" + tblId + " ul[type=filter] a[code=et]").attr("selected", "selected")
//        }
//    }
//    else {
//        //showMdlLoadIframe('Sitepages/SM/Opportunity.aspx?contactNumber=' + contactNumber, 'Add Opportunity', 550, 770);
//        var k = 20;  // contact number
//    }
 

    var array = makeSearchCond(tblId);
    highlightFilteredColumn(tblId, array);
    var newSortColumn = $.trim($("#" + tblId).attr("newSortColumn"));
    var newSortDirection = $("#" + tblId).attr("newSortDirection");
    var service = $("#" + tblId).attr("service");
    var webMethod = $("#" + tblId).attr("method");
    var datas = "{'searchCond':" + JSON.stringify(array) + ",'startIndex':" + _startIndex + ",'lastIndex':" + _lastIndex + ",'newSortColumn':'" + newSortColumn + "','newSortDirection':'" + newSortDirection + "'}";
    var jsTempleteToCall = $("#" + tblId + " script[type='text/html']").attr("id");
    var jsTempleteToAppendInTbody = $("#" + tblId + " tbody").attr("id");
    callJTemplateServiceMethodURL(datas, service, webMethod, "", jsTempleteToAppendInTbody, jsTempleteToCall, true, bindGrid_Scs, true, tblId, shallCountpagesAgain);
    //    callJTemplateServiceMethodURL(datas, service, webMethod, "",
    //            "tblShowSoTaskList", "tblShowSoTaskListTempleteJS", true, bindGrid_Scs, true, tblId, shallCountpagesAgain);
}

function bindGrid_Scs(findRowsGenerated, tblId, shallCountpagesAgain) {
    hideProcess(); 

    _formatGridData(tblId)
    if (!shallCountpagesAgain) {
        generatePages(tblId);

    }
    else {
        if (findRowsGenerated) {
            var sumArray = makeSumCondition(tblId)
            var service = $("#" + tblId).attr("service");
            var webMethod = $("#" + tblId).attr("methodCount");
            callServiceMethod("{'searchCond':" + JSON.stringify(makeSearchCond(tblId)) + ",'sumCond':" + JSON.stringify(sumArray) + "}", service, webMethod, "", "", "",
                     false, "", true, bindGrid_Scs_Scs, tblId, "", "");
        }
        else {
            generatePages(tblId);
        }
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

    $("#" + tblId + " tbody td[formatCurrency]").each(function () {
        var values = $(this).html();
        if (typeof $("#" + tblId + " tbody td[nd]") == "undefined") {
            values = ($.trim(values)).formatMoney('$ ', 2, ',', '.');
        }
        else {
            values = ($.trim(values)).formatMoney('$', 2, ',', '.');
        }
        $(this).html(values);
    });

    $("#" + tblId + " tbody td [formatCurrency]").each(function () {
        var values = $(this).html();
        if (typeof $("#" + tblId + " tbody td [nd]") == "undefined") {
            values = ($.trim(values)).formatMoney('$ ', 2, ',', '.');
        }
        else {
            values = ($.trim(values)).formatMoney('$', 2, ',', '.');
        }
        $(this).html(values);
    });

    $("#" + tblId + " tbody td[formatDateTime]").each(function () {
        var formatDateTime = $(this).attr("formatDateTime");
        var values = $(this).html();
        var values2 = values.formatDateTimeWithParameter(formatDateTime);
        $(this).html(values2);
    });
    
    $("#" + tblId + " tbody td [formatDateTime]").each(function () {
        var formatDateTime = $(this).attr("formatDateTime");
        var values = $(this).html();
        var values2 = values.formatDateTimeWithParameter(formatDateTime);
        $(this).html(values2);
    });
}


function bindGrid_Scs_Scs(tblId, parameter2, parameter3, data) {
    var datas = data.toString().split('***');
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
    generatePages(tblId);

    $("#" + tblId + " tfoot td[formatCurrency]").each(function () {
        var values = $(this).html();
        if ($.trim(values) != "") {
            values = ($.trim(values)).formatMoney('$ ', 2, ',', '.');
            $(this).html(values);
        }
    });


}
function clearAllFilter(tblId) {
    $("#" + tblId).attr("calculatepagesAgain", "y");
    $("#" + tblId).attr("currentpage", "1")
    $("#" + tblId + " thead input[type=text]").val("");
    $("#" + tblId + " thead input[type=checkbox]").prop('checked', false);
    $("#" + tblId + " thead th[type=bool] .btn-group ul.dropdown-menu a").removeAttr('selected');
    $("#" + tblId + " thead th[type=bool] .btn-group ul.dropdown-menu a[code='nf']").attr('selected', 'y');
    $("#" + tblId + " thead th[type=bool] .btn-group ul.dropdown-menu a[code='nf']").attr('selected', 'y');
    $("#" + tblId + " thead tr[role='row']  th").each(function () {
        var headerType = $("#" + this.id).attr("type");
        if (headerType == "ddl") {
            var columnName = $("#" + this.id).attr("column");
            var ddlId = "ddl_" + tblId + "_" + columnName;
            var dataarray = new Array();
            $("#" + ddlId).val(dataarray);
            $('#' + ddlId).multiselect("refresh");
        }
    });
    bindGrid(1, tblId);
}


function exportGrid(tblId,isExcel) {
    var array = makeSearchCond(tblId);
    var newSortColumn = $.trim($("#" + tblId).attr("newSortColumn"));
    var newSortDirection = $("#" + tblId).attr("newSortDirection");

    var service = $("#" + tblId).attr("service");
    var webMethod = $("#" + tblId).attr("exportMethod");

    if (typeof isExcel !== "undefined") {
        webMethod = webMethod + "ToExcel";
    }

    var colsToExportArr = new Array();
    var i = 1;
    var colsToExport = 8;
    if (typeof $("#" + tblId).attr("colsToExport") != "undefined")
    { colsToExport = $("#" + tblId).attr("colsToExport"); }
    $("#" + tblId + " thead> tr th").each(function () {
        if (i <= colsToExport) {
            var shallFormatCurrency = false;
            if (typeof $(this).attr("formatcurrency") != "undefined") {
                shallFormatCurrency = true;
            }
            var obj = new Object();
            obj.name = $(this).attr("column");
            obj.descr = $(this).attr("columnfriendlyname");
            obj.descr2 = $(this).attr("ew");
            obj.formatCurrency = shallFormatCurrency;
            if ($(this).attr("sum")) { obj.status = true; }
            else { obj.status = false; }
            obj.price1_str = $.trim($("#tdFooter_" + tblId + "_" + obj.name).html());
            colsToExportArr.push(obj);
            i = parseInt(i) + 1;
        }
    });
    var datas = "{'searchCond':" + JSON.stringify(array) + ",'newSortColumn':'" + newSortColumn + "','newSortDirection':'" + newSortDirection + "','totalRows':" + $("#" + tblId).attr("totalrecords") + ",'colsToExport':" + JSON.stringify(colsToExportArr) + "}";
    //    callServiceMethod(datas, service, webMethod, test, "", "",
    //                        false,"", false,
    //                         "", "", "", "");
    $.ajax({
        type: "POST",
        url: service + "/" + webMethod,
        data: datas,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#ancDownloadFile").attr("href", "../df.ashx?FileName=" + data);
            document.getElementById('ancDownloadFile').click();

        },
        error: OnError
    });

}
 

function test(p1, p2, p3, p4) {

}

function lbtnNextPrev_Command(tblId, command, pageNo) {
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
    bindGrid(currentPage, tblId);
}


function sortGrid(tblId, thiss, isByHeader) {
    if (!isByHeader) {
        var filterType = $(thiss).attr("code");
        if (filterType == "nf") {
            var columnType = $(thiss).closest("th").attr("type");
            if (columnType == "bool") {
                $(thiss).closest("th").find("input[getvalue=y]").prop("checked", false);
            }
            else {
                $(thiss).closest("th").find("input[getvalue=y]").val("");
            }
        }
        $(thiss).closest("ul").find("a").removeAttr("selected");
        if ($(thiss).attr("isSearchBtn"))
        { }
        else {
            $(thiss).attr("selected", "y");
        }
        $("#" + tblId).attr("calculatepagesAgain", "y");
        $("#" + tblId).attr("currentpage", "1")
    }


    var newSortColumn = "";
    var newSortDirection = "";
    var prevSortColumn = $("#" + tblId + " th[sorted='y']").attr("column");
    var prevSortDirection = $("#" + tblId).attr("newSortDirection");

    $(".sortGrid ul  a").removeAttr("selected");
    $("#" + tblId + " th").removeAttr("sorted");
    $("#" + tblId + " th").removeClass("currentSort");
    $("#" + tblId + " th i.dropdown-toggle").removeClass("fa-arrows-v");
    $("#" + tblId + " th i.dropdown-toggle").removeClass("fa-caret-up");
    $("#" + tblId + " th i.dropdown-toggle").removeClass("fa-caret-down");
    $("#" + tblId + " th i.dropdown-toggle").addClass("fa-arrows-v");



    if (isByHeader) {
        var newSortColumn = $(thiss).closest("th").attr("column");
        if (prevSortColumn == newSortColumn) {
            switch (prevSortDirection) {
                case "asc":
                    newSortDirection = "";
                    $("#" + tblId + " th[column='" + newSortColumn + "']   i.dropdown-toggle").addClass("fa-arrows-v");
                    break;
                case "desc":
                    $("#" + tblId + " th[column='" + newSortColumn + "']   i.dropdown-toggle").addClass("fa-caret-up");
                    newSortDirection = "asc";
                    break;
                case "":
                    $("#" + tblId + " th[column='" + newSortColumn + "']   i.dropdown-toggle").addClass("fa-caret-down");
                    newSortDirection = "desc";
                    break;
                default:
                    $("#" + tblId + " th[column='" + newSortColumn + "']   i.dropdown-toggle").addClass("fa-caret-down");
                    newSortDirection = "desc";
                    break;
            }
        }
        else {

            $("#" + tblId + " th[column='" + newSortColumn + "']   i.dropdown-toggle").addClass("fa-caret-down");
            newSortDirection = "desc";
        }
    }
    else {
        newSortColumn = $("#" + tblId).attr("newSortColumn");
        newSortDirection = $("#" + tblId).attr("newSortDirection");
    }

    $("#" + tblId + " th[column='" + newSortColumn + "']").addClass("currentSort");
    $("#" + tblId + " th[column='" + newSortColumn + "']").attr("sorted", "y");
    $("#" + tblId + " th[column='" + newSortColumn + "'] a[sortDirection='" + newSortDirection + "']").attr("selected", "y");
    $("#" + tblId).attr("newSortDirection", newSortDirection);
    $("#" + tblId).attr("newSortColumn", newSortColumn);

    bindGrid($("#" + tblId).attr("currentPage"), tblId);
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

    $("#" + tblId + " thead th").each(function () {
        var shallFormatCurrency = false;
        if (typeof $(this).attr("formatcurrency") != "undefined") {
            shallFormatCurrency = true;
        }
        var columnNameFriendly = $(this).attr("columnfriendlyname");
        var columnName = $(this).attr("column");
        var columnType = $(this).attr("type");
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
            values = $("#" + this.id + " input[getValue='y']").val();
            var filterType = "";
            if (!$("#" + this.id + " ul[type='filter'] a[selected='y']").attr("code")) {
                filterType = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code");
            }
            else {
                filterType = $("#" + this.id + " ul[type='filter'] a[selected='y']").attr("code");
            }
            if (filterType == "ninl" || filterType == "inl" || filterType == "ie" || filterType == "nie")
                values = "isNull";
            if (values != "") {
                obj.data = values;
                obj.columnName = columnName;
                obj.columnNameFriendly = columnNameFriendly;
                obj.columnType = columnType;
                obj.formatCurrency = shallFormatCurrency;
                obj.filterType = $("#" + this.id + " ul[type='filter'] a[selected='y']").attr("code");
                if (!obj.filterType)
                { obj.filterType = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code"); }
                searchCondArray.push(obj);
            }
        }
        else if (columnType == "ddl" && shallIncludeFilter == true) {
            values = "";
            var ddlId = $("#" + this.id + " select").attr("id");
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
            var startDate = $("#" + this.id + " .startDate").val();
            var endDate = $("#" + this.id + " .endDate").val();
//            endDate = addDaysInDateInGrdVMR(endDate, 1);
//            alert(endDate+"ebd");
            if (startDate != "" || endDate != "") {
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
    if (typeof $("#" + tblId).attr("shallIncludeExternalDates") != "undefined") {

        var values = "";
        var startDate = $("#" + tblId).attr("startDateId");
        var endDate = $("#" + tblId).attr("endDateId");
        startDate = $.trim($("#" + startDate).val())
        endDate = $.trim($("#" + endDate).val())
        if (startDate != "") {
            var obj = new Object();
            obj.data = startDate;
            obj.columnName = $("#" + tblId).attr("externalDateColumn");
 
            obj.filterType = "startDate";
            obj.columnType = "externalDates";
            obj.formatCurrency = false;
            searchCondArray.push(obj);
        }
        if (endDate != "") {
            var obj = new Object();
            obj.data = endDate;
            obj.columnName = $("#" + tblId).attr("externalDateColumn");
 
            obj.filterType = "endDate";
            obj.columnType = "externalDates";
            obj.formatCurrency = false;
            searchCondArray.push(obj);
        }
    }
    return searchCondArray;
}

function highlightFilteredColumn(tblId, searchCondArray) {
    $("#" + tblId + " th").removeClass("thFilteredData")
    for (var i in searchCondArray) {
        var columnName = searchCondArray[i].columnName;
        $("#" + tblId + " th[column=" + columnName+"]").addClass("thFilteredData")
    }
}




function generatePages(tblId) {
    if (typeof $("#" + tblId).attr("pagesToBeDisplayed") != "undefined") {
        pagesToBeDisplayed = $("#" + tblId).attr("pagesToBeDisplayed");
    }
     else {
            pagesToBeDisplayed = 9;
        }
        if ($("div[fortbl='" + tblId + "'] #ddlSelectFilter").length > 0) {

         var allowDrag = $("#" + tblId).attr("allowDrag");
         if (allowDrag == 'y') {

             $('#' + tblId).dragtable({ items: 'thead th:not( .notdraggable ):not( :has( .dragtable-drag-handle ) ), .dragtable-drag-handle' });
             var currentcolumnorder = $.trim($("#" + tblId).attr("currentcolumnorder"));
             if (currentcolumnorder != "") {
                 currentcolumnorder = '"' + currentcolumnorder.replaceAll(",", '","') + '"';
                 $('#' + tblId).dragtable('order', currentcolumnorder.split(","));
             }

             //$('#' + tblId).dragtable('order', ["AccountName", "AccountNumber", "ContactNumber", "ContactName", "PrimaryPhone", "PrimaryEmail", "isActive", "Address1", "Address2", "City", "PostalCode", "Department"])
         }
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
        generatePagesNumber(tblId, pageSize, 1, totalNoOfpages);
    }
    else if (parseInt(currentPage) <= parseInt(addOn)) {
        generatePagesNumber(tblId, pageSize, 1, pagesToBeDisplayed);
    }
    else if ((parseInt(currentPage) + parseInt(addOn)) < totalNoOfpages) {
        generatePagesNumber(tblId, pageSize, (currentPage - addOn), (parseInt(currentPage) + parseInt(addOn)));
    }
    else {
        generatePagesNumber(tblId, pageSize, (totalNoOfpages - pagesToBeDisplayed), totalNoOfpages);
    }

}


function generatePagesNumber(tblId, pageSize, startIndex, maxPageToBeDisplayedAtBottom) {
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



    $("#" + tblId + "_totalRecordsInfo").html("Showing " + _startIndex + " to " + _lastIndex + " of " + totalRecords + " entries");

    $("#" + tblId + " #pagesGenerated").html('');
    if (currentPage == 1) {
        $("#" + tblId + " #pagesGenerated").append(" <li class='prev disabled'><a nextPrev href='javascript:void(0)' ><i class='fa fa-fw fa-fast-backward'></i></a></li>");
        $("#" + tblId + " #pagesGenerated").append(" <li class='prev disabled'><a nextPrev  href='javascript:void(0)' ><i class='fa fa-fw fa-backward'></i></a></li>");
    }
    else {
        $("#" + tblId + " #pagesGenerated").append("<li><a href='javascript:void(0)' nextPrev onclick=lbtnNextPrev_Command('" + tblId + "','',1)  ><i class='fa fa-fw fa-fast-backward'></i></a></li>");
        $("#" + tblId + " #pagesGenerated").append("<li><a href='javascript:void(0)' nextPrev onclick=lbtnNextPrev_Command('" + tblId + "','Prev')  ><i class='fa fa-fw fa-backward'></i></a></li>");
    }

    for (i = startIndex; i <= maxPageToBeDisplayedAtBottom; i++) {
        if (i == currentPage)
        { $("#" + tblId + " #pagesGenerated").append(" <li class='active'><a href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "',''," + i + ") >" + i + "</a></li>"); }
        else { $("#" + tblId + " #pagesGenerated").append(" <li><a href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "',''," + i + ") >" + i + "</a></li>"); }
    }

    if (currentPage == totalNoOfpages) {
        $("#" + tblId + "#pagesGenerated").append(" <li class='prev disabled'><a nextPrev href='javascript:void(0)' ><i class='fa fa-fw fa-forward'></i></a></li>");
        $("#" + tblId + "#pagesGenerated").append(" <li class='prev disabled'><a nextPrev href='javascript:void(0)' ><i class='fa fa-fw fa-fast-forward'></i></a></li>");
    }
    else {
        $("#" + tblId + " #pagesGenerated").append("<li><a nextPrev href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "','Next') ><i class='fa fa-fw fa-forward'></i></a></li>");
        $("#" + tblId + " #pagesGenerated").append("<li><a nextPrev href='javascript:void(0)' onclick=lbtnNextPrev_Command('" + tblId + "',''," + totalNoOfpages + ") ><i class='fa fa-fw fa-fast-forward'></i></a></li>");
    }
}






function makeFilterPresetCond(tblId) {
    var searchCondArray = new Array();

    $("#" + tblId + " thead th").each(function () {
        var columnName = $(this).attr("column");
        var columnType = $(this).attr("type");
        var obj = new Object();

        var values = "";
        if (columnType == "int" || columnType == "float" || columnType == "string") {
            values = $("#" + this.id + " input[getValue='y']").val();
            if (values != "") {
                obj.data = values;
                obj.columnName = columnName;
                obj.columnType = columnType;
                obj.filterType = $("#" + this.id + " ul[type='filter'] a[selected='y']").attr("code");
                if (!obj.filterType)
                { obj.filterType = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code"); }
                searchCondArray.push(obj);
            }
        }
        else if (columnType == "ddl") {
            values = "";
            var ddlId = $("#" + this.id + " select").attr("id");
            var valSelected = $('#' + ddlId).val();
            if (valSelected != null && valSelected.indexOf("multiselect-all") == -1) {

                var len = valSelected.length;
                values = "";
                if (len > 1) {
                    for (i = 0; i < len; i++) {
                        values = values + "," + valSelected[i];
                    }
                    values = values.substring(1);
                }
                else
                    values = valSelected[0];
                var obj = new Object();
                obj.data = values;
                obj.columnName = columnName;
                obj.columnType = columnType;
                obj.filterType = (valSelected.length == 1) ? "et" : "in";
                searchCondArray.push(obj);
            }
        }
        //        else if (columnType == "dates") {
        //            values = "";
        //            var startDate = $("#" + this.id + " .startDate").val();
        //            var endDate = $("#" + this.id + " .endDate").val();

        //            if (startDate != "" || endDate != "") {
        //                var obj = new Object();
        //                values = "";
        //                if (startDate == "") {
        //                    values = "ltetDate"
        //                    obj.data = " <='" + endDate + "'";
        //                }
        //                else if (endDate == "") {
        //                    values = "gtetDate"
        //                    obj.data = " >='" + startDate + "'";
        //                }
        //                else {
        //                    values = "btDate"
        //                    obj.data = "'" + startDate + "' and '" + endDate + "'";
        //                }

        //                obj.columnName = columnName;
        //                obj.filterType = values;
        //                obj.columnType = columnType;
        //                searchCondArray.push(obj);
        //            }
        //        }
        else if (columnType == "bool") {
            var code = $("#" + this.id + " ul[type='filter'] a[selected='y']").attr("code");
            if (!code) { code = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code"); }
            if (code) {
                if (code != "nf") {
                    var obj = new Object();
                    obj.data = ($("#" + this.id + " input[getValue='y']").is(":checked")) ? 1 : 0
                    obj.columnName = columnName;
                    obj.columnType = columnType;
                    obj.filterType = $("#" + this.id + " ul[type='filter'] a[selected='y']").html();
                    { obj.filterType = $("#" + this.id + " ul[type='filter'] a[selected='selected']").attr("code"); }
                    searchCondArray.push(obj);
                }
            }
        }
    })
    var setting = "";
    if (searchCondArray.length > 0) {
        setting = JSON.stringify(searchCondArray) + "*-*-*-*" + $("#" + tblId).attr("newsortcolumn") + " " + $("#" + tblId).attr("newsortdirection");
    }
    return setting;
}


function loadFilterSetting(tblId) {
    $("#" + tblId).attr("calculatepagesagain", "y");
    $("#" + tblId + " thead th").each(function () {
        var columnType = $(this).attr("type");
        var columnName = $(this).attr("column");
        if (columnType == "int" || columnType == "float" || columnType == "string") {
           // $("#" + this.id + " input[getValue='y']").val('');

        }
        else if (columnType == "ddl") {

            //                $("#" + this.id + " select option").each(function () {
            //                    $(this).removeAttr("selected");
            //                })

            var ddlId = "ddl_" + tblId + "_" + columnName;
            var dataarray = new Array();
            $("#" + ddlId).val(dataarray);
            $('#' + ddlId).multiselect("refresh");
        }
        else if (columnType == "bool") {
            $('#' + this.id + " [getvalue=y]").prop('checked', false);
            $('#' + this.id + " ul[type='filter'] a").removeAttr('selected');
            $('#' + this.id + " ul[type='filter'] a[code=nf]").attr('selected', "y");
        }
    })

    $("#" + tblId + " th").removeAttr("sorted");
    $("#" + tblId + " th ul[type=sort] a").removeAttr("selected");



    var filterSettingValue = $("div[forTbl=" + tblId + "] [filterSetting]").html();
    if (filterSettingValue != "" && typeof filterSettingValue != 'undefined') {
        var filterSetting = JSON.parse(filterSettingValue.split('*-*-*-*')[0]);
        var sortOrderDirection = filterSettingValue.split('*-*-*-*')[1];
        var sortOrder = $.trim(sortOrderDirection.split(' ')[0]);
        var sortDirection = $.trim(sortOrderDirection.split(' ')[1]);



        $("#" + tblId + " th[column='" + sortOrder + "']").attr("sorted", "y");
        $("#" + tblId + " th[column='" + sortOrder + "'] ul[type=sort] a[sortdirection='" + sortDirection + "']").attr("selected", "y");

        $("#" + tblId).attr("newSortColumn", sortOrder);
        $("#" + tblId).attr("newSortDirection", sortDirection);


        for (var i in filterSetting) {
            var columnType = filterSetting[i].columnType;
            var filterType = filterSetting[i].filterType;
            var columnName = filterSetting[i].columnName;
            var data = filterSetting[i].data;

            $("#" + tblId + " th[column='" + columnName + "']  [type='filter'] a").removeAttr("selected");

            if (columnType == "int" || columnType == "float" || columnType == "string") {
                $("#" + tblId + " th[column=" + columnName + "] input[type=text]").val(data);
                $("#" + tblId + " th[column='" + columnName + "']  [type='filter'] a[code=" + filterType + "]").attr("selected", "y");

            }
            else if (columnType == "bool") {
                if (data == 1) {
                    $("#" + tblId + " th[column='" + columnName + "']  input[type='checkbox']").prop('checked', true);
                }
                $("#" + tblId + " th[column='" + columnName + "']  [type='filter'] a[code=" + filterType + "]").attr("selected", "y");
            }
            else if (columnType == "ddl") {
                var dataarray = data.split(',');
                var ddlId = "ddl_" + tblId + "_" + columnName;

                //                    $("#" + ddlId + " option").each(function () {
                //                        $(this).removeAttr("selected");
                //                    })


                //                    for (i = 0; i < values.length; i++) {
                //                        $("#" + ddlId + " option[value='" + values[i] + "']").attr("selected", "selected");
                //                    }


                $("#" + ddlId).val(dataarray);

                $('#' + ddlId).multiselect("refresh");
            }

            //                obj.data = values;
            //                obj.columnName = columnName;
            //                obj.columnType = columnType;
            //                obj.filterType
        }
    }
    else // these are for default settings
    {
        var sortOrder = $.trim($("#" + tblId).attr("defaultsort"));
        var sortDirection = $.trim($("#" + tblId).attr("defaultsortdirection"));

        $("#" + tblId).attr("newsortcolumn", sortOrder)
        $("#" + tblId).attr("newsortdirection", sortDirection)


        $("#" + tblId + " th[column='" + sortOrder + "']").attr("sorted", "y");
        $("#" + tblId + " th[column='" + sortOrder + "'] ul[type=sort] a[sortdirection='" + sortDirection + "']").attr("selected", "y");

    }


}






//Starts: Filter Preset Settings


function saveFilterPresetSetting(tblId) {
    if (validateTextBoxes("divFilterSettings")) {
        var filterCond = makeFilterPresetCond(tblId);
        if (filterCond == "") {
            alert("Please select at least 1 filter.")
        }
        else {
            var filterNo = $("div[fortbl=" + tblId + "] select[ddlAllFilters]").val();
            var settingName = $("div[fortbl=" + tblId + "] input[txtFilterName]").val();
            var isDefault = $("div[fortbl=" + tblId + "] input[type=checkbox]").is(":checked");
            var currentcolumnorder = ""
             var allowDrag = $("#" + tblId).attr("allowDrag");
             if (allowDrag == 'y') {
                 currentcolumnorder = $("#" + tblId).dragtable('order');
             }
            var datas = "{'FilterNo':" + filterNo + ",'gridId':'" + $("#" + tblId).attr("gridId") + "','settingName':'" + settingName + "','settingCond':'" + filterCond +
                        "','isDefault':" + isDefault + ",'currentcolumnorder':'" + currentcolumnorder + "'}";
            callServiceMethod(datas, $("div[fortbl=" + tblId + "]").attr("service"), $("div[fortbl=" + tblId + "]").attr("filterPresetSaveMethod"), "", "", "", false, "", true, saveFilterPresetSetting_Scs, tblId, datas, "");
        }
    }
    return false;
}

function updateColumnOrder(tblId) {
    var currentcolumnorder = ""
    var allowDrag = $("#" + tblId).attr("allowDrag");
    if (allowDrag == 'y') {
        currentcolumnorder = $("#" + tblId).dragtable('order');
    }
    var datas = "{'gridId':'" + $("#" + tblId).attr("gridId") + "','currentcolumnorder':'" + currentcolumnorder + "'}";
    callServiceMethod(datas, $("div[fortbl=" + tblId + "]").attr("service"), "saveFilterColumnOrder", "", "", "", false, "", true, updateColumnOrder_scs, tblId, currentcolumnorder, "");
    return false;
}
function updateColumnOrder_scs(tblId, currentcolumnorder) {
    $("#" + tblId).attr("defaultcolumnorder", currentcolumnorder)
}


function saveFilterPresetSetting_Scs(p1, p2, p3, datas) {
    hideProcess();
    var filterNo = $("div[fortbl=" + p1 + "] select[ddlAllFilters]").val();
    if (filterNo == "0") {
        var data = datas.d.split('***')[1];
        var filters = data.split(',');
        var filterNo = filters[0];
        var originalDDL = $("div[fortbl=" + p1 + "] select[ddlAllFilters]");
        var FilterName = $("div[fortbl=" + p1 + "] input[txtFilterName]").val();
        originalDDL.append("<option value='" + filterNo + "'>" + FilterName + "</option>");
        $("div[fortbl=" + p1 + "] select[ddlAllFilters]").val(filterNo);
        $("div[fortbl=" + p1 + "] span[filterSetting]").html(p2);
        $("div[fortbl=" + p1 + "] [btnDelete]").removeClass("hide");
    }
}

function deleteFilterSettings(tblId) {
    var ddlval = $("div[fortbl=" + tblId + "] select[ddlAllFilters]").val();
    var datas = "{'id':'" + ddlval + "'}"; $("#" + tblId).attr("service")
    callServiceMethod(datas, $("div[fortbl=" + tblId + "]").attr("service"), $("div[fortbl=" + tblId + "]").attr("filterPresetDeleteMethod"), "", "", "", false, "",
                                        true, deleteFilterSettings_Scs, tblId, ddlval, true);
    return false;
}

function deleteFilterSettings_Scs(tblId, ddlvalToDelete, shallDeleteItemfromDDL, data) {
    var originalDDL = $("div[fortbl=" + tblId + "] select[ddlAllFilters]").attr("id");
    if (shallDeleteItemfromDDL) {
        removeItemsFromDDL(originalDDL, ddlvalToDelete, true);
    }
    $("#" + tblId).attr("currentcolumnorder", $("#" + tblId).attr("defaultColumnOrder"));
    $("div[fortbl=" + tblId + "] span[filterSetting]").html("");
    $("div[fortbl=" + tblId + "] input[txtfiltername]").val("");
    $("div[fortbl=" + tblId + "] input[type='checkbox']").prop('checked', false);
    $("div[fortbl=" + tblId + "] [btnDelete]").addClass("hide");
    checkUncheckModifedChecRadiokBoxesGrd();
    loadFilterSetting(tblId);
    bindGrid(1, tblId);
}


function ddlChange_GetFilterValues(tblId) {
    var allowDrag = $("#" + tblId).attr("allowDrag");
    if (allowDrag == 'y') {
        var originalColumnOrder = $('#' + tblId).attr("originalColumnOrder");
        if (originalColumnOrder != "") {
            originalColumnOrder = '"' + originalColumnOrder.replaceAll(",", '","') + '"';
            var originalColumnOrderSplit = originalColumnOrder.split(",");
            $('#' + tblId).dragtable('order', originalColumnOrderSplit);
        }
        else {
            $('#' + tblId).dragtable();
        }
    }
    var ddlval = $("div[fortbl=" + tblId + "] select[ddlAllFilters]").val();
    if (ddlval == 0) {
        deleteFilterSettings_Scs(tblId, 0, false, "");
    }
    else {
        var datas = "{'id':'" + ddlval + "'}";
        callServiceMethod(datas, $("div[fortbl=" + tblId + "]").attr("service"), $("div[fortbl=" + tblId + "]").attr("filterPresetGetFilterMethod"), "", "", "", false, "", true, ddlChange_GetFilterValues_Scs, tblId, ddlval, "");
    }
}

function ddlChange_GetFilterValues_Scs(tblId, ddlval, p3, data) {
    $("div[fortbl=" + tblId + "] [btnDelete]").removeClass("hide");
    var datas = data.split('***');
    //if (datas[0] == "true") {
    var filterName = datas[1];
    var filter = datas[2];
    $("div[fortbl=" + tblId + "] span[filterSetting]").html(filter);
    $("div[fortbl=" + tblId + "] input[txtfiltername]").val(filterName);

    if (ddlval != 0) {
        $("#" + tblId).attr("currentcolumnorder", datas[4]);
    }

    var isPrimary = datas[3]
    var primary = (isPrimary.toLowerCase() == "true") ? true : false
    $("div[fortbl=" + tblId + "] input[type='checkbox']").prop('checked', primary);
    checkUncheckModifedChecRadiokBoxesGrd();
    loadFilterSetting(tblId);

    bindGrid(1, tblId);
    //              }
    //              else {
    //                  showScsMsgWithMsg(datas[1], 'warningErr');
    //              }
}


//Ends : Filter Preset Settings



function checkUncheckModifedChecRadiokBoxesGrd() {
    $("div[aria-checked] input[type=checkbox]").each(function () {
        if ($(this).is(":checked")) {
            $(this).parent().addClass("checked");
            $(this).parent().attr("aria-checked", "true");
        }
        else {
            $(this).parent().removeClass("checked");
            $(this).parent().attr("aria-checked", "false");
        }
    });
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
    {return datePassed; }
}