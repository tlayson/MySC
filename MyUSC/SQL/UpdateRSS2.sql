use MyUSC;

update RSSFeeds SET URL='http://www.nba.com/rss/nba_rss.xml' where RSSID=34
update RSSFeeds SET URL='http://www.lpga.com/news/feeds/headlines-rss.aspx' where RSSID=187
update RSSFeeds SET URL='http://www.oursportscentral.com/feeds/Football.xml' where RSSID=341

update SportName SET LogoURL='/MLS/MLS.png' where NameKeyID=4
update SportName SET LogoURL='/WNBA/WNBA.png' where NameKeyID=6
update SportName SET LogoURL='/NASCAR/Nascar.jpg' where NameKeyID=7
update SportName SET LogoURL='/PGA/PGA.png' where NameKeyID=8
update SportName SET LogoURL='/LPGA/LPGA.png' where NameKeyID=9
update SportName SET LogoURL='/PBA/PBA.png' where NameKeyID=10
update SportName SET LogoURL='/UFC/UFC.png' where NameKeyID=12
update SportName SET LogoURL='/ExtremeSports/ExtremeSportslogo.png' where NameKeyID=17
update SportName SET LogoURL='/OutdoorSports/OutdoorSports.png' where NameKeyID=18
update SportName SET LogoURL='/Amateur/Amateur.png' where NameKeyID=19
update SportName SET LogoURL='/LAX/LAX.jpg' where NameKeyID=10019

update SportType SET LogoURL='/WNBA/WNBA.png' where TypeKeyID=7
update SportType SET LogoURL='/WNBA/WNBA.png' where TypeKeyID=8
update SportType SET LogoURL='/PGA/PGA.png' where TypeKeyID=11
update SportType SET LogoURL='/LPGA/LPGA.png' where TypeKeyID=12
update SportType SET RSSID=188 where TypeKeyID=11
update SportType SET RSSID=187 where TypeKeyID=12
update SportType SET LogoURL='/UFC/UFC.png' where TypeKeyID=9
update SportType SET LogoURL='/UFC/UFC.png' where TypeKeyID>=13 AND TypeKeyID<=18
update SportType SET LogoURL='/PBA/PBA.png' where TypeKeyID=21
update SportType SET RSSID=189 where TypeKeyID=21

update SportName SET Sequence=1 where NameKeyID=1
update SportName SET Sequence=2 where NameKeyID=3
update SportName SET Sequence=3 where NameKeyID=11
update SportName SET Sequence=4 where NameKeyID=2
update SportName SET Sequence=5 where NameKeyID=5
update SportName SET Sequence=6 where NameKeyID=6
update SportName SET Sequence=7 where NameKeyID=4
update SportName SET Sequence=8 where NameKeyID=8
update SportName SET Sequence=9 where NameKeyID=9
update SportName SET Sequence=10 where NameKeyID=12
update SportName SET Sequence=11 where NameKeyID=17
update SportName SET Sequence=12 where NameKeyID=10019
update SportName SET Sequence=13 where NameKeyID=18
update SportName SET Sequence=14 where NameKeyID=16
update SportName SET Sequence=15 where NameKeyID=7
update SportName SET Sequence=16 where NameKeyID=10
update SportName SET Sequence=17 where NameKeyID=13
update SportName SET Sequence=18 where NameKeyID=15
update SportName SET Sequence=19 where NameKeyID=14
update SportName SET Sequence=20 where NameKeyID=19
