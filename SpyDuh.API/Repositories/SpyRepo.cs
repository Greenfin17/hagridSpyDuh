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
            }
        };
        internal IEnumerable<Spy> GetAll()
        {
            return _spies;
        }

        internal void Add(Spy newSpy)
        {
            newSpy.Id = Guid.NewGuid();

            _spies.Add(newSpy);
        }
    }
}
