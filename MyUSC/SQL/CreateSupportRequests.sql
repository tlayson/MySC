CREATE TABLE dbo.SupportRequests
(
	SupportRequestID bigint NOT NULL IDENTITY,
	userID bigint,
	EmailSent bit,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Email nvarchar(50),
	Phone nvarchar(50) DEFAULT(' '),
	Browser nvarchar(50) DEFAULT(' '),
	[Description] nvarchar(50),
	Details nvarchar(max),
	intReserved1 int DEFAULT(0),
	intReserved2 int DEFAULT(0),
	intReserved3 int DEFAULT(0),
	bitReserved1 bit DEFAULT(0),
	bitReserved2 bit DEFAULT(0),
	bitReserved3 bit DEFAULT(0),
	strReserved1 nvarchar(50) DEFAULT(' '),
	strReserved2 nvarchar(50) DEFAULT(' '),
	strReserved3 nvarchar(50) DEFAULT(' '),
	CreationUser nvarchar(50) DEFAULT(' '),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdateUserTime nvarchar(max) DEFAULT(' '),
	CONSTRAINT [PK_SUPTREQ] PRIMARY KEY CLUSTERED 
(
      [SupportRequestID] ASC
)

);

