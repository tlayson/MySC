SELECT TOP 1000 [UserId]
      ,[DisplayName]
      ,[Description]
      ,[BuildinIgnores]
      ,[BuildinContacts]
      ,[ServerProperties]
      ,[PublicProperties]
      ,[PrivateProperties]
  FROM [MyUSC].[dbo].[CuteChat4_User]

INSERT INTO [CuteChat4_User]([UserId], [DisplayName], [Description], [BuildinIgnores], [BuildinContacts], [ServerProperties], [PublicProperties], [PrivateProperties])
SELECT [UserId], [DisplayName], [Description], [BuildinIgnores], [BuildinContacts], [ServerProperties],[PublicProperties],[PrivateProperties]
FROM [CuteChat4_UserOLD]

CREATE TABLE dbo.[CuteChat4_User]
(
	[CCUserId] int NOT NULL IDENTITY,
	[UserId] nvarchar(50) NOT NULL,
	[DisplayName] nvarchar(100),
	[Description] nvarchar(300),
	[BuildinIgnores] nvarchar(max),
	[BuildinContacts] nvarchar(max),
	[ServerProperties] nvarchar(max),
	[PublicProperties] nvarchar(max),
	[PrivateProperties] nvarchar(max),
	CONSTRAINT [PK_CC_USER] PRIMARY KEY CLUSTERED 
(
      [CCUserId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_SupportSession]
(
	[SessionId] int NOT NULL IDENTITY,
	[BeginTime] datetime NOT NULL,
	[DepartmentId] int NOT NULL,
	[AgentUserId] nvarchar(100) NOT NULL,
	[CustomerId] nvarchar(100) NOT NULL,
	[DisplayName] nvarchar(100) NOT NULL,
	[ActiveTime] datetime NOT NULL,
	[Email] nvarchar(50),
	[IPAddress] nvarchar(50) NOT NULL,
	[Culture] nvarchar(50) NOT NULL,
	[Platform] nvarchar(50) NOT NULL,
	[Browser] nvarchar(50) NOT NULL,
	[AgentRating] int NOT NULL,
	[SessionData] nvarchar(max),
	CONSTRAINT [PK_CC_SESSION] PRIMARY KEY CLUSTERED 
(
      [SessionId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_SupportMessage]
(
	[MessageId] int NOT NULL IDENTITY,
	[MsgTime] datetime NOT NULL,
	[SessionId] int NOT NULL,
	[MsgType] nvarchar(50) NOT NULL,
	[Sender] nvarchar(100),
	[SenderId] nvarchar(100),
	[Target] nvarchar(100),
	[TargetId] nvarchar(100),
	[Text] nvarchar(max),
	[Html] nvarchar(max),
	CONSTRAINT [PK_CC_MSG] PRIMARY KEY CLUSTERED 
(
      [MessageId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_SupportFeedback]
(
	[FeedbackId] int NOT NULL IDENTITY,
	[FbTime] datetime NOT NULL,
	[CustomerId] nvarchar(100),
	[DisplayName] nvarchar(100),
	[Name] nvarchar(100) NOT NULL,
	[Email] nvarchar(100) NOT NULL,
	[Title] nvarchar(200) NOT NULL,
	[Content] nvarchar(max) NOT NULL,
	[Comment] nvarchar(max),
	[CommentBy] nvarchar(100),
	CONSTRAINT [PK_CC_FEEDBACK] PRIMARY KEY CLUSTERED 
(
      [FeedbackId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_SupportDepartment]
(
	[DepartmentId] int NOT NULL IDENTITY,
	[DepartmentName] nvarchar(50) NOT NULL,
	CONSTRAINT [PK_CC_SPTDEPT] PRIMARY KEY CLUSTERED 
(
      [DepartmentId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_SupportCustomer]
(
	[SptCustId] int NOT NULL IDENTITY,
	[CustomerId] nvarchar(50) NOT NULL,
	[CustomerData] nvarchar(max) NOT NULL,
	CONSTRAINT [PK_CC_SPTCUST] PRIMARY KEY CLUSTERED 
(
      [SptCustId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_SupportAgent]
(
	[DepartmentId] int NOT NULL IDENTITY,
	[AgentUserId] nvarchar(50) NOT NULL,
	CONSTRAINT [PK_CC_SPTAGENT] PRIMARY KEY CLUSTERED 
(
      [DepartmentId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_Settings]
(
	[SettingId] int NOT NULL IDENTITY,
	[SettingName] nvarchar(50) NOT NULL,
	[SettingData] nvarchar(max) NOT NULL,
	CONSTRAINT [PK_CC_SETTING] PRIMARY KEY CLUSTERED 
(
      [SettingId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_Rule]
(
	[RuleId] int NOT NULL IDENTITY,
	[Category] nvarchar(50) NOT NULL,
	[SortIndex] int NOT NULL,
	[RuleMode] nvarchar(50) NOT NULL,
	[Expression] nvarchar(50) NOT NULL,
	[Disabled] bit NOT NULL,
	CONSTRAINT [PK_CC_RULE] PRIMARY KEY CLUSTERED 
(
      [RuleId] ASC
)
);


CREATE TABLE dbo.[CuteChat4_Portal]
(
	[PortalId] int NOT NULL IDENTITY,
	[PortalName] nvarchar(50) NOT NULL,
	[Properties] nvarchar(max),
	CONSTRAINT [PK_CC_PORTAL] PRIMARY KEY CLUSTERED 
(
      [PortalId] ASC
)
);

CREATE TABLE dbo.[CuteChat4_LogMessage]
(
	[MessageId] int NOT NULL IDENTITY,
	[MsgTime] datetime NOT NULL,
	[Location] nvarchar(50) NOT NULL,
	[Place] nvarchar(50) NOT NULL,
	[Sender] nvarchar(100),
	[SenderId] nvarchar(100),
	[Target] nvarchar(100),
	[TargetId] nvarchar(100),
	[Whisper] int,
	[IPAddress] nvarchar(25) NOT NULL,
	[Text] nvarchar(max),
	[Html] nvarchar(max),
	CONSTRAINT [PK_CC_LOGMSG] PRIMARY KEY CLUSTERED 
(
      [MessageId] ASC
)
);

CREATE TABLE dbo.CuteChat4_InstantMessage
(
	MessageId int NOT NULL IDENTITY,
	MsgTime datetime NOT NULL,
	Sender nvarchar(100) NOT NULL,
	[SenderId] nvarchar(100) NOT NULL,
	[Target] nvarchar(100) NOT NULL,
	TargetId nvarchar(100) NOT NULL,
	[Offline] int,
	[DeletedBySender] int,
	[DeletedByTarget] int,
	[IPAddress] nvarchar(125) NOT NULL,
	[Text] nvarchar(max),
	[Html] nvarchar(max),
	CONSTRAINT [PK_CC_IMID] PRIMARY KEY CLUSTERED 
(
      [MessageId] ASC
)
);

INSERT INTO Comments(AccountID, CommentType, Heading, CommentText, CreationUser, CreationDate, LastUpdateUserTime)
SELECT AccountID, CommentType, Heading, CommentText, 'tlayson',GETDATE(),''
FROM CommentsOld

