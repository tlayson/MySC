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
CREATE PROCEDURE sp_UpdateUserPrefs 
	-- Add the parameters for the stored procedure here
	@OffersFromUs bit, 
	@OffersFromPartners bit, 
	@DeleteFriendsWarning bit, 
	@DeleteMessageWarning bit,
	@NewsSubjects nvarchar(200), 
	@Interests nvarchar(500), 
	@Archive bit, 
	@PublicSportsInterest bit, 
	@CommentsEmails bit, 
	@KeepLoggedIn bit,
	@LastUpdate nvarchar(max),
	@ShowNickname bit,
	@ProfileUpdated bit,
	@ProvideSecurityQuestion bit,
	@AcctID bigint

AS
BEGIN
UPDATE [dbo].[Preferences]
   SET [OffersFromUs] = @OffersFromUs
      ,[OffersFromPartners] = @OffersFromPartners
      ,[DeleteFriendsWarning] = @DeleteFriendsWarning
      ,[DeleteMessageWarning] = @DeleteMessageWarning
      ,[NewsSubjects] = @NewsSubjects
      ,[Interests] = @Interests
      ,[Archive] = @Archive
      ,[PublicSportsInterest] = @PublicSportsInterest
      ,[CommentsEmails] = @CommentsEmails
      ,[KeepLoggedIn] = @KeepLoggedIn
      ,[LastUpdateUserTime] = @LastUpdate
      ,[ShowNickname] = @ShowNickname
      ,[ProfileUpdated] = @ProfileUpdated
	  ,[ProvideSecurityQuestion] = @ProvideSecurityQuestion
 WHERE [AccountID]=@AcctID
END
GO
