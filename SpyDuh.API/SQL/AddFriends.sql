Select * from SpyFriendRelationship;

Insert into SpyFriendRelationship(SpyId, SpyFriendId)
	Values('b1b1e512-aea4-48bc-a266-2f456912871d', 'aa437028-1aec-4238-8654-3f8ffbbf0fe8');

	select S.Name as [Spy], SF.Name as [Friend] from Spy S
	Join SpyFriendRelationship SR
		On S.Id = SR.SpyId
	Join Spy SF 
		On SF.Id = SR.SpyFriendId;

	select SF.Id as [Friend] from Spy S
	Join SpyFriendRelationship SR
		On S.Id = SR.SpyId
	Join Spy SF 
		On SF.Id = SR.SpyFriendId;

	select SF.Id as [Friend] from Spy SF
	                            Join SpyFriendRelationship SR
		                        on SR.Id = SF.SpyId
	                            Join Spy S

	select  * from SpyFriendRelationship

	delete from SpyFriendRelationship
	where SpyId = 'AA437028-1AEC-4238-8654-3F8FFBBF0FE8';

