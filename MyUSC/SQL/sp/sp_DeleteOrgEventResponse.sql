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
	--RSVPID bigint NOT NULL IDENTITY,
	--OrgID bigint NOT NULL,
	--EventID bigint NOT NULL,
	--MemberID bigint NOT NULL,
	--Response int NOT NULL,
	--Notes nvarchar(250) NOT NULL,
	--CreationUser nvarchar(50),
	--CreationDate datetime2(7) DEFAULT(getdate()),
	--LastUpdate nvarchar(max),
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_DeleteOrgEventResponse
	@OrgID bigint,
	@EventID bigint,
	@MemberID bigint
AS
BEGIN
DELETE FROM [dbo].[MyOrgEventResponse]
WHERE [OrgID]=@OrgID AND [EventID]=@EventID AND [MemberID]=@MemberID
END
GO
