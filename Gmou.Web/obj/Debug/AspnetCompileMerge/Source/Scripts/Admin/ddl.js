function getDDLSelectedValue(ddlId) {
    var ddl = document.getElementById(ddlId);
    if (ddl.selectedIndex == -1) {
        if (ddl[0].value == "0" || ddl[0].value == "") {
            if (ddl[0].value) {
                return ddl[0].value;
            }
            else return "";
        }
        else {
            return "";
        }
    }
    else
        return ddl[ddl.selectedIndex].value;
}


//function getDDLSelectedAttr(ddlId, attrName) {
//    var ddl = document.getElementById(ddlId);
//    if (ddl.selectedIndex == -1) { return ""; }
//    else {
//        return $("#" + ddlId + " option:nth-child(" + (parseInt(ddl.selectedIndex) + 1) + ")").attr(attrName);
//    }
//}

function getDDLSelectedAttr(ddlId,attrName) {
    var ddl = document.getElementById(ddlId);
    if (ddl.selectedIndex == -1) {
        if (ddl[0].value == "0" || ddl[0].value == "") {
            if (ddl[0].value) {
                ddl[0].value
            }
            else return "";
        }
        else {
            return "";
        }
    }
    else {
         return $("#" + ddlId + " option:nth-child(" + (parseInt(ddl.selectedIndex)+1) + ")").attr(attrName);
    }
}

function getDDLSelectedText(ddlId) {
    var ddl = document.getElementById(ddlId);
    if (ddl.selectedIndex == -1) {
        if (ddl[0].value == "0" || ddl[0].value == "") {
            if (ddl[0].text) {
                return ddl[0].text;
            }
            else return "";
        }
        else {
            return "";
        }
    }
    else
       return ddl[ddl.selectedIndex].text;
}



function getDDLTextByValue(ddlId,valuePassed) {
    var ddl = document.getElementById(ddlId);
    var val = "";
    for (var i = 0; i < ddl.options.length; i++) {
        if (ddl.options[i].value == valuePassed) {
            isFound = true;
            val= ddl.options[i].text;
            break;
        }
    }
    return val;
}

function getDDLAttrByValue(ddlId, valuePassed, attrName) {
    var ddl = document.getElementById(ddlId);
    var val = "";
    for (var i = 0; i < ddl.options.length; i++) {
        if (ddl.options[i].value == valuePassed) {
            isFound = true;
            //val = ddl.options[i].text;
            val = $("#" + ddlId + " option:nth-child(" + (parseInt(i) + 1) + ")").attr(attrName);
            break;
        }
    }
    return val;
}

function setDDLSelectedValue(ddlId, textToFind) {
    var ddl = document.getElementById(ddlId);
    for (var i = 0; i < ddl.options.length; i++) {
        if (ddl.options[i].value == textToFind) {
            ddl.selectedIndex = i;
            break;
        }
    }
}


function setDDLSelectedText(ddlId, textToFind) {
    var ddl = document.getElementById(ddlId);
    for (var i = 0; i < ddl.options.length; i++) {
        if (ddl.options[i].text == textToFind) {
            ddl.selectedIndex = i;
            break;
        }
    }
}


function setDDLSelectedTextParent(ddlId, textToFind) {
    var ddl = window.parent.document.getElementById(ddlId);
    for (var i = 0; i < ddl.options.length; i++) {
        if (ddl.options[i].text == textToFind) {
            ddl.selectedIndex = i;
            break;
        }
    }
}


function getsetDDLSelectedValueFromText(ddlId, textToFind) {
    if ($("#" + ddlId).length > 0) {
        var ddl = document.getElementById(ddlId);
        for (var i = 0; i < ddl.options.length; i++) {
            if (ddl.options[i].text.toLowerCase() == textToFind.toLowerCase()) {
                return ddl.options[i].value;
                break;
            }
        }
    }
}


function removeItemsFromDDL(ddlId, itemsToRemove, isValues) {
    var ddl = document.getElementById(ddlId);
    var items = itemsToRemove.split(",");
    var countItems = items.length;
    var ddlLength = ddl.options.length;
    for (j = 0; j < countItems; j++) {
        for (var i = 0; i < ddlLength; i++) {
            var textTomatch = ddl.options[i].value;
            if (!isValues) {
                textTomatch = ddl.options[i].text.toLowerCase();
            }
            if (textTomatch == items[j].toLowerCase()) {
                var val = items[j];
                if (!isValues) {
                    val = ddl.options[i].value;
                }
                $("#" + ddlId + " option[value='" + val + "']").remove();
                break;
            }
        }
    }
}


function removeItemsFromDDLExceptValuesPassed(ddlId, itemsNotToRemove, isValues) {
    var ddl = document.getElementById(ddlId);
    var items = itemsNotToRemove.split(",");
    var countItems = items.length;
    var ddlLength = ddl.options.length;
    var currentDDLItem = 0;
    for (var i = 0; i < ddlLength; i++) {
        var removeThisItem = true;
        var textTomatch = ddl.options[currentDDLItem].value;
        if (!isValues) {
            textTomatch = ddl.options[currentDDLItem].text.toLowerCase();
        }
        for (j = 0; j < countItems; j++) {
            if (textTomatch == items[j].toLowerCase()) {
                removeThisItem = false;
                break;
            }
        }
        if (removeThisItem) {
            var val = ddl.options[currentDDLItem].value;
            $("#" + ddlId + " option[value='" + val + "']").remove();
            currentDDLItem = currentDDLItem - 1;
        }
        currentDDLItem = currentDDLItem + 1;
    }
}


function loadDDLFromAnotherDDL(parentDDLId, childDDLId) {
    $('#' + childDDLId).empty();
    var childDDLId = $("#" + childDDLId);
    $("#" + parentDDLId + " option").each(function () {
        childDDLId.append("<option value='" + $(this).val() + "'>" + ($(this).html()) + "</option>");
    })
}


function countDCheckBoxCheckedInContainer(containerid) {
    var checkCount = 0;
    $('#' + containerid + " input[type='checkbox']").each(function () {
        // console.log($(this).is(':checked'));
        if ($(this).is(':checked')) {
            checkCount++;
        }
    })
    return checkCount;
}



function makeAllChildDDlSelectedViaParent(parentDDLId) {
    var valToSelect = $("#" + parentDDLId).val();
    $("select[parentddlid='" + parentDDLId + "']").each(function () {
        $(this).val(valToSelect);
    })
}


function createDDLFromAnotherDDL(originalDDLId, parentDDLId, shallShowSelected) {
    $('#' + originalDDLId).empty();
    var originalDDL = $("#" + originalDDLId);
    $("#" + parentDDLId + " option").each(function () {
        originalDDL.append("<option value='" + $(this).val() + "'>" + ($(this).html()) + "</option>");
    })
    if (typeof shallShowSelected != "undefined" && shallShowSelected==true) {
        var selectedId = getDDLSelectedValue(parentDDLId);
        $("#" + originalDDLId).val(selectedId);
    }
}


 