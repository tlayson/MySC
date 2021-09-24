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
-- Description:	Get all pairings for a single org
--	VenueID bigint NOT NULL,
--	OrgID bigint NOT NULL,
--	HideVenue bit NOT NULL,  -- Allows removal of owned public venues from orgs list
--	CreationUser nvarchar(50),
--	CreationDate datetime2(7) DEFAULT(getdate()),
--	LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_GetOrgVenueList
	@OrgID bigint
AS
BEGIN
SELECT [VenueID]
      ,[OrgID]
      ,[HideVenue]
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdate]
FROM [dbo].[MyOrgVenueList]
WHERE [OrgID] = @OrgID
END
GO
