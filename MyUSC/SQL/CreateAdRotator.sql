CREATE TABLE dbo.AdRotator
(
	AdID bigint NOT NULL IDENTITY,
	AltText nvarchar(100),
	ImageURL nvarchar(500),
	NavigateURL nvarchar(500),
	RotatorName nvarchar(50),
	Language int,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_ADROT] PRIMARY KEY CLUSTERED 
(
      [AdID] ASC
)
);

INSERT INTO AdRotator(AltText, ImageURL, NavigateURL, RotatorName, Language,
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT AlternateText, ImageURL, NavigateURL, RotatorName, Language,
		'tlayson',GETDATE(),''
FROM AdRotatorOLD

Select * from AdRotator