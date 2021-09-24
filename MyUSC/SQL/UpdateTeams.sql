select * from SportName
select * from SportType
select * from Localization

update Localization SET Sequence=10 WHERE Sequence IS NULL;
update Localization SET Editable=1 WHERE Editable IS NULL;
update Localization SET Deletable=0 WHERE Deletable IS NULL;

INSERT INTO SportName (SportNameID,RSSID,Name,[Description],LogoURL,Sequence,SeasonStarts,SeasonEnds,[Language],Active,CreationUser,CreationDate,LastUpdateUserTime)
VALUES (30,0,'Pro Lacrosse','Professional Lacrosse','',0,GETDATE(),GETDATE(),1,1,'',GETDATE(),'');

Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (68,24,0,'Lacrosse','Youth Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (69,29,0,'Baseball','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (70,29,0,'Football','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (71,29,0,'Soccer','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (72,29,0,'Basketball','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (73,29,0,'Hockey','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (74,29,0,'Lacrosse','Amateur Sports','',1,'tlayson',GETDATE(),'')

Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (75,30,0,'MLL','Professional Lacrosse','/LAX/MLL/MLLLogo.png',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (76,30,0,'NLL','Professional Lacrosse','/LAX/NLL/nlllogo.png',1,'tlayson',GETDATE(),'')

select * from SportDivision
Insert INTO SportDivision (DivisionID,SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (293,75,30,0,'Teams','Professional Lacrosse','/LAX/MLL/MLLLogo.png',1,'tlayson',GETDATE(),'')
Insert INTO SportDivision (DivisionID,SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (294,76,30,0,'Teams','Professional Lacrosse','/LAX/NLL/nlllogo.png',1,'tlayson',GETDATE(),'')

select * from RSSFeeds
Insert into RSSFeeds (Name,URL,[Description],CreationUser, CreationDate,LastUpdateUserTime)
values ('Major League Lacrosse','http://www.majorleaguelacrosse.com/','Professional Lacrosse','tlayson',GETDATE(),'');
Insert into RSSFeeds (Name,URL,[Description],CreationUser, CreationDate,LastUpdateUserTime)
values ('National Lacrosse League','http://www.nll.com/news_rss_feed?tags=389123','Professional Lacrosse','tlayson',GETDATE(),'');

Insert into RSSFeeds (Name,URL,[Description],CreationUser, CreationDate,LastUpdateUserTime)
values ('Edmonton Rush','http://www.edmontonrush.com/news_rss_feed?tags=670717','Professional Lacrosse','tlayson',GETDATE(),'');
Insert into RSSFeeds (Name,URL,[Description],CreationUser, CreationDate,LastUpdateUserTime)
values ('Minnesota Swarm','http://www.mnswarm.com/news_rss_feed?tags=253476%252C320757%252C257808%252C270277%252C264885%252C262637%252C180645','Professional Lacrosse','tlayson',GETDATE(),'');
Insert into RSSFeeds (Name,URL,[Description],CreationUser, CreationDate,LastUpdateUserTime)
values ('Philadelphia Wings','http://www.wingslax.com/news_article/rss_instructions?tags=611451','Professional Lacrosse','tlayson',GETDATE(),'');
Insert into RSSFeeds (Name,URL,[Description],CreationUser, CreationDate,LastUpdateUserTime)
values ('Vancouver Stealth','http://www.stealthlax.com/news_rss_feed?tags=396091%252C421546%252C413927','Professional Lacrosse','tlayson',GETDATE(),'');

select * from SportTeam
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1808,30,75,293,10906,'Boston Cannons','Professional Lacrosse','/LAX/MLL/BostonCannons.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1809,30,75,293,10906,'Charlotte Hounds','Professional Lacrosse','/LAX/MLL/charlottehounds.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1810,30,75,293,10906,'Chesapeake Bayhawks','Professional Lacrosse','/LAX/MLL/ChesapeakeBayhawks.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1811,30,75,293,10906,'Denver Outlaws','Professional Lacrosse','/LAX/MLL/denveroutlaws.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1812,30,75,293,10906,'Hamilton Nationals','Professional Lacrosse','/LAX/MLL/HamiltonNationals.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1813,30,75,293,10906,'New York Lizards','Professional Lacrosse','/LAX/MLL/nylizards.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1814,30,75,293,10906,'Ohio Machine','Professional Lacrosse','/LAX/MLL/ohiomachine.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1815,30,75,293,10906,'Rochester Rattlers','Professional Lacrosse','/LAX/MLL/rochesterrattlers.png',1,'tlayson',GETDATE(),'')

Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1816,30,76,294,10907,'Buffalo Bandits','Professional Lacrosse','/LAX/NLL/BuffaloBandits.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1817,30,76,294,10907,'Calgary Roughnecks','Professional Lacrosse','/LAX/NLL/CalgaryRoughnecks.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1818,30,76,294,10907,'Colorado Mammoth','Professional Lacrosse','/LAX/NLL/Coloradomammoth.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1819,30,76,294,10908,'Edmonton Rush','Professional Lacrosse','/LAX/NLL/Edmontonrush.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1820,30,76,294,10909,'Minnesota Swarm','Professional Lacrosse','/LAX/NLL/MinnesotaSwarm.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1821,30,76,294,10910,'Philadelphia Wings','Professional Lacrosse','/LAX/NLL/PhiladelphiaWings.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1822,30,76,294,10907,'Rochester Knighthawks','Professional Lacrosse','/LAX/NLL/RochesterKnighthawks.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1823,30,76,294,10907,'Toronto Rock','Professional Lacrosse','/LAX/NLL/TorontoRock.png',1,'tlayson',GETDATE(),'')
Insert INTO SportTeam(SportTeamID,SportNameID,SportTypeID,SportDivisionID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (1824,30,76,294,10911,'Vancouver Stealth','Professional Lacrosse','/LAX/NLL/VancouverStealth.png',1,'tlayson',GETDATE(),'')


UPDATE SportType SET RSSID=10906, LogoURL='/LAX/MLL/MLLLogo.png' WHERE SportTypeID=75
UPDATE SportType SET RSSID=10907, LogoURL='/LAX/NLL/nlllogo.png' WHERE SportTypeID=76

UPDATE SportTeam SET SortName='' WHERE SortName IS NULL

