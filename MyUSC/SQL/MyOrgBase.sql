insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'OrgBaseFolder','OrgFolders','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'OrgLogoFolder','Logo','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'OrgMediaFolder','Media','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'InfoEmail','mysportsconnectnet@live.com','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'InfoPswd','USCAdmin2013','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'SupportEmail','myscsupport@live.com','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'SupportPswd','MSCSprt2013','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'SMTPAddress','smtp.live.com','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'EmailPortNumber','80','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(1,'EnableSSL','0','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([SiteAdminID],[Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(44,1,'SiteColor','16737792','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([SiteAdminID],[Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(45,1,'HomeAnnouncement','','tlayson',GETDATE(),'');
insert into [SiteAdministration]
([SiteAdminID],[Language],[KeyName],[Value], [CreationUser], [CreationDate], [LastUpdateUserTime])
values(46,1,'HomeNewsURL','http://sports.espn.go.com/espn/rss/news','tlayson',GETDATE(),'');

CREATE TABLE dbo.MyOrg
(
	OrgID bigint NOT NULL IDENTITY,
	OrgName nvarchar(250) NOT NULL,
	OrgDescription nvarchar(500) NOT NULL,
	OrgType int NOT NULL,
	OwnerID bigint NOT NULL,
	[Language] int NOT NULL DEFAULT(1),
	Address1 nvarchar(50),
	Address2 nvarchar(50),
	City nvarchar(50),
	[State] nvarchar(50),
	PostalCode  nvarchar(50),
	Country  nvarchar(50),
	EmailAddress  nvarchar(50),
	Phone  nvarchar(50),
	Cell nvarchar(50),
	Fax nvarchar(50),
	URL nvarchar(250),
	LogoURL nvarchar(250),
	ShowContact bit NOT NULL,
	AllowMemberRequests bit NOT NULL,
	AllowFollowerRequests bit NOT NULL,
	AllowGuestViews bit NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_ORGID] PRIMARY KEY CLUSTERED 
(
      [OrgID] ASC
)

);

CREATE TABLE dbo.MyOrgPageOptions
(
	PageOptionIdx bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	PageID int NOT NULL,
	Visible bit NOT NULL,
	AdminLevel int NOT NULL,
	EditLevel int NOT NULL,
	AccessLevel int NOT NULL,
	ViewLevel int NOT NULL,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_PAGEOPTIONIDX] PRIMARY KEY CLUSTERED 
(
      [PageOptionIdx] ASC
)
);

CREATE TABLE dbo.MyOrgAffiliates
(
	AffiliateIdx bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	AffiliateID bigint NOT NULL,
	AffiliateType int NOT NULL,
	ParentID bigint NOT NULL,
	Note nvarchar(200),
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_AFFILIATEIDX] PRIMARY KEY CLUSTERED 
(
      [AffiliateIdx] ASC
)
);

CREATE TABLE dbo.MyOrgOfficials
(
	OfficialsIdx bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	MemberID bigint NOT NULL,
	Title nvarchar(50),
	ShowInfo bit NOT NULL,
	ShowExtInfo bit NOT NULL,
	ExtEmail nvarchar(50),
	ExtPhone nvarchar(50),
	ExtAddress nvarchar(200),
	Note nvarchar(200),
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_OFFICIALSIDX] PRIMARY KEY CLUSTERED 
(
      [OfficialsIdx] ASC
)
);

CREATE TABLE dbo.MyOrgMembers
(
	MemberIdx bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	UserID bigint NOT NULL,
	MemberType int NOT NULL,
	Number nvarchar(10),
	Positions nvarchar(150),
	Note nvarchar(200),
	Deleted bit NOT NULL DEFAULT(0),
	AcceptedInvite bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_MEMBERIDX] PRIMARY KEY CLUSTERED 
(
      [MemberIdx] ASC
)
);

CREATE TABLE dbo.TransactionLog
(
	[Creator] nvarchar(50) NOT NULL,
	[CreationDate] datetime2(7) DEFAULT(getdate()),
	[Title] nvarchar(100) NOT NULL,
	[Detail] nvarchar(2000) NOT NULL,
	[Source] nvarchar(50) NOT NULL,
	[TID] int NOT NULL,
	[Level] int NOT NULL,
	[Type] int NOT NULL,
	[Keywords] nvarchar(50) NOT NULL,
	[Category] nvarchar(50) NOT NULL
);

CREATE TABLE dbo.MyOrgVenueList
(
	VenueID bigint NOT NULL,
	OrgID bigint NOT NULL,
	HideVenue bit NOT NULL,  -- Allows removal of owned public venues from orgs list
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
);

CREATE TABLE dbo.Venue
(
	VenueID bigint NOT NULL IDENTITY,
	VenueName nvarchar(250) NOT NULL,
	OwnerID bigint NOT NULL,
	OrgID bigint NOT NULL,
	VenueType  nvarchar(25),
	DisplayLocation nvarchar(50),
	Address1 nvarchar(50),
	Address2 nvarchar(50),
	City nvarchar(50),
	[State] nvarchar(50),
	PostalCode  nvarchar(50),
	Country  nvarchar(50),
	Phone  nvarchar(50),
	Website nvarchar(250),
	MapURL nvarchar(250),
	ImageURL nvarchar(250),
	Note nvarchar(1000),
	MakePublic bit NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_VENUEID] PRIMARY KEY CLUSTERED 
(
      [VenueID] ASC
)
);

CREATE TABLE dbo.MyOrgEvent
(
	EventID bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	VenueID bigint NOT NULL,
	SeasonID bigint NOT NULL,
	EventType int NOT NULL,
	EventName nvarchar(250) NOT NULL,
	AltLocation nvarchar(50) NOT NULL,
	EventDate datetime2(7) NOT NULL,
	OpponentID bigint NOT NULL,
	Opponent nvarchar(50) NOT NULL,
	HomeAway nvarchar(10) NOT NULL,
	Uniform nvarchar(20) NOT NULL,
	EventResult nvarchar(50) NOT NULL,
	Comments nvarchar(max),
	URL nvarchar(250),
	RequestResponse bit NOT NULL,
	ResponseLevel int NOT NULL,
	SendReminders bit NOT NULL,
	ReminderLevel int NOT NULL,
	ReminderDays int NOT NULL,
	EditLevel int NOT NULL,
	ViewLevel int NOT NULL,
	ReservedInt int NOT NULL,
	ReservedLong bigint NOT NULL,
	ReservedString nvarchar(50) NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_EVENTID] PRIMARY KEY CLUSTERED 
(
      [EventID] ASC
)
);

CREATE TABLE dbo.MyOrgEventResponse
(
	OrgID bigint NOT NULL,
	EventID bigint NOT NULL,
	MemberID bigint NOT NULL,
	Response int NOT NULL,
	Notes nvarchar(250) NOT NULL,
	ResponseDate datetime2(7) DEFAULT(getdate())
);

CREATE TABLE dbo.MyOrgSeason
(
	SeasonID bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	SeasonName nvarchar(250) NOT NULL,
	SeasonStart datetime2(7),
	Comments nvarchar(200),
	IsDefault bit NOT NULL,
	Share bit NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_SEASONID] PRIMARY KEY CLUSTERED 
(
      [SeasonID] ASC
)
);
