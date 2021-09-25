using SpyDuh.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace SpyDuh.API.Repositories
{
    public class SpyRepo
    {
        HandlerRepo _handlers;

        readonly string _connectionString;
        public SpyRepo(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SpyDuh");
            _handlers = new HandlerRepo(config);
        }
        internal IEnumerable<Spy> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            // connection must be opened, not open by default
            connection.Open();

            // ADO.NET SQL COMMANDS
            // Tells SQL what we want to do.
            var command = connection.CreateCommand();
            command.CommandText = @"Select * 
                                    From Spy";

            // execute reader is for when we care about getting all the results of our query
            var reader = command.ExecuteReader();
            var spies = new List<Spy>();
            // reader can hold only one row of data at a time.
            while (reader.Read())
            {
                var spyObj = MapFromReader(reader);
                spies.Add(spyObj);

            }
            return spies;
        }

        internal Spy GetSpy(Guid spyGuid)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Select * 
                                    From Spy
                                    where id = @id";
            cmd.Parameters.AddWithValue("id", spyGuid);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapFromReader(reader);
            }
            else return null;
        }

        internal bool AddSkill(Spy spy, string spySkill)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Select * from SpySkills
                                Where Description = @spySkill";
            cmd.Parameters.AddWithValue("spySkill", spySkill);
            var reader = cmd.ExecuteReader();
            if (reader.Read() & spy != null && spy.Skills != null)
            {
                var skillGuid = reader["Id"];
                reader.Close();
                cmd.CommandText = @"Select SpyId, SkillId from SpySkillRelationship
                                    Where SpyId = @spyId and SkillId = @skillId";
                cmd.Parameters.AddWithValue("spyId", spy.Id);
                cmd.Parameters.AddWithValue("skillId", skillGuid);
                var reader2 = cmd.ExecuteReader();
                // check if the skill is already listed for this spy.
                if (!reader2.Read())
                {
                    reader2.Close();
                    cmd.CommandText = @"Insert into SpySkillRelationship (SpyId, SkillId)
                                        output inserted.*
	                                    Values(@spyId, @skillId)";
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0) return true;
                }
            }
            return false;
        }

        internal IEnumerable<Spy> ListFriends(Guid spyGuid)
        {
            var spyObj = GetSpy(spyGuid);
            var friendList = new List<Spy>();
            if (spyObj != null && spyObj.Friends.Count > 0)
            {
                // loop through the list of friend Id's and retrieve the full friend objects
                // and add them to a list.
                foreach (var friendGuid in spyObj.Friends)
                {
                    var friendObj = GetSpy(friendGuid);
                    if (friendObj != null)
                    {
                        friendList.Add(friendObj);
                    }
                }
            }
            return friendList;
        }

        internal bool AddFriend(Guid spyId, Guid friendId)
        {
            using var connection = new SqlConnection(_connectionString);
            bool returnVal = false;
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Insert into SpyFriendRelationship(SpyId, SpyFriendId)
                                Values(@spyId, @friendId)";
            cmd.Parameters.AddWithValue("spyId", spyId);
            cmd.Parameters.AddWithValue("friendId", friendId);
            var result = cmd.ExecuteNonQuery();
            if (result == 1) returnVal = true;
            return returnVal;
        }

        internal bool AddEnemy(Guid spyId, Guid enemyId)
        {
            bool returnVal = false;
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Insert into SpyEnemiesRelationship(SpyId, SpyEnemyId)
                                Values(@spyId, @enemyId)";
            cmd.Parameters.AddWithValue("spyId", spyId);
            cmd.Parameters.AddWithValue("enemyId", enemyId);
            var result = cmd.ExecuteNonQuery();
            if (result == 1) returnVal = true;
            return returnVal;
        }

        internal IEnumerable<Spy> ListEnemies(Guid spyGuid)
        {
            var enemyList = new List<Spy>();
            var spyObj = GetSpy(spyGuid);
            if(spyObj != null && spyObj.Enemies.Count > 0)
            {
                foreach (var enemyGuid in spyObj.Enemies)
                {
                    var enemyObj = GetSpy(enemyGuid);
                    enemyList.Add(enemyObj);
                }
            }
            return enemyList;
        }

        internal String ListSkillsAndServices(Guid spyGuid)
        {
            var spyObj = GetSpy(spyGuid);
            StringBuilder output = new StringBuilder();
            if (spyObj != null)
            {
                output.Append("Skills:\n");

                foreach ( var skill in spyObj.Skills)
                {
                    output.Append(skill.ToString());
                    output.Append('\n');
                }

                output.Append("\nServices:\n");
                foreach (var service in spyObj.Services)
                {
                    output.Append(service.ToString());
                    output.Append('\n');
                }
            }
            else
            {
                output.Append("This spy is not in our database.");

            }

            return output.ToString();
        }

        internal IEnumerable<Spy> GetFriendsFriends(Guid spyGuid)
        {
            var spyObj = GetSpy(spyGuid);
            var friendList = new List<Spy>();
            if (spyObj != null && spyObj.Friends.Count > 0)
            {
                // loop through the list of friend Id's 
                foreach (var friendGuid in spyObj.Friends)
                {
                    var tempList = new List<Spy>();
                    var friendObj = GetSpy(friendGuid);
                    if (friendObj != null)
                    {
                        // get the list of the friend's friends
                        tempList = ListFriends(friendGuid).ToList();
                    }
                    if (tempList != null && tempList.Count > 0)
                    {
                        friendList.AddRange(from friend in tempList// add the friend's friends to the list, excepting the original spy or a duplicate.
                                            where friend.Id != spyGuid && !friendList.Contains(friend)
                                            select friend);
                    }
                }
            }
            return friendList;
        }
        internal bool AddSpy(ref Spy newSpy)
        {
            bool returnVal = false;
            using var db = new SqlConnection(_connectionString);
            // design of app requires the following to be added separately
            if (newSpy.Skills == null) newSpy.Skills = new List<SpySkills>();
            else newSpy.Skills.Clear();
            if (newSpy.Services == null) newSpy.Services = new List<SpyServices>();
            else newSpy.Services.Clear();
            if (newSpy.Friends == null) newSpy.Friends = new List<Guid>();
            else newSpy.Friends.Clear();
            if (newSpy.Enemies == null) newSpy.Enemies = new List<Guid>();
            else newSpy.Enemies.Clear();
            if (newSpy.Handlers == null) newSpy.Handlers = new List<Guid>();
            else newSpy.Handlers.Clear();
            db.Open();
            var sql = @"insert into Spy (Name)
                                output inserted.*
                                values(@Name)";
            // adonet code 
            // cmd.Parameters.AddWithValue("spyName", spyName);
            var id = db.ExecuteScalar<Guid>(sql, newSpy);
            var oldId = newSpy.Id;
            newSpy.Id = id;
            if (newSpy.Id != oldId) returnVal = true;
            
            /* adonet version             
           var result = cmd.ExecuteReader();
            if (result.Read()){
                newSpy = GetSpy((Guid) result["Id"]);
                returnVal = true;
            }
            */
            return returnVal;
        }


        internal IEnumerable<Spy> GetBySkills(string spySkill)
        {
            SpySkills skillEnum;
            if (Enum.TryParse(spySkill, out skillEnum))
            {
                var db = new SqlConnection(_connectionString);
                var sql = @"select SP.* from SpySkillRelationship SKR
                              Join Spy SP
                                on SP.Id = SKR.SpyId
                              Join SpySkills SK
                                on SK.Id = SKR.SkillId
                              Where SK.Description = @skill";
                var result = db.Query<Spy>(sql, new { skill = spySkill });
                foreach (var spy in result)
                {
                    AddLists(spy);
                    UpdateSpyLists(spy);
                }
                return result;
            }
            else return Enumerable.Empty<Spy>();
        }

        // Get spies associated with a handler
        internal bool GetByHandler(Guid handlerGuid, StringBuilder returnStr)
        {
            // get the full handler object
            var handlerObj = _handlers.GetHandlerById(handlerGuid);

            // test for valid handler Guid
            if (handlerObj != null)
            {
                // get spies associated with the handler, if any
                var db = new SqlConnection(_connectionString);
                var sql = @"Select S.Name from HandlerSpyRelationship HSR
                                Join Handler H
                                    on H.Id = HSR.HandlerId
                                Join Spy S
                                    on S.Id = HSR.SpyId
                                Where H.Id = @handlerGuid";
                var agencySpies = db.Query<String>(sql, new { handlerGuid });

                if (agencySpies.Count() > 0)
                {
                    // return message
                    returnStr.Append($"Spies for {handlerObj.Name}:\n");

                    // copy list to return message
                    foreach (var spy in agencySpies)
                    {
                        returnStr.Append($"{spy}\n");
                    }
                }
                // no spies found
                else returnStr.Append($"{handlerObj.Name} has no spies currently.");
                return true;
            }
            else
            {
                // handler Id not found
                returnStr.Append($"Handler with id {handlerGuid} not found");
                return false;
            }
        }

        #region Mapping
        Spy MapFromReader(SqlDataReader reader)
        {
            var spy = new Spy();
            spy.Id = reader.GetGuid(0);
            spy.Name = reader["Name"].ToString();
            spy.Skills = new List<SpySkills>();
            spy.Services = new List<SpyServices>();
            spy.Friends = new List<Guid>();
            spy.Enemies = new List<Guid>();
            UpdateSkills(spy);
            UpdateServices(spy);
            UpdateFriends(spy);
            UpdateEnemies(spy);
            return spy;
        }

        internal void UpdateSpyLists(Spy spy)
        {
            UpdateSkills(spy);
            UpdateServices(spy);
            UpdateFriends(spy);
            UpdateEnemies(spy);
        }

        internal void AddLists(Spy spy)
        {
            if (spy.Skills == null) spy.Skills = new List<SpySkills>();
            if (spy.Services == null) spy.Services= new List<SpyServices>();
            if (spy.Friends == null) spy.Friends = new List<Guid>();
            if (spy.Enemies == null) spy.Enemies = new List<Guid>();
        }

        internal void UpdateSkills(Spy spy)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"select SS.Enum from Spy S
		                        Join SpySkillRelationship SR
			                        on S.Id = SR.SpyId
		                        Join SpySkills SS
			                        on SS.Id = SR.SkillId
                                where S.Id = @spyId";

            var result = db.Query<Int32>(sql, new { spyId = spy.Id });
            spy.Skills.Clear();
            if (result.Count() > 0)
            {
                spy.Skills.AddRange(result.Select(x => (SpySkills) x));
            }
        }
        internal void UpdateServices(Spy spy)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"select SS.Enum from Spy S
		                        Join SpyServicesRelationship SSR
			                        on S.Id = SSR.SpyId
		                        Join SpyServices SS
			                        on SS.Id = SSR.ServiceId
                                Where S.Id = @spyId";
            var result = db.Query<Int32>(sql, new { spyId = spy.Id });
            spy.Services.Clear();
            if (result.Count() > 0) {
                spy.Services.AddRange(result.Select(x => (SpyServices) x));
            }
        }

        internal void UpdateFriends(Spy spy)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"select SF.Id as [Friend] from Spy S
	                            Join SpyFriendRelationship SR
		                            on S.Id = SR.SpyId
	                            Join Spy SF 
		                            on SF.Id = SR.SpyFriendId
                                where S.Id = @spyId";
            var results = db.Query<Guid>(sql, new { spyId = spy.Id });
            spy.Friends.Clear();
            if ( results.Count() > 0)
            {
                spy.Friends.AddRange(results);
            }

            // Add as friend anybody who has friended this spy
            // -change from original model
            var sql2 = @"select SF.Id as [Friend] from Spy SF
	                            Join SpyFriendRelationship SR
		                            on SF.Id = SR.SpyId
	                            Join Spy S
		                            on S.Id = SR.SpyFriendId
                                where S.Id = @spyId";
            var results2 = db.Query<Guid>(sql2, new { spyId = spy.Id });
            if (results2.Count() > 0)
            {
                // Add the friend if they are not already in the list
                spy.Friends.AddRange(results2.Except(spy.Friends));
            }

        }
        internal void UpdateEnemies(Spy spy)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"Select SE.id as [Enemy] from Spy S
                            	Join SpyEnemiesRelationship SR
                                    on S.Id = SR.SpyId
	                            Join Spy SE
		                            on SE.Id = SR.SpyEnemyId
                                Where S.id = @spyId";
            var results = db.Query<Guid>(sql, new { spyId = spy.Id });
            spy.Enemies.Clear();
            if( results.Count() > 0)
            {
                spy.Enemies.AddRange(results);
            }
        }
        #endregion
    }
}
