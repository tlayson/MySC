USE [MyUSC]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddPageView]    Script Date: 6/9/2020 6:24:18 PM ******/
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
CREATE PROCEDURE [sp_AddPageView]
	@URL nvarchar(max),
	@IP nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[Metrics]
           ([TimeStamp]
           ,[URL]
           ,[IP])
     VALUES
           (GETDATE(),
           @URL,
           @IP)
END

