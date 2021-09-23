DROP TABLE IF EXISTS dbo.Spy

CREATE TABLE dbo.Spy
(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	Name nchar(50) NULL
) 

drop table IF EXISTS dbo.SpySkills

CREATE TABLE dbo.SpySkills
(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	Description varchar(50) NOT NULL,
	ENUM int NOT NULL identity(0,1)
)

DROP TABLE IF EXISTS dbo.SpySkillRelationship

CREATE TABLE dbo.SpySkillRelationship
(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	SpyId uniqueidentifier NOT NULL,
	SkillId uniqueidentifier NOT NULL,
	CONSTRAINT FK_SpyId FOREIGN KEY (SpyId)
       REFERENCES dbo.Spy (Id),
	CONSTRAINT FK_SkillId FOREIGN KEY (SkillId)
       REFERENCES dbo.SpySkills (Id)
)

select * from Orders
join Hats h
	on h.Id = Orders.HatId
join Birds b
	on b.Id = Orders.BirdId;

drop table IF EXISTS dbo.SpyServices

CREATE TABLE dbo.SpyServices
(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	Description varchar(50) NOT NULL,
	ENUM int NOT NULL identity(0,1)
)

DROP TABLE IF EXISTS dbo.SpyServicesRelationship

CREATE TABLE dbo.SpyServicesRelationship
(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	SpyId uniqueidentifier NOT NULL,
	ServiceId uniqueidentifier NOT NULL,
	CONSTRAINT FK_SpyId_SpyServices FOREIGN KEY (SpyId)
       REFERENCES dbo.Spy (Id),
	CONSTRAINT FK_ServiceId_SpyServices FOREIGN KEY (ServiceId)
       REFERENCES dbo.SpyServices (Id)
)

DROP TABLE IF EXISTS dbo.SpyFriendRelationship;

CREATE TABLE dbo.SpyFriendRelationship
(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	SpyId uniqueidentifier NOT NULL,
	SpyFriendId uniqueidentifier NOT NULL,
	CONSTRAINT FK_SpyId_SpyFriends FOREIGN KEY (SpyId)
       REFERENCES dbo.Spy (Id),
	CONSTRAINT FK_SpyFriend_SpyFriends FOREIGN KEY (SpyFriendId)
       REFERENCES dbo.Spy (Id)
)

DROP TABLE IF EXISTS dbo.SpyEnemiesRelationship;

CREATE TABLE dbo.SpyEnemiesRelationship
(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	SpyId uniqueidentifier NOT NULL,
	SpyEnemyId uniqueidentifier NOT NULL,
	CONSTRAINT FK_SpyId_SpyEnemies FOREIGN KEY (SpyId)
       REFERENCES dbo.Spy (Id),
	CONSTRAINT FK_SpyEnemy_SpyEnemies FOREIGN KEY (SpyEnemyId)
       REFERENCES dbo.Spy (Id)
)
describe tables;

DROP TABLE IF EXISTS dbo.Handlers;

CREATE TABLE dbo.Handler(
	ID uniqueidentifier NOT NULL Primary Key default(newid()),
	Name varchar(50) NOT NULL,
	AgencyName varchar(50) NOT NULL
)
