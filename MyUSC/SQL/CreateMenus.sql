CREATE TABLE dbo.[MenuStepChild]
(
	MenuStepChildID bigint NOT NULL IDENTITY,
	SportsName bigint,
	SportsType bigint,
	MenuParent bigint,
	MenuChild bigint,
	[Name] nvarchar(50),
	[Description] nvarchar(50),
	[Sequence] float,
	[Active] bit,
	[Language] int,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_MENUSTEPCHILD] PRIMARY KEY CLUSTERED 
(
      [MenuStepChildID] ASC
)
);

INSERT INTO [MenuStepChild](SportsName, SportsType, MenuParent, MenuChild, [Name],[Description],[Sequence],[Active],[Language], CreationUser, CreationDate, LastUpdateUserTime)
SELECT SportsName_FK, SportsType_FK, MenuParent_FK, MenuChild_FK, [Name],[Description],[Sequence],[Active],[Language], CreationUser,CreationDate,LastUpdateUserDate
FROM [MenuStepChildOLD]

CREATE TABLE dbo.[MenuParent]
(
	MenuParentID bigint NOT NULL IDENTITY,
	[MenuName] nvarchar(50),
	[Name] nvarchar(50),
	[Sequence] float,
	[Language] int,
	[ShowWithoutLogin] bit,
	[ShowWithGroups] smallint,
	[Active] bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_MENUPARENT] PRIMARY KEY CLUSTERED 
(
      [MenuParentID] ASC
)
);

INSERT INTO [MenuParent]([MenuName], [Name], [Sequence], [Language],[ShowWithoutLogin],[ShowWithGroups],[Active],CreationUser, CreationDate, LastUpdateUserTime)
SELECT [MenuName], [Name], [Sequence], [Language], [ShowWithoutLogin],[ShowWithGroups],[Active],CreationUser,CreationDate,LastUpdateUserDate
FROM [MenuParentOLD]

CREATE TABLE dbo.[MenuChild]
(
	MenuChildID bigint NOT NULL IDENTITY,
	SportsName bigint NOT NULL,
	MenuParent bigint NOT NULL,
	[Name] nvarchar(50),
	[Description] nvarchar(50),
	[Sequence] float,
	[Language] int,
	[Active] bit,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_MENUCHILD] PRIMARY KEY CLUSTERED 
(
      [MenuChildID] ASC
)
);

INSERT INTO [MenuChild](SportsName,MenuParent,[Name],[Description],[Sequence], [Language],[Active],CreationUser, CreationDate, LastUpdateUserTime)
SELECT SportsName_FK, MenuParent_FK, [Name], [Description], [Sequence],[Language],[Active],CreationUser,CreationDate,LastUpdateUserDate
FROM [MenuChildOLD]


