USE [MyUSC]
GO

/****** Object:  Table [dbo].[SiteAdministration]    Script Date: 9/2/2014 11:02:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SiteAdministration](
	[SiteAdminID] [bigint] IDENTITY(1,1) NOT NULL,
	[Language] [int] NULL,
	[KeyName] [nvarchar](50) NULL,
	[Value] [nvarchar](max) NULL,
	[CreationUser] [nvarchar](50) NULL,
	[CreationDate] [datetime2](7) NULL DEFAULT (getdate()),
	[LastUpdateUserTime] [nvarchar](max) NULL,
 CONSTRAINT [PK_SITEADMIN] PRIMARY KEY CLUSTERED 
(
	[SiteAdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

INSERT INTO [SiteAdministration]([Language], [KeyName], [Value],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT [Language], [KeyName], [Value],
		'tlayson',GETDATE(),''
FROM [SiteAdministrationOLD]


