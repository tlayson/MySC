USE [MyUSC]
GO

UPDATE [dbo].[Accounts]
   SET [UserName] = <UserName, nvarchar(50),>
      ,[UserType] = <UserType, int,>
      ,[Password] = <Password, nvarchar(50),>
      ,[Language] = <Language, int,>
      ,[IsActive] = <IsActive, bit,>
      ,[AcceptedTOU] = <AcceptedTOU, bit,>
      ,[Title] = <Title, nvarchar(50),>
      ,[FirstName] = <FirstName, nvarchar(50),>
      ,[MI] = <MI, nvarchar(1),>
      ,[LastName] = <LastName, nvarchar(50),>
      ,[Nickname] = <Nickname, nvarchar(50),>
      ,[Suffix] = <Suffix, nvarchar(50),>
      ,[BirthDate] = <BirthDate, nvarchar(50),>
      ,[Address1] = <Address1, nvarchar(50),>
      ,[Address2] = <Address2, nvarchar(50),>
      ,[City] = <City, nvarchar(50),>
      ,[State] = <State, nvarchar(50),>
      ,[Zipcode] = <Zipcode, nvarchar(50),>
      ,[Country] = <Country, nvarchar(50),>
      ,[EmailAddress] = <EmailAddress, nvarchar(50),>
      ,[EmailVerified] = <EmailVerified, bit,>
      ,[DefaultPage] = <DefaultPage, nvarchar(50),>
      ,[PhotoFile] = <PhotoFile, nvarchar(50),>
      ,[SecurityQuestion] = <SecurityQuestion, nvarchar(100),>
      ,[SecurityAnswer] = <SecurityAnswer, nvarchar(50),>
      ,[LoginAttempts] = <LoginAttempts, int,>
      ,[LastLogin] = <LastLogin, datetime2(7),>
      ,[CreatorID] = <CreatorID, bigint,>
      ,[CreationUser] = <CreationUser, nvarchar(50),>
      ,[CreationDate] = <CreationDate, datetime2(7),>
      ,[LastUpdateUserTime] = <LastUpdateUserTime, nvarchar(max),>
 WHERE <Search Conditions,,>
GO


