using SpyDuh.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                Handlers = new List<Guid> {}
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
                Handlers = new List<Guid> {}
            },
             new Spy
            {
                Name = "Vadim Kirpichenko",
                Id = new Guid("1359b3a6-dc47-4a9b-b50f-10ad8739653b"),
                Skills = new List<SpySkills> {SpySkills.Alcoholic, SpySkills.DefensiveDriving, SpySkills.Hacker, SpySkills.Interrogation},
                Services = new List<SpyServices> {SpyServices.Framing, SpyServices.IntelligenceGathering},
                // Friends with Jona
                Friends = new List<Guid> {new Guid("d296f36f-bd32-427f-bc07-a111e5c055e6")},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {}
            },
             new Spy
            {
                Name = "Whittaker Chambers",
                Id = new Guid("57351a82-65f8-4518-a49a-c62a87134af3"),
                Skills = new List<SpySkills> {SpySkills.Languages, SpySkills.DefensiveDriving, SpySkills.Forgery, SpySkills.Interrogation},
                Services = new List<SpyServices> {SpyServices.Framing, SpyServices.IntelligenceGathering},
                Friends = new List<Guid> {},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {}
            },
             new Spy
            {
                Name = "Jona von Ustinov",
                Id = new Guid("d296f36f-bd32-427f-bc07-a111e5c055e6"),
                Skills = new List<SpySkills> {SpySkills.Languages, SpySkills.Blackjack, SpySkills.Disguises, SpySkills.Interrogation},
                Services = new List<SpyServices> {SpyServices.IntelligenceGathering},
                Friends = new List<Guid> {},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {}
            }
        };

        internal IEnumerable<Spy> GetAll()
        {
            return _spies;
        }

        internal Spy GetSpy(Guid spyGuid)
        {
            return _spies.FirstOrDefault(spy => spy.Id == spyGuid);
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
            var spyObj = _spies.FirstOrDefault(spy => spy.Id == spyGuid);
            var enemiesList = new List<Spy>();
            if (spyObj != null && spyObj.Enemies.Count > 0)
            {
                foreach (var enemyGuid in spyObj.Enemies)
                {
                    var enemyObj = _spies.FirstOrDefault(spy => spy.Id == enemyGuid);
                    if (enemyObj != null)
                    {
                        enemiesList.Add(enemyObj);
                    }
                }
            }
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
                        // add the friend's friends to the list, excepting the original spy or a duplicate.
                        friendList.AddRange(from friend in tempList
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

    }
}
