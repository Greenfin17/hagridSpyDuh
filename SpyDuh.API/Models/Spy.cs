using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.API.Models
{
    public class Spy
    {
        
        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<SpySkills> Skills { get; set; }
        public List<SpyServices> Services { get; set; }
        public List<Guid> Friends { get; set; }
        public List<Guid> Enemies { get; set; }
        public List<Guid> Handlers { get; set; }
        // public List<Guid> Assignments { get; set; }
    }

    public enum SpyServices
    {
        Assasination,
        Theft,
        Interrogation, 
        SearchAndRescue,
        SaveTheWorld,
        Dossier,
        ThwartEvilPlans,
        Infiltration,
        CarryOutEvilPlans,
        HelpTheBadGuys,
        StealingFinancialRecords,
        Framing,
        IntelligenceGathering,
    }

    public enum SpySkills
    {
        None,
        Locksmith,
        Marksmanship,
        Pilot,
        RockClimbing,
        Combat,
        Interrogation,
        Hacker,
        Disguises,
        Dancing,
        Alcoholic,
        DefensiveDriving,
        Poker,
        Blackjack,
        CardCounting,
        Forgery,
        Fencing,
        Linguist,
        Languages,
        MicrosoftExcel,
        Whistling,
        SocialEngineering,
        Charisma,
        OneLiners,
        SnarkyComebacks,
        Seduction,
        ScubaDiving
    }
}
