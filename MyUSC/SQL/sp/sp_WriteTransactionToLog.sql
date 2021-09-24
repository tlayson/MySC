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
-- Author:		tlayson
-- Create date: 10/5/2015
-- Description:	Write an entry in the transaction log
--	[Creator] nvarchar(50) NOT NULL,
--	[CreationDate] datetime2(7) DEFAULT(getdate()),
--	[Title] nvarchar(100) NOT NULL,
--	[Detail] nvarchar(2000) NOT NULL,
--	[Source] nvarchar(50) NOT NULL,
--	[TID] int NOT NULL,
--	[Level] int NOT NULL,
--	[Type] int NOT NULL,
--	[Keywords] nvarchar(50) NOT NULL,
--	[Category] nvarchar(50) NOT NULL
-- =============================================
CREATE PROCEDURE sp_WriteTransactionToLog
	@Creator nvarchar(50),
	@Title nvarchar(100),
	@Detail nvarchar(2000),
	@Source nvarchar(50),
	@TID int,
	@Level int,
	@Type int,
	@Keywords nvarchar(50),
	@Category nvarchar(50)
AS
BEGIN
INSERT INTO [dbo].[TransactionLog]
           ([Creator]
           ,[CreationDate]
           ,[Title]
           ,[Detail]
           ,[Source]
           ,[TID]
           ,[Level]
           ,[Type]
           ,[Keywords]
           ,[Category])
     VALUES
           (@Creator
           ,GETDATE()
           ,@Title
           ,@Detail
           ,@Source
           ,@TID
           ,@Level
           ,@Type
           ,@Keywords
           ,@Category)
END
GO
