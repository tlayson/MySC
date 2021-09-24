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
-- Description:	Create  pairings for a single venue
--	VenueID bigint NOT NULL,
--	OrgID bigint NOT NULL,
--	HideVenue bit NOT NULL,
--	CreationUser nvarchar(50),
--	CreationDate datetime2(7) DEFAULT(getdate()),
--	LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_AddVenueUsage
	@VenueID bigint,
	@OrgID bigint, 
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[MyOrgVenueList]
           ([VenueID]
           ,[OrgID]
           ,[HideVenue]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@VenueID
           ,@OrgID
           ,0
           ,@Creator
           ,GETDATE())
END
GO

GO

