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
-- Description:	Deletes a pairing for a single venue/org.
--				Can only be used if not the owner.  Otherwise hide it
--	VenueID bigint NOT NULL,
--	OrgID bigint NOT NULL,
--	HideVenue bit NOT NULL,
--	CreationUser nvarchar(50),
--	CreationDate datetime2(7) DEFAULT(getdate()),
--	LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_DeleteVenueUsage
	@VenueID bigint,
	@OrgID bigint
	
AS
BEGIN
DELETE FROM [dbo].[MyOrgVenueList]
WHERE [VenueID] = @VenueID AND [OrgID] = @OrgID
END
GO

