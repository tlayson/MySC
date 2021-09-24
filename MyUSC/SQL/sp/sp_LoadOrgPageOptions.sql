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
CREATE PROCEDURE sp_LoadOrgPageOptions
	@OrgID bigint
AS
BEGIN
SELECT * from [dbo].[MyOrgPageOptions]
 WHERE [OrgID]=@OrgID
END
GO
