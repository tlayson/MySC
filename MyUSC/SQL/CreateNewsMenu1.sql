CREATE TABLE dbo.[NewsMenu]
(
	[NewsMenuKeyID] bigint NOT NULL IDENTITY,
	[ParentID] bigint,
	[Name] nvarchar(50),
	[Description] nvarchar(50),
	[RSSID] bigint,
	[Website] nvarchar(500),
	[Notes] nvarchar(200),
	[LogoURL] nvarchar(100),
	[Sequence] float,
	[Language] int,
	[Active] bit,
	[MenuDepth] int,
	[Target] nvarchar(20),
	[CreationUser] nvarchar(50),
	[CreationDate] datetime2(7) DEFAULT(getdate()),
	[LastUpdate] nvarchar(max),
	[OldID] bigint,
	CONSTRAINT [PK_NEWSMENU] PRIMARY KEY CLUSTERED 
(
      [NewsMenuKeyID] ASC
)
);

/* Level 1 - Was SportName */
INSERT INTO [NewsMenu]([ParentID],[Name],[Description],[RSSID],[Website],[Notes],[LogoURL],[Sequence],[Language],[Active],[MenuDepth],[Target],
					[CreationUser], [CreationDate], [LastUpdate],[OldID])
SELECT 0,[Name],[Description],[RSSID],'','',[LogoURL],[Sequence],[Language],[Active],1,'',
		'tlayson',GETDATE(),'',[SportNameID]
FROM [SportName]

/* Level 2 - Was SportType */
INSERT INTO [NewsMenu]([ParentID],[Name],[Description],[RSSID],[Website],[Notes],[LogoURL],[Sequence],[Language],[Active],[MenuDepth],[Target],
					[CreationUser], [CreationDate], [LastUpdate],[OldID])
SELECT nm.[NewsMenuKeyID],st.[Name],st.[Description],st.[RSSID],st.[Website],st.[RSSNotes],st.[LogoURL],0,st.[Language],1,2,'',
		'tlayson',GETDATE(),'',[SportTypeID]
FROM [SportType] st, NewsMenu nm
where st.SportNameID=nm.OldID AND nm.MenuDepth=1;

/* Level 3 - Was SportDivision */
INSERT INTO [NewsMenu]([ParentID],[Name],[Description],[RSSID],[Website],[Notes],[LogoURL],[Sequence],[Language],[Active],[MenuDepth],[Target],
					[CreationUser], [CreationDate], [LastUpdate],[OldID])
SELECT nm.[NewsMenuKeyID],sd.[Name],sd.[Description],sd.[RSSID],sd.[Website],sd.[RSSNotes],sd.[LogoURL],0,sd.[Language],1,3,'',
		'tlayson',GETDATE(),'',[DivisionID]
FROM [SportDivision] sd, NewsMenu nm
where sd.SportTypeID=nm.OldID AND nm.MenuDepth=2;

/* Level 4 - Was SportTeam */
INSERT INTO [NewsMenu]([ParentID],[Name],[Description],[RSSID],[Website],[Notes],[LogoURL],[Sequence],[Language],[Active],[MenuDepth],[Target],
					[CreationUser], [CreationDate], [LastUpdate],[OldID])
SELECT nm.[NewsMenuKeyID],team.[Name],team.[Description],team.[RSSID],team.[Website],team.[RSSNotes],team.[LogoURL],0,team.[Language],1,4,'',
		'tlayson',GETDATE(),'',team.[SportTeamID]
FROM [SportTeam] team, NewsMenu nm
where team.SportDivisionID=nm.OldID AND nm.MenuDepth=3;

ALTER TABLE dbo.RSSFeeds ADD [UseWebsite] bit DEFAULT(0);

UPDATE RSSFeeds SET [UseWebsite]=0;

