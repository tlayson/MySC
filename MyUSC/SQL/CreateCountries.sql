CREATE TABLE dbo.[Country]
(
	CountryID bigint NOT NULL IDENTITY,
	Name nvarchar(50),
	Abbreviation nvarchar(5),
	FlagURL nvarchar(200),
	CONSTRAINT [PK_COUNTRY] PRIMARY KEY CLUSTERED 
(
      [CountryID] ASC
)
);

INSERT INTO [Country](Name,Abbreviation,FlagURL) VALUES ('','','');


