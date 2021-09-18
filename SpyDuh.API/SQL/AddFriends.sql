Select * from SpyFriendRelationship;

Insert into SpyFriendRelationship(SpyId, SpyFriendId)
	Values('b1b1e512-aea4-48bc-a266-2f456912871d', 'aa437028-1aec-4238-8654-3f8ffbbf0fe8');

	select Name, Friend from Spy S
	Join SpyFriendRelationship SF
		On S.Id = SF.SpyId
	Join Spy on
		SF.SpyFriendId = Spy.Id;

	