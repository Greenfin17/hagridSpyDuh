insert into Spy (Name)
values('James Bond'),
		('Pierre LaKlutz'),
		('Vadim Kirpichenko'),
		('Whitaker Chambers'),
		('Jona von Ustinov');

insert into Spy (Name)
	output inserted.*
    values('John Croft');
	