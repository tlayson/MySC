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
CREATE PROCEDURE sp_UpdateUserAccount
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(50), 
	@UserType int,
	@Pswd nvarchar(50),
	@Language int,
	@IsActive bit,
	@AcceptedTOU bit,
	@Title nvarchar(50),
	@First nvarchar(50),
	@MI nvarchar(1),
	@Last nvarchar(50),
	@Nickname nvarchar(50),
	@Suffix nvarchar(50),
	@BirthDate nvarchar(50),
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@Zip nvarchar(50),
	@Country nvarchar(50),
	@Email nvarchar(50),
	@EmailVerified bit,
	@DefaultPage nvarchar(50),
	@PhotoFile nvarchar(50),
	@SecurityQuestion nvarchar(100),
	@SecurityAnswer nvarchar(50),
	@LoginAttempts int,
	@LastLogin datetime2(7),
	@CreatorID bigint,
	@LastUpdate nvarchar(max),
	@AcctID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[Accounts]
   SET [UserName] = @UserName
      ,[UserType] = @UserType
      ,[Password] = @Pswd
      ,[Language] = @Language
      ,[IsActive] = @IsActive
      ,[AcceptedTOU] = @AcceptedTOU
      ,[Title] = @Title
      ,[FirstName] = @First
      ,[MI] = @MI
      ,[LastName] = @Last
      ,[Nickname] = @Nickname
      ,[Suffix] = @Suffix
      ,[BirthDate] = @BirthDate
      ,[Address1] = @Address1
      ,[Address2] = @Address2
      ,[City] = @City
      ,[State] = @State
      ,[Zipcode] = @Zip
      ,[Country] = @Country
      ,[EmailAddress] = @Email
      ,[EmailVerified] = @EmailVerified
      ,[DefaultPage] = @DefaultPage
      ,[PhotoFile] = @PhotoFile
      ,[SecurityQuestion] = @SecurityQuestion
      ,[SecurityAnswer] = @SecurityAnswer
      ,[LoginAttempts] = @LoginAttempts
      ,[LastLogin] = @LastLogin
      ,[CreatorID] = @CreatorID
      ,[LastUpdateUserTime] = @LastUpdate
 WHERE AccountID=@AcctID
END
GO
