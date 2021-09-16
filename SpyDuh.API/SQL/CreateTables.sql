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

	
