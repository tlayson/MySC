SELECT TOP 1000 [SportType_PK]
      ,[SportName_FK]
      ,[RSSFeed_FK]
      ,[Name]
      ,[Description]
      ,[LogoURL]
      ,[Language]
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdateUser]
      ,[LastUpdateDate]
  FROM [MyUSC].[dbo].[SportType]

CREATE TABLE dbo.[SportType]
(
	TypeKeyID bigint NOT NULL IDENTITY,
	[SportTypeID] bigint,
	[SportNameID] bigint,
	[RSSID] bigint,
	[Name] nvarchar(50),
	[Description] nvarchar(50),
	[LogoURL] nvarchar(500),
	[Language] int,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_SPORTTYPE] PRIMARY KEY CLUSTERED 
(
      [TypeKeyID] ASC
)
);

INSERT INTO [SportType]([SportTypeID],[SportNameID],[RSSID],[Name],[Description],[LogoURL],[Language],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT SportType_PK,SportName_FK,RSSFeed_FK,[Name],[Description],[LogoURL],[Language],
		'tlayson',GETDATE(),''
FROM [SportTypeOLD]


CREATE TABLE dbo.[SportTeam]
(
	TeamKeyID bigint NOT NULL IDENTITY,
	[SportTeamID] bigint,
	[SportNameID] bigint,
	[SportTypeID] bigint,
	[SportDivisionID] bigint,
	[RSSID] bigint,
	[Description] nvarchar(50),
	[Name] nvarchar(50),
	[SortName] nvarchar(50),
	[LogoURL] nvarchar(500),
	[Language] int,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_SPORTTEAM] PRIMARY KEY CLUSTERED 
(
      [TeamKeyID] ASC
)
);

INSERT INTO [SportTeam]([SportTeamID],[SportNameID],[SportTypeID],[SportDivisionID],[RSSID],[Description],[Name],[SortName],[LogoURL],[Language],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT SportTeam_PK,SportName_FK,SportType_FK,SportDivision_FK,RSSFeed_FK,[Description],[Name],[SortName],[LogoURL],[Language],
		'tlayson',GETDATE(),''
FROM [SportTeamOLD]

CREATE TABLE dbo.[SportName]
(
	NameKeyID bigint NOT NULL IDENTITY,
	[SportNameID] bigint,
	[RSSID] bigint,
	[Name] nvarchar(50),
	[Description] nvarchar(50),
	[LogoURL] nvarchar(500),
	[Sequence] float,
	[SeasonStarts] datetime2(7),
	[SeasonEnds] datetime2(7),
	[Language] int,
	[Active] bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_SPORTNAME] PRIMARY KEY CLUSTERED 
(
      [NameKeyID] ASC
)
);

INSERT INTO [SportName]([SportNameID],[RSSID],[Name],[Description],[LogoURL],[Sequence],[SeasonStarts],[SeasonEnds],[Language],[Active],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT SportName_PK,RSSFeed_FK,[Name],[Description],[LogoURL],[Sequence],[SeasonStarts],[SeasonEnds],[Language],[Active],
		'tlayson',GETDATE(),''
FROM [SportNameOLD]


CREATE TABLE dbo.[SportDivision]
(
	DivKeyID bigint NOT NULL IDENTITY,
	[DivisionID] bigint,
	[SportTypeID] bigint,
	[SportNameID] bigint,
	[RSSID] bigint,
	[Name] nvarchar(50),
	[Description] nvarchar(50),
	[LogoURL] nvarchar(500),
	[Language] int,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_DIVISION] PRIMARY KEY CLUSTERED 
(
      [DivKeyID] ASC
)
);

INSERT INTO [SportDivision]([DivisionID], [SportTypeID], [SportNameID],[RSSID],[Name],[Description],[LogoURL],[Language],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT SportDivision_PK,SportType_FK,SportName_FK,RSSFeed_FK,[Name],[Description],[LogoURL],[Language],
		'tlayson',GETDATE(),''
FROM [SportDivisionOLD]

