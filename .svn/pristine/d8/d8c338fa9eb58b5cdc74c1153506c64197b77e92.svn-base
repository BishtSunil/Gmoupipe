-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_GetEmployeeByID-- 2017
	(
	@EmpID int
	)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
DECLARE @EMPLOYEEID INT;

SET @EMPLOYEEID = (SELECT empid FROM dbo.tblEmployee WHERE employee_id =@EmpID)
    -- Insert statements for procedure here
	SELECT E.first_name,E.middle_name, E.last_name, E.cast,E.father_husband_name,E.village
	,E.postoffice,E.patti, E.thana,E.tehsil,E.district,E.state,E.date_of_birth,E.date_of_joining,E.contact_number,E.qaulification,E.local_address,
	E.identification_mark_1,E.identification_mark_2,E.UAN,E.PAN,
	F.wife_name,F.next_kin,F.wife_dob,F.nominee_name,F.nominee_identification1,F.nominee_identification2,
	FD.relationship,FD.member_name,O.Office_Name,D.department_name
	
	FROM dbo.tblEmployee E JOIN dbo.tblEmployeeFamily F ON (E.empid=F.employee_id)
	JOIN dbo.tblGmouOffice O ON (E.working_office=O.id)
	JOIN dbo.tblMasterDepartment D ON (E.working_department=D.id)
	JOIN dbo.tblEmployeeFamilyDetails FD ON (E.empid=FD.emp_id)
		
	WHERE E.empid=@EMPLOYEEID
	
END
GO
