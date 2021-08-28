using SpyDuh.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.API.Repositories
{
    public class SpyRepo
    {
        static List<Spy> _spies = new List<Spy>
        {
            new Spy
            {
                Name = "James Bond",
                Id = Guid.NewGuid(),
                Skills = new List<SpySkills> {SpySkills.Alcoholic, SpySkills.Charisma, SpySkills.Seduction, SpySkills.DefensiveDriving},
                Services = new List<SpyServices> {SpyServices.SaveTheWorld, SpyServices.IntelligenceGathering},
                Friends = new List<Guid> {},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {}
            },
             new Spy
            {
                Name = "Pierre LaKlutz",
                Id = Guid.NewGuid(),
                Skills = new List<SpySkills> {SpySkills.Dancing, SpySkills.Disguises, SpySkills.Fencing,SpySkills.MicrosoftExcel},
                Services = new List<SpyServices> {SpyServices.Dossier, SpyServices.Theft},
                Friends = new List<Guid> {},
                Enemies = new List<Guid> {},
                Handlers = new List<Guid> {}
            },
             new Spy
            {
                Name = "Vadim Kirpichenko",
                Id = Guid.NewGuid(),
                Skills = new List<SpySkills> {SpySkills.Alcoholic, SpySkills.DefensiveDriving, SpySkills.Hacker, SpySkills.Interrogation},
                Services = new List<SpyServices> {SpyServices.Framing, SpyServices.IntelligenceGathering},
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

        internal Boolean ListSkillsAndServices(Guid spyGuid, List<SpySkills> spySkills,
                                                                    List<SpyServices> spyServices)
        {
            var spyObj = _spies.FirstOrDefault(spy => spy.Id == spyGuid);
            
            if (spyObj != null)
            {
                foreach ( var skill in spyObj.Skills)
                {
                    spySkills.Add(skill);
                }

                foreach (var service in spyObj.Services)
                {
                    spyServices.Add(service);
                }

                return true;
            }
            else
            {
                return false;
            }
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
