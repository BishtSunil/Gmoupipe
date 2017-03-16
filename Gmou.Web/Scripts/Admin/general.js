﻿$(document).ready(function () {

   //Starts: Support PlaceHodler in IE Lower Version
//    (function ($) {
//        $.support.placeholder = ('placeholder' in document.createElement('input'));
//    })(jQuery);
//       //fix for IE7 and IE8
//        $(function () {
//            if (!$.support.placeholder) {
//                $("[placeholder]").focus(function () {
//                    if ($(this).val() == $(this).attr("placeholder")) $(this).val("");
//                }).blur(function () {
//                    if ($(this).val() == "") $(this).val($(this).attr("placeholder"));
//                }).blur();

//                $("[placeholder]").parents("form").submit(function () {
//                    $(this).find('[placeholder]').each(function () {
//                        if ($(this).val() == $(this).attr("placeholder")) {
//                            $(this).val("");
//                        }
//                    });
//                });
//            }
//         });
//        })
//Ends: Support PlaceHodler in IE Lower Version


    if ($("[defaultFocus]:first").length > 0) {
        $("[defaultFocus]:first").focus();
    }
    else {

    }
    attachValidation();
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scrollup').fadeIn();
        } else {
            $('.scrollup').fadeOut();
        }
    });

    $('.scrollup').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 500);
        return false;
    });
    //  scrollToTop(); 

    // $("a[class!='telPhone']").live('click', function () { showProcess('Loading...'); })

    //   $(document).bind('keyup keydown', function (e) { shifted = e.shiftKey }); // This is for scroll To top
})



function getactivetab(tabId) {
    $(".tab-pane").removeClass("active");
    $(".nav-tabs li").removeClass("active");
    $("#" + tabId).addClass("active");
    $("#" + tabId + "Li").addClass("active");
}


//function closeModalPopUpForTemplete() { // For closing Modal Pop up taht comes with temeplete
//    $(".modal").removeClass("in");
//    $(".modal").removeAttr("aria-hidden");
//    $(".modal").css({ "display": "none" });
//    $("body").removeClass("modal-open");
//    $(".modal-backdrop").removeClass("in");
//}

function scrollToTop() {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scrollup').fadeIn();
            $('.scrollupText').fadeIn();
        } else {
            $('.scrollup').fadeOut();
            $('.scrollupText').fadeOut();
        }
    });

    $('.scrollup').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
    $(document).keydown(function (e) {
        if (e.keyCode == 113) {
            $("html, body").animate({ scrollTop: 0 }, 600);
            return false;
        }
    });
}

 

 
function testParentWindow() {
    if (self == window.parent) {
        return false;
    }
    else {
        return true;
    }
}

function testParentParentWindow() {
    if (self == window.parent.window.parent) {
        return false;
    }
    else if (self == window.parent) {
        return false;
    }
    else {
        return true;
    }
}

//showScsMsg('scsErr','here comes success message');
//showScsMsg('warningErr','here comes warning message');
function showScsMsg(cssClasss) {
   
    hideProcess();
    //$(".errMsg").attr("style", "display:block");
    $("." + cssClasss).attr("style", "display:block");
    if (cssClasss == "scsErr") {
        setTimeout(function () {
            $("." + cssClasss).fadeOut();
            $(".errMsg").attr("style", "display:none");
            $(".scsErr").attr("style", "display:none");
        }, 2000);
    }
}

function showScsMsgWithMsg(msg, cssClasss) {
    
    $(".errMsg").attr("style", "display:block");
    cssClasss = cssClasss.toLowerCase();
    if (cssClasss == "scstrue") {
        $("#errMsgClasses").attr("class", "alert  alert-success alert-dismissable scsErr");
        $("#errMsgClasses i").attr("class", "fa fa-check")
         setTimeout(function () {
            $(".errMsg").fadeOut();
           // $(".scsErr").fadeOut();
           // $(".errMsg").attr("style", "display:none");
        }, 3000);
    }
    else {
        $("#errMsgClasses").attr("class", "alert  alert-danger alert-dismissable warningErr");
        $("#errMsgClasses i").attr("class", "fa fa-ban")
    }
    if ($.trim(msg) == "") {
        msg = $.trim($(".errMsgInPages").html());
    }
    $("#errMsgClasses msg").html(msg);
    attachValidation();
}



function showScsMsgWithTab(cssClasss, tabToShowLi) {
    showScsMsg(cssClasss);
    tabToShow(tabToShowLi);
}

// hideScsMsg('warningErr') 
function hideMsg() {
  
  //  $(".scsErr").fadeOut();
    //$(".warningErr").fadeOut();
    $(".errMsg").attr("style", "display:none");
}


function showProcess(val) {
    // if (!testParentWindow()) {
     
    if (typeof val == "undefined") {val = ""; }
    if ($("#processing").length > 0) {
        document.getElementById("processing").innerHTML = '&nbsp;&nbsp;&nbsp;' + val + '&nbsp;&nbsp;&nbsp;'
        document.getElementById("processing").setAttribute("class", "show")
    }
    //    }
    //    else if (testParentParentWindow()) {
    //        window.parent.window.parent.document.getElementById("processing").innerHTML = '&nbsp;&nbsp;&nbsp;' + val + '&nbsp;&nbsp;&nbsp;'
    //        window.parent.window.parent.document.getElementById("processing").setAttribute("class", "show")
    //    }
    //    else {
    //        window.parent.document.getElementById("processing").innerHTML = '&nbsp;&nbsp;&nbsp;' + val + '&nbsp;&nbsp;&nbsp;'
    //        window.parent.document.getElementById("processing").setAttribute("class", "show")
    //    }
}

function hideProcess() {
    if ($("#processing").length > 0) {
        //$(".errMsg").attr("style", "display:none");
        document.getElementById("processing").innerHTML = ''
        document.getElementById("processing").setAttribute("class", "hide")
    }
    $("[data-mask]").each(function () {
        var dataMask = $(this).attr("data-mask");
        // $('#cp1_txtCardNumber').mask('9999-9999-9999-9999');
        $(this).mask(dataMask);
    })
    //bindMask();
}



function getControlValueN(id, isControl) {
    var values = getControlValue(id, isControl);
    if (values.toString() == "") {
        values = null;
    }
    return values;
}

 
function getControlValue(id, isControl) {
    var values = "";
    if (typeof isControl == "undefined") { isControl = false; }
    if (!isControl) {
        if ($("#" + id).length > 0) {
            var control = $("#" + id);
            var isCurrency = $("#" + id).attr("formatcurrency");
            var controlName = control[0].tagName.toLowerCase();
            if (controlName == "input" || controlName == "textarea") {
                var controlType = control[0].type.toLowerCase();
                if (controlType == "checkbox" || controlType == "radio") {
                    values = $("#" + id).is(":checked");
                }
                else {
                    values = $.trim($(control).val());
                }
            }
            else if (controlName == "select" || controlName == "textarea") {
                values = $.trim($(control).val());
            }
            else {
                values = $.trim($(control).html());
            }
            if (typeof isCurrency != "undefined") {
                values = values.replaceAll("$", "").replaceAll(",", "").trim();
            }
            if (values == "" ) {
                var defaultValue = $("#" + id).attr("default");
                if (typeof defaultValue != "undefined") {
                    values = defaultValue;
                }
            }
        }
    }
    else {
        if ($(id).length > 0) {
            var control = $(id);
            var isCurrency = $(id).attr("formatcurrency");
            var controlName = control[0].tagName.toLowerCase();
            if (controlName == "input" || controlName == "textarea") {
                var controlType = control[0].type.toLowerCase();
                if (controlType == "checkbox" || controlType == "radio") {
                    values = $(id).is(":checked");
                }
                else {
                    values = $.trim($(control).val());
                }
            }
            else if (controlName == "select" || controlName == "textarea") {
                values = $.trim($(control).val());
            }
            else {
                values = $.trim($(control).html());
            }
            if (typeof isCurrency != "undefined") {
                values = values.replaceAll("$", "").replaceAll(",", "").trim();
            }
            if (values == "") {
                var defaultValue = $(id).attr("default");
                if (typeof defaultValue != "undefined") {
                    values = defaultValue;
                }
            }
        }
    }
    return values;
}

function getRadioValue(containerId, attrValueToGet) {
    if (typeof attrValueToGet == "undefined") { attrValueToGet = "value"; }
    var chkId = null;
    $("#" + containerId + " input[type=radio]").each(function () {
        var isChecked = $(this).is(":checked");
        if (isChecked) {
            chkId = ($(this).attr(attrValueToGet));
        }
    });
    return chkId;
}


function setRadioValue(containerId,valueToSet, attrValueToGet) {
    if (typeof attrValueToGet == "undefined") { attrValueToGet = "value"; }
    var chkId = null;
    $("#" + containerId + " input[type=radio]").each(function () {
        var currentValue = ($(this).attr(attrValueToGet));
        if (currentValue == valueToSet) {
            this.checked = true;
        }
    });
}



function setControlValue(controlId, values, isControl) {
    if (typeof isControl == "undefined") { isControl = false; }
    if (!isControl) {
        if ($("#" + controlId).length > 0) {
            var control = $("#" + controlId);
            var controlName = control[0].tagName.toLowerCase();
            if (controlName == "input" || controlName == "textarea") {
                var controlType = control[0].type.toLowerCase();
                if (controlType == "checkbox" || controlType == "radio") {
                    //  $(control).prop('checked', Boolean(values));
                    if (values.toString().toLowerCase() == "true") {
                        $("#" + controlId).prop('checked', true);
                    }
                    else {
                        $("#" + controlId).prop('checked', false);
                    }
                    //$("#" + controlId).prop('checked', values);
                    //  control[0].checked = Boolean(values);
                }
                else {
                    $(control).val(values);
                }
            }
            else if (controlName == "select") {
                $(control).val(values);
            }
            else {
                $(control).html(values);
            }
        }
    }
    else {
        if ($(controlId).length > 0) {
            var control = $(controlId);
            var controlName = control[0].tagName.toLowerCase();
            if (controlName == "input" || controlName == "textarea") {
                var controlType = control[0].type.toLowerCase();
                if (controlType == "checkbox" || controlType == "radio") {
                    //$(control).prop('checked', values);
                    control.checked = Boolean(values);
                }
                else {
                    $(control).val(values);
                }
            }
            else if (controlName == "select") {
                $(control).val(values);
            }
            else {
                $(control).html(values);
            }
        }
    }
}



// If you dont know parent Id then call method like  getControlValueByAttr("", "[content] [accountNumber]");
// getControlValueByAttr("trItemids_1", "accountNumber");  

function getControlValueByAttr(parentRowId, controlAttr) {
    var values = "";
    var newControlAttr = controlAttr;

    if (controlAttr.indexOf('[') == -1) {
   // if (controlAttr.indexOf(' ') == -1) {
        newControlAttr = " [" + newControlAttr + "]";
    }
    if (parentRowId != "") {
        parentRowId = "#" + parentRowId;
    }
    var isCurrency = $(parentRowId + newControlAttr).attr("formatcurrency");
    if ($(parentRowId + newControlAttr).length > 0) {
        var control = $(parentRowId + newControlAttr);
        var controlName = control[0].tagName.toLowerCase();
        if (controlName == "input" || controlName == "textarea") {
            var controlType = control[0].type.toLowerCase();
            if (controlType == "checkbox" || controlType == "radio") {
                // values = $("#" + id).is(":checked");
                values = $(control).is(":checked");
            }
            else {
                values = $.trim($(control).val());
            }
        }
        else if (controlName == "select" || controlName == "textarea") {
            values = $.trim($(control).val());
        }
        else {
            values = $.trim($(control).html());
        }
        if (typeof isCurrency != "undefined") {
            values = values.replaceAll("$", "").replaceAll(",", "").trim();
        }
    }
       
    return values;
}

function setControlValueByAttr(parentRowId, controlAttr, values) {
    if (controlAttr != "") {
        // if(controlAttr.indexOf("***")>-1){
        var attr = controlAttr.split("&");
        var attr2 = "";
        for (i in attr) {
            attr2 = attr2 + "[" + attr[i] + "]";
        }
        //  }
    }
    controlAttr = attr2;
    if ($("#" + parentRowId + " " + attr2 + "").length > 0) {
        var control = $("#" + parentRowId + "  " + controlAttr + "");
        var controlName = control[0].tagName.toLowerCase();
        if (controlName == "input" || controlName == "textarea") {
            var controlType = control[0].type.toLowerCase();
            if (controlType == "checkbox" || controlType == "radio") {
                $(control).prop('checked', values);
            }
            else {
                $(control).val(values);
            }
        }
        else if (controlName == "select") {
            $(control).val(values);
        }
        else {
            $(control).html(values);
        }
    }
}



function getCheckedRadioInContainer(containerId, attrToMatch, attrToReturn) {
    var valueChecked = "";
    if (typeof attrToMatch == "undefined" || attrToMatch == "") { attrToMatch = ""; }
    else { attrToMatch = "[" + attrToMatch + "]"; }
    if (typeof attrToReturn == "undefined" || attrToReturn == "") { attrToReturn = "value"; }
    $("#" + containerId + " input[type=radio] " + attrToMatch).each(function () {
        if (this.checked) {
            valueChecked = ($(this).attr(attrToReturn));
        }
    })
    return valueChecked;
}


function getCheckedCheckInContainer(containerId, attrToMatch, attrToReturn) {
    var valueChecked = "";
    if (typeof attrToMatch == "undefined" || attrToMatch == "") { attrToMatch = ""; }
    else { attrToMatch = "[" + attrToMatch + "]"; }
    if (typeof attrToReturn == "undefined" || attrToReturn == "") { attrToReturn = "value"; }
    $("#" + containerId + " input[type=checkbox] " + attrToMatch).each(function () {
        if (this.checked) {
            valueChecked = valueChecked+","+($(this).attr(attrToReturn));
        }
    })
    if (valueChecked.length > 0) {valueChecked = valueChecked.substring(1); }
    return valueChecked;
}


function bindMask() {
    $("input[mask='phone']").mask("(999) 999-9999");
}
function bindPhoneMask(txtId) {
    $("#" + txtId).mask("(999) 999-9999");
}

function deleteConfirm(valToShowInModal, valueToShowInProcess) {
    if (confirm(valToShowInModal)) {
        showProcess(valueToShowInProcess);
        return true;
    }
    else
        return false;
}

function getNumbersFromString(values) {
    // return values.match(/\d+/);
    return values.replace(/\D/g, '');
}


//onkeypress="makeDefaultButtonInDiv(this,event,'btnSearch')"
function makeDefaultButtonInDiv(thiss, e, btnId) {
    if (e.keyCode == '13') {
        //$(thiss).find("#"+btnId).click();
        //$("#"+btnId).click();
       // alert(btnId)
        document.getElementById(btnId).click();
    }
}

function showDivInModapPopUp(divId) {
    $(".modal-content > [showHide] ").addClass("hide");
    $(".modal-content #" + divId).removeClass("hide");
}


function loadDataForGridViewInPage(tabToHighlight, path) {
    

    var originLocation = getWindowLocation();
    var iframeSrc = originLocation + "/" + "sitepages/settings/default.aspx";

    $(".sidebar-menu > li").removeClass("active");
    $(".sidebar-menu a").removeClass("active");

    if ($(".sidebar-menu #" + tabToHighlight).length > 0) {
        //        var prevSrc = $("#iframe" + tabToHighlight).attr("src");
        //        if (oldURL.length < 2) {

        //        }
        //        if (prevSrc != iframeSrc && oldURL.length > 2) {
        //            $("#iframe" + tabToHighlight).attr("src", iframeSrc);
        //        }

        $(".sidebar-menu .treeview-menu").css({ "display": "none" });
        $(".sidebar-menu #" + tabToHighlight).parent().parent().css({ "display": "block" });
        $(".sidebar-menu #" + tabToHighlight).parent().parent().parent().addClass("active");
        $(".sidebar-menu #" + tabToHighlight).addClass("active");
    }
 
    if (typeof path != "undefined") {

        var iframe_object = document.getElementById("iframeDefault");

       var doc;

       if (iframe_object.contentWindow) {
           iframe_object= iframe_object.contentWindow;
       }

       else if (iframe_object.window) {
           iframe_object = iframe_object.window;
       }

       else if (!doc && iframe_object.contentDocument) {
           iframe_object=   iframe_object.contentDocument;
       }

       else if (!doc && iframe_object.document) {
           iframe_object=  iframe_object.document;
       }

       else if (doc && doc.defaultView) {
           iframe_object=  doc.defaultView;
       }

       else if (doc && doc.parentWindow) {
           iframe_object=  doc.parentWindow;
       }

       iframe_object.loadGrdVmrTableInChildFrame(path);
    }
}

function loadGrdVmrTableInChildFrame(path) {
    $("[content]").html("");
    $("[content]").css({ "min-height": "0", "display": "none" });
    if ($("#modalMsg.showmodal").length > 0) {
        $(".clsoeBtnDiv img").click();
    }
    loadDataForGridView(path)
}
 

function loadIframePage(tabToHighlight, url,path) {
  
    var oldURL = url;
    var originLocation = getWindowLocation();
    var iframeSrc = originLocation + "/" + url;

    $(".sidebar-menu > li").removeClass("active");
    $(".sidebar-menu a").removeClass("active");

    if ($(".sidebar-menu #" + tabToHighlight).length > 0) {
        var prevSrc = $("#iframe" + tabToHighlight).attr("src");
        if (oldURL.length < 2) {

        }
        if (prevSrc != iframeSrc && oldURL.length > 2) {
            $("#iframe" + tabToHighlight).attr("src", iframeSrc);
        }

        $(".sidebar-menu .treeview-menu").css({ "display": "none" });
        $(".sidebar-menu #" + tabToHighlight).parent().parent().css({ "display": "block" });
        $(".sidebar-menu #" + tabToHighlight).parent().parent().parent().addClass("active");
        $(".sidebar-menu #" + tabToHighlight).addClass("active");
    }

 
    var iframeSrc = originLocation + "/" + url;
    $("#iframeDefault").attr("src", iframeSrc);
    
}


function loadIframePageInParent(tabToHighlight, url) {
    var originLocation = getWindowLocation();
    if (self == window.parent) {
        window.location.href = originLocation + "/" + url;
    }
    else {
        window.parent.loadIframePage(tabToHighlight, url);
    }
}


function loadIframePageInParentsParent(tabToHighlight, url) {
    var originLocation = getWindowLocation();
//    if (testParentParentWindow()) {
//        window.parent.parent.loadIframePage(tabToHighlight, url);
//    }
//    else if (testParentWindow()) {
//        window.parent.loadIframePage(tabToHighlight, url);
//    }
//    else {
//        window.location.href = originLocation + "/" + url;
//        }

    loadIframePageInParent(tabToHighlight, url);
       
}



function getWindowLocation() {
    var originLocation = window.location.origin;
    if (!window.location.origin) {
        originLocation = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
    }
    return originLocation;
}



function reloadPageRemoveQueryStr() {
    window.location = window.location.split("?")[0];
}

function openUrl(url, newTab) {
    if (typeof newTab == "undefined") {
        if (url.indexOf("http") > -1) {
            window.location.href = url;
        }
        else {
              url = "http://" + url;
            window.location.href = url;
        }
    }
}

function getQuerystring(key) {
    if (window.location.search.length > 0) {
        var query = window.location.search.substring(1);
        //alert(query);
        var vars = query.split("&");
        var vals = null;
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == key) {
                vals = pair[1];
            }
        }
        return vals;
    }
    else
    { return null; }
}

function decodeQryStr(s) {
    s = s.replace(/%([EF][0-9A-F])%([89AB][0-9A-F])%([89AB][0-9A-F])/gi,
                function (code, hex1, hex2, hex3) {
                    var n1 = parseInt(hex1, 16) - 0xE0;
                    var n2 = parseInt(hex2, 16) - 0x80;
                    if (n1 == 0 && n2 < 32) return code;
                    var n3 = parseInt(hex3, 16) - 0x80;
                    var n = (n1 << 12) + (n2 << 6) + n3;
                    if (n > 0xFFFF) return code;
                    return String.fromCharCode(n);
                });
    s = s.replace(/%([CD][0-9A-F])%([89AB][0-9A-F])/gi,
                function (code, hex1, hex2) {
                    var n1 = parseInt(hex1, 16) - 0xC0;
                    if (n1 < 2) return code;
                    var n2 = parseInt(hex2, 16) - 0x80;
                    return String.fromCharCode((n1 << 6) + n2);
                });
    s = s.replace(/%([0-7][0-9A-F])/gi,
                function (code, hex) {
                    return String.fromCharCode(parseInt(hex, 16));
                });
    return s;
}

function filterArray(arr, criteria) {
    return arr.filter(function (obj) {
        return Object.keys(criteria).every(function (c) {
            return obj[c] == criteria[c];
        });
    });
}

function isValueBool(val) { 
  val=val.toString().trim().toLowerCase();
  if (val == "1" || val == "true") {
      return true;
  }
  else { false };
}

function selectTblVMRRow(rowId) {
    $("#" + rowId).closest("tbody").find("tr").removeClass("trSelected");
    $("#" + rowId).addClass("trSelected");
}


function formatData(conatinerId) {
    if (typeof conatinerId == "undefined") {
        conatinerId = "";
    }
    else if (conatinerId != "") {
        conatinerId = "#" + conatinerId + " ";
    }
    $(conatinerId + "[formatCurrency]").each(function () {
        var myMoney = "";
        var dollarSymbol = "$";
        if (typeof $(this).attr("nd") != "undefined") { //nd means do'nt show $
            dollarSymbol = "";
        }
        var control = $(this);
        var controlName = control[0].tagName.toLowerCase();
        if (controlName == "input") {
            myMoney = $.trim($(this).val());
            $(this).val(myMoney.formatMoney(dollarSymbol, 2, ',', '.'));
        }
        else {
            myMoney = $.trim($(this).html());
            $(this).html(myMoney.formatMoney(dollarSymbol, 2, ',', '.'));
        }
    })

    $(conatinerId + "[formatDateTime]").each(function () {
        var formatDateTime = $(this).attr("formatDateTime");
        var values = getControlValue(this, true);
        if (values !== null || values != "") {
            var values2 = values.formatDateTimeWithParameter(formatDateTime);
            setControlValue(this, values2, true);
        }
    });

    $(conatinerId + "[formatTimeToHM]").each(function () {
        var values = getControlValue(this, true);
        if (values !== null || values != "") {
            var values2 = values.formatTimeInMinsToHM(formatDateTime);
            setControlValue(this, values2, true);
        }
    });

    $(conatinerId + "[formatPhone]").each(function () {
        var myPhone = "";
        var control = $(this);
        var controlName = control[0].tagName.toLowerCase();
        if (controlName == "input") {
            myPhone = $.trim($(this).val());
            myPhone = myPhone.replace(/(\d{3})(\d{3})(\d{4})/, '($1)$2-$3');
            $(this).val(myPhone);
        }
        else {
            myPhone = $.trim($(this).html());
            myPhone = myPhone.replace(/(\d{3})(\d{3})(\d{4})/, '($1)$2-$3');
            $(this).html(myPhone);
        }
    });

    $(conatinerId + "[minData]").each(function () {  // show soem characters from text stream , show rest when we click on read more
        //        var minData = $.trim($(this).attr("minData"));
        //        if (minData == "") { minData=""}
        var datas = $.trim($(this).html());
        if (datas.indexOf("<restData>") == -1) {
            var charsToShow = parseInt($(this).attr("showChars"));
            var totallength = datas.length;
            if (totallength > charsToShow) {
                var lessData = datas.substring(0, charsToShow);
                var moreData = datas.substring(charsToShow, totallength - 1);
                $(this).html(lessData + "<readMore onclick='showReadMoreData(this,true)'>>>></readMore> <restData>" + moreData + "<readLess onclick='showReadMoreData(this,false)'><<<</readLess> </restData> ");
            }
        }
    });
}

function rateStarsHover(containerId) {
    $("#" + containerId + " span").hover(function () {
        var currentRate = parseInt($(this).attr("rate"));
        for (var i = 1; i <= currentRate; i++) {
            $("#" + containerId + " span[rate=" + i + "]").addClass("hover");
        }
    },
          function () {
              var currentRate = parseInt($(this).attr("rate"));
              for (var i = 1; i <= currentRate; i++) {
                  $("#" + containerId + " span[rate=" + i + "]").removeClass("hover");
              }
          }
         )
    $("#" + containerId + " span").click(function () {
        $("#" + containerId + " span[rate]").removeClass("rateAlways");
        var currentRate = parseInt($(this).attr("rate"));
        for (var i = 1; i <= currentRate; i++) {
            $("#" + containerId + " span[rate=" + i + "]").addClass("rateAlways");
        }
    })
}

function selectRow(thisId, isFromCheckBox) {
    if (typeof isFromCheckBox == "undefined") {
        isFromCheckBox = false;
    }
    if (isFromCheckBox) {
        if ($("#" + thisId).find('input[type=checkbox][chk]').is(":checked")) {
            // $("#" + thisId).find('input[type=checkbox][chk]').prop('checked', false);
            $("#" + thisId).addClass("trSelected");
        }
        else {
            //  $("#" + thisId).find('input[type=checkbox][chk]').prop('checked', true);
            $("#" + thisId).removeClass("trSelected");
        }
    }
    else {
        if ($("#" + thisId).find('input[type=checkbox][chk]').is(":checked")) {
            $("#" + thisId).find('input[type=checkbox][chk]').prop('checked', false);
            $("#" + thisId).removeClass("trSelected");
        } else {
            $("#" + thisId).find('input[type=checkbox][chk]').prop('checked', true);
            $("#" + thisId).addClass("trSelected");
        }
    }
}

 

function concateDateTime(txtDateId, txtTimeId) {
    var dateTime = "";
    if (getControlValue(txtDateId) != "") {
        dateTime = getControlValue(txtDateId);
        if (getControlValue(txtTimeId) != "") {
            dateTime = getControlValue(txtDateId) + " " + getControlValue(txtTimeId);
        }
    }
    return dateTime;
}
 
function uploadFileWithLoading(sender) {
    // url: "../../Handler/uploadFileJS.ashx?folder=Images/Blog/Original",
    var service = $(sender).attr("service");
    var path = $(sender).attr("path");

    var newURL = service + "?folder=" + path;
    if (typeof $(sender).attr("thumb1") != "undefined") {
        newURL = newURL + "&thumb1=" + $(sender).attr("thumb1");
    }
    if (typeof $(sender).attr("thumb2") != "undefined") {
        newURL = newURL + "&thumb2=" + $(sender).attr("thumb2");
    }

    $('form').ajaxForm({
        url: newURL,
        type: "POST",
        beforeSend: function () {
            showProcess("Loading...");
        },
        uploadProgress: function (event, position, total, percentComplete) {

        },
        complete: function (data) {
            hideProcess();
            var imgName = data.responseText.replace("<pre>", "").replace("</pre>", "");
            var spimageName = $(sender).attr("imageName");
            setControlValue(spimageName, imgName);
            var btnToClick = $(sender).attr("btnToClick");
            $("#" + btnToClick).click();
        }
    });
}

function uploadFileWithProgressBar(service, path) {
    var status = $('.status');
    var percent = $('.percent');
    var bar = $('.bar');
    // url: "../../Handler/uploadFileJS.ashx?folder=Images/Blog/Original",
    $('form').ajaxForm({
        url: service + "?folder=" + path,
        type: "POST",
        beforeSend: function () {
            status.fadeOut();
            bar.width('0%');
            percent.html('0%');
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var pVel = percentComplete + '%';
            $(".bar").css({ "width": 4 * percentComplete + "px" });
            percent.html(percentComplete + '%');
        },
        complete: function (data) {
            //status.html(data.responseText).fadeIn().fadeOut(1600);
            // $(".bar").width('100%');
            $(".percent").html('100%');
        }
    });
}

$.fn.hasAttr = function (name, val) {
    if (val) {
        return $(this).attr(name) === val;
    }
    return $(this).attr(name) !== undefined;
};

function getfunctionNameFromFunction(fun) {
    var ret = fun.toString();
    ret = ret.substr('function '.length);
    ret = ret.substr(0, ret.indexOf('('));
    return ret;
}

// makeConfirmBoxDynamic("Are you sure?",methodName,p1,p2,p3,p4, "You will not be able to attend the event!", 'Decline Lead!');
function confirmVMR(title1, confirmMethodToCall, param1, param2, param3, param4, text1, confirmButtonText1) {
    if (typeof text1 == "undefined") { text1 = ""; }
    if (typeof confirmButtonText1 == "undefined") { confirmButtonText1 = "Confirm"; }
    swal({
        title: title1,
        text: text1,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#dd1533',
        confirmButtonText: confirmButtonText1,
        closeOnConfirm: true,
        closeOnCancel: true
    },
          function (isConfirm) {
              if (isConfirm) {
                  confirmMethodToCall(param1, param2, param3, param4);
                  //swal("Success!", "You have refused the invitation!", "success");
              }
              else {

              }
          });
 }



 
