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
	--RSVPID bigint NOT NULL IDENTITY,
	--OrgID bigint NOT NULL,
	--EventID bigint NOT NULL,
	--MemberID bigint NOT NULL,
	--Response int NOT NULL,
	--Notes nvarchar(250) NOT NULL,
	--CreationUser nvarchar(50),
	--CreationDate datetime2(7) DEFAULT(getdate()),
	--LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_AddOrgEventResponse
	@OrgID bigint, 
	@EventID bigint, 
	@MemberID bigint, 
	@Response int,
	@Notes nvarchar(250)
AS
BEGIN
INSERT INTO [dbo].[MyOrgEventResponse]
           ([OrgID]
		   ,[EventID]
           ,[MemberID]
		   ,[Response]
		   ,[Notes]
           ,[ResponseDate])
     VALUES
           (@OrgID
		   ,@EventID
           ,@MemberID
           ,@Response
		   ,@Notes
           ,GETDATE())
END
GO
