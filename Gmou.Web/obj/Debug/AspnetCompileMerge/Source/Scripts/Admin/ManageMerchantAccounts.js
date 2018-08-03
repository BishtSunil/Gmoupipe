
$(document).ready(function () {
    ;
    alert('1')
    loadMerchantAccount();

});

function loadMerchantAccount() {
    ;
    callJTemplateServiceMethod("", "/Admin", "getAllEmployee", "divBinBaseExist", "divBinBaseExistTemplateJS", true, formatData, "", "", "");
}

 
