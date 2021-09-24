USE [MyUSC]
GO
select * from [dbo].[Accounts]
select * from [dbo].[Preferences]


select * from [dbo].[AccountsOLD]

select * into [dbo].[AccountsOLD] from [dbo].[Accounts]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 9/5/2014 3:36:19 PM ******/
ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [PK_ACCOUNTID]
DROP TABLE [dbo].[Accounts]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 9/5/2014 3:32:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Accounts](
	[AccountID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserType] [int] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Language] [int] NOT NULL DEFAULT ((1)),
	[IsActive] [bit] NULL,
	[AcceptedTOU] [bit] NULL,
	[Title] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[MI] [nvarchar](1) NULL,
	[LastName] [nvarchar](50) NULL,
	[Nickname] [nvarchar](50) NULL DEFAULT (''),
	[Suffix] [nvarchar](50) NULL,
	[BirthDate] [nvarchar](50) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Zipcode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[EmailVerified] [bit] NULL,
	[DefaultPage] [nvarchar](50) NULL,
	[PhotoFile] [nvarchar](50) NULL,
	[SecurityQuestion] [nvarchar](100) NULL,
	[SecurityAnswer] [nvarchar](50) NULL,
	[LoginAttempts] [int] NULL,
	[LastLogin] [datetime2](7) NULL DEFAULT (getdate()),
	[CreatorID] [bigint] NOT NULL DEFAULT(0),
	[CreationUser] [nvarchar](50) NULL,
	[CreationDate] [datetime2](7) NULL DEFAULT (getdate()),
	[LastUpdateUserTime] [nvarchar](max) NULL,
 CONSTRAINT [PK_ACCOUNTID] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

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
           ,[LastUpdateUserTime]
           )
SELECT [UserName]
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
      ,[PhotoURL]
      ,[SecurityQuestion]
      ,[SecurityAnswer]
      ,[LoginAttempts]
      ,[LastLogin]
      ,0
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdateUserTime]
FROM [dbo].[AccountsOLD]
ORDER BY AccountID ASC


USE [MyUSCLocal]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 9/5/2014 3:36:19 PM ******/
ALTER TABLE [dbo].[Preferences] DROP CONSTRAINT [PK_PREFID]
GO

/****** Object:  Table [dbo].[Preferences]    Script Date: 11/3/2014 9:32:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

select * into [dbo].[PrefsOLD] from [dbo].[Preferences]
GO

DROP TABLE [dbo].[Preferences]
GO

CREATE TABLE [dbo].[Preferences](
	[PrefID] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountID] [bigint] NOT NULL,
	[OffersFromUs] [bit] NULL,
	[OffersFromPartners] [bit] NULL,
	[DeleteFriendsWarning] [bit] NULL,
	[DeleteMessageWarning] [bit] NULL,
	[NewsSubjects] [nvarchar](200) NULL,
	[Interests] [nvarchar](500) NULL,
	[Archive] [bit] NULL,
	[PublicSportsInterest] [bit] NULL,
	[CommentsEmails] [bit] NULL,
	[KeepLoggedIn] [bit] NULL,
	[CreationUser] [nvarchar](50) NULL,
	[CreationDate] [datetime2](7) NULL DEFAULT (getdate()),
	[LastUpdateUserTime] [nvarchar](max) NULL,
	[ShowNickname] [bit] NULL DEFAULT ((0)),
	[ProfileUpdated] [bit] NULL DEFAULT ((0)),
	[ProvideSecurityQuestion] [bit] NULL DEFAULT ((1)),
 CONSTRAINT [PK_PREFID] PRIMARY KEY CLUSTERED 
(
	[PrefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

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
SELECT
           [AccountID]
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
           ,[ProvideSecurityQuestion]
FROM [dbo].[PrefsOLD]
ORDER BY PrefID ASC

