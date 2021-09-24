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
CREATE PROCEDURE sp_UpdateRSSFeed 
	@Name nvarchar(50),
	@URL nvarchar(500),
	@Description nvarchar(50),
	@Update nvarchar(max),
	@Notes nvarchar(200),
	@Website nvarchar(500),
	@UseWebsite bit,
	@Key bigint
AS
BEGIN
UPDATE [dbo].[RSSFeeds]
   SET [Name] = @Name
      ,[URL] = @URL
      ,[Description] = @Description
      ,[LastUpdateUserTime] = @Update
      ,[Notes] = @Notes
      ,[AltWebsite] = @Website
      ,[UseWebsite] = @UseWebsite
 WHERE RSSID = @Key
END
GO
