function callServiceMethod(datas, webservicepath, methodname, successMethodToCall, rowIdToDelete, rowParentContainerId, shallLoadTemplete, templeteToLoad, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    if (datas != "") {
        $.ajax({
            type: "POST",
            url: webservicepath + "/" + methodname,
            data: datas,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (successMethodToCall != "") {
                    successMethodToCall(data, rowIdToDelete, rowParentContainerId, shallLoadTemplete, templeteToLoad, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod);
                }
                else if (shallCallOtherMethod) {
                    if (otherMethodToCall != "")
                    {
                        otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data);
                    }
                }
            },
            error: OnError
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: webservicepath + "/" + methodname,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (successMethodToCall != "") {
                    successMethodToCall(data, rowIdToDelete, rowParentContainerId, shallLoadTemplete, templeteToLoad, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod);
                }
                else if (shallCallOtherMethod) {
                    if (otherMethodToCall != "")
                    { otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data); }
                }
            },
            error: OnError
        });
    }
}

function successMethod(data, rowIdToDelete, rowParentContainerId, shallLoadTemplete, templeteToLoad, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    alert(data.d);
}

function successMethodAfterDelete(data, rowIdToDelete, rowParentContainerId, shallLoadTemplete, templeteToLoad, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    var datas = data.split('***');
    // showScsMsgWithMsg('Data cannot  successfully', 'warningErr');
    if (datas[0] == "true") {
        showScsMsgWithMsg(datas[1], 'scstrue');
        //      $("#"+rowIdToDelete).fadeOut();
        $("#" + rowIdToDelete).remove();
        if (shallLoadTemplete) {
            templeteToLoad();
        }
        if (shallCallOtherMethod) {
            otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data);
        }
    }
    else {
        showScsMsgWithMsg(datas[1], 'scsfalse');
    }
}

function successMethodAfterInsertUpdate(data, rowIdToUpdate, rowParentContainerId, shallLoadTemplete, templeteToLoad, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    debugger;
    if (data.toString().indexOf("***") > -1) {
        var datas = data.split('***');
        // showScsMsgWithMsg('Data cannot  successfully', 'warningErr');
        if (datas[0] == "true") {
            if (shallLoadTemplete) {
                showScsMsgWithMsg(datas[1], 'scstrue');
                templeteToLoad();
            }
            else
            { showScsMsgWithMsg(datas[1], 'scstrue'); }
            if (shallCallOtherMethod) {
                if (datas.length > 2) {
                    if (datas.length > 3) {
                        otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data, datas[2], datas[3]);
                    }
                    else { otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data, datas[2]); }
                }
                else {
                    otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data);
                }
                if (rowIdToUpdate.length > 0)
                { hideRowEditMode(rowParentContainerId, rowIdToUpdate); }
            }
            else {
                //alert(shallCallOtherMethod + "," + parameter2InOtherMethod + "," + parameter1InOtherMethod)
                showScsMsgWithMsg(datas[1], 'scstrue');
                if (rowIdToUpdate.length > 0)
                { hideRowEditMode(rowParentContainerId, rowIdToUpdate); }
            }
        }
        else {
            showScsMsgWithMsg(datas[1], 'scsfalse');
        }
    }
    else {   // we get Only Data 
        if (shallLoadTemplete) {
            //showScsMsgWithMsg(datas[1], 'scsfalse');
            templeteToLoad();
        }
        if (shallCallOtherMethod) {
            otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data);
            if (rowIdToUpdate.length > 0)
            { hideRowEditMode(rowParentContainerId, rowIdToUpdate); }
        }
    }
    hideProcess();
}

function callJTemplateServiceMethod(datas, webservicepath, methodname, containerId, templateId, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    $("#" + containerId).html("Loading...");
    if (datas != "") {
        $.ajax({
            type: "POST",
            url: webservicepath + "/" + methodname,
            data: datas,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) { bindTemplateSuccess(containerId, data, templateId, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) },
            error: OnError
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: webservicepath + "/" + methodname,
            // crossDomain: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // jsonp: 'jsonp_callback',
            success: function (data) { bindTemplateSuccess(containerId, data, templateId, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) },
            error: OnError
        });
    }
}
function bindTemplateSuccess(containerId, data, templateId, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    debugger;
    alert(data);
    $('#' + containerId).setTemplate($("#" + templateId).html());
    $('#' + containerId).processTemplate(data.d);
    if (data.length == 0) {
        if ($("#" + containerId).hasAttr("showEmptyRow")) {
            $("#" + containerId).html("No Data Found.")
        }
    }
    if (shallCallOtherMethod) {
        otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data);
    }
}

//if URL Name is passed then  containerId will contain urlname***containerId
function callJTemplateServiceMethodURL(datas, webservicepath, methodname, URLPath, containerId, templateId, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    $("#" + containerId).html("Loading...");
    if (datas != "") {
        $.ajax({
            type: "POST",
            url: webservicepath + "/" + methodname,
            data: datas,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) { bindTemplateSuccessURL(URLPath, containerId, data, templateId, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod); },
            error: OnError
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: webservicepath + "/" + methodname,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) { bindTemplateSuccessURL(URLPath, containerId, data, templateId, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod); },
            error: OnError
        });
    }
}

function bindTemplateSuccessURL(URLPath, containerId, data, templateId, shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    //$('#' + containerId).setTemplateURL(URLPath, $("#" + templateId).html());
    if (URLPath.length > 0) {
        $('#' + containerId).setTemplateURL(URLPath, $("#" + templateId).html());
        $('#' + containerId).processTemplate(data);
    }
    else {
        $('#' + containerId).setTemplate($("#" + templateId).html());
        $('#' + containerId).processTemplate(data);
    }
    $("#" + containerId + " input[type=checkbox][active=true]").prop('checked', true);
    if (shallCallOtherMethod) {
        if (otherMethodToCall != "")
        { otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data); }
    }
}
 

function callServiceMethodForDDl(datas, webservicepath, methodname,  ddlId , shallCallOtherMethod, otherMethodToCall, parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod) {
    if (datas != "") {
        $.ajax({
            type: "POST",
            url: webservicepath + "/" + methodname,
            data: datas,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var dataValueField = $("#" + ddlId).attr("valueField");
                var dataTextField = $("#" + ddlId).attr("textField");
                var insertBlankRow = false;
                var blankRowValueField = $("#" + ddlId).attr("blankRowValue");
                if (typeof blankRowValueField != "undefined") { insertBlankRow = true; }
                var blankRowTextField = $("#" + ddlId).attr("blankRowText");
                bindDdlWithData(ddlId, data, dataValueField, dataTextField, insertBlankRow, blankRowValueField, blankRowTextField)
                if (otherMethodToCall != "" && otherMethodToCall != false && typeof otherMethodToCall!="undefined")
                    { otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data); }
                
            },
            error: OnError
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: webservicepath + "/" + methodname,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var dataValueField = $("#" + ddlId).attr("valueField");
                var dataTextField = $("#" + ddlId).attr("textField");
                var insertBlankRow = false;
                var blankRowValueField = $("#" + ddlId).attr("blankRowValue");
                if (typeof blankRowValueField != "undefined") { insertBlankRow = true; }
                var blankRowTextField = $("#" + ddlId).attr("blankRowText");
                bindDdlWithData(ddlId, data, dataValueField, dataTextField, insertBlankRow, blankRowValueField, blankRowTextField)
                if (otherMethodToCall != "" && otherMethodToCall != false && typeof otherMethodToCall != "undefined")
                { otherMethodToCall(parameter1InOtherMethod, parameter2InOtherMethod, parameter3InOtherMethod, data); }

            },
            error: OnError
        });
    }
}



function bindDdlWithData(ddlId, data, dataValueField, dataTextField, insertBlankRow, blankRowValueField, blankRowTextField) {
    $('#' + ddlId).empty();
    if (typeof data.d != "undefined") {
        var dataLength = data.length;
        if (dataLength > 0) {
            var originalDDL = $("#" + ddlId);
            if (insertBlankRow)
            { originalDDL.append("<option value='" + blankRowValueField + "'>" + blankRowTextField + "</option>"); }
            for (i = 0; i < dataLength; i++) {
                originalDDL.append("<option value='" + data.d[i][dataValueField] + "'>" + data.d[i][dataTextField] + "</option>");
            }

            if ($("#" + ddlId).parent().children("ddlValue").length > 0) {
                var valToSelect = $("#" + ddlId).parent().children("ddlValue").html();
                if (valToSelect.length > 0) {
                    $("#" + ddlId).val(valToSelect);
                }
                else
                { var ddl = document.getElementById(ddlId); ddl.selectedIndex = 0; }
            }
            else
            { var ddl = document.getElementById(ddlId); ddl.selectedIndex = 0; }

        }
    }
    else {
        var dataLength = data.length;
        if (dataLength > 0) {
            var originalDDL = $("#" + ddlId);
            if (insertBlankRow)
            { originalDDL.append("<option value='" + blankRowValueField + "'>" + blankRowTextField + "</option>"); }
            for (i = 0; i < dataLength; i++) {
                originalDDL.append("<option value='" + data[i][dataValueField] + "'>" + data[i][dataTextField] + "</option>");
            }

            if ($("#" + ddlId).parent().children("ddlValue").length > 0) {
                var valToSelect = $("#" + ddlId).parent().children("ddlValue").html();
                if (valToSelect.length > 0) {
                    $("#" + ddlId).val(valToSelect);
                }
                else
                { var ddl = document.getElementById(ddlId); ddl.selectedIndex = 0; }
            }
            else
            { var ddl = document.getElementById(ddlId); ddl.selectedIndex = 0; }

        }
    }

}


function OnError(request, status, error) {
    alert(error);
}


function autoCompleteTextBoxWS(wsPath, wsParameter, shallIncludeValueField, sourceTextBoxId, destinationTextBoxIdForId, shallCallOthermethod, otherMethod1, param1Inmethod1, param2Inmethod1) {
    $("#" + sourceTextBoxId).autocomplete({
        source: function (request, response) {
            var obj = new Object();
            { obj.name = getControlValue(sourceTextBoxId); }
            var datas = "{'" + wsParameter + "':" + JSON.stringify(obj) + "}";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: wsPath,
                data: datas,
                dataType: "json",
                success: function (data) {
                    if (shallIncludeValueField) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('***')[0],
                                val: item.split('***')[1]
                            }
                        }))
                    }
                    else {
                        response($.map(data.d, function (item) {
                            return {
                                label: item
                            }
                        }))
                    }
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (event, ui) {
            if (typeof destinationTextBoxIdForId != "undefined" || destinationTextBoxIdForId != "") {
                //  $("#" + destinationTextBoxIdForId).val(ui.item.val);
                setControlValue(destinationTextBoxIdForId, ui.item.val);
            }
            if (shallCallOthermethod) {
                if (typeof ui.item.val != "undefined") {
                    otherMethod1(ui.item.val, param1Inmethod1, param2Inmethod1);
                }
                else {
                    otherMethod1(ui.item.label, param1Inmethod1, param2Inmethod1);
                }
            }
        }
    });
}


/*Starts:Xml Manipulation*/

function loadDDLFromXMLDocument(path, ddlId, tagName, dataValue, dataText, appendFirstRow, appendFirstRowValue, appendFirstRowText, loadMatchedDDLBool, setDDLValueBool,
                                    otherMethodToCall, p1, p2, p3, dataAttrField) {
    if ($("#" + ddlId).length > 0) {
        var originLocation = window.location.origin;

        var x;

        var pathtemp = path.replaceAll("/", "_").replaceAll(".", "***");
        if ($("#divXMLS > div[path='" + pathtemp + "']", window.parent.document).length > 0) {
            var datas = $("#divXMLS > div[path='" + pathtemp + "']", window.parent.document).html();
            var xmlDoc = datas.replaceAll('<?xml version="1.0" standalone="yes"?>', "");
            var xmlDocq = $.parseXML(xmlDoc)
            x = xmlDocq.getElementsByTagName(tagName);
        }
        else {
            if (!window.location.origin) {
                originLocation = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
            }
            path = originLocation + "/" + path;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.open("GET", path, false);
            xmlhttp.send();
            xmlDoc = xmlhttp.responseXML;

            x = xmlDoc.getElementsByTagName(tagName);

            $("#divXMLS", window.parent.document).append("<div  path='" + pathtemp + "'>" + xmlhttp.responseText.toString().replaceAll('<?xml version="1.0" standalone="yes"?>', "") + "</div>");

        }

        var originalDDL = $("#" + ddlId);
        $('#' + ddlId).empty();

        if (appendFirstRow) {
            originalDDL.append("<option value='" + appendFirstRowValue + "'>" + appendFirstRowText + "</option>");
        }

        var shallAppendDDlAttribute = false;
        if (typeof dataAttrField != "undefined") {
            if (dataAttrField != "") {
                shallAppendDDlAttribute = true;
            }
        }
        var attrValue = "";
        for (i = 0; i < x.length; i++) {
            var text = x[i].getElementsByTagName(dataText)[0].childNodes[0].nodeValue;
            var values = x[i].getElementsByTagName(dataValue)[0].childNodes[0].nodeValue;


            if (shallAppendDDlAttribute) {
                attrValue = x[i].getElementsByTagName(dataAttrField)[0].childNodes[0].nodeValue;
                originalDDL.append("<option attr='" + attrValue + "' value='" + values + "'>" + text + "</option>");
            }
            else {
                originalDDL.append("<option value='" + values + "'>" + text + "</option>");
            }
            if (i == x.length - 1) {
                loadOtherDDLDromThis(ddlId, setDDLValueBool);
                if ($("#" + ddlId).parent().children("ddlValue").length > 0) {
                    var valToSelect = $("#" + ddlId).parent().children("ddlValue").html();
                    if (valToSelect.length > 0) {
                        $("#" + ddlId).val(valToSelect);
                        // setDDLSelectedValue(ddlId, valToSelect);
                    }
                    else
                    { var ddl = document.getElementById(ddlId); ddl.selectedIndex = 0; }
                }
                else
                { var ddl = document.getElementById(ddlId); ddl.selectedIndex = 0; }
            }
        }
    }
    if (typeof otherMethodToCall != "undefined" && otherMethodToCall != "") {
        otherMethodToCall(p1, p2, p3);
    }
}

      
function loadOtherDDLDromThis(ddlId, setDDLValueBool) {
    var matchDDL = $("#" + ddlId).attr("match");
    var totalOtherDDL = $("select[match='" + matchDDL + "']").length;
    if (totalOtherDDL > 1) {
        $("select[match='" + matchDDL + "']").each(function (index) {
            if (this.id != ddlId) {
                loadDDLFromAnotherDDL(ddlId, this.id);
                $("#" + this.id).val($("#" + this.id).parent().children("ddlValue").html());
            }

        });
    }
}
/*Ends:Xml Manipulation*/


/*Starts:Jtemplete Manipulation*/
function showRowEditMode(containerId, rowId) {
    $(".updateTray").attr("style", "display:none");
    // $("#" + rowId + " .defaultFocus").focus();
    var txtIdToFocus = "";
    $("#" + containerId + " #" + rowId + " td").each(function () {
        var id = this.id;
        if ($("#" + id + " .modeShow").length > 0) {
            var values = $.trim($("#" + id + " .modeShow").html());

            if ($("#" + id + " .modeShowHide").length > 0) {
                values = $("#" + id + " .modeShowHide").html();
            }
            var typeOfElement = ($("#" + id + " .modeEdit").attr('type')).toLowerCase();
            if (typeOfElement == "text") {
                txtIdToFocus = $("#" + id + " .modeEdit").attr("class");
                if (txtIdToFocus.indexOf("defaultFocus") > -1) {
                    txtIdToFocus = $("#" + id + " .modeEdit").attr("id");
                }
                $("#" + id + " .modeEdit").val(values);
            }
            else if (typeOfElement == "checkbox") {
                if (values.toLowerCase() == "false") {
                    $("#" + id + " .modeEdit").removeAttr("checked");
                }
                else
                { $("#" + id + " .modeEdit").attr("checked", "checked") }
            }
            else if (typeOfElement == "ddl") {
                var parentDDLID = $("#" + id + " .modeEdit").attr("parentDDLId");
                var thisId = $("#" + id + " .modeEdit").attr("id");
                createDDLFromAnotherDDL(thisId, parentDDLID);
                //setDDLSelectedText(thisId,values);
                $("#" + thisId).val(values);
            }
        }
    })

    $("#" + containerId + " tr ").removeClass("trEditMode");
    $("#" + containerId + " tr ").addClass("trShowMode");
    $(".updateTray").attr("style", "display:none");
    $("#" + rowId).removeClass("trShowMode");
    $("#" + rowId).addClass("trEditMode");
    $("#" + rowId + " .updateTray").attr("style", "display:block");
    if ($("#" + txtIdToFocus).length > 0) {
        document.getElementById(txtIdToFocus).focus();
    }
}



function processJQueryJSTemplete(containerId, JScriptId, data, othermethodToCall, parameter1Formethod, parameter2Formethod, parameter3Formethod) {
    $("#" + containerId).html('');
    $('#' + containerId).setTemplate($("#" + JScriptId).html());
    if (typeof data.d == "undefined") {
        $('#' + containerId).processTemplate(data);
        formatData();
        if (typeof othermethodToCall != "undefined") {
            othermethodToCall(parameter1Formethod, parameter2Formethod, parameter3Formethod, data);
        }
    }
    else {
        $('#' + containerId).processTemplate(data.d);
        formatData();
        if (typeof othermethodToCall != "undefined") {
            othermethodToCall(parameter1Formethod, parameter2Formethod, parameter3Formethod, data);
        }
    }
    formatData();
}
function cancelRowEditMode(containerId, rowId) {
    $("#" + containerId + " tr ").removeClass("trEditMode");
    $("#" + containerId + " tr ").addClass("trShowMode");
    $("#" + containerId + " .updateTray").attr("style", "display:none");
}

function hideRowEditMode(containerId, rowId) {
    $("#" + containerId + " #" + rowId + " td").each(function () {
        var id = this.id;
        if ($("#" + id + " .modeEdit").length > 0) {
            var typeOfElement = ($("#" + id + " .modeEdit").attr('type')).toLowerCase();
            var values = "";
            if (typeOfElement == "text") {
                values = $("#" + id + " .modeEdit").val();
            }
            else if (typeOfElement == "ddl") {
                var ddlId =  ($("#" + id + " .modeEdit").attr('id'));
                values = getDDLSelectedText(ddlId);
            }
            else if (typeOfElement == "checkbox") {
                values = ($("#" + id + " input[type='checkbox']").is(":checked")) ? "true" : "false";

                if ($("#" + id + " img.imgTrueFalse").length > 0) {
                    var prevVal = $("#" + id + " img.imgTrueFalse").attr("prevValue");
                    if (prevVal != values) {
                        // alert($("#" + id + " img.imgTrueFalse").length + "," + id + "," + prevVal);
                        $("#" + id + " img.imgTrueFalse").attr("prevValue", values);
                        var srcImage = $("#" + id + " img.imgTrueFalse").attr("src");
                        srcImage = srcImage.replace(prevVal, values);
                        $("#" + id + " img.imgTrueFalse").attr("src", srcImage);
                    }
                }

            }
            $("#" + id + " .modeShow").html(values);
        }
    })
    cancelRowEditMode(containerId, rowId);
}


function testIfValuesAreChanged(rowId) {
    var changed = false;
    $("#" + rowId + " td").each(function () {
        var id = this.id;
        if ($("#" + id + " .modeEdit").length > 0) {
            var typeOfElement = ($("#" + id + " .modeEdit").attr('type')).toLowerCase();
            var oldValue = $("#" + id + " .modeShow").html();
            if (typeOfElement == "ddl") {
                oldValue = $("#" + id + " .modeShowHide").html();
            }
            var values = "";
            if (typeOfElement == "text" || typeOfElement == "ddl") {
                values = $("#" + id + " .modeEdit").val();
            }
            else if (typeOfElement == "checkbox") {
                values = ($("#" + id + " input[type='checkbox']").is(":checked")) ? "true" : "false";
                oldValue = oldValue.toLowerCase();
            }
            if (oldValue != values) {
                changed = true;
            }
        }
    })
    return changed;
}

/*Ends:Jtemplete Manipulation*/
