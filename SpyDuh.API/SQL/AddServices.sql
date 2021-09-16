 Insert into SpyServices (Description)
 VALUES
        ('Assasination'),
        ('Theft'),
        ('Interrogation') ,
        ('SearchAndRescue'),
        ('SaveTheWorld'),
        ('Dossier'),
        ('ThwartEvilPlans'),
        ('Infiltration'),
        ('CarryOutEvilPlans'),
        ('HelpTheBadGuys'),
        ('StealingFinancialRecords'),
        ('Framing'),
        ('IntelligenceGathering')

		select * from SpyServices
		order by enum;

		/* update SpySkills
		set Description = 'ScubaDiving'
		where ENUM = 26; */
