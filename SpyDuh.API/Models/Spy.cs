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
        public List<string> Skills { get; set; }
        public List<string> Services { get; set; }
        public List<Guid> Friends { get; set; }
        public List<Guid> Enemies { get; set; }
        public List<Guid> Handlers { get; set; }
        // public List<Guid> Assignments { get; set; }
    }
}
