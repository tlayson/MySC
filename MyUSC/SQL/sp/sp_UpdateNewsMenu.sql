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
CREATE PROCEDURE sp_UpdateNewsMenu
	@ParentID bigint, 
	@Name nvarchar(50),
	@Description nvarchar(50),
	@RSSID bigint,
	@Website nvarchar(500),
	@Notes nvarchar(200),
	@LogoURL nvarchar(100),
	@Sequence float,
	@Language int,
	@Active bit,
	@MenuDepth int,
	@Target nvarchar(20),
	@Update nvarchar(max),
	@Key bigint
AS
BEGIN
UPDATE [dbo].[NewsMenu]
   SET [ParentID] = @ParentID
      ,[Name] = @Name
      ,[Description] = @Description
      ,[RSSID] = @RSSID
      ,[Website] = @Website
      ,[Notes] = @Notes
      ,[LogoURL] = @LogoURL
      ,[Sequence] = @Sequence
      ,[Language] = @Language
      ,[Active] = @Active
      ,[MenuDepth] = @MenuDepth
	  ,[Target] = @Target
      ,[LastUpdate] = @Update
 WHERE [NewsMenuKeyID]=@Key
END
GO
