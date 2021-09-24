CREATE TABLE dbo.Blogs
(
	BlogID bigint NOT NULL IDENTITY,
	SportName bigint,
	SportType bigint,
	SportDivision bigint,
	SportTeam bigint,
	Title nvarchar(200),
	RSSURL nvarchar(500),
	Content nvarchar(max),
	BlogDate datetime2(7) DEFAULT(getdate()),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_BLOGS] PRIMARY KEY CLUSTERED 
(
      [BlogID] ASC
)

);

CREATE TABLE dbo.BlogChildren
(
	BlogChildID bigint NOT NULL IDENTITY,
	AccountID bigint NOT NULL,
	BlogID bigint NOT NULL,
	Comments nvarchar(1500),
	CommentDate datetime2(7) DEFAULT(getdate()),
	LikeBlog bit,
	DislikeBlog bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_BLOGCHILD] PRIMARY KEY CLUSTERED 
(
      [BlogChildID] ASC
)

);

INSERT INTO Blogs(SportName, SportType, SportDivision, SportTeam, Title, RSSURL, Content, BlogDate, 
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT SportName_FK, SportType_FK, SportDivision_FK, SportTeam_FK, Name, RSSURL, Text, DateOfBlog,
		'tlayson',GETDATE(),''
FROM BlogParent

INSERT INTO BlogChildren(AccountID, BlogID, Comments, CommentDate, LikeBlog, DislikeBlog,
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT MyAccount_FK, BlogParent_FK, Comments, DateOfComment, LikeBlog, DislikeBlog,
		'tlayson',GETDATE(),''
FROM BlogChild

