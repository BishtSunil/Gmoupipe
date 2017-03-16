$(document).ready(function () {
    //bindGrid(1, 'tblGrid');
    callServiceMethodForDDl("", "ViewOrders", "loadStatus", "ddlStatus", true, bindGrid, 1, "tblGrid", "");
    callServiceMethodForDDl("", "ViewOrders", "loadShippingSource", "ddlShipper", true, bindGrid, 1, "tblGrid", "");
 
});


function showOrderDetail(orderNo) {
    callServiceMethod("{'orderNo':'" + orderNo + "'}", "ViewOrders", "getOrderDetails", successMethodAfterInsertUpdate, "", "",
                                        false, "", true, showOrderDetail_scs, orderNo, "", "","");
}

function showOrderDetail_scs(orderNo, p2, p3, data) {
    // setControlValue("spRepNumber", orderNo);
    setControlValue("mdlTitle",   data.Id);
    setControlValue("lblOrderId", data.IdEnc);
    setControlValue("lblOrderQty", data.Quantity);
    setControlValue("lblTestName", data.TestName);
    setControlValue("ddlStatus", data.Status);
    setControlValue("lblOrderStatusOld", data.Status);
    setControlValue("lblTotalprice", "$" + data.TotalPrice);
    setControlValue("lblDateCreated", data.DateCreated);
    setControlValue("lblShippingName", data.ShippingName);
    var address = data.ShippingAddress1 + ", " + data.ShippingAddress2 + "<br/>" + data.ShippingCity + ", " + data.ShippingState + ", " + data.ShippingZip + "<br/>" + data.ShippingCountry;
    setControlValue("lblAddress1", address);
    setControlValue("lblPrice", data.Price);
    setControlValue("ddlShipper", data.ShippingSource);
    setControlValue("spFileName", data.PdfPath);
    setControlValue("fileUpload", "");
    setControlValue("lblCustomerName", data.Customer);
    setControlValue("ancCustomerEmail", "<i class='fa fa-envelope'></i> " + data.CustomerEmail);
    $("#ancCustomerEmail").attr("href", "mailto:" + data.CustomerPhoneNo);
    setControlValue("lblTrackingNumber", data.TrackingNumber);
    setControlValue("lblSpecimenNumber", data.SpecimenNumber);
    $("#ancCustomerPhone").attr("href", "tel:" + data.CustomerPhoneNo);
    setControlValue("ancCustomerPhone", "<i class='fa fa-phone'></i>" + data.CustomerPhoneNo.replace(/(\d{3})(\d{3})(\d{4})/, '($1) $2-$3'));
    if (data.d.DiscountType == "f") {
        setControlValue("lblTotalDiscount", "$" + data.d.Discount + "(" + data.PromoCode + ")");
    }
    else {
        setControlValue("lblTotalDiscount", data.Discount + "(" + data.PromoCode + ")");
    }
    if (data.d.PdfPath != "") {
            $("#ancPdfPth").attr("href", "http://localhost:1705/Documents/Orders/" + data.PdfPath);
      //  $("#ancPdfPth").attr("href", "http://mydnanowadmin.vmrvision.com/Documents/Orders/" + data.d.PdfPath);
        $('#txtPdfPth').removeClass("hide");
    }
    else {
        $('#txtPdfPth').addClass("hide");
    }
    formatData();
}



function saveOrderStatusChange() {
    showProcess('Processing...');
    var completeFilePath = document.getElementById('fileUpload').value;
    if (completeFilePath != "") {
      $("#btnUploadFile").click();
       
    }
    else
    { saveOrderStatusChange1(); }
}


//function fileUpload1() {
//    $('form').ajaxForm({
//        // url: "../uploadDocTemplete.ashx",
//        url: "FileUpload.ashx?fl=" + getControlValueN("lblOrderId"),
//        type: "POST",
//        /* reset before submitting */
//        beforeSend: function () {
//        },
//        complete: function (data) {
//            //  var imgName = data.responseText.replace("<pre>", "").replace("</pre>", "");
//            var imageName = data.responseText.replace("<pre>", "").replace("</pre>", "");
            
//            setControlValue("spFileName", imageName)
//            //status.html(data.responseText).fadeIn().fadeOut(1600);
//            saveOrderStatusChange1();
//        }
//    });

//}


function saveOrderStatusChange1(shallCancelChange) {
    if (shallCancelChange) {
        setControlValue("ddlStatus", getControlValue("lblOrderStatusOld"));
    }
    
    else {
        showProcess('Processing...');
        var obj = new Object();
        obj.Id = getControlValueN("lblOrderId");
        obj.Status = getControlValueN("ddlStatus");
        obj.ShippingSource = getControlValueN("ddlShipper");
        obj.TrackingNumber = getControlValueN("lblTrackingNumber");
        obj.SpecimenNumber = getControlValueN("lblSpecimenNumber");
        obj.PdfPath = getControlValueN("spFileName");
        callServiceMethod("{'or':" + JSON.stringify(obj) + ",'prevStatus':'" + getControlValueN("lblOrderStatusOld") + "'}", "ViewOrders", "saveOrderDetails", successMethodAfterInsertUpdate, "", "",
                                        false, "", true, saveOrderStatusChange1_scs, "", "","");
    }
}

function saveOrderStatusChange1_scs() {
    setControlValue("lblOrderStatusOld", getControlValue("ddlStatus"));
    $("#btnCloseMdl").click();
    
    bindGrid(1, 'tblGrid');
    hideProcess();
}
