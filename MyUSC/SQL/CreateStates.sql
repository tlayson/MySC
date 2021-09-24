CREATE TABLE dbo.[States]
(
	StateID bigint NOT NULL IDENTITY,
	CountryID bigint NOT NULL,
	Name nvarchar(50),
	Abbreviation nvarchar(5),
	FlagURL nvarchar(200),
	CONSTRAINT [PK_STATE] PRIMARY KEY CLUSTERED 
(
      [StateID] ASC
)
);

INSERT INTO [States](Name,CountryID,Abbreviation,FlagURL) VALUES (1,'','','');


