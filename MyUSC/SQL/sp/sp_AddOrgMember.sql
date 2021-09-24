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
-- =============================================
CREATE PROCEDURE sp_AddOrgMember
	@OrgID bigint, 
	@UserID bigint, 
	@MemberType int, 
	@Number nvarchar(10),
	@Positions nvarchar(150),
	@Note nvarchar(200),
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[MyOrgMembers]
           ([OrgID]
           ,[UserID]
           ,[MemberType]
		   ,[Number]
		   ,[Positions]
           ,[Note]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@OrgID
           ,@UserID
           ,@MemberType
		   ,@Number
		   ,@Positions
           ,@Note
           ,@Creator
           ,GETDATE())
END
GO
