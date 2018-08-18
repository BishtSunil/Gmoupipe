
$(document).ready(function () {
    loadMerchantAccount();

});

function loadMerchantAccount() {
    ;
        $.ajax({
            type: 'GET',
            data: {},
            url: "/Admin/ShowEmployee",
            success: function (data) {
                alert(data);
                for (i in data)
                {
                    setControlValue("lblfirstname", data[i].FirstName)
                   
                }
                
            }
        });
  



    // callJTemplateServiceMethod("", "/Admin", "ShowEmployee", "divBinBaseExist", "divBinBaseExistTemplateJS", true, formatData, "", "", "");
}


  