CREATE TABLE dbo.Advertise
(
	AdID bigint NOT NULL IDENTITY,
	CompanyName nvarchar(50),
	Address nvarchar(50),
	City nvarchar(50),
	State nvarchar(50),
	Zipcode nvarchar(25),
	ContactFirstName nvarchar(max),
	ContactLastName nvarchar(50),
	WorkPhone nvarchar(50),
	CellPhone nvarchar(50),
	Email nvarchar(50),
	CompanyWebsite nvarchar(50),
	LocalAdvertising bit,
	NationalAdvertising bit,
	Comments nvarchar(max),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_ADID] PRIMARY KEY CLUSTERED 
(
      [AdID] ASC
)
);

INSERT INTO AdRotator(AltText, ImageURL, NavigateURL, RotatorName, Language,
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT AlternateText, ImageURL, NavigateURL, RotatorName, Language,
		'tlayson',GETDATE(),''
FROM AdRotatorOLD

Select * from AdvertiseOLD