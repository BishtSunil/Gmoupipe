USE [GMOUMIS]
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveEmployeeFamilyDetails]    Script Date: 05/11/2016 22:42:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveEmployeeFamilyDetails]
	(
	@emp_id int, 
	@relationship nvarchar(50), 
	@member_name nvarchar(50), 
	@IsUpdate bit
	)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    IF @IsUpdate=0
    begin
	INSERT INTO dbo.tblEmployeeFamilyDetails Values(@emp_id,@relationship,@member_name)
	end
	IF @IsUpdate=1
	BEGIN
	UPDATE dbo.tblEmployeeFamilyDetails set  relationship = @relationship , member_name = @member_name
	WHERE id=@emp_id
	END
END
