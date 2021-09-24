CREATE TABLE dbo.Accounts
(
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
	[CreatorID] [bigint] NOT NULL DEFAULT ((0)),
	[CreationUser] [nvarchar](50) NULL,
	[CreationDate] [datetime2](7) NULL DEFAULT (getdate()),
	[LastUpdateUserTime] [nvarchar](max) NULL,
	CONSTRAINT [PK_ACCOUNTID] PRIMARY KEY CLUSTERED 
(
      [AccountID] ASC
)

);

CREATE TABLE dbo.Preferences
(
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
)
);

INSERT INTO Accounts(UserName, UserType,[Password], [Language], IsActive,AcceptedTOU,Title, 
					FirstName, MI, LastName,Suffix,BirthDate,Address1,Address2,City,[State],
					Zipcode, Country, EmailAddress, EmailVerified, DefaultPage, PhotoFile,
					SecurityQuestion, SecurityAnswer, LoginAttempts, CreationUser, CreationDate, LastUpdateUserTime)
SELECT UserName, UserType, [Password], [Language], 1, 1, Title, 
		FirstName, MI, LastName, Suffix,Birthdate,Address1,Address2,City,[State],
		Zipcode, Country, EmailAddress, EmailVerified, DefaultPage, PhotoURL,
		SecurityQuestion, SecurityAnswer, LoginAttempts, 'tlayson',GETDATE(),''
FROM AccountsOLD

UPDATE Accounts 
SET UserType=1
WHERE AccountID = 91

INSERT INTO Preferences(AccountID,OffersFromUs,OffersFromPartners,DeleteFriendsWarning,DeleteMessageWarning,NewsSubjects,Interests,Archive,PublicSportsInterest,CommentsEmails,KeepLoggedIn,CreationUser,CreationDate,LastUpdateUserTime)
SELECT AccountID, 1, 0, 0, 0, '', '', 0, 1, 1, 1, 'tlayson',GETDATE(), ''
FROM PreferencesOLD

