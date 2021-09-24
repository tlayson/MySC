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
	--OfficialsIdx bigint NOT NULL IDENTITY,
	--OrgID bigint NOT NULL,
	--MemberID bigint NOT NULL,
	--Title nvarchar(50),
	--ShowInfo bit NOT NULL,
	--ShowExtInfo bit NOT NULL,
	--ExtEmail nvarchar(50),
	--ExtPhone nvarchar(50),
	--ExtAddress nvarchar(50),
	--Note nvarchar(200),
	--Deleted bit NOT NULL DEFAULT(0),
	--CreationUser nvarchar(50),
	--CreationDate datetime2(7) DEFAULT(getdate()),
	--LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_AddOrgOfficial
	@OrgID bigint, 
	@MemberID bigint, 
	@Title nvarchar(50),
	@ShowInfo bit,
	@ShowExtInfo bit,
	@ExtEmail nvarchar(50),
	@ExtPhone nvarchar(50),
	@ExtAddress nvarchar(200),
	@Note nvarchar(200),
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[MyOrgOfficials]
           ([OrgID]
           ,[MemberID]
           ,[Title]
		   ,[ShowInfo]
		   ,[ShowExtInfo]
		   ,[ExtEmail]
		   ,[ExtPhone]
		   ,[ExtAddress]
           ,[Note]
		   ,[Deleted]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@OrgID
           ,@MemberID
           ,@Title
		   ,@ShowInfo
		   ,@ShowExtInfo
		   ,@ExtEmail
		   ,@ExtPhone
		   ,@ExtAddress
           ,@Note
		   ,0
           ,@Creator
           ,GETDATE())
END
GO
