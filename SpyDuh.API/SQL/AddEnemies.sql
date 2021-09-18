Insert into SpyEnemiesRelationship(SpyId, SpyEnemyId)
	Values('b1b1e512-aea4-48bc-a266-2f456912871d', '366a8d11-a5ca-4ed3-983b-9708ca64b173');


Select * from SpyEnemiesRelationship;


Select SE.id from Spy S
	Join SpyEnemiesRelationship SR
		on S.Id = SR.SpyId
	Join Spy SE
		on SE.Id = SR.SpyEnemyId
Where S.id = 'b1b1e512-aea4-48bc-a266-2f456912871d';