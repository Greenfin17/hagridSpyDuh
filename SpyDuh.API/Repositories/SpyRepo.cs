using SpyDuh.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;

namespace SpyDuh.API.Repositories
{
    public class SpyRepo
    {
      static List<Spy> _spies = new List<Spy>
        {

            new Spy
            {
                Name = "James Bond",
                Id = new Guid("6b1d5e06-ff21-45d8-a66c-45788bbfd387"),
                Skills = new List<SpySkills> {SpySkills.Alcoholic, SpySkills.Charisma, SpySkills.Seduction, SpySkills.DefensiveDriving},
                Services = new List<SpyServices> {SpyServices.SaveTheWorld, SpyServices.IntelligenceGathering},
                // Friends with Pierre and Vadim
                Friends = new List<Guid> {new Guid("8120e025-e534-4ff7-8722-e09bb478281f"), new Guid("1359b3a6-dc47-4a9b-b50f-10ad8739653b")},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {new Guid("626c99be-a979-4d56-ba8b-3353e4165145")}
            },
             new Spy
            {
                Name = "Pierre LaKlutz",
                Id = new Guid("8120e025-e534-4ff7-8722-e09bb478281f"),
                Skills = new List<SpySkills> {SpySkills.Dancing, SpySkills.Disguises, SpySkills.Fencing,SpySkills.MicrosoftExcel},
                Services = new List<SpyServices> {SpyServices.Dossier, SpyServices.Theft},
                // Friends with Vadim and Whittaker
                Friends = new List<Guid> {new Guid("57351a82-65f8-4518-a49a-c62a87134af3"), new Guid("d296f36f-bd32-427f-bc07-a111e5c055e6")},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {new Guid("626c99be-a979-4d56-ba8b-3353e4165145")}
            },
             new Spy
            {
                Name = "Vadim Kirpichenko",
                Id = new Guid("1359b3a6-dc47-4a9b-b50f-10ad8739653b"),
                Skills = new List<SpySkills> {SpySkills.Alcoholic, SpySkills.DefensiveDriving, SpySkills.Hacker, SpySkills.Interrogation},
                Services = new List<SpyServices> {SpyServices.Framing, SpyServices.IntelligenceGathering},
                // Friends with Jona
                Friends = new List<Guid> {new Guid("626c99be-a979-4d56-ba8b-3353e4165145")},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {new Guid("3732f2d5-3291-4494-8470-f6e7f719efde")}
            },
             new Spy
            {
                Name = "Whittaker Chambers",
                Id = new Guid("57351a82-65f8-4518-a49a-c62a87134af3"),
                Skills = new List<SpySkills> {SpySkills.Languages, SpySkills.DefensiveDriving, SpySkills.Forgery, SpySkills.Interrogation},
                Services = new List<SpyServices> {SpyServices.Framing, SpyServices.IntelligenceGathering},
                Friends = new List<Guid> {},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {new Guid("ee8467a1-971a-4b4a-8af1-cd2ae5a7f197")}
            },
             new Spy
            {
                Name = "Jona von Ustinov",
                Id = new Guid("d296f36f-bd32-427f-bc07-a111e5c055e6"),
                Skills = new List<SpySkills> {SpySkills.Languages, SpySkills.Blackjack, SpySkills.Disguises, SpySkills.Interrogation},
                Services = new List<SpyServices> {SpyServices.IntelligenceGathering},
                Friends = new List<Guid> {},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {new Guid("ee8467a1-971a-4b4a-8af1-cd2ae5a7f197")}
            }
        }; 
        HandlerRepo _handlers = new HandlerRepo();

        const string _connectionString = "Server = localhost; Database = SpyDuh; Trusted_Connection = True";
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

        internal bool AddSkill(Guid spyGuid, string spySkill)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"Select * from SpySkills
                                Where Description = @spySkill";
            cmd.Parameters.AddWithValue("spySkill", spySkill);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            return false;
        }

        internal IEnumerable<Spy> ListFriends(Guid spyGuid)
        {
            var spyObj = _spies.FirstOrDefault(spy => spy.Id == spyGuid);
            var friendList = new List<Spy>();
            if (spyObj != null && spyObj.Friends.Count > 0)
            {
                // loop through the list of friend Id's and retrieve the full friend objects
                // and add them to a list.
                foreach (var friendGuid in spyObj.Friends)
                {
                    var friendObj = _spies.FirstOrDefault(spy => spy.Id == friendGuid);
                    if (friendObj != null)
                    {
                        friendList.Add(friendObj);
                    }
                }
            }
            return friendList;
        }

        internal IEnumerable<Spy> ListEnemies(Guid spyGuid)
        {
            var enemiesList = new List<Spy>();
            return enemiesList;
        }

        internal String ListSkillsAndServices(Guid spyGuid)
        {
            var spyObj = _spies.FirstOrDefault(spy => spy.Id == spyGuid);
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
            var spyObj = _spies.FirstOrDefault(spy => spy.Id == spyGuid);
            var friendList = new List<Spy>();
            if (spyObj != null && spyObj.Friends.Count > 0)
            {
                // loop through the list of friend Id's 
                foreach (var friendGuid in spyObj.Friends)
                {
                    var tempList = new List<Spy>();
                    var friendObj = _spies.FirstOrDefault(spy => spy.Id == friendGuid);
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
        internal void Add(Spy newSpy)
        {
            newSpy.Id = Guid.NewGuid();
            newSpy.Friends.Clear();
            newSpy.Enemies.Clear();
            newSpy.Handlers.Clear();

            _spies.Add(newSpy);
        }

        internal IEnumerable<Spy> GetBySkills(string skill)
        {
            SpySkills skillEnum;
            if (Enum.TryParse(skill, out skillEnum))
            {
                return _spies.Where(spy => spy.Skills.Contains(skillEnum));
            }
            else return Enumerable.Empty<Spy>();
        }

        // Get spies associated with a handler
        internal bool GetByHandler(Guid handlerGuid, StringBuilder returnStr)
        {
            // get the full handler object
            var handlerObj = _handlers.GetHandler(handlerGuid);

            // test for valid handler Guid
            if (handlerObj != null)
            {
                // get spies associated with the handler, if any
                var agencySpies = _spies.Where(spy => spy.Handlers.Contains(handlerGuid));
                if (agencySpies.Count() > 0)
                {
                    // temporary list to store spy names
                    var agencySpiesByName = new List<string>();
                    // return message
                    returnStr.Append($"Spies for {handlerObj.Name}:\n");
                    // add names of spies to list
                    agencySpiesByName.AddRange(from spy in agencySpies
                                               select spy.Name);
                    // copy list to return message
                    agencySpiesByName.ForEach(spy => returnStr.Append($"{spy}\n"));
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
       Spy MapFromReader(SqlDataReader reader)
        {
            var spy = new Spy();
            spy.Id = reader.GetGuid(0);
            spy.Name = reader["Name"].ToString();
            spy.Skills = new List<SpySkills>();
            spy.Services = new List<SpyServices>();
            UpdateSkills(spy);
            UpdateServices(spy);
            return spy;
        }

        internal void UpdateSkills(Spy spy)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"select SS.Description, SS.Enum from Spy S
		                        Join SpySkillRelationship SR
			                    on S.Id = SR.SpyId
		                        Join SpySkills SS
			                    on SS.Id = SR.SkillId
                                where S.Id = @spyId";
            cmd.Parameters.AddWithValue("spyId", spy.Id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                spy.Skills.Add((SpySkills)reader["Enum"]);
            }
        }
        internal void UpdateServices(Spy spy)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"select SS.Description, SS.Enum from Spy S
		                        Join SpyServicesRelationship SSR
			                    on S.Id = SSR.SpyId
		                        Join SpyServices SS
			                    on SS.Id = SSR.ServiceId
                                where S.Id = @spyId";
            cmd.Parameters.AddWithValue("spyId", spy.Id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                spy.Services.Add((SpyServices)reader["Enum"]);
            }
        }

    }
}
