USE [MyUSCLocal]
GO

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
 WHERE <Search Conditions,,>
GO


