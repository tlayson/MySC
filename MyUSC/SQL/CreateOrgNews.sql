CREATE TABLE dbo.MyOrgInfo
(
	InfoIdx bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	News nvarchar(max),
	String2 nvarchar(max),
	String3 nvarchar(max),
	Long1 bigint NOT NULL,
	Long2 bigint NOT NULL,
	Long3 bigint NOT NULL,
	LastUpdate nvarchar(max),
	CONSTRAINT [PK_INFOIDX] PRIMARY KEY CLUSTERED 
(
      [InfoIdx] ASC
)
);



