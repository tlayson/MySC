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
	--OrgID bigint NOT NULL IDENTITY,
	--OrgName nvarchar(250) NOT NULL,
	--OrgDescription nvarchar(500) NOT NULL,
	--OrgType int NOT NULL,
	---OwnerID bigint NOT NULL,
	--[Language] int NOT NULL DEFAULT(1),
	--Address1 nvarchar(50),
	--Address2 nvarchar(50),
	--City nvarchar(50),
	--[State] nvarchar(50),
	--PostalCode  nvarchar(50),
	--Country  nvarchar(50),
	--EmailAddress  nvarchar(50),
	--Phone  nvarchar(50),
	--Cell nvarchar(50),
	--Fax nvarchar(50),
	--URL nvarchar(250),
	--LogoURL nvarchar(250),
	--ShowContact bit NOT NULL,
	--AllowMemberRequests bit NOT NULL,
	--AllowFollowerRequests bit NOT NULL,
	--AllowGuestViews bit NOT NULL,
	--Deleted bit NOT NULL DEFAULT(0),
	--CreationUser nvarchar(50),
	--CreationDate datetime2(7) DEFAULT(getdate()),
	--LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_AddOrg
	@OrgName nvarchar(250),
	@OrgDescription nvarchar(500),
	@OrgType int, 
	@OwnerID bigint, 
	@Language int, 
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@PostalCode nvarchar(50),
	@Country nvarchar(50),
	@EmailAddress nvarchar(50),
	@Phone nvarchar(50),
	@Cell nvarchar(50),
	@Fax nvarchar(50),
	@URL nvarchar(250),
	@LogoURL nvarchar(250),
	@ShowContact bit, 
	@AllowMemberRequests bit, 
	@AllowFollowerRequests bit, 
	@AllowGuestViews bit, 
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[MyOrg]
           ([OrgName]
           ,[OrgDescription]
           ,[OrgType]
		   ,[OwnerID]
           ,[Language]
           ,[Address1]
           ,[Address2]
           ,[City]
           ,[State]
           ,[Country]
           ,[PostalCode]
           ,[EmailAddress]
           ,[Phone]
           ,[Cell]
           ,[Fax]
           ,[URL]
           ,[LogoURL]
           ,[ShowContact]
           ,[AllowMemberRequests]
           ,[AllowFollowerRequests]
           ,[AllowGuestViews]
		   ,[Deleted]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@OrgName
           ,@OrgDescription
           ,@OrgType
		   ,@OwnerID
           ,@Language
           ,@Address1
           ,@Address2
           ,@City
           ,@State
           ,@PostalCode
           ,@Country
           ,@EmailAddress
           ,@Phone
           ,@Cell
           ,@Fax
           ,@URL
		   ,@LogoURL
           ,@ShowContact
           ,@AllowMemberRequests
           ,@AllowFollowerRequests
           ,@AllowGuestViews
		   ,0
           ,@Creator
           ,GETDATE())
END
GO
