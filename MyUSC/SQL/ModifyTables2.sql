ALTER TABLE dbo.SportType ADD [Sequence] float DEFAULT(1);
ALTER TABLE dbo.SportDivision ADD [Sequence] float DEFAULT(1);
ALTER TABLE dbo.SportTeam ADD [Sequence] float DEFAULT(1);

ALTER TABLE dbo.Accounts ADD Nickname NVARCHAR(50) DEFAULT('')
ALTER TABLE dbo.Preferences ADD ShowNickname bit DEFAULT(0)
ALTER TABLE dbo.Preferences ADD ProfileUpdated bit DEFAULT(0)

UPDATE SportType SET [Sequence]=1;
UPDATE SportDivision SET [Sequence]=1;
UPDATE SportTeam SET [Sequence]=1;

UPDATE Accounts SET Nickname='';
UPDATE Preferences SET ShowNickname=0;
UPDATE Preferences SET ProfileUpdated=0;

ALTER TABLE dbo.Preferences ADD ProvideSecurityQuestion bit DEFAULT(1)

UPDATE Preferences SET ProvideSecurityQuestion=1;
