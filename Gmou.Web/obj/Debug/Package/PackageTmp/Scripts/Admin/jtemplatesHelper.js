function showRowEditMode(containerId, rowId) {
    $(".updateTray").attr("style", "display:none");
    // $("#" + rowId + " .defaultFocus").focus();
     var txtIdToFocus="";
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



//function processJQueryJSTemplete(containerId, JScriptId, data) {
//    $("#" + containerId).html('');
//    $('#' + containerId).setTemplate($("#" + JScriptId).html());
//    $('#' + containerId).processTemplate(data);
//}
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
            if (typeOfElement == "text" || typeOfElement == "ddl") {
                values = $("#" + id + " .modeEdit").val();
            }
            else if (typeOfElement == "checkbox") {
                values = ($("#" + id + " input[type='checkbox']").is(":checked")) ? "true" : "false";

                if ($("#" + id + " img.imgTrueFalse").length > 0) {
                    var prevVal = $("#" + id + " img.imgTrueFalse").attr("prevValue");
                    if (prevVal != values) {
                        // alert($("#" + id + " img.imgTrueFalse").length + "," + id + "," + prevVal);
                        $("#" + id + " img.imgTrueFalse").attr("prevValue", values);
                        var srcImage=  $("#" + id + " img.imgTrueFalse").attr("src");
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

function createDDLFromAnotherDDL(originalDDLId, parentDDLId) {
    $('#' + originalDDLId).empty();
    var originalDDL = $("#" + originalDDLId);
    $("#" + parentDDLId + " option").each(function () {
        originalDDL.append("<option value='"+$(this).val()+"'>" + ($(this).html()) + "</option>");
    })
}


function createDDLFromAnotherDDLShowSelected(originalDDLId, parentDDLId,shallShowSelected) {
    $('#' + originalDDLId).empty();
    var originalDDL = $("#" + originalDDLId);
    $("#" + parentDDLId + " option").each(function () {
        originalDDL.append("<option value='" + $(this).val() + "'>" + ($(this).html()) + "</option>");
    })
    if (shallShowSelected) {
        var selectedId = getDDLSelectedValue(parentDDLId);
        $("#" + originalDDLId).val(selectedId);
      
    }
}

