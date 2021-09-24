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
	--PageOptionIdx bigint NOT NULL IDENTITY,
	--OrgID bigint NOT NULL,
	--PageID int NOT NULL,
	--Visible bit NOT NULL,
	--AdminLevel int NOT NULL,
	--EditLevel int NOT NULL,
	--AccessLevel int NOT NULL,
	--ViewLevel int NOT NULL,
	--CreationUser nvarchar(50),
	--CreationDate datetime2(7) DEFAULT(getdate()),
	--LastUpdate nvarchar(max),
-- =============================================
CREATE PROCEDURE sp_AddOrgPageOption
	@OrgID bigint, 
	@PageID int, 
	@Visible bit,
	@AdminLevel int, 
	@EditLevel int, 
	@AccessLevel int, 
	@ViewLevel int, 
	@Creator nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[MyOrgPageOptions]
           ([OrgID]
           ,[PageID]
		   ,[Visible]
           ,[AdminLevel]
		   ,[EditLevel]
           ,[AccessLevel]
		   ,[ViewLevel]
           ,[CreationUser]
           ,[CreationDate])
     VALUES
           (@OrgID
           ,@PageID
		   ,@Visible
           ,@AdminLevel
		   ,@EditLevel
           ,@AccessLevel
		   ,@ViewLevel
           ,@Creator
           ,GETDATE())
END
GO
