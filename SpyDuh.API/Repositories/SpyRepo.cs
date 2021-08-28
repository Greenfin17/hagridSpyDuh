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
