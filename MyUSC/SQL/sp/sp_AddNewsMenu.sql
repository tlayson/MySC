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
CREATE PROCEDURE sp_AddNewsMenu
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
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[NewsMenu]
           ([ParentID]
           ,[Name]
           ,[Description]
           ,[RSSID]
           ,[Website]
           ,[Notes]
           ,[LogoURL]
           ,[Sequence]
           ,[Language]
           ,[Active]
           ,[MenuDepth]
		   ,[Target]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@ParentID
           ,@Name
           ,@Description
           ,@RSSID
           ,@Website
           ,@Notes
           ,@LogoURL
           ,@Sequence
           ,@Language
           ,@Active
           ,@MenuDepth
		   ,@Target
           ,@Creator
           ,GETDATE())
END
GO
