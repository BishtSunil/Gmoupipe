USE [GMOUMIS]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAdminLogin]    Script Date: 05/11/2016 22:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetAdminLogin] --'admin','1234'
	(
	@username nvarchar(50),
	@password nvarchar(50)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  username,password FROM dbo.tblAdminRegistration WHERE USERNAME =@username and  password=@password
END
