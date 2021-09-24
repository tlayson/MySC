SELECT TOP 1000 [RSSFeeds_PK]
      ,[Name]
      ,[URL]
      ,[Description]
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdateUserDate]
  FROM [MyUSC].[dbo].[RSSFeeds]

CREATE TABLE dbo.[RSSFeeds]
(
	RSSID bigint NOT NULL IDENTITY,
	[Name] nvarchar(50),
	[URL] nvarchar(200),
	[Description] nvarchar(50),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_RSSFEED] PRIMARY KEY CLUSTERED 
(
      [RSSID] ASC
)
);

INSERT INTO [RSSFeeds]([Name], [URL], [Description],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT [Name], [URL], [Description],
		'tlayson',GETDATE(),''
FROM [RSSFeedsOLD]

