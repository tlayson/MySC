CREATE TABLE dbo.FavoriteTeams
(
	FavTeamID bigint NOT NULL IDENTITY,
	AccountID bigint NOT NULL,
	SportsName bigint NOT NULL,
	SportsType bigint NOT NULL,
	SportsDivision bigint NOT NULL,
	SportsTeam bigint NOT NULL,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_FAVTEAMS] PRIMARY KEY CLUSTERED 
(
      [FavTeamID] ASC
)
);

INSERT INTO FavoriteTeams(AccountID, SportsName, SportsType, SportsDivision, SportsTeam, CreationUser, CreationDate, LastUpdateUserTime)
SELECT AccountID, SportsName, SportsType, SportsDivision, SportsTeam, CreationUser,CreationDate,LastUpdateUserTime
FROM FavoriteTeamsOLD

select * from FavoriteTeams
order by AccountID

