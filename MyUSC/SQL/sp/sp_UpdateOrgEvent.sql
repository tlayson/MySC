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
	--EventID bigint NOT NULL IDENTITY,
	--OrgID bigint NOT NULL,
	--VenueID bigint NOT NULL,
	--SeasonID bigint NOT NULL,
	--EventType int NOT NULL,
	--EventName nvarchar(250) NOT NULL,
	--AltLocation nvarchar(50) NOT NULL,
	--EventDate datetime2(7) NOT NULL,
	--OpponentID bigint NOT NULL,
	--Opponent nvarchar(50) NOT NULL,
	--HomeAway nvarchar(10) NOT NULL,
	--Uniform nvarchar(20) NOT NULL,
	--EventResult nvarchar(50) NOT NULL,
	--Comments nvarchar(max),
	--URL nvarchar(250),
	--RequestResponse bit NOT NULL,
	--ResponseLevel int NOT NULL,
	--SendReminders bit NOT NULL,
	--ReminderLevel int NOT NULL,
	--ReminderDays int NOT NULL,
	--EditLevel int NOT NULL,
	--ViewLevel int NOT NULL,
	--ReservedInt int NOT NULL,
	--ReservedLong bigint NOT NULL,
	--ReservedString nvarchar(50) NOT NULL,
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
CREATE PROCEDURE sp_UpdateOrgEvent
	@OrgID bigint, 
	@VenueID bigint, 
	@SeasonID bigint, 
	@EventType int,
	@EventName nvarchar(250),
	@AltLocation nvarchar(50),
	@EventDate datetime2(7),
	@OpponentID bigint, 
	@Opponent nvarchar(50),
	@HomeAway nvarchar(10),
	@Uniform nvarchar(20),
	@EventResult nvarchar(50),
	@Comments nvarchar(max),
	@URL nvarchar(250),
	@RequestResponse bit,
	@ResponseLevel int,
	@SendReminders bit,
	@ReminderLevel int,
	@ReminderDays int,
	@EditLevel int,
	@ViewLevel int,
	@ReminderSent int,
	@Deleted bit,
	@Update nvarchar(max),
	@EventID bigint
AS
BEGIN
UPDATE [dbo].[MyOrgEvent]
   SET [OrgID] = @OrgID
	  ,[VenueID] = @VenueID
	  ,[SeasonID] = @SeasonID
	  ,[EventType] = @EventType
      ,[EventName] = @EventName
	  ,[AltLocation] = @AltLocation
      ,[EventDate] = @EventDate
	  ,[OpponentID] = @OpponentID
	  ,[Opponent] = @Opponent
	  ,[HomeAway] = @HomeAway
	  ,[Uniform] = @Uniform
      ,[EventResult] = @EventResult
      ,[Comments] = @Comments
      ,[URL] = @URL
	  ,[RequestResponse] = @RequestResponse
	  ,[ResponseLevel] = @ResponseLevel
	  ,[SendReminders] = @SendReminders
	  ,[ReminderLevel] = @ReminderLevel
	  ,[ReminderDays] = @ReminderDays
	  ,[EditLevel] = @EditLevel
	  ,[ViewLevel] = @ViewLevel
	  ,[ReservedInt] = @ReminderSent
	  ,[ReservedLong] = 0
	  ,[ReservedString] = ''
	  ,[Deleted] = @Deleted
      ,[LastUpdate] = @Update
 WHERE [EventID]=@EventID
END
GO
