USE [GMOUMIS]
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveEmployeeDetails]    Script Date: 05/11/2016 22:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveEmployeeDetails] 
(
@firstname nvarchar(50),
@middlename nvarchar(50),
@lastname nvarchar(50), 
@caste nvarchar(50), 
@fathername nvarchar(50), 
@village nvarchar(50), 
@postoffice nvarchar(50), 
@patti nvarchar(50), 
@thana nvarchar(50), 
@tehsil nvarchar(50), 
@district nvarchar(50), 
@state nvarchar(50), 
@birthdate date, 
@joiningdate date, 
@contactnumber bigint, 
@qualification nvarchar(50), 
@localaddress nvarchar(1000),
@identification1 nvarchar(100)=NULL,
@identification2 nvarchar(100)=NULL,
@uan nvarchar(50)=NULL,
@pan nvarchar(50)=NULL,
@workingoffice int, 
@workingdepartment int,
@empID INT, 
@IsUpdate BIT

)

AS


	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if @IsUpdate=0
	BEGIN
	DECLARE @EmployeeID nvarchar(10)
	
set @EmployeeID  = '2016'+(Select isnull(max(empid),1) from dbo.tblEmployee)
INSERT INTO dbo.tblEmployee VALUES (@EmployeeID, @firstname ,@middlename ,@lastname ,@caste ,@fathername ,@village ,@postoffice,@patti  ,@thana ,@tehsil ,@district ,@state,@birthdate ,@joiningdate ,@contactnumber ,@qualification ,@localaddress ,@identification1 ,@identification2 ,@uan,@pan ,@workingoffice ,@workingdepartment,0,getdate(),'Admin')
  SELECT CAST(scope_identity() AS int)
  END
  ELSE
  BEGIN
 UPDATE dbo.tblEmployee SET first_name =@firstname,middle_name=@middlename,last_name=@lastname,[cast]=@caste, father_husband_name=@fathername, village=@village, patti=@patti,thana=@thana, tehsil=@tehsil, district=@district,postoffice=@postoffice,[state]=@state,date_of_birth=@birthdate,date_of_joining=@joiningdate,contact_number=@contactnumber,qaulification=@qualification,local_address=@localaddress,identification_mark_1=@identification1,identification_mark_2=@identification2,UAN=@uan,PAN=@pan,working_office=@workingoffice,working_department=@workingdepartment,is_deleted=0,last_access_date=getdate(),last_modified_by='Admin'
 WHERE empid=@empID
 --SELECT CAST(scope_identity() AS int)
  END
 
