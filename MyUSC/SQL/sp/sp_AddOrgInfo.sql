USE [MyUSC]
GO
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
-- =============================================
CREATE PROCEDURE sp_AddOrgInfo
	@OrgID bigint, 
	@News nvarchar(1024),
	@String2 nvarchar(1024),
	@String3 nvarchar(1024),
	@Long1 bigint,
	@Long2 bigint,
	@Long3 bigint,
	@LastUpdate nvarchar(1024)
AS
BEGIN
INSERT INTO [dbo].[MyOrgInfo]
           ([OrgID]
           ,[News]
           ,[String2]
           ,[String3]
           ,[Long1]
           ,[Long2]
           ,[Long3]
           ,[LastUpdate]
		   )
     VALUES
           (@OrgID
           ,@News
           ,@String2
           ,@String3
           ,@Long1
           ,@Long2
           ,@Long3
           ,@LastUpdate
		   )
END
GO
