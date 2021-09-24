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
CREATE PROCEDURE sp_GetOrgMembers
	@OrgID bigint
AS
BEGIN

SELECT Member.MemberIdx, Member.OrgID, Member.UserID, Member.MemberType, Member.Number, Member.Positions, Member.Note, Member.Deleted, Member.AcceptedInvite, Member.[CreationUser], Member.[CreationDate], Member.[LastUpdate], account.LastName 
 FROM MyOrgMembers Member
  INNER JOIN Accounts account 
     ON Member.UserID=account.AccountID AND OrgID = @OrgID
ORDER BY Member.MemberType, account.LastName

END
GO
