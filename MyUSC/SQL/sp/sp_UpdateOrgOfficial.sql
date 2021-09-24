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
CREATE PROCEDURE sp_UpdateOrgOfficial
	@OrgID bigint, 
	@MemberID bigint, 
	@Title nvarchar(50),
	@ShowInfo bit,
	@ShowExtInfo bit,
	@ExtEmail nvarchar(50),
	@ExtPhone nvarchar(50),
	@ExtAddress nvarchar(200),
	@Note nvarchar(200),
	@Deleted bit,
	@Update nvarchar(max),
	@Key bigint
AS
BEGIN
UPDATE [dbo].[MyOrgOfficials]
   SET [OrgID] = @OrgID
      ,[MemberID] = @MemberID
      ,[Title] = @Title
      ,[ShowInfo] = @ShowInfo
      ,[ShowExtInfo] = @ShowExtInfo
	  ,[ExtEmail] = @ExtEmail
	  ,[ExtPhone] = @ExtPhone
	  ,[ExtAddress] = @ExtAddress
      ,[Note] = @Note
	  ,[Deleted] = @Deleted
      ,[LastUpdate] = @Update
 WHERE [OfficialsIdx]=@Key
END
GO
