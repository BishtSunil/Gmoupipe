USE [GMOUMIS]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAllEmployee]    Script Date: 12-May-16 9:44:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[sp_GetAllEmployee]
 AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT E.empid,E.employee_id,E.first_name,E.middle_name, E.last_name, E.cast,E.father_husband_name,E.village
 ,E.postoffice,E.patti, E.thana,E.tehsil,E.district,E.state,E.date_of_birth,E.date_of_joining,E.contact_number,E.qaulification,E.local_address,
 E.identification_mark_1,E.identification_mark_2,E.UAN,E.PAN,
 F.wife_name,F.next_kin,F.wife_dob,F.nominee_name,F.nominee_identification1,F.nominee_identification2,
 FD.relationship,FD.member_name,O.Office_Name,D.department_name
 
 FROM dbo.tblEmployee E JOIN dbo.tblEmployeeFamily F ON (E.empid=F.employee_id)
 JOIN dbo.tblGmouOffice O ON (E.working_office=O.id)
 JOIN dbo.tblMasterDepartment D ON (E.working_department=D.id)
 JOIN dbo.tblEmployeeFamilyDetails FD ON (E.empid=FD.emp_id)
END
GO

