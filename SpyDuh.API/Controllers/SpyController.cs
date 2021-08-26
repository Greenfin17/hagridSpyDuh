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

        [HttpPost]
         public IActionResult AddSpy(Spy newSpy)
        {
            if (string.IsNullOrEmpty(newSpy.Name))
            {
                return BadRequest("Name needed");
            }

            _repo.Add(newSpy);

            return Created("/api/spies/1", newSpy);
        }
    }
}
