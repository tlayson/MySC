CREATE TABLE dbo.[MyMessageImages]
(
	MsgImgID bigint NOT NULL IDENTITY,
	MessageID bigint,
	ImageURL nvarchar(500),
	Sequence float,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_MSGIMG] PRIMARY KEY CLUSTERED 
(
      [MsgImgID] ASC
)
);

CREATE TABLE dbo.Messages
(
	MsgID bigint NOT NULL IDENTITY,
	AccountID bigint NOT NULL,
	FriendID bigint NOT NULL,
	ThreadID bigint NOT NULL,
	UserName nvarchar(100),
	DataText1 nvarchar(max),
	DataText2 nvarchar(50),
	DataText3 nvarchar(50),
	DataText4 nvarchar(50),
	MessageDate datetime2(7) DEFAULT(getdate()),
	DiscussionURL nvarchar(500),
	DiscussionURLText nvarchar(500),
	PhotoURL1 nvarchar(500),
	PhotoUploadURL nvarchar(500),
	PrivateMsg bit,
	ThreadParent bit,
	VideoLink nvarchar(500),
	NewMessage bit,
	LikeMessage bit,
	DislikeMessage bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_MESSAGES] PRIMARY KEY CLUSTERED 
(
      [MsgID] ASC
)
);

INSERT INTO Messages(AccountID, FriendID, ThreadID, UserName, DataText1, DataText2, DataText3, DataText4, 
					MessageDate, DiscussionURL, DiscussionURLText, PhotoURL1, PhotoUploadURL, PrivateMsg,
					ThreadParent, VideoLink, NewMessage, LikeMessage, DislikeMessage,
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT AccountID, FriendID, ThreadID, UserName, DataText1, DataText2, DataText3, DataText4,
		MessageDate, DiscussionURL, DiscussionURLText, PhotoURL1, PhotoUploadURL, PrivateMsg, 
		ThreadParent, VideoLink, NewMessage, LikeMessage, DislikeMessage,
		CreationUser, CreationDate, LastUpdateUserTime
FROM MessagesOLD


CREATE TABLE dbo.MessageArchives
(
	MsgArchID bigint NOT NULL IDENTITY,
	MsgID bigint NOT NULL,
	AccountID bigint NOT NULL,
	FriendID bigint NOT NULL,
	ThreadID bigint NOT NULL,
	UserName nvarchar(100),
	DataText1 nvarchar(max),
	DataText2 nvarchar(50),
	DataText3 nvarchar(50),
	DataText4 nvarchar(50),
	MessageDate datetime2(7) DEFAULT(getdate()),
	DiscussionURL nvarchar(500),
	DiscussionURLText nvarchar(500),
	PhotoURL1 nvarchar(500),
	PhotoUploadURL nvarchar(500),
	PrivateMsg bit,
	ThreadParent bit,
	VideoLink nvarchar(500),
	NewMessage bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_MSGARCHIVE] PRIMARY KEY CLUSTERED 
(
      [MsgArchID] ASC
)
);

INSERT INTO MessageArchives(MsgID, AccountID, FriendID, ThreadID, UserName, DataText1, DataText2, DataText3, DataText4, 
					MessageDate, DiscussionURL, DiscussionURLText, PhotoURL1, PhotoUploadURL, PrivateMsg,
					ThreadParent, VideoLink, NewMessage,
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT MsgID, AccountID, FriendID, ThreadID, UserName, DataText1, DataText2, DataText3, DataText4,
		MessageDate, DiscussionURL, DiscussionURLText, PhotoURL1, PhotoUploadURL, PrivateMsg, 
		ThreadParent, VideoLink, NewMessage,
		CreationUser, CreationDate, LastUpdateUserTime
FROM MessageArchivesOLD


