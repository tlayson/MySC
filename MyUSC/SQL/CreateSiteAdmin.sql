SELECT TOP 1000 [SiteAdministration_PK]
      ,[Language]
      ,[KeyName]
      ,[Value]
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdateUserDate]
  FROM [MyUSC].[dbo].[SiteAdministration]


CREATE TABLE dbo.[SiteAdministration]
(
	SiteAdminID bigint NOT NULL IDENTITY,
	[Language] int,
	[KeyName] nvarchar(50),
	[Value] nvarchar(max),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_SITEADMIN] PRIMARY KEY CLUSTERED 
(
      [SiteAdminID] ASC
)
);

INSERT INTO [SiteAdministration]([Language], [KeyName], [Value],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT [Language], [KeyName], [Value],
		'tlayson',GETDATE(),''
FROM [SiteAdministrationOLD]

