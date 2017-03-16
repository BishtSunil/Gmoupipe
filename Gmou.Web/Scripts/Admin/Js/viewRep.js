$(document).ready(function () {
 
    bindGrid(1, 'tblGrid');
  //  createDDLFromAnotherDDL("ddlUserType", "cp1_ddlRoleId", false);
});


function showRepDetail(repNo) {
    setControlValue("mdlTitle", "Update Rep");
    $("#divCreatedBy").removeClass("hide");
    $("#divPassword").addClass("hide");
    callServiceMethod("{'repNo':'" + repNo + "'}", "/ViewUsers", "getRepDetails", successMethodAfterInsertUpdate, "", "",
                                        false, "", true, showRepDetail_scs, repNo, "", "");
}

function showRepDetail_scs(repNo, p2, p3, datas) {
    setControlValue("spRepNumber", repNo);
    setControlValue("txtFirstName", datas.FName);
    setControlValue("txtLastName", datas.LName);
    setControlValue("ddlUserType", datas.RoleId);
    setControlValue("txtEmail", datas.Email);
    setControlValue("txtDateOfBirth", datas.DOB.formatDateTimeWithParameter('d,,'));
    setControlValue("txtCity", datas.City);
    setRadioValue("divSexContainer", datas.Gender);
    setControlValue("chkIsActive", datas.isActive);

    formatData();
}

function saveRepCompleteDetail() {
    if (validateTextBoxes("divCreateUser")) {
        showProcess("Loading...");
        var obj = new Object();
        obj.Id = getControlValue("spRepNumber");
        obj.FName = getControlValue("txtFirstName");
        obj.LName = getControlValue("txtLastName");
        obj.Email = getControlValue("txtEmail");
        obj.RoleId = getControlValue("ddlUserType");
        obj.PWD = getControlValue("txtPassword");
        obj.DOB = getControlValue("txtDateOfBirth");
        obj.City = getControlValue("txtCity");
        obj.isActive = getControlValue("chkIsActive");
        obj.Gender = getRadioValue("divSexContainer");
        callServiceMethod("{'us':" + JSON.stringify(obj) + "}", "/ViewUsers", "saveRepDetails", successMethodAfterInsertUpdate, "", "",
                                    false, "", true, saveRepCompleteDetail_scs, "", "", "");
    }
}

function saveRepCompleteDetail_scs(p1, p2, p3, datas) {
    if (getControlValue("spRepNumber") == null) {
        clearAllTextBoxes("divCreateUser");
    }
    $("#Button1").click();
    bindGrid(1, 'tblGrid');
    hideProcess();
}

function showAddNewUser() {
    setControlValue("spRepNumber", "");
    setControlValue("mdlTitle", "Add New User");
   // $("#divAddMode").addClass("hide");
    clearAllTextBoxes("divCreateUser");
    $("#divCreatedBy").addClass("hide");
    $("#divPassword").removeClass("hide");

}

function deleteUsers(id) {
    if (confirm('Are you sure')) {
       callServiceMethod("{'id':'" + id + "'}", "/ViewUsers", "deleteUser", successMethodAfterInsertUpdate, "", "",
                                    false, "", true, deleteUsers_scs, "1", "", "");
    }
}
function deleteUsers_scs(p1, p2, p3, datas) {
    bindGrid(1, 'tblGrid');
}