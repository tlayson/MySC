CREATE TABLE dbo.Comments
(
	CommentID bigint NOT NULL IDENTITY,
	AccountID bigint NOT NULL,
	CommentType bigint NOT NULL,
	Heading nvarchar(50),
	CommentText nvarchar(2000),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_COMMENT] PRIMARY KEY CLUSTERED 
(
      [CommentID] ASC
)
);

CREATE TABLE dbo.CommentType
(
	CommentTypeID bigint NOT NULL IDENTITY,
	[Name] nvarchar(50),
	Sequence float,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_COMMENTTYPE] PRIMARY KEY CLUSTERED 
(
      [CommentTypeID] ASC
)
);

INSERT INTO Comments(AccountID, CommentType, Heading, CommentText, CreationUser, CreationDate, LastUpdateUserTime)
SELECT AccountID, CommentType, Heading, CommentText, 'tlayson',GETDATE(),''
FROM CommentsOld

INSERT INTO CommentType(Name, Sequence, CreationUser, CreationDate, LastUpdateUserTime)
SELECT Name, Sequence, 'tlayson',GETDATE(),''
FROM CommentTypeOLD


select * from CommentType
order by AccountID

