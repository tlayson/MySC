select * from accounts
where FirstName LIKE '%cin%'

select * from MyOrg
select * from [MyOrgAffiliates]

update MyOrgAffiliates set [AffiliateType] = 1 Where [AffiliateIdx]=6

delete MyOrgAffiliates


select * from Venue
select * from MyOrgVenueList

SELECT * FROM Accounts 
WHERE LastName LIKE '%Hi%' 

SELECT * FROM Accounts WHERE LastName LIKE '%Rivera%'

SELECT * FROM Accounts WHERE LastName LIKE '%ri%'

select * from MyOrg
select * from MyOrgSeason 

select * from MyOrgEvent

WHERE FirstName LIKE '%Tom%' 
AND LastName LIKE '%Lay%' 
AND City LIKE '%Mon%' 
AND Zipcode LIKE '%98%' 
AND EmailAddress LIKE '%tla%'

SELECT * FROM [MyOrg]

--CREATE PROCEDURE sp_AddOrgPageOption
--	@OrgID bigint, 
--	@PageID int, 
--	@Visible bit,
--	@AdminLevel int, 
--	@EditLevel int, 
--	@AccessLevel int, 
--	@ViewLevel int, 
--	@Creator nvarchar(50)

--	public enum OrgPageID { Home = 0, Roster = 1, Schedule = 2, MsgBoard = 3, Media = 4, Email = 5, Stats = 6, Venue = 7, Manage = 100 }
--	public enum OrgAccessTypes { Owner = 1, Admin = 2, Contributor = 3, Member = 4, Follower = 5, Guest = 6, Banned = 10 }

use MyUSC;

CREATE PROCEDURE sp_AddVenue
	@VenueName nvarchar(250),
	@OwnerID bigint, 
	@OrgID bigint, 
	@DisplayLocation nvarchar(50),
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@PostalCode nvarchar(50),
	@Country nvarchar(50),
	@Phone nvarchar(50),
	@Website nvarchar(250),
	@MapURL nvarchar(250),
	@ImageURL nvarchar(250),
	@Note nvarchar(1000),
	@MakePublic bit,
	@Creator nvarchar(50)

CREATE PROCEDURE sp_AddVenueUsage
	@VenueID bigint,
	@OrgID bigint, 
	@Creator nvarchar(50)
[dbo].[sp_AddUserPrefs]
SELECT TOP 10 * FROM [MyUSC].[dbo].[Venue]
WHERE VenueName LIKE '%Bell%' 
		AND City LIKE ''
		AND State LIKE ''
		AND VenueType = 1

SELECT * FROM [MyOrg]
exec sp_GetAllVenues
exec sp_GetAllVenueUsage

exec sp_AddVenue 'Bannerwood Park', 91, 10002, 1, 'Richards Road', '123 Richards Road', '', 'Bellevue', 'Washington', '98123', 'United States', 
				'123-456-7890', 'www.bannerwood.com', 'www.bing.com', '', 'Notes go here', 1, 'tlayson'
exec sp_AddVenue 'Bellevue College', 91, 10002, 1, '148th', '12345 148th St NE', '', 'Bellevue', 'Washington', '98125', 'United States', 
				'123-456-7890', 'www.bcc.com', 'www.bing.com', '', 'Notes go here', 1, 'tlayson'
exec sp_AddVenue 'Century Link', 91, 10002, 2, 'Seattle', '12345 1st Ave', '', 'Seattle', 'Washington', '98125', 'United States', 
				'123-456-7890', 'www.bcc.com', 'www.bing.com', '', 'Notes go here', 1, 'tlayson'
exec sp_AddVenue 'Key Arena', 91, 10002, 5, 'Seattle', '345 Mercer St', '', 'Seattle', 'Washington', '98125', 'United States', 
				'123-456-7890', 'www.bcc.com', 'www.bing.com', '', 'Notes go here', 1, 'tlayson'
exec sp_AddVenue 'Madison Square Garden', 91, 10002, 100, 'New York', '23 5th Ave', '', 'New York', 'New York', '98125', 'United States', 
				'123-456-7890', 'www.bcc.com', 'www.bing.com', '', 'Notes go here', 1, 'tlayson'

exec sp_AddVenueUsage 1, 1, 'tlayson'
exec sp_AddVenueUsage 2, 1, 'tlayson'

CREATE PROCEDURE sp_AddOrgEvent
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
	@Deleted bit,
	@Creator nvarchar(50)

	ALTER PROCEDURE [dbo].[sp_AddOrgEventResponse]
	@OrgID bigint, 
	@EventID bigint, 
	@MemberID bigint, 
	@Response int,
	@Notes nvarchar(250)


select * from MyOrg
select * from Venue
select * from MyOrgSeason
select * from MyOrgEvent
select * from MyOrgEventResponse

update MyOrgEvent set [EventResult]=''
update MyOrgEvent set [OrgID]=1
update MyOrgEventResponse set [OrgID]=1

exec sp_AddOrgEvent 10002, 1, 1, 1, 'Event Name', 'Alt', 'Date', 0, 'Opponent', 'Home', 'Uniform', 'Result', 'Comments', 'URL', 1, 4, 1, 4, 2, 3, 5, 0, 'tlayson'

exec sp_AddOrgEvent 10002, 1, 1, 1, 'Game 1', 'Bellevue', '2015-05-10 18:00:00', 0, 'Diablos', 'Home', 'White', 'Result', 'Comments', 'URL', 1, 4, 1, 4, 2, 3, 5, 0, 'tlayson'
exec sp_AddOrgEvent 10002, 2, 1, 1, 'Game 2', 'Bellevue', '2015-05-12 18:00:00', 0, 'Colts', 'Home', 'White', 'Result', 'Comments', 'URL', 1, 4, 1, 4, 2, 3, 5, 0, 'tlayson'
exec sp_AddOrgEvent 10002, 1, 1, 1, 'Game 3', 'Bellevue', '2015-05-14 18:00:00', 0, 'Lugnuts', 'Away', 'White', 'Result', 'Comments', 'URL', 1, 4, 1, 4, 2, 3, 5, 0, 'tlayson'
exec sp_AddOrgEvent 10002, 2, 1, 1, 'Game 4', 'Bellevue', '2015-05-16 18:00:00', 0, 'Diamonds', 'Home', 'White', 'Result', 'Comments', 'URL', 1, 4, 1, 4, 2, 3, 5, 0, 'tlayson'

exec sp_AddOrgEventResponse 10002, 1, 91, 1, ''
exec sp_AddOrgEventResponse 10002, 2, 91, 1, ''

ALTER PROCEDURE [dbo].[sp_AddOrgSeason]
	@OrgID bigint, 
	@SeasonName nvarchar(250),
	@SeasonStart datetime2(7),
	@Comments nvarchar(200),
	@IsDefault bit,
	@Share bit,
	@Creator nvarchar(50)

exec sp_AddOrgSeason 1, '2015', '2015-05-10 18:00:00', '', 1, 1, 'tlayson'

select * from [MyOrgPageOptions]
delete [MyOrgPageOptions] where PageOptionIdx=1

exec sp_AddOrgPageOption 1,0,1,2,3,4,6,'tlayson' -- Home
exec sp_AddOrgPageOption 1,1,1,2,3,4,6,'tlayson' -- Roster
exec sp_AddOrgPageOption 1,2,1,2,3,4,6,'tlayson' -- Schedule
exec sp_AddOrgPageOption 1,3,1,2,3,4,4,'tlayson' -- MsgBoard
exec sp_AddOrgPageOption 1,4,1,2,3,4,5,'tlayson' -- Media
exec sp_AddOrgPageOption 1,5,1,2,3,4,4,'tlayson' -- Email
exec sp_AddOrgPageOption 1,6,0,2,3,4,5,'tlayson' -- Stats
exec sp_AddOrgPageOption 1,7,1,2,3,4,6,'tlayson' -- Venue
exec sp_AddOrgPageOption 1,100,1,2,2,2,2,'tlayson' -- Manage

select * from MyOrg
select * from MyOrgMembers
select * from MyOrgPageOptions

delete MyOrg
delete MyOrgMembers
delete MyOrgPageOptions

SELECT * FROM MyOrgMembers
WHERE UserID=91
ORDER BY MemberType

-- Join practice
select * from MyOrg
select * from MyOrgMembers
select * from Accounts
exec sp_AddOrgMember 1,91,1,'00','P/C/IF','','tlayson' -- Tom
exec sp_AddOrgMember 1,19,2,'19','2B','','tlayson' -- Eddie
exec sp_AddOrgMember 1,10,3,'12','OF','','tlayson' -- Renee
exec sp_AddOrgMember 1,10127,4,'21','OF','','tlayson' -- Michael
exec sp_AddOrgMember 1,10126,5,'22','3B','','tlayson' -- Riley
exec sp_AddOrgMember 1,10125,6,'3','1B','','tlayson' -- Cindy
exec sp_AddOrgMember 1,1,10,'X','Banned','','tlayson' -- Terry
exec sp_AddOrgMember 1,67,2,'1','Tester','','tlayson' -- Jeff

exec sp_GetOrgMembers 1

INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (1,1,91,1,'00','P/C/IF','',0,1,'tlayson',GETDATE(),'')
INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (2,1,19,2,'19','2B','',0,1,'tlayson',GETDATE(),'')
INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (3,1,10,3,'12','OF','',0,1,'tlayson',GETDATE(),'')
INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (4,1,128,4,'21','OF','',0,1,'tlayson',GETDATE(),'')

INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (5,1,127,5,'22','3B','',0,1,'tlayson',GETDATE(),'')
INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (6,1,126,6,'3','1B','',0,1,'tlayson',GETDATE(),'')
INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (7,1,1,11,'X','Banned','',0,1,'tlayson',GETDATE(),'')
INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (8,1,67,2,'1','Tester','',0,1,'tlayson',GETDATE(),'')
INSERT INTO [dbo].[MyOrgMembers]
           ([MemberIdx],[OrgID],[UserID],[MemberType],[Number],[Positions],[Note],[Deleted],[AcceptedInvite],[CreationUser],[CreationDate],[LastUpdate])
VALUES
           (9,1,11,4,'11','Member','',0,1,'tlayson',GETDATE(),'')


exec sp_GetOrgEvents 1
exec sp_AddOrgEvent 1, 1, 1, 'EventName', '2015-04-30 13:00:00.000', 'URL', 'Result', 'Comments', 'tlayson'
exec sp_AddOrgEvent 1, 1, 1, 'Event 2', '2015-05-01 13:00:00.000', 'URL', 'Result', 'Comments', 'tlayson'
exec sp_AddOrgEvent 1, 1, 1, 'Event 3', '2015-05-02 13:00:00.000', 'URL', 'Result', 'Comments', 'tlayson'
exec sp_AddOrgEvent 1, 1, 1, 'Event 4', '2015-05-03 13:00:00.000', 'URL', 'Result', 'Comments', 'tlayson'

exec sp_UpdateOrgEvent 1, 1, 1, 'Event 1', '2015-04-30 13:00:00.000', 'URL', 'Result', '', 0, '', 1

exec sp_UpdateUserLastLogin GETDATE(),91

exec sp_AddOrgMember 1,91,1,'00','The Boss','','tlayson' -- Me

exec sp_GetOrgMembers 10002

delete MyOrgMembers

exec sp_AddOrgMember 10003,19,1,'','tlayson' -- Eddie
UPDATE MyOrgMembers SET MemberType=3 where UserID=91 AND OrgID=10003;

exec sp_GetOrgMembers 10002


DECLARE @UserID bigint
SET @UserID = 1;
select * 
from MyOrgEvent evt
	LEFT JOIN 
	(
	select * from MyOrgMembers om
	where om.UserID=@UserID
	) b ON b.OrgID = evt.OrgID
Order BY evt.EventDate

	select * from MyOrgMembers om
	where om.UserID=157

select * from Accounts

exec sp_GetUserDashboardEvents 19
exec sp_GetUserDashboardEvents 91
exec sp_GetUserDashboardEvents 123
exec sp_GetUserDashboardEvents 157

exec sp_GetUserDashboardFutureEvents 91
exec sp_GetUserDashboardPastEvents 91

select * from MyOrgEvent
select * from MyOrgMembers

select * from MyOrgEvent evt
where exists
(
	select * from MyOrgMembers om
	where om.UserID=@UserID
)b ON b.OrgID = om.OrgID
Order BY evt.EventDate

	select * from MyOrgMembers om
	where om.UserID=157


DECLARE @UserID bigint
SET @UserID = 91;
select * from MyOrgEvent as evt
where evt.OrgID IN
(
select OrgID from MyOrgMembers
where UserID=@UserID
)
AND evt.EventDate > GETDATE()
Order BY evt.EventDate ASC

select * from MyOrgEvent as evt
where evt.OrgID IN
(
select OrgID from MyOrgMembers
where UserID=@UserID
)
AND evt.EventDate < GETDATE()
Order BY evt.EventDate DESC


SELECT p.FirstName, p.LastName, e.JobTitle
FROM Person.Person AS p JOIN HumanResources.Employee AS e
   ON e.BusinessEntityID = p.BusinessEntityID 
JOIN HumanResources.EmployeeDepartmentHistory AS edh
   ON e.BusinessEntityID = edh.BusinessEntityID 
WHERE edh.DepartmentID IN
(SELECT DepartmentID
   FROM HumanResources.Department
   WHERE Name LIKE 'P%');
GO

GO
SELECT p.FirstName, p.LastName, e.JobTitle
FROM Person.Person AS p 
JOIN HumanResources.Employee AS e
   ON e.BusinessEntityID = p.BusinessEntityID 
WHERE EXISTS
(SELECT *
    FROM HumanResources.Department AS d
    JOIN HumanResources.EmployeeDepartmentHistory AS edh
       ON d.DepartmentID = edh.DepartmentID
    WHERE e.BusinessEntityID = edh.BusinessEntityID
    AND d.Name LIKE 'P%');
GO












SELECT Member.MemberIdx, Member.OrgID, Member.MemberType, Member.Note, Member.Deleted, account.AccountID , account.LastName 
 FROM MyOrgMembers Member
  INNER JOIN Accounts account 
     ON Member.UserID=account.AccountID AND OrgID = 10002
ORDER BY Member.MemberType, account.LastName


SELECT [MemberIdx]
      ,[OrgID]
      ,[UserID]
      ,[MemberType]
      ,[Note]
      ,[Deleted]
      ,[CreationUser]
      ,[CreationDate]
      ,[LastUpdate]
  FROM [MyUSC].[dbo].[MyOrgMembers]

SELECT * FROM MyOrgMembers Member 
  INNER JOIN Accounts account 
     ON Emp.Departmentid=Dept.Departmenttid
ORDER BY Emp.EmpLastName


CREATE TABLE [dbo]. [Employee](
[Empid] [Int] IDENTITY (1, 1) NOT NULL Primary key,
[EmpNumber] [nvarchar](50) NOT NULL,
[EmpFirstName] [nvarchar](150) NOT NULL,
[EmpLastName] [nvarchar](150) NULL,
[EmpEmail] [nvarchar](150) NULL,
[Managerid] [int] NULL,
[Departmentid] [INT]
)
CREATE TABLE [dbo].[Department](
[Departmenttid] [int] IDENTITY (1, 1) NOT NULL primary key,
[DepartmentName] [nvarchar](255) NOT NULL
)
delete [Employee]
delete [Department]

drop table [Employee]
drop table [Department]

insert into Employee
(EmpNumber,EmpFirstName,EmpLastName,EmpEmail,Managerid,Departmentid)
values('A001','Samir','Singh','samir@abc.com',2,2)
insert into Employee
(EmpNumber,EmpFirstName,EmpLastName,EmpEmail,Managerid,Departmentid)
values('A002','Amit','Kumar','amit@abc.com',1,1)
insert into Employee (EmpNumber,EmpFirstName,EmpLastName,EmpEmail,Managerid,Departmentid)
values('A003','Neha','Sharma','neha@abc.com',1,2)
insert into Employee (EmpNumber,EmpFirstName,EmpLastName,EmpEmail,Managerid,Departmentid)
values('A004','Vivek','Kumar','vivek@abc.com',1,NULL)

insert into Department(DepartmentName)
values('Accounts')
insert into Department(DepartmentName)
values('Admin')
insert into Department(DepartmentName)
values('HR')
insert into Department(DepartmentName)
values('Technology')

-- Inner join 
SELECT Emp.Empid, Emp.EmpFirstName, Emp.EmpLastName, Dept.DepartmentName 
 FROM Employee Emp 
  INNER JOIN Department dept 
     ON Emp.Departmentid=Dept.Departmenttid
ORDER BY Emp.EmpLastName



-- Outer left join
SELECT Emp.Empid, 
       Emp.EmpFirstName, 
    Emp.EmpLastName, 
    Dept.DepartmentName
  FROM Employee Emp 
     LEFT OUTER JOIN Department dept 
     ON Emp.Departmentid=Dept.Departmenttid

-- Empid EmpFirstName EmpLastName DepartmentName
-- 1     Samir        Singh       Admin
-- 2     Amit         Kumar       Accounts
-- 3     Neha         Sharma      Admin
-- 4     Vivek        Kumar       NULL

-- Outer right join
SELECT Dept.DepartmentName, 
       Emp.Empid, Emp.EmpFirstName, 
    Emp.EmpLastName 
  FROM Employee Emp 
    RIGHT OUTER JOIN Department dept 
   ON Emp.Departmentid=Dept.Departmentid

-- DepartmentName Empid EmpFirstName EmpLastName
-- Accounts       2     Amit         Kumar
-- Admin          1     Samir        Singh
-- Admin          3     Neha         Sharma
-- HR             NULL  NULL         NULL
-- Technology     NULL  NULL         NULL

-- Full outer join
SELECT Emp.Empid, 
       Emp.EmpFirstName, 
    Emp.EmpLastName, 
    Dept.DepartmentName 
  FROM Employee Emp 
     FULL OUTER JOIN Department dept 
    ON Emp.Departmentid=Dept.Departmenttid

-- Empid EmpFirstName EmpFirstName DepartmentName
-- 1     Samir        Singh        Admin
-- 2     Amit         Kumar        Accounts
-- 3     Neha         Sharma       Admin
-- 4     Vivek        Kumar        NULL
-- NULL  NULL         NULL         HR
-- NULL  NULL         NULL         Technology
