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
CREATE PROCEDURE sp_AddUserAccount 
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
	@CreatorID bigint,
    @CreationUser nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[Accounts]
           ([UserName]
           ,[UserType]
           ,[Password]
           ,[Language]
           ,[IsActive]
           ,[AcceptedTOU]
           ,[Title]
           ,[FirstName]
           ,[MI]
           ,[LastName]
           ,[Nickname]
           ,[Suffix]
           ,[BirthDate]
           ,[Address1]
           ,[Address2]
           ,[City]
           ,[State]
           ,[Zipcode]
           ,[Country]
           ,[EmailAddress]
           ,[EmailVerified]
           ,[DefaultPage]
           ,[PhotoFile]
           ,[SecurityQuestion]
           ,[SecurityAnswer]
           ,[LoginAttempts]
           ,[LastLogin]
           ,[CreatorID]
           ,[CreationUser]
           ,[CreationDate]
           ,[LastUpdateUserTime])
     VALUES
           (@UserName
           ,@UserType
           ,@Pswd
           ,@Language
           ,@IsActive
           ,@AcceptedTOU
           ,@Title
           ,@First
           ,@MI
           ,@Last
		   ,@Nickname
           ,@Suffix
           ,@BirthDate
           ,@Address1
           ,@Address2
           ,@City
           ,@State
           ,@Zip
           ,@Country
           ,@Email
           ,@EmailVerified
           ,@DefaultPage
           ,@PhotoFile
           ,@SecurityQuestion
           ,@SecurityAnswer
           ,0
           ,GETDATE()
		   ,@CreatorID
           ,@CreationUser
           ,GETDATE()
		   ,''
           )
END
GO
