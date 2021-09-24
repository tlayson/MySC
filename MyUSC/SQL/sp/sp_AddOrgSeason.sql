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
	--SeasonID bigint NOT NULL IDENTITY,
	--OrgID bigint NOT NULL,
	--SeasonName nvarchar(250) NOT NULL,
	--SeasonStart datetime2(7),
	--Comments nvarchar(200),
	--Deleted bit NOT NULL DEFAULT(0),
	--CreationUser nvarchar(50),
	--CreationDate datetime2(7) DEFAULT(getdate()),
	--LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_AddOrgSeason
	@OrgID bigint, 
	@SeasonName nvarchar(250),
	@SeasonStart datetime2(7),
	@Comments nvarchar(200),
	@IsDefault bit,
	@Share bit,
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[MyOrgSeason]
           ([OrgID]
           ,[SeasonName]
		   ,[SeasonStart]
           ,[Comments]
		   ,[IsDefault]
		   ,[Share]
		   ,[Deleted]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@OrgID
           ,@SeasonName
           ,@SeasonStart
           ,@Comments
		   ,@IsDefault
		   ,@Share
		   ,0
           ,@Creator
           ,GETDATE())
END
GO
