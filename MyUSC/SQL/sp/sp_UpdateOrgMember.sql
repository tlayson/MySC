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
	--MemberIdx bigint NOT NULL IDENTITY,
	--OrgID bigint NOT NULL,
	--UserID bigint NOT NULL,
	--MemberType int NOT NULL,
	--Number nvarchar(10),
	--Positions nvarchar(150),
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
CREATE PROCEDURE sp_UpdateOrgMember
	@OrgID bigint, 
	@UserID bigint,
	@MemberType int,
	@Number nvarchar(10),
	@Positions nvarchar(150),
	@Note nvarchar(200),
	@Deleted bit,
	@Update nvarchar(max),
	@Key bigint
AS
BEGIN
UPDATE [dbo].[MyOrgMembers]
   SET [OrgID] = @OrgID
      ,[UserID] = @UserID
      ,[MemberType] = @MemberType
      ,[Number] = @Number
      ,[Positions] = @Positions
      ,[Note] = @Note
	  ,[Deleted] = @Deleted
      ,[LastUpdate] = @Update
 WHERE [MemberIdx]=@Key
END
GO
