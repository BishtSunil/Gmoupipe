﻿function attachValidation() {
    hideProcess();
    $("span[formatcurrency]").each(function () {
        var formatCurrency = $(this).attr("formatcurrency");
        if (typeof formatCurrency != "undefined") {
            var values = $(this).html();
            var formatCurrency1 = formatCurrency.split(',');
            if (formatCurrency1.length > 1) {
                 $(this).html(values.formatMoney(formatCurrency1[0], formatCurrency1[1], ',', '.'));
            }
        }
    });


   

    $("input[type=text]").each(function () {
        var formatCurrency = $(this).attr("formatcurrency");
        if (typeof formatCurrency != "undefined") {
            var values = this.value;
            var formatCurrency1 = formatCurrency.split(',');
            if (formatCurrency1.length > 1) {
                this.value = values.formatMoney(formatCurrency1[0], formatCurrency1[1], ',', '.');
            }
        }


        var access = $(this).attr("accesskey")
        if (access == 'a') {
            var currentClass = $(this).attr("class")
            // this.setAttribute("onfocus", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + "')")
            this.setAttribute("onblur", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + " ValidBreak')")
        }

        if ($(this).attr("validat") == "email") {
            var currentClass = $(this).attr("class");
            //    this.setAttribute("onfocus", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + "')")
            this.setAttribute("onblur", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + " ValidBreak')")
        }
        if ($(this).attr("validat") == "url") {
            var currentClass = $(this).attr("class");
            //    this.setAttribute("onfocus", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + "')")
            this.setAttribute("onblur", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + " ValidBreak')")
        }
    });
    $("textarea").each(function() {
        var access = $(this).attr("accesskey")
        if (access == 'a') {
            var currentClass = $(this).attr("class")
           // this.setAttribute("onfocus", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + "')")
            this.setAttribute("onblur", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + " ValidBreak')")
        }
    });
    $("input[type=password]").each(function() {
        var access = $(this).attr("accesskey")
        if (access == 'a') {
            var currentClass = $(this).attr("class")
          //  this.setAttribute("onfocus", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + "')")
            this.setAttribute("onblur", "txtBoxFocus(this,'" + currentClass + "','" + currentClass + " ValidBreak')")
        }
    });
//    $("select").each(function() {
//        var access = $(this).attr("accesskey")
//        if (access == 'a') {
//            var currentClass = $(this).attr("class")
//            this.setAttribute("onClick", "ddlFocus(this,'" + currentClass + "','" + currentClass + "ValidBreak')")
//        }
//    });
  
}
 

// this is to validate email address
function validateEmailFormat(val) {
    if (val.length > 0) {
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        if (reg.test(val) == false) {
            return false;
        }
        else
            return true;
    }
    else { return true; }
}
// this is to validate email address
function validateURLFormat(val) {
    if (val.length > 0) {
        var reg = /(http(s)?:\\)?([\w-]+\.)+[\w-]+[.com|.in|.org]+(\[\?%&=]*)?/;
        if (reg.test(val) == false) {
            return false;
        }
        else
            return true;
    }
    else { return true; }
}


function changePwd(id1, id2) {
    var browser = navigator.userAgent.toLowerCase()
    if (browser.indexOf("msie") > -1) {
        browser = "ie"
    }
    else {
        browser = "other"
    }
    var check = true;
    check = checkTextBoxes();
    if (!check)
        return false;
    if (document.getElementById(id1).value != document.getElementById(id2).value) {
        if (browser == "other") {
            document.getElementById(id2).setAttribute("class", "txtValidation")
        }
        else {
            document.getElementById(id2).setAttribute("className", "txtValidation")
        }
        
        check = false;
    }
    return check;
}


function txtBoxFocus(ids, cssclass1, cssclass2) {
    var access = $("#" + ids.id).attr("accesskey")
    var browser = navigator.userAgent.toLowerCase()
    if (browser.indexOf("msie") > -1) {
        browser = "ie"
    }
    else {
        browser = "other"
    }
    document.getElementById(ids.id).setAttribute("class", cssclass1)
    var data = document.getElementById(ids.id).value;
    if (data == "" && access=="a") {
        if (browser == "other") {
            document.getElementById(ids.id).setAttribute("class", cssclass2)
        }
        else {
            //document.getElementById(ids.id).setAttribute("className", cssclass2)
            document.getElementById(ids.id).className = cssclass2;
        }
    }
 
    if ($(ids).attr("validat") == "email") {
      //  document.getElementById(ids.id).setAttribute("class", cssclass1)
        var email = $.trim(document.getElementById(ids.id).value);
        if (!validateEmailFormat(email)) {
            if (browser == "other") {
                document.getElementById(ids.id).setAttribute("class", cssclass2)
            }
            else {
                //document.getElementById(ids.id).setAttribute("className", cssclass2)
                document.getElementById(ids.id).className = cssclass2;
            }
            if ($("#" + ids.id).parent().children(".emailNotValid").length == 0) {
                $("#" + ids.id).parent().append(" <div style='margin-top:-67px;' class='emailNotValid tooltip fade top in'>"
                                                       + "<div class='tooltip-arrow'></div>"
                                                      + "<div class='tooltip-inner'>Email not in valid format.</div>"
                                                       + "  </div>" );
            }
            else {
                if ($("#" + ids.id).parent().children(".in").length == 0) {
                    $("#" + ids.id).parent().children(".emailNotValid").addClass("in");
                }
            }
        }
        else {
            if ($("#" + ids.id).parent().children(".emailNotValid").length >= 1) {
                $("#" + ids.id).parent().children(".emailNotValid").removeClass("in");
            }
        }
    }

     if ($(ids).attr("validat") == "url") {
        //  document.getElementById(ids.id).setAttribute("class", cssclass1)
        var email = document.getElementById(ids.id).value;
        if (!validateURLFormat(email)) {
            if (browser == "other") {
                document.getElementById(ids.id).setAttribute("class", cssclass2)
            }
            else {
                //document.getElementById(ids.id).setAttribute("className", cssclass2)
                document.getElementById(ids.id).className = cssclass2;
            }
            if ($("#" + ids.id).parent().children(".emailNotValid").length == 0) {
                $("#" + ids.id).parent().append(" <div style='margin-top:-67px;' class='emailNotValid tooltip fade top in'>"
                                                       + "<div class='tooltip-arrow'></div>"
                                                      + "<div class='tooltip-inner'>URL not in valid format.</div>"
                                                       + "  </div>");
            }
            else {
                if ($("#" + ids.id).parent().children(".in").length == 0) {
                    $("#" + ids.id).parent().children(".emailNotValid").addClass("in");
                }
            }
        }
        else {
            if ($("#" + ids.id).parent().children(".emailNotValid").length >= 1) {
                $("#" + ids.id).parent().children(".emailNotValid").removeClass("in");
            }
        }
    }

}


function ddlFocus(ids, cssclass1, cssclass2) {
    var browser = navigator.userAgent.toLowerCase()
    if (browser.indexOf("msie") > -1) {
        browser = "ie"
    }
    else {
        browser = "other"
    }
    document.getElementById(ids.id).setAttribute("class", cssclass1)
    var val = getDDLSelectedText(ids.id)
    if (val == "--Select--") {
        if (browser == "other") {
            document.getElementById(ids.id).setAttribute("class", cssclass2)
        }
        else {
            document.getElementById(ids.id).className = cssclass2;
        }
    }
}

function validateTextBoxes(containerId) {
    var isFocused = false;
    var browser = navigator.userAgent.toLowerCase()
    if (browser.indexOf("msie") > -1) {
        browser = "ie"
    }
    else {
        browser = "other"
    }
    var check = true;


    $("#" + containerId + " [matchwith]").each(function () {
        var parentId = $(this).attr("matchwith");
        var parentValue = getControlValue(parentId);
        var thisValue = getControlValue(this.id);
        if (thisValue != parentValue) {
            check = false; 
              var currentClass = $(this).attr("class")
                currentClass = currentClass.replace("ValidBreak", "");
            if (browser == "other") {
             
            this.setAttribute("class", currentClass + " ValidBreak")
            }
            else {
                document.getElementById(this.id).className = currentClass + " ValidBreak";
            }
        }
       
    })


    $("#" + containerId + "  select[accesskey]").each(function () {
        if (this.value == "" || this.value == "0") {
            var access = $(this).attr("accesskey")

            if (access == 'a') {
                var currentClass = $(this).attr("class")
                currentClass = currentClass.replace("ValidBreak", "");
                if (browser == "other") {
                    this.setAttribute("class", currentClass + " ValidBreak")
                }
                else {
                    // this.setAttribute("className", currentClass + " ValidBreak");
                    document.getElementById(this.id).className = currentClass + " ValidBreak";
                }
                check = false;
                if (!isFocused) {
                    document.getElementById(this.id).focus();
                }
                isFocused = true;
            }
        }
    });


    $("#" + containerId + "  input[type=text], #" + containerId + " input[disabled!='disabled']").each(function () {
        if (this.value == "") {
            var access = $(this).attr("accesskey")
            if (access == 'a') {
                var currentClass = $(this).attr("class")
                currentClass = currentClass.replace("ValidBreak", "");
                if (browser == "other") {
                    this.setAttribute("class", currentClass + " ValidBreak")
                }
                else {
                    // this.setAttribute("className", currentClass + " ValidBreak");
                    document.getElementById(this.id).className = currentClass + " ValidBreak";
                }
                check = false;
                if (!isFocused) {
                    document.getElementById(this.id).focus();
                }
                isFocused = true;
            }
        }

      

        if ($(this).attr("validat") == "email") {
            //  document.getElementById(ids.id).setAttribute("class", cssclass1)
            var email = this.value;
            if (!validateEmailFormat(email)) {
                var currentClass = $(this).attr("class")
                cssclass2 = currentClass.replace("ValidBreak", "");
                cssclass2 = cssclass2 + " ValidBreak";
                if (browser == "other") {
                    document.getElementById(this.id).setAttribute("class", cssclass2)
                }
                else {
                    //document.getElementById(this.id).setAttribute("className", cssclass2)
                    document.getElementById(this.id).className = cssclass2;
                }
                if ($("#" + this.id).parent().children(".emailNotValid").length == 0) {
                    $("#" + this.id).parent().append(" <div style='margin-top:-67px;' class='emailNotValid tooltip fade top in'>"
                                                       + "<div class='tooltip-arrow'></div>"
                                                      + "<div class='tooltip-inner'>Email not in valid format.</div>"
                                                       + "  </div>");
                }
                else {
                    if ($("#" + this.id).parent().children(".in").length == 0) {
                        $("#" + this.id).parent().children(".emailNotValid").addClass("in");
                    }
                }
                check = false;
                if (!isFocused) {
                    document.getElementById(this.id).focus();
                }
                isFocused = true;
            }
            else {
                if ($("#" + this.id).parent().children(".emailNotValid").length >= 1) {
                    $("#" + this.id).parent().children(".emailNotValid").removeClass("in");
                }
            }
        }



        if ($(this).attr("validat") == "url") {
            //  document.getElementById(ids.id).setAttribute("class", cssclass1)
            var email = this.value;
            if (!validateURLFormat(email)) {
                var currentClass = $(this).attr("class")
                cssclass2 = currentClass.replace("ValidBreak", "");
                cssclass2 = cssclass2 + " ValidBreak";
                if (browser == "other") {
                    document.getElementById(this.id).setAttribute("class", cssclass2)
                }
                else {
                    //document.getElementById(this.id).setAttribute("className", cssclass2)
                    document.getElementById(this.id).className = cssclass2;
                }
                if ($("#" + this.id).parent().children(".emailNotValid").length == 0) {
                    $("#" + this.id).parent().append(" <div style='margin-top:-67px;' class='emailNotValid tooltip fade top in'>"
                                                       + "<div class='tooltip-arrow'></div>"
                                                      + "<div class='tooltip-inner'>URL not in valid format.</div>"
                                                       + "  </div>");
                }
                else {
                    if ($("#" + this.id).parent().children(".in").length == 0) {
                        $("#" + this.id).parent().children(".emailNotValid").addClass("in");
                    }
                }
                check = false;
                if (!isFocused) {
                    document.getElementById(this.id).focus();
                }
                isFocused = true;
            }
            else {
                if ($("#" + this.id).parent().children(".emailNotValid").length >= 1) {
                    $("#" + this.id).parent().children(".emailNotValid").removeClass("in");
                }
            }
        }


        //        if ($("#" + this.id).parent().children("dfn").length > 0) {
        //            var emData = $(this).parent().children("dfn").html();
        //            if (emData.indexOf("email") > -1) {
        //                if (!validateEmailFormat(this.value)) {
        //                    var currentClass = $(this).attr("class")
        //                    currentClass = currentClass.replace("ValidBreak", "");
        //                    if (browser == "other") {
        //                        this.setAttribute("class", currentClass + "ValidBreak")
        //                    }
        //                    else {
        //                        // this.setAttribute("className", currentClass + "ValidBreak");
        //                        document.getElementById(this.id).className = currentClass + "ValidBreak";
        //                    }
        //                    check = false;
        //                }
        //            }
        //        }
    });
    $("#" + containerId + "  input[type=password]").each(function() {
        if (this.value == "") {
            var access = $(this).attr("accesskey")

            if (access == 'a') {
                var currentClass = $(this).attr("class")
                currentClass = currentClass.replace("ValidBreak", "");
                if (browser == "other") {
                    this.setAttribute("class", currentClass + " ValidBreak")
                }
                else {
                    // this.setAttribute("className", currentClass + "ValidBreak");
                    document.getElementById(this.id).className = currentClass + " ValidBreak";
                }
                check = false;
                if (!isFocused) {
                    document.getElementById(this.id).focus();
                }
                isFocused = true;
            }
        }
    });
    
    $("#" + containerId + "  textarea").each(function() {
        if (this.value == "") {
            var access = $(this).attr("accesskey")

            if (access == 'a') {
                var currentClass = $(this).attr("class")
                currentClass = currentClass.replace("ValidBreak", "");
                if (browser == "other") {
                    this.setAttribute("class", currentClass + " ValidBreak")
                }
                else {
                    // this.setAttribute("className", currentClass + " ValidBreak");
                    document.getElementById(this.id).className = currentClass + " ValidBreak";
                }
                check = false;
                if (!isFocused) {
                    document.getElementById(this.id).focus();
                }
                isFocused = true;
            }
        }
    });

    $("#" + containerId + "  input[type='checkbox']").each(function () {
        if (!this.checked) {
            var access = $(this).attr("accesskey")
            if (access == 'a') {
                var currentClass = "ValidBreak";
                if (browser == "other") {
                    //   this.setAttribute("class", currentClass + " ValidBreak")
                    $(this).parent().addClass("ValidBreak");
                }
                else {
                    // this.setAttribute("className", currentClass + " ValidBreak");
                    //document.getElementById(this.id).className = currentClass + " ValidBreak";
                }
                check = false;
                if (!isFocused) {
                    document.getElementById(this.id).focus();
                }
                isFocused = true;
            }
        }
    });
 


    if (check) {
      
    }
    return check;
}



// This is to reset all textBoxes
function resetTextBoxes() {
    $("input[type=text]").each(function() {
        this.value = "";
    });
    $("textarea").each(function() {
        this.value = "";
    });
}

 
function checkTextBoxeswithclass2(clas, processText) {
    var check = true;
    $("input[type=text]").each(function() {
        if (this.value == "") {
            var access = $(this).attr("accesskey")
            if (access == 'a') {
                this.setAttribute("class", clas)
                check = false;
            }
        }
    });
    $("input[type=password]").each(function() {
        if (this.value == "") {
            var access = $(this).attr("accesskey")
            if (access == 'a') {
                this.setAttribute("class", clas)
                check = false;
            }
        }
    });
    if (!check) {
        showProcess('')
    }
    if (check) {
        if (confirm('Are you sure')) {
            check = true;
        }
        else {
            check = false;
        }
    }
    return check;
}
 


//this functions allows to type numbers only   onkeypress="return numbersonly(event)"
function numbersonly(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
        if (unicode < 48 || unicode > 57) //if not a number
            return false //disable key press
    }
}

//this functions allows to type price only with one decimal  onkeypress= "return priceonly(event,this)"
function priceonly(e, thiss) {
     var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
        if (unicode == 46) {
            var val = $(thiss).val();
            var decimalCount = (val.split(".").length - 1)
            if (decimalCount >= 1)
                return false
            else {
                return true
            }
        }
        else if (unicode < 48 || unicode > 57) //if not a number
            return false //disable key press
        else {
            var price = $(thiss).val();
            price = price.formatMoney('', 2, '', '.');
            if (!(/^\d*(?:\.\d{0,2})?$/.test(price))) {
                return false;
            }
            else
                return true;
        }
    }
}


function disableAll(containerId) {
    $("#" + containerId + " input").each(function() {
            this.setAttribute("disabled", "disabled")
    });
}

function enableAll(containerId) {
    $("#" + containerId + " input").each(function() {
        this.removeAttribute("disabled")
    });
}

//Usage : src.toBoolean()
String.prototype.toBoolean = function () {
    var vals = this.toString().toLowerCase();
    if (vals == "false" || vals == "" || vals=="0") {
        return false;
    }
    else {
        return true;
    }
}

//Usage : src.toFloat()
String.prototype.toFloat = function () {
    return parseFloat(this.toString().replace("$", "").replaceAll(",", ""));
}




//Usage : src.replaceAll('value coes here','new value')
String.prototype.replaceAll = function (find, replace) {
    //return this.replace(new RegExp(find, 'g'), replace);
    return this.split(find).join(replace);
}

//Usage : val.parseFloat2()
String.prototype.parseFloat2 = function () {
    return parseFloat(this.replaceAll("$", "").replaceAll(",", ""));
}
function parseFloat2(val) {
    if (val == "") {
        return "";
    }
    else {
        return parseFloat(val.replaceAll("$", "").replaceAll(",", ""));
    }
}

//var myMoney = 3543.75873;
//var formattedMoney =   myMoney.formatMoney('$',2, ',', '.'); // "$3,543.76"
String.prototype.formatMoney = function (symbol, decPlaces, thouSeparator, decSeparator) {

    var n = this.replaceAll("$", "").replaceAll(",", "").replaceAll("(", "").replaceAll(")", ""),
        decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
        decSeparator = decSeparator == undefined ? "." : decSeparator,
        thouSeparator = thouSeparator == undefined ? "," : thouSeparator,
        sign = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
   // return symbol + sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
    if (this.toString().indexOf("-") == -1) {
        return symbol + sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
    }
    else {
        return "("+symbol + sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "")+")";
    }
};

// Usage : String.IsNullOrEmpty('value coes here')
String.IsNullOrEmpty = function (value) {
    var isNullOrEmpty = true;
    if (value) {
        if (typeof (value) == 'string') {
            if (value.length > 0)
                isNullOrEmpty = false;
        }
    }
    return isNullOrEmpty;
}

// Usage : value.IsNullOrEmpty2()
String.prototype.IsNullOrEmpty2 = function (value) {
    var values = $.trim(this);
    var isNullOrEmpty = true;
    if (values) {
        if (typeof (values) == 'string') {
            if (values.length > 0)
                isNullOrEmpty = false;
        }
    }
    return isNullOrEmpty;
}

String.prototype.formatTimeInMinsToHM = function () {
    if ($.trim(this) != "") {
        var val = $.trim(this);
        if (val.split(":").length == 1) {
            var hrs = parseInt(parseInt(val) / 60);
            var mins = parseInt(val) % 60;
            if (mins < 10) {
                mins = "0" + parseInt(mins);
            }
            return hrs + ":" + mins;
        }
        else {
            return val;
        }
    }
    else {
        return "";
    }
}

//Usage:    var date = "10/27/2014 3:54:31 PM";  var newDate = date.formatDateTimeWithParameter('d,,a');
String.prototype.formatDateTimeWithParameter = function (value) {
    if ($.trim(this) != "") {

        var dateSplit = value.split(',');
        var date = $.trim(this);
        if (date.length > 11) {
            var newDate = "";
            if (dateSplit[0] != "") {
                newDate = newDate + date.split(' ')[0] + " ";
            }
            if (dateSplit[1] != "") {
                var time = date.split(' ')[1].split(":");
                if (parseInt(time[0]) < 10) { time[0] = "0" + parseInt(time[0]); }
                if (parseInt(time[1]) < 10) { time[1] = "0" + parseInt(time[1]); }
                var newTime = time[0] + ":" + time[1];
//                if (time.length == 3) {  // This is for milli sec
//                    if (parseInt(time[2]) < 10) { time[2] = "0" + parseInt(time[2]); }
//                    newTime = newTime + ":" + time[2]
                //                }

                //newDate = newDate + date.split(' ')[1] + " ";
                newDate = newDate + newTime + " ";
            }
            if (dateSplit[2] != "") {
                newDate = newDate + date.split(' ')[2];
            }
            return newDate;
        }
        else {
            return date;
        }
    }
    else {
        return "";
    }
}






//Uasge: var end = new Date('11/25/2014 10:10 AM').addHours(2)
Date.prototype.addHours = function (h) {
    this.setHours(this.getHours() + h);
    return this;
}



function rgbToHex(red, green, blue) {
    var rgb = blue | (green << 8) | (red << 16);
    return '#' + (0x1000000 + rgb).toString(16).slice(1);
}


function disableAllControlsInContainer(containerId) {
    $('#' + containerId).find('input, textarea, button, select, btn').attr('disabled', 'disabled');
    $('#' + containerId).find('[nd]').removeAttr('disabled');
    $('#' + containerId + " img").addClass("hide");
    $('#' + containerId + " .iconTray").addClass("hide");
    $('#' + containerId + " [hide]").addClass("hide");
    $('#' + containerId).addClass("disableAll");
}

function enableAllControlsInContainer(containerId) {
    $('#' + containerId).find('input, textarea, button, select, btn').removeAttr('disabled', 'disabled');
    $('#' + containerId + " img").removeClass("hide");
    $('#' + containerId + " .iconTray").removeClass("hide");
    $('#' + containerId + " [hide]").removeClass("hide");
    $('#' + containerId).removeClass("disableAll");
}



function clearAllTextBoxes(containerId) {
    if ($("#" + containerId).length > 0) {
        $("#" + containerId + " input, #" + containerId + "select").each(function () {
            var control = $(this);
            var controlName = control[0].tagName.toLowerCase();
            if (controlName == "input") {
                var controlType = control[0].type.toLowerCase();
                if (controlType == "checkbox") {
                    //   values = $("#" + id).is(":checked");
                    this.checked =false
                }
                else if (controlType == "text") {//textboxex
//                    if ($(this).attr("value")) {
//                        this.value = $(this).attr("value");
//                    }
//                    else {
                        this.value = "";
                   // }
                }
            }
            else if (controlName == "select" || controlName == "textarea") {
                if ($(this).parent().children("ddlvalue").length > 0) {
                    var ddlValue = $(this).parent().children("ddlvalue").html();
                    $(this).val(ddlValue);
                }
            }
            else {
                $(control).html('');
            }
        });
    }
}

function formatDateZeroAppend(dateTimePassed) {
    var currentTime = new Date();
    if (typeof dateTimePassed != "undefined") {
        currentTime = new Date(dateTimePassed);
    }
    var year = currentTime.getFullYear();
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    if (parseInt(day) < 10) { day = "0" + parseInt(day); }
    if (parseInt(month) < 10) { month = "0" + parseInt(month); }
    return month + "/" + day + "/" + year;
}




function getCurrentDateTimeInJS(dateTimePassed) {
    var now = new Date();
    if (typeof dateTimePassed != "undefined") {
        now = new Date(dateTimePassed);
    }
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();
    var amPm = "AM";
    if (hour < 12) {
        amPm = "AM";
    }
    if (hour >= 12) {
        if (hour == 12) {
            hour = 12;
        }
        else {
            hour = hour - 12;
        }
        amPm = "PM"
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
    var dateTime = month + '/' + day + '/' + year + ' ' + hour + ':' + minute + ' ' + amPm;
    return dateTime;
}


function getCurrentDateInJS() {
    return getCurrentDateTimeInJS().split(' ')[0];
}

//function addDaysInDateInJS(datePassed, daysToAdd) {
//    var dt = new Date(datePassed);
//    var date = new Date(dt.getDate() + daysToAdd);
//    return getCurrentDateTimeInJS(date).split(' ')[0];
//}

function addDaysInDateInJS(datePassed, daysToAdd) {
    var dt = new Date(datePassed);
    var date = new Date(dt.setDate(dt.getDate() + daysToAdd));
    //var date = new Date(dt.getDate() + daysToAdd);
    return getCurrentDateTimeInJS(date).split(' ')[0];
}

function getCurrentTimeInJS() {
    var time = getCurrentDateTimeInJS().split(' ');
    return time[1] + " " + time[2];
}

//pass format '12/30/2014 9:10 AM'
function getDifferenceBetweenTwoDates(start, end) {
    var startDate = new Date(start);
    var endDate = new Date(end);
    var seconds = (endDate.getTime() - startDate.getTime()) / 1000;
    var hrs = parseInt(seconds / 3600);
    var mins = parseInt((seconds % 3600) / 60);
    return [hrs, mins];
}

function alphanumeric(e, t) {
    try {

        if (window.event) {
            var charCode = window.event.keyCode;
        } else if (e) {
            var charCode = e.keyCode || e.charCode;
        } else {
            return true;
        }
        if ((charCode > 64 && charCode < 91) || (charCode == 9 || charCode == 8 || charCode == 32) || (charCode > 96 && charCode < 123)) return true;
        else return false;
    } catch (err) {
        alert(err.Description);
    }
}

