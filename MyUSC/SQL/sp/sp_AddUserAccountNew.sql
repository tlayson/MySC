USE [MyUSCLocal]
GO

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
           (<UserName, nvarchar(50),>
           ,<UserType, int,>
           ,<Password, nvarchar(50),>
           ,<Language, int,>
           ,<IsActive, bit,>
           ,<AcceptedTOU, bit,>
           ,<Title, nvarchar(50),>
           ,<FirstName, nvarchar(50),>
           ,<MI, nvarchar(1),>
           ,<LastName, nvarchar(50),>
           ,<Nickname, nvarchar(50),>
           ,<Suffix, nvarchar(50),>
           ,<BirthDate, nvarchar(50),>
           ,<Address1, nvarchar(50),>
           ,<Address2, nvarchar(50),>
           ,<City, nvarchar(50),>
           ,<State, nvarchar(50),>
           ,<Zipcode, nvarchar(50),>
           ,<Country, nvarchar(50),>
           ,<EmailAddress, nvarchar(50),>
           ,<EmailVerified, bit,>
           ,<DefaultPage, nvarchar(50),>
           ,<PhotoFile, nvarchar(50),>
           ,<SecurityQuestion, nvarchar(100),>
           ,<SecurityAnswer, nvarchar(50),>
           ,<LoginAttempts, int,>
           ,<LastLogin, datetime2(7),>
           ,<CreatorID, bigint,>
           ,<CreationUser, nvarchar(50),>
           ,<CreationDate, datetime2(7),>
           ,<LastUpdateUserTime, nvarchar(max),>)
GO


