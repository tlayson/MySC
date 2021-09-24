SELECT TOP 1000 [Exceptions_PK]
      ,[Message]
      ,[DateOfException]
  FROM [MyUSC].[dbo].[ExceptionMessages]

CREATE TABLE dbo.[ExceptionMessages]
(
	ExceptionID bigint NOT NULL IDENTITY,
	[Message] nvarchar(1000),
	[DateOfException] datetime2(7) DEFAULT(getdate()),
	CONSTRAINT [PK_EXCEPT] PRIMARY KEY CLUSTERED 
(
      [ExceptionID] ASC
)
);

INSERT INTO AdRotator(AltText, ImageURL, NavigateURL, RotatorName, Language,
					CreationUser, CreationDate, LastUpdateUserTime)
SELECT AlternateText, ImageURL, NavigateURL, RotatorName, Language,
		'tlayson',GETDATE(),''
FROM AdRotatorOLD

Select * from AdvertiseOLD