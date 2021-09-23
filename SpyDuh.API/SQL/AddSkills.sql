 Insert into SpySkills (Description)
 VALUES
        ('None'),
        ('Locksmith'),
        ('Marksmanship'),
        ('Pilot'),
        ('RockClimbing'),
        ('Combat'),
        ('Interrogation'),
        ('Hacker'),
        ('Disguises'),
        ('Dancing'),
        ('Alcoholic'),
        ('DefensiveDriving'),
        ('Poker'),
        ('Blackjack'),
        ('CardCounting'),
        ('Forgery'),
        ('Fencing'),
        ('Linguist'),
        ('Languages'),
        ('MicrosoftExcel'),
        ('Whistling'),
        ('SocialEngineering'),
        ('Charisma'),
        ('OneLiners'),
        ('SnarkyComebacks'),
        ('Seduction'),
        ('ScubaDiving');

		select * from SpySkills
		order by enum;

		/* update SpySkills
		set Description = 'ScubaDiving'
		where ENUM = 26; */
