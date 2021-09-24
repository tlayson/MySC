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
-- =============================================
CREATE PROCEDURE sp_AddOrgAffiliate
	@OrgID bigint, 
	@AffiliateID bigint, 
	@AffiliateType int, 
	@ParentID bigint,
	@Note nvarchar(200),
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[MyOrgAffiliates]
           ([OrgID]
           ,[AffiliateID]
           ,[AffiliateType]
		   ,[ParentID]
           ,[Note]
		   ,[Deleted]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@OrgID
           ,@AffiliateID
           ,@AffiliateType
		   ,@ParentID
           ,@Note
		   ,0
           ,@Creator
           ,GETDATE())
END
GO
