USE [GMOUMIS]
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveEmployeeFamily]    Script Date: 05/11/2016 22:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveEmployeeFamily](
	@wife_name nvarchar(50), 
	@next_kin nvarchar(50),
	@wife_dob date, 
	@nominee_name nvarchar(50),
	@nominee_identification1 nvarchar(500),
	@nominee_identification2 nvarchar(500),
	@employee_id int, 
	@IsUpdate bit )
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    IF @IsUpdate=0
    BEGIN
	INSERT INTO dbo.tblEmployeeFamily VALUES(@wife_name,@next_kin,@wife_dob,@nominee_name,@nominee_identification1,@nominee_identification2,@employee_id)
	end
	IF @IsUpdate=1
	begin
	UPDATE  dbo.tblEmployeeFamily SET wife_name =@wife_name,next_kin=@next_kin,wife_dob=@wife_dob,nominee_name=@nominee_name,nominee_identification1=@nominee_identification1,nominee_identification2=@nominee_identification2
	WHERE employee_id=@employee_id
	end
END
