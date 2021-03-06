/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [MyUSC].[dbo].[NewsMenu]
  Where ParentID = 5

SELECT *
  FROM [MyUSC].[dbo].[NewsMenu]
  Where ParentID = 42 or ParentID = 43

SELECT *
  FROM [MyUSC].[dbo].[NewsMenu]
  Where ParentID = 100 or ParentID = 101 or ParentID = 102

SELECT *
  FROM [MyUSC].[dbo].[NewsMenu]
  Where ParentID = 103 or ParentID = 104 or ParentID = 105

SELECT *
  FROM [MyUSC].[dbo].[NewsMenu]
  Where ParentID = 102 or ParentID = 105

SELECT *
  FROM [MyUSC].[dbo].[NewsMenu]
  Where RSSID = 881

SELECT *
  FROM [MyUSC].[dbo].[RSSFeeds]

update [NewsMenu] set Name = 'Metropolitan' Where NewsMenuKeyID = 101
update [NewsMenu] set Name = '' Where NewsMenuKeyID = 102
update [NewsMenu] set Name = 'Central' Where NewsMenuKeyID = 103
update [NewsMenu] set Name = 'Pacific' Where NewsMenuKeyID = 104
update [NewsMenu] set Name = '' Where NewsMenuKeyID = 105
update [NewsMenu] set Name = 'Arizona Coyotes' Where NewsMenuKeyID = 470


/** Atlantic **/
update [NewsMenu] set ParentID = 100 Where NewsMenuKeyID = 447 /** Bruins **/
update [NewsMenu] set ParentID = 100 Where NewsMenuKeyID = 448 /** Leafs **/
update [NewsMenu] set ParentID = 100 Where NewsMenuKeyID = 449 /** Senators **/
update [NewsMenu] set ParentID = 100 Where NewsMenuKeyID = 450 /** Sabres **/
update [NewsMenu] set ParentID = 100 Where NewsMenuKeyID = 451 /** Canadiens **/
update [NewsMenu] set ParentID = 100 Where NewsMenuKeyID = 452 /** Panthers **/
update [NewsMenu] set ParentID = 100 Where NewsMenuKeyID = 455 /** Lightning **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 459 /** Detroit **/

/** Metropolitan **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 442 /** Rangers **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 443 /** Flyers **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 444 /** Penguins **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 445 /** Devils **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 446 /** Islanders **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 454 /** Capitals **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 456 /** Hurricanes **/
update [NewsMenu] set ParentID = 101 Where NewsMenuKeyID = 458 /** Columbus **/

/** Central **/
update [NewsMenu] set ParentID = 103 Where NewsMenuKeyID = 453 /** Winnipeg **/
update [NewsMenu] set ParentID = 103 Where NewsMenuKeyID = 457 /** Chicago **/
update [NewsMenu] set ParentID = 103 Where NewsMenuKeyID = 460 /** Nashville **/
update [NewsMenu] set ParentID = 103 Where NewsMenuKeyID = 461 /** Blues **/
update [NewsMenu] set ParentID = 103 Where NewsMenuKeyID = 463 /** Colorado **/
update [NewsMenu] set ParentID = 103 Where NewsMenuKeyID = 465 /** Wild **/
update [NewsMenu] set ParentID = 103 Where NewsMenuKeyID = 468 /** Stars **/


/** Pacific **/
update [NewsMenu] set ParentID = 104 Where NewsMenuKeyID = 462 /** Calgary **/
update [NewsMenu] set ParentID = 104 Where NewsMenuKeyID = 467 /** Ducks **/
update [NewsMenu] set ParentID = 104 Where NewsMenuKeyID = 466 /** Canucks **/
update [NewsMenu] set ParentID = 104 Where NewsMenuKeyID = 469 /** Kings **/
update [NewsMenu] set ParentID = 104 Where NewsMenuKeyID = 471 /** Sharks **/
update [NewsMenu] set ParentID = 104 Where NewsMenuKeyID = 464 /** Oilers **/
update [NewsMenu] set ParentID = 104 Where NewsMenuKeyID = 470 /** Arizona **/

delete [NewsMenu] Where NewsMenuKeyID = 102
delete [NewsMenu] Where NewsMenuKeyID = 105

update [NewsMenu] set RSSID = 0 Where NewsMenuKeyID = 51 /** CFL West **/
