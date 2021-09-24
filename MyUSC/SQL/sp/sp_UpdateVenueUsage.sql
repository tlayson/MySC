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
-- Author:		tlayson
-- Create date: 10/5/2015
-- Description:	Updates a pairing for a single venue
--				Currently only hide/shows pairing in the orgs list
--	VenueID bigint NOT NULL,
--	OrgID bigint NOT NULL,
--	HideVenue bit NOT NULL,
--	CreationUser nvarchar(50),
--	CreationDate datetime2(7) DEFAULT(getdate()),
--	LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_UpdateVenueUsage
	@HideVenue bit,
	@LastUpdate nvarchar(max),
	@VenueID bigint,
	@OrgID bigint
	
AS
BEGIN
UPDATE [dbo].[MyOrgVenueList]
SET [HideVenue] = @HideVenue
   ,[LastUpdate] = @LastUpdate
WHERE [VenueID] = @VenueID AND [OrgID] = @OrgID
END
GO

