/// <reference path="../Admin/general.js" />
/// <reference path="../Admin/general.js" />


function AddEmoployee()
{
    $.ajax({
        url: "/Admin/CreateEmployee",
        success: function (data) {
            $('#divEmployee').html(data);

        },
    });
}

//Document Ready 
$(document).ready(function ()
{
    
    $.getScript("/Scripts/Admin/general.js", function(){

      

    });
    $.getScript("/Scripts/Admin/jshelper.js", function () {

   

    });

    $('#btnsignout').click(function () {
        $.post("/Login/Logout", function () {
            location.reload();  // prevents redirection
        });
    });
  
 
    $('#btnEmployeeSearch').click(function () {
        
        var abc = validateTextBoxes('searchContainer');
    
        if (abc == true) {
            
            var employeeID = $('#txtEmployeeSearch').val().trim();
            $.ajax({
                type: 'GET',
                data: { 'employeeID': employeeID },
                url: "/Admin/GetEmployeeDetails",

                success: function (data) {
                    if (data != "") {
                        
                        var txt = "";
                        var wifeDob = new Date(parseInt(data[0].WifeDOB.substr(6))).format("dd/mm/yyyy")
                        var doj = new Date(parseInt(data[0].JoiningDate.substr(6))).format("dd/mm/yyyy")
                        var dob = new Date(parseInt(data[0].BirthDate.substr(6))).format("dd/mm/yyyy")
                        $("#employeedetails").removeClass("hidden");
                        setControlValue("lblfirstname", data[0].FirstName + " " + data[0].MiddleName + " " + data[0].LastName)
                        setControlValue("lblcast", data[0].Caste)
                        setControlValue("lblhusbandname", data[0].FatherName)
                        setControlValue("lblvillage", data[0].Village)
                        setControlValue("lblpostoffice", data[0].PostOffice)
                        setControlValue("lblpatti", data[0].Patti)
                        setControlValue("lblthana", data[0].thana)
                        setControlValue("lbltehsil", data[0].Tehsil)
                        setControlValue("lbldistrict", data[0].District)
                        setControlValue("lblstate", data[0].State)
                        setControlValue("lbldate_of_birth", dob)
                        setControlValue("lbljoiningdate", doj)
                        setControlValue("txtremark", data[0].Remarks);
                        setControlValue("lblcontactname", data[0].Contact)
                        setControlValue("lblqaulification", data[0].Qaulification)
                        setControlValue("lbllocaladdress", data[0].LocalAddress)
                        setControlValue("lblidentificationmark1", data[0].IdentificationMark1)
                        setControlValue("lblidentificationmark2", data.IdentificationMark2)
                        setControlValue("lbluan", data[0].UAN)
                        setControlValue("lblpan", data[0].PAN)
                        setControlValue("lbloffice", data[0].WorkingOffice)
                        setControlValue("lblretirementdate", data[0].UAN)
                        setControlValue("lbldepartment", data[0].WorkingDepartment)
                        setControlValue("lblwifename", data[0].WifeName)
                        setControlValue("lblnexttokin", data[0].NextToKin)
                        setControlValue("lblwifedob", wifeDob)
                        setControlValue("lblnomineename", data[0].NomineeName)
                        setControlValue("lblwifename", data[0].NomineeIdentificationMark1)
                        setControlValue("lblnomineeIdentificationMark1", data[0].NomineeIdentificationMark1)
                        setControlValue("lblnomineeIdentificationMark2", data[0].NomineeIdentificationMark2)
                      
                         
                        document.getElementById('imgemp').setAttribute('src', 'data:image/png;base64,' + data[0].ImageString);
                        var img = ('data:image/png;base64,'+  data[0].ImageString);
                        alert(img)
                        if (data[0].ImageString != "") {
                          
                            txt += "<tr><td><img src= '" + img + "' style='height:20px' ></td>"
                                              + "<td><a href='" + img + "'   ><i class='fa fa-eye'></i></a></td>"
                                                 + "<td><a href='javascript:void(0)' onclick= downloadDoc(" + '"' + img + '"' + ")><img src='../Images/download.png' style='height:20px' /></i></td></tr>";
                        }
                                      
                        if (txt != "") {
                            $("#EmployeesTable").append(txt);

                        }
                                 
                          
                    }
                    else
                    {
                        $("#myModal").modal('show');
                    }
                }

            });
        }
    });



    function editEmployee()
    {

        var employeeID = $('#txtEmployeeSearch').val().trim();
        $.ajax({
            type: 'GET',
            data: { 'employeeID': employeeID },
            url: "/Admin/GetEmployeeDetails",

            success: function (data) {
                $("#employeedetails").removeClass("hidden");  
                var formateddob = new Date(parseInt(data[0].BirthDate.substr(6))).format("dd/mm/yyyy")
                var dojDate = new Date(parseInt(data[0].JoiningDate.substr(6))).format("dd/mm/yyyy")
                var wifeDob = new Date(parseInt(data[0].WifeDOB.substr(6))).format("dd/mm/yyyy")
                setControlValue("lblfirstname", data[0].FirstName + " " + data[0].MiddleName + " " + data[0].LastName)
                setControlValue("lblcast", data[0].Caste)
                setControlValue("lblhusbandname", data[0].FatherName)
                setControlValue("lblvillage", data[0].Village)
                setControlValue("lblpostoffice", data[0].PostOffice)
                setControlValue("lblpatti", data[0].Patti)
                setControlValue("lblthana", data[0].thana)
                setControlValue("lbltehsil", data[0].Tehsil)
                setControlValue("lbldistrict", data[0].District)
                setControlValue("lblstate", data[0].State)
                setControlValue("lbldate_of_birth", formateddob)
                setControlValue("lbljoiningdate", dojDate)
                setControlValue("lblcontactname", data[0].Contact)
                setControlValue("lblqaulification", data[0].Qaulification)
                setControlValue("lbllocaladdress", data[0].LocalAddress)
                setControlValue("lblidentificationmark1", data[0].IdentificationMark1)
                setControlValue("lblidentificationmark2", data.IdentificationMark2)
                setControlValue("lbluan", data[0].UAN)
                setControlValue("lblpan", data[0].PAN)
                setControlValue("txtremark" , data[0].Remarks);
                setControlValue("lbloffice", data[0].WorkingOffice)
                setControlValue("lblretirementdate", data[0].UAN)
                setControlValue("lbldepartment", data[0].WorkingDepartment)
                setControlValue("lblwifename", data[0].WifeName)
                setControlValue("lblnexttokin", data[0].NextToKin)
                setControlValue("lblwifedob", wifeDob)
                setControlValue("lblnomineename", data[0].NomineeName)
                setControlValue("lblwifename", data[0].NomineeIdentificationMark1)
                setControlValue("lblnomineeIdentificationMark1", data[0].NomineeIdentificationMark1)
                setControlValue("lblnomineeIdentificationMark2", data[0].NomineeIdentificationMark2)
            }
        });
    }

    function formatJSONDate(jsonDate) {
        var newDate = dateFormat(jsonDate, "mm/dd/yyyy");
        return newDate;
    }
 
        $('#Searchbyseats').change(function () {
            alert("Done");
        });
    $('#btnSaveEmployee').click(function () {
        var abc = validateTextBoxes('employeeContainer');
        if (abc == true) {
            saveEmployee();
        }
        else
        {
            alert('Please Fill Detail');
        }
        
    });
    function saveEmployee() {
   
       
        var familyobj = new Object();
        familyobj.WifeName = getControlValueN("txtWifeName");
        familyobj.NextToKin = getControlValueN("txtNextkin");
        familyobj.WifeDOB = DateTimeFunction("txtWifeDob");
       
        familyobj.NomineeName = getControlValueN("txtNomineeName");
        familyobj.NomineeIdentificationMark1 = getControlValueN("txtNominee1");
        familyobj.NomineeIdentificationMark2 = getControlValueN("txtNominee2");

        var familydetails = new Object();
        familydetails.Relationship = getControlValueN("txtRelationShip");
        familydetails.MemberName = getControlValueN("txtMemberName");
        var employeeservicerecord = new Object();
        employeeservicerecord.Designation = getControlValueN("txtDesignation");
        employeeservicerecord.workingOffice = getControlValueN("ddlWorkingOfficeservice");
        employeeservicerecord.AppointmentDate = DateTimeFunction("txtAppointmentdate");
        employeeservicerecord.PayScale = getControlValueN("txtPayScale");
        employeeservicerecord.workingDepartment = getControlValueN("ddlDepartMentService");
        
        var obj = new Object();
        obj.EmployeeId=getControlValueN("IDhidden");
        obj.FirstName = getControlValueN("txtFirstName");
        obj.MiddleName = getControlValueN("txtMiddleName");
        obj.LastName = getControlValueN("txtLastName");
        obj.Caste = getControlValueN("txtCast");
        obj.FatherName = getControlValueN("txtFatherName");
        obj.Village = getControlValueN("txtVillage");
        obj.PostOffice = getControlValueN("txtPostOffice");
        obj.Patti = getControlValueN("txtPatti");
        obj.Thana = getControlValueN("txtThana");
        obj.Tehsil = getControlValueN("txtTehsil");
        obj.District = getControlValueN("txtDistrict");
        obj.State = getControlValueN("txtState");
        obj.BirthDate = DateTimeFunction("txtDob");
        obj.JoiningDate = DateTimeFunction("txtDoj");
        obj.Contact = getControlValueN("txtContactNo");
        obj.Qaulification = getControlValueN("ddlQualification");
        obj.LocalAddress = getControlValueN("txtLocalAddress");
        obj.IdentificationMark1 = getControlValueN("txtIdentificationmark1");
        obj.IdentificationMark2 = getControlValueN("txtIdentificationmark2");
        obj.UAN = getControlValueN("txtUAN");
        obj.PAN = getControlValueN("txtPAN");
        obj.Remarks = getControlValueN("txtremark");
        obj.WorkingOffice = getControlValueN("ddlWorkingOffice");
        obj.WorkingDepartment = getControlValueN("ddlDepartMent");
        obj.employeeFamily = familyobj;
        obj.employeeFamilyDetails = familydetails; obj.employeeservicerecord = employeeservicerecord;
        //obj.images = Images;
        //    callServiceMethod("{'or':" + JSON.stringify(obj) + "'}", "", "SaveEmpolyee", successMethodAfterInsertUpdate, "", "",
        //                                    false, "", true, saveEmployee_scs, "", "", "");
        //}

        //function saveEmployee_scs() {
        //    alert('success')
        //}
       
        $.ajax({
            type: "POST",
            url: '/Admin/SaveEmpolyee',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#myModal").modal('show');
                window.location.href ='http://localhost:57387/admin/employee'
                //window.location.href ='http://www.himtrip.com/admin/employee'
            },
            error: OnError
        });
    }

  
    $('#btnSaveEmployeeRegistration').click(function() {
    
    var obj = new Object();
    obj.FirstName=getControlValueN("txtFirstName")
     obj.MiddleName=getControlValueN("txtMiddleName")
      obj.LastName=getControlValueN("txtLastName")
       obj.UserName=getControlValueN("txtUserName")
        obj.Password=getControlValueN("txtPwd")
       obj.ConfirmPassword=getControlValueN("txtConfirmPassword")
          obj.DateOfBirth=getControlValueN("txtDob")
             obj.Department=getControlValueN("ddlDepartMent")
              $.ajax({
            type: "POST",
            url: '/Admin/SaveEmpolyeeRegistartion',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#myModal").modal('show');
                window.location.href = 'http://localhost:57387/admin/EmployeeRegitration'
                //window.location.href ='http://www.himtrip.com/admin/employee'
            },
            error: OnError
        });

    });

    function DateTimeFunction(id)
    {
        var dt = getControlValueN(id);
        var day = dt.split("/");
        var d = new Date(day[2], day[1], day[0]);
        return d.toLocaleDateString();
    }
});


   