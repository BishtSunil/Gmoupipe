$(document).ready(function () {
    bindGrid(1, 'tblGrid');
   // document.getElementById("ancAddtaksMdl").click();
    createDDLFromAnotherDDL("ddlUserType", "cp1_ddlRoleId", false);
});


function showPromoCodeDetail(promoCodeId) {
    setControlValue("mdlTitle", "Update Promo Code");
    $("#divCreatedBy").removeClass("hide");
    $("#divPassword").addClass("hide");
    callServiceMethod("{'promoCodeId':'" + promoCodeId + "'}", "/PromoCode", "getPromoCodeDetails", successMethodAfterInsertUpdate, "", "",
                                        false, "", true, showPromoCodeDetail_scs, promoCodeId, "", "");
}

function showPromoCodeDetail_scs(promoCodeId, p2, p3, datas) {
     
    setControlValue("spPromoCodeId", promoCodeId);
    setControlValue("txtTitle", datas.Title);
    setControlValue("txtCode", datas.Code);
    setControlValue("txtDiscount", datas.Discount);
    setControlValue("ddlDiscountType", datas.DiscountType);
    setControlValue("txtStartDate", datas.StartDate);
    setControlValue("txtEndDate", datas.EndDate);
    setControlValue("chkIsActive", datas.isActive);
    formatData("divCreateUser");
}

function savePromoCodeCompleteDetail() {
    if (validateTextBoxes("divCreateUser")) {
        showProcess("Loading...");
        var obj = new Object();
        obj.Id = getControlValue("spPromoCodeId");
        obj.Title = getControlValue("txtTitle");
        obj.Code = getControlValue("txtCode");
        obj.Discount = getControlValue("txtDiscount");
        obj.isActive = getControlValue("chkIsActive");
        obj.DiscountType = getControlValue("ddlDiscountType");
        obj.StartDate = getControlValue("txtStartDate");
        obj.EndDate = getControlValue("txtEndDate");
       callServiceMethod("{'pc':" + JSON.stringify(obj) + "}", "/PromoCode", "savePromoCodeDetails", successMethodAfterInsertUpdate, "", "",
                            false, "", true, savePromoCodeCompleteDetail_scs, "", "", "");
    }
}




function savePromoCodeCompleteDetail_scs(p1, p2, p3, datas) {
    if (getControlValue("spPromoCodeId") == null) {
        clearAllTextBoxes("divCreateUser");
    }
    $("#mdlClose").click();
    bindGrid(1, 'tblGrid');
}

function showAddNewUser() {
    setControlValue("spPromoCodeId", "");
    setControlValue("mdlTitle", "Add New Promo Code");
   // $("#divAddMode").addClass("hide");
    clearAllTextBoxes("divCreateUser");
       
    $("#divCreatedBy").addClass("hide");
     

}

function deletePromoCodeDetails(id) {
    if (confirm('Are you sure')) {
        callServiceMethod("{'id':'" + id + "'}", "/PromoCode", "deletePromoDetails", successMethodAfterInsertUpdate, "", "",
                                    false, "", true, deletePromoCodeDetails_scs, "1", "", "");
    }
}
function deletePromoCodeDetails_scs(p1, p2, p3, datas) {
    bindGrid(1, 'tblGrid');
}