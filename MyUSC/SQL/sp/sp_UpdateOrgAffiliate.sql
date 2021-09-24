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
	--AffiliateIdx bigint NOT NULL IDENTITY,
	--OrgID bigint NOT NULL,
	--AffiliateID bigint NOT NULL,
	--AffiliateType int NOT NULL,
	---ParentID bigint NOT NULL,
	--Note nvarchar(200),
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
CREATE PROCEDURE sp_UpdateOrgAffiliate
	@OrgID bigint, 
	@AffiliateID bigint,
	@AffiliateType int,
	@ParentID bigint,
	@Note nvarchar(200),
	@Deleted bit,
	@Update nvarchar(max),
	@Key bigint
AS
BEGIN
UPDATE [dbo].[MyOrgAffiliates]
   SET [OrgID] = @OrgID
      ,[AffiliateID] = @AffiliateID
      ,[AffiliateType] = @AffiliateType
      ,[ParentID] = @ParentID
      ,[Note] = @Note
	  ,[Deleted] = @Deleted
      ,[LastUpdate] = @Update
 WHERE [AffiliateIdx]=@Key
END
GO
