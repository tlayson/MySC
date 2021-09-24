CREATE TABLE dbo.[MyRoster]
(
	RosterID bigint NOT NULL IDENTITY,
	MyTeamsID bigint,
	[PlayerFirstName] nvarchar(50),
	[PlayerLastName] nvarchar(50),
	[PlayerPosition] nvarchar(50),
	[Info] nvarchar(50),
	[Stat1Type] nvarchar(50),
	[Stat1] nvarchar(50),
	[Stat2Type] nvarchar(50),
	[Stat2] nvarchar(50),
	[Stat3Type] nvarchar(50),
	[Stat3] nvarchar(50),
	[Stat4Type] nvarchar(50),
	[Stat4] nvarchar(50),
	[ContactFirstName] nvarchar(50),
	[ContactLastName] nvarchar(50),
	[ContactPhoneNumber1] nvarchar(50),
	[ContactPhoneNumber2] nvarchar(50),
	[ContactEmail1] nvarchar(150),
	[ContactEmail2] nvarchar(150),
	[ContactAddress] nvarchar(50),
	[ContactCity] nvarchar(50),
	[ContactState] nvarchar(50),
	[ContactZipcode] nvarchar(50),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_MYROSTER] PRIMARY KEY CLUSTERED 
(
      [RosterID] ASC
)
);

CREATE TABLE dbo.[MySchedule]
(
	SchedID bigint NOT NULL IDENTITY,
	MyTeamsID bigint NOT NULL,
	[GameType] nvarchar(50),
	[Date] datetime2(7) DEFAULT(getdate()),
	[Time] nvarchar(50),
	[FieldName] nvarchar(50),
	[Location] nvarchar(50),
	[Address] nvarchar(50),
	[Venue] nvarchar(50),
	[City] nvarchar(50),
	[State] nvarchar(50),
	[Opponent] nvarchar(50),
	[FinalScoreMyTeam] nvarchar(50),
	[FinalScoreOpposingTeam] nvarchar(50),
	[CancelReason] nvarchar(50),
	[MessageSent] bit,
	[txtDirections] nvarchar(250),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_SCHEDULE] PRIMARY KEY CLUSTERED 
(
      [SchedID] ASC
)
);

CREATE TABLE dbo.[MyTeams]
(
	MyTeamsID bigint NOT NULL IDENTITY,
	AccountID bigint,
	[TeamName] nvarchar(50),
	[SportType] nvarchar(50),
	[LeagueName] nvarchar(50),
	[DivisionName] nvarchar(50),
	[AgeGroup] nvarchar(50),
	[Notes] nvarchar(50),
	[City] nvarchar(50),
	[State] nvarchar(50),
	[FirstName] nvarchar(50),
	[LastName] nvarchar(50),
	[WorkPhone] nvarchar(50),
	[HomePhone] nvarchar(50),
	[CellPhone] nvarchar(50),
	[Email] nvarchar(150),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_MYTEAMS] PRIMARY KEY CLUSTERED 
(
      [MyTeamsID] ASC
)
);

