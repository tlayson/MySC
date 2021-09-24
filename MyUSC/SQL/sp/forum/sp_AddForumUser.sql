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
CREATE PROCEDURE sp_AddForumUser 
    @BoardID			int,
    @UserName			nvarchar(255),
    @DisplayName		nvarchar(255),
    @Email				nvarchar(255),
    @UTCTIMESTAMP datetime
AS
BEGIN
        insert into [dbo].[yaf_User](
		BoardID,
		RankID,
		[Name],
		DisplayName,
		[Password],
		Email,
		Joined,
		LastVisit,
		NumPosts,
		TimeZone,
		Flags,
		PMNotification,
		AutoWatchTopics,
		NotificationType,
		ProviderUserKey) 
        values(
			@BoardID,
			3,
			@UserName,
			@DisplayName,
			'-',
			@Email,
			@UTCTIMESTAMP ,
			@UTCTIMESTAMP ,
			0,
			-480, 
			98,
			1,
			0,
			30,
			null)		
END
GO
