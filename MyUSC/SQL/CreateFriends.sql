SELECT TOP 1000 [FriendsFilter_PK]
      ,[Name]
      ,[Sequence]
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdateUserDate]
  FROM [MyUSC].[dbo].[FriendsFilter]

CREATE TABLE dbo.[FriendsFilter]
(
	[FriendsFltrID] bigint NOT NULL IDENTITY,
	[Name] nvarchar(15),
	[Sequence] float,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_FRIENDFLTR] PRIMARY KEY CLUSTERED 
(
      [FriendsFltrID] ASC
)
);

INSERT INTO [FriendsFilter]([Name], [Sequence],  CreationUser, CreationDate, LastUpdateUserTime)
SELECT [Name], [Sequence],'tlayson',GETDATE(),''
FROM [FriendsFilterOLD]

CREATE TABLE dbo.[FriendsCategories]
(
	[FriendsCatID] bigint NOT NULL IDENTITY,
	[Category] nvarchar(50),
	[PhotoURL] nvarchar(150),
	[Sequence] float,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_FRIENDCAT] PRIMARY KEY CLUSTERED 
(
      [FriendsCatID] ASC
)
);

INSERT INTO [FriendsCategories]([Category], [PhotoURL], [Sequence],  CreationUser, CreationDate, LastUpdateUserTime)
SELECT [Category], [PhotoURL], [Sequence],'tlayson',GETDATE(),''
FROM [FriendsCategoriesOLD]


CREATE TABLE dbo.Friends
(
	FriendKeyID bigint NOT NULL IDENTITY,
	AccountID bigint NOT NULL,
	FriendID bigint NOT NULL,
	IsActive bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_FRIENDS] PRIMARY KEY CLUSTERED 
(
      [FriendKeyID] ASC
)
);

CREATE TABLE dbo.FriendsRequest
(
	FriendReqID bigint NOT NULL IDENTITY,
	AccountID bigint NOT NULL,
	FriendID bigint NOT NULL,
	RequestDate datetime2(7) DEFAULT(getdate()),
	Comments nvarchar(200),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_FREINDREQ] PRIMARY KEY CLUSTERED 
(
      [FriendReqID] ASC
)
);

CREATE TABLE dbo.FriendsBlocked
(
	FriendBlkID bigint NOT NULL IDENTITY,
	AccountID bigint NOT NULL,
	FriendID bigint NOT NULL,
	RequestDate datetime2(7) DEFAULT(getdate()),
	BlockedDate datetime2(7) DEFAULT(getdate()),
	Blocked bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_FRIENDBLK] PRIMARY KEY CLUSTERED 
(
      [FriendBlkID] ASC
)
);

INSERT INTO Friends(AccountID, FriendID, IsActive, CreationUser, CreationDate, LastUpdateUserTime)
SELECT AccountID, FriendID, IsActive, 'tlayson',GETDATE(),''
FROM FriendsOLD

INSERT INTO FriendsRequest( AccountID, FriendID, RequestDate, Comments, CreationUser, CreationDate, LastUpdateUserTime )
SELECT AccountID, FriendID, RequestDate, Comments, 'tlayson', GETDATE(),''
FROM FriendsRequestOLD


INSERT INTO FriendsBlocked(AccountID, FriendID, RequestDate, BlockedDate, Blocked, CreationUser, CreationDate, LastUpdateUserTime)
SELECT AccountID, FriendID, RequestDate, BlockedDate, Blocked,'tlayson',GETDATE(),''
FROM FriendsBlockedOLD

select * from FriendsRequest
order by AccountID

