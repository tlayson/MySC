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
CREATE PROCEDURE sp_UpdateOrg
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
	@ShowContact bigint, 
	@AllowMemberRequests bit, 
	@AllowFollowerRequests bit, 
	@AllowGuestViews bit, 
	@Deleted bit,
	@Update nvarchar(max),
	@Key bigint
AS
BEGIN
UPDATE [dbo].[MyOrg]
   SET [OrgName] = @OrgName
      ,[OrgDescription] = @OrgDescription
      ,[OrgType] = @OrgType
      ,[OwnerID] = @OwnerID
      ,[Language] = @Language
      ,[Address1] = @Address1
      ,[Address2] = @Address2
      ,[City] = @City
      ,[State] = @State
      ,[PostalCode] = @PostalCode
      ,[Country] = @Country
      ,[EmailAddress] = @EmailAddress
      ,[Phone] = @Phone
      ,[Cell] = @Cell
      ,[Fax] = @Fax
      ,[URL] = @URL
      ,[LogoURL] = @LogoURL
      ,[ShowContact] = @ShowContact
      ,[AllowMemberRequests] = @AllowMemberRequests
      ,[AllowFollowerRequests] = @AllowFollowerRequests
      ,[AllowGuestViews] = @AllowGuestViews
	  ,[Deleted] = @Deleted
      ,[LastUpdate] = @Update
 WHERE [OrgID]=@Key
END
GO
