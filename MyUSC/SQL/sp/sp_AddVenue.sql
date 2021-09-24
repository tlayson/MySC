-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--	VenueID bigint NOT NULL IDENTITY,
--	VenueName nvarchar(250) NOT NULL,
--	OwnerID bigint NOT NULL,
--	OrgID bigint NOT NULL,
--	DisplayLocation nvarchar(50),
--	Address1 nvarchar(50),
--	Address2 nvarchar(50),
--	City nvarchar(50),
--	[State] nvarchar(50),
--	PostalCode  nvarchar(50),
--	Country  nvarchar(50),
--	Phone  nvarchar(50),
--	Website nvarchar(250),
--	MapURL nvarchar(250),
--	ImageURL nvarchar(250),
--	Note nvarchar(1000),
--	MakePublic bit NOT NULL,
--	Deleted bit NOT NULL DEFAULT(0),
--	CreationUser nvarchar(50),
--	CreationDate datetime2(7) DEFAULT(getdate()),
--	LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_AddVenue
	@VenueName nvarchar(250),
	@OwnerID bigint, 
	@OrgID bigint, 
	@VenueType nvarchar(25),
	@DisplayLocation nvarchar(50),
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@PostalCode nvarchar(50),
	@Country nvarchar(50),
	@Phone nvarchar(50),
	@Website nvarchar(250),
	@MapURL nvarchar(250),
	@ImageURL nvarchar(250),
	@Note nvarchar(1000),
	@MakePublic bit,
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[Venue]
           ([VenueName]
           ,[OwnerID]
		   ,[OrgID]
		   ,[VenueType]
		   ,[DisplayLocation]
		   ,[Address1]
		   ,[Address2]
		   ,[City]
		   ,[State]
		   ,[PostalCode]
		   ,[Country]
		   ,[Phone]
		   ,[Website]
		   ,[MapURL]
		   ,[ImageURL]
           ,[Note]
		   ,[MakePublic]
		   ,[Deleted]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@VenueName
           ,@OwnerID
		   ,@OrgID
		   ,@VenueType
		   ,@DisplayLocation
           ,@Address1
           ,@Address2
           ,@City
           ,@State
           ,@PostalCode
           ,@Country
           ,@Phone
		   ,@Website
           ,@MapURL
           ,@ImageURL
           ,@Note
		   ,@MakePublic
		   ,0
           ,@Creator
           ,GETDATE())
END
GO
