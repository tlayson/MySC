ALTER TABLE dbo.RSSFeeds ADD Notes NVARCHAR(200) NULL
ALTER TABLE dbo.RSSFeeds ADD AltWebsite NVARCHAR(500) NULL

ALTER TABLE dbo.Accounts ADD LastLogin datetime2(7) DEFAULT(getdate())
ALTER TABLE dbo.Accounts ADD Nickname NVARCHAR(50) DEFAULT('')
ALTER TABLE dbo.Preferences ADD ShowNickname bit DEFAULT(0)
ALTER TABLE dbo.Preferences ADD ProfileUpdated bit DEFAULT(0)

ALTER TABLE dbo.SportName ADD Website NVARCHAR(500) DEFAULT('')
ALTER TABLE dbo.SportType ADD Website NVARCHAR(500) DEFAULT('')
ALTER TABLE dbo.SportDivision ADD Website NVARCHAR(500) DEFAULT('')
ALTER TABLE dbo.SportTeam ADD Website NVARCHAR(500) DEFAULT('')

ALTER TABLE dbo.SportName ADD RSSNotes NVARCHAR(200) DEFAULT('')
ALTER TABLE dbo.SportType ADD RSSNotes NVARCHAR(200) DEFAULT('')
ALTER TABLE dbo.SportDivision ADD RSSNotes NVARCHAR(200) DEFAULT('')
ALTER TABLE dbo.SportTeam ADD RSSNotes NVARCHAR(200) DEFAULT('')

UPDATE RSSFeeds SET Notes='', AltWebsite='';
UPDATE Accounts SET LastLogin=SMALLDATETIMEFROMPARTS ( 2010, 12, 31, 0, 0 );
UPDATE Accounts SET LastLogin=getdate() where AccountID=91;
UPDATE Accounts SET Nickname='';
UPDATE Preferences SET ShowNickname=0;
UPDATE Preferences SET ProfileUpdated=0;
UPDATE SportName SET Website='', RSSNotes='';
UPDATE SportType SET Website='', RSSNotes='';
UPDATE SportDivision SET Website='', RSSNotes='';
UPDATE SportTeam SET Website='', RSSNotes='';

