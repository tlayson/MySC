--USE [MyUSC]

select * into [dbo].[SiteAdministrationOLD] from [dbo].[SiteAdministration]
GO

CREATE TABLE [dbo].[SiteAdministration](
	[SiteAdminID] [bigint] NOT NULL,
	[Language] [int] NULL,
	[KeyName] [nvarchar](50) NULL,
	[Value] [nvarchar](max) NULL,
	[CreationUser] [nvarchar](50) NULL,
	[CreationDate] [datetime2](7) NULL,
	[LastUpdateUserTime] [nvarchar](max) NULL
)

GO

INSERT INTO [SiteAdministration]
			([SiteAdminID],
			[Language], 
			[KeyName], 
			[Value],
			[CreationUser], 
			[CreationDate], 
			[LastUpdateUserTime])
SELECT	[SiteAdminID],
		[Language], 
		[KeyName], 
		[Value],
		'tlayson',
		GETDATE(),
		''
FROM [SiteAdministrationOLD]
ORDER BY SiteAdminID ASC
