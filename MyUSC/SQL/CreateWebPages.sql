SELECT TOP 1000 [WebPages_PK]
      ,[Name]
      ,[PageName]
      ,[DisplayName]
      ,[Sequence]
      ,[Language]
      ,[DisplayToUser]
      ,[AdministrativePage]
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdateUserDate]
  FROM [MyUSC].[dbo].[WebPages]

CREATE TABLE dbo.[WebPages]
(
	WebPageID bigint NOT NULL IDENTITY,
	[Name] nvarchar(50),
	[PageName] nvarchar(50),
	[DisplayName] nvarchar(50),
	[Sequence] float,
	[Language] int,
	[DisplayToUser] bit,
	[AdministrativePage] bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_WEBPAGE] PRIMARY KEY CLUSTERED 
(
      [WebPageID] ASC
)
);

INSERT INTO [WebPages]([Name],[PageName],[DisplayName],[Sequence],[Language],[DisplayToUser],[AdministrativePage],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT [Name],[PageName],[DisplayName],[Sequence],[Language],[DisplayToUser],[AdministrativePage],
		'tlayson',GETDATE(),''
FROM [WebPagesOLD]

