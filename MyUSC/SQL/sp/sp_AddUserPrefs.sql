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
CREATE PROCEDURE sp_AddUserPrefs 
	-- Add the parameters for the stored procedure here
	@AcctID bigint,
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
    @CreationUser nvarchar(50),
	@ShowNickname bit,
	@ProfileUpdated bit,
	@ProvideSecurityQuestion bit

AS
BEGIN
INSERT INTO [dbo].[Preferences]
           ([AccountID]
           ,[OffersFromUs]
           ,[OffersFromPartners]
           ,[DeleteFriendsWarning]
           ,[DeleteMessageWarning]
           ,[NewsSubjects]
           ,[Interests]
           ,[Archive]
           ,[PublicSportsInterest]
           ,[CommentsEmails]
           ,[KeepLoggedIn]
           ,[CreationUser]
           ,[CreationDate]
           ,[LastUpdateUserTime]
           ,[ShowNickname]
           ,[ProfileUpdated]
		   ,[ProvideSecurityQuestion])
     VALUES
           (@AcctID
           ,@OffersFromUs
           ,@OffersFromPartners
           ,@DeleteFriendsWarning
           ,@DeleteMessageWarning
           ,@NewsSubjects
           ,@Interests
           ,@Archive
           ,@PublicSportsInterest
           ,@CommentsEmails
           ,@KeepLoggedIn
           ,@CreationUser
           ,GETDATE()
           ,SMALLDATETIMEFROMPARTS ( 2010, 12, 31, 0, 0 )
           ,@ShowNickname
           ,@ProfileUpdated
		   ,@ProvideSecurityQuestion)
END
GO
