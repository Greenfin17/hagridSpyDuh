using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.API.Models;
using SpyDuh.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.API.Controllers
{
    [Route("api/Spy")]
    [ApiController]
    public class SpyController : ControllerBase
    {
        SpyRepo _repo;

        public SpyController()
        {
            _repo = new SpyRepo();
        }

        [HttpGet]
        public IActionResult GetAllSpies()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("skills/{skill}")]
        public IEnumerable<Spy> GetSpiesBySkill(SpySkills skills)
        {
            return _repo.GetBySkills(skills);
        }

    }
}
