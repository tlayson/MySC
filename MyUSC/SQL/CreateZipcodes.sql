SELECT TOP 1000 [Zipcode_PK]
      ,[Zipcode]
      ,[City]
      ,[State]
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdateUserDate]
  FROM [MyUSC].[dbo].[Zipcode]


CREATE TABLE dbo.[Zipcode]
(
	ZipID bigint NOT NULL IDENTITY,
	[Zipcode] nvarchar(14),
	[City] nvarchar(50),
	[State] nvarchar(50),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max),
	CONSTRAINT [PK_ZIPCODE] PRIMARY KEY CLUSTERED 
(
      [ZipID] ASC
)
);

INSERT INTO [Zipcode]([Zipcode],[City],[State],
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT [Zipcode],[City],[State],
		'tlayson',GETDATE(),''
FROM [ZipcodeOLD]

