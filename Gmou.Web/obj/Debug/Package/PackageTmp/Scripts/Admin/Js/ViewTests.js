$(document).ready(function () {
    bindGrid(1, 'tblGrid');
  //  createDDLFromAnotherDDL("ddlUserType", "cp1_ddlRoleId", false);
});


function showTestDetail(testId) {
    setControlValue("mdlTitle", "Update Test");
    $("#divCreatedBy").removeClass("hide");
    $("#divPassword").addClass("hide");
    callServiceMethod("{'testId':'" + testId + "'}", "/ViewTests", "getTestDetails", successMethodAfterInsertUpdate, "", "",
                                        false, "", true, showTestDetail_scs, testId, "", "");
}

function showTestDetail_scs(testId, p2, p3, datas) {
 
    setControlValue("spTestId", datas.Id);
    setControlValue("txtTitle", datas.Title);
    setControlValue("txtTestCode", datas.Code);
    setControlValue("txtPrice", datas.Price);
    setControlValue("txtSortOrder", datas.SortOrder);
    setControlValue("txtDescription", datas.Description);
    setControlValue("chkIsActive", datas.isActive);
    setControlValue("txtAbbreviation", datas.Abbrevation);
  

    formatData();
}

function saveTestCompleteDetail() {
    if (validateTextBoxes("divCreateUser")) {
        showProcess("Loading...");
        var obj = new Object();
        obj.Id = getControlValue("spTestId");
        obj.Title = getControlValue("txtTitle");
        obj.Code = getControlValue("txtTestCode");
        obj.Price = getControlValue("txtPrice");
        obj.SortOrder = getControlValue("txtSortOrder");
        obj.Description = getControlValue("txtDescription");
        obj.isActive = getControlValue("chkIsActive");
        obj.Abbrevation = getControlValue("txtAbbreviation");
        callServiceMethod("{'tl':" + JSON.stringify(obj) + "}", "/ViewTests", "saveTestDetails", successMethodAfterInsertUpdate, "", "",
                                    false, "", true, saveTestCompleteDetail_scs, "", "", "");
    }
}




function saveTestCompleteDetail_scs(p1, p2, p3, datas) {
    if (getControlValue("spTestId") == null) {
        clearAllTextBoxes("divCreateUser");
    }
    $("#mdlClose").click();
    bindGrid(1, 'tblGrid');
}

function showAddNewUser() {
    setControlValue("spTestId", "");
    setControlValue("mdlTitle", "Add New Test");
   // $("#divAddMode").addClass("hide");
    clearAllTextBoxes("divCreateUser");
    $("#txtDescription").val("");
    $("#divCreatedBy").addClass("hide");
     

}

function deleteTestDetails(id) {
    if (confirm('Are you sure')) {
        callServiceMethod("{'id':'" + id + "'}", "/ViewTests", "deleteTestDetails", successMethodAfterInsertUpdate, "", "",
                                    false, "", true, deleteTestDetails_scs, "1", "", "");
    }
}
function deleteTestDetails_scs(p1, p2, p3, datas) {
    bindGrid(1, 'tblGrid');
}