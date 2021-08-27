using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.API.Models;
using SpyDuh.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

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

        [HttpPatch("{spyGuid}/AddFriend/{friendGuid}")]
        public IActionResult AddFriend(Guid spyGuid, Guid friendGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            var friendObj = _repo.GetSpy(friendGuid);
            StringBuilder returnStr = new StringBuilder("");
            if (spyObj != null && friendObj != null)
            {
                if (!spyObj.Friends.Contains(friendGuid))
                {
                    spyObj.Friends.Add(friendGuid);
                    return Ok($"Friend with Id {friendGuid} added.\n");
                }
                else return BadRequest($"Friend with Id: {friendGuid} already in friend list\n");
            }
            if (spyObj == null) returnStr.Append($"Spy with id: {spyGuid} not found\n");
            if (friendObj == null) returnStr.Append($"Friend with id: {spyGuid} not found\n");

            return NotFound(returnStr.ToString());
        }

        [HttpGet("{spyGuid}/ListFriends")]
        public IActionResult ListFriends(Guid spyGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            if (spyObj != null)
            {
                var response = _repo.ListFriends(spyGuid);
                if (response.Count() == 0)
                {
                    // response is valid but empty
                    return Ok("No friends (:\n");
                }
                // response is valid
                else return Ok(response);
            }
            // spy not found
            else return NotFound($"Spy with id: {spyGuid} not found");

        }

        [HttpPatch("{spyGuid}/AddEnemy/{enemyGuid}")]
        public IActionResult AddEnemy(Guid spyGuid, Guid enemyGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            var enemyObj = _repo.GetSpy(enemyGuid);
            StringBuilder returnStr = new StringBuilder("");
            if (spyObj != null && enemyObj != null)
            {
                if (!spyObj.Enemies.Contains(enemyGuid))
                {
                    spyObj.Enemies.Add(enemyGuid);
                    return Ok($"Enemy with ID {enemyGuid} added.\n");
                }
                else return BadRequest($"Enemy with ID {enemyGuid} already in enemy list\n");
            }
            if (spyObj == null) returnStr.Append($"Spy with ID {spyGuid} not found\n");
            if (enemyObj == null) returnStr.Append($"Friend with ID {spyGuid} not found\n");

            return NotFound(returnStr.ToString());
        }

        [HttpGet("{spyGuid}/ListEnemies")]
        public IActionResult ListEnemies(Guid spyGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            if (spyObj != null)
            {
                var response = _repo.ListEnemies(spyGuid);
                if (response.Count() == 0)
                {
                    return Ok("No enemies!\n");
                }
                else return Ok(response);
            }
            // spy not found
            else return NotFound($"Spy with ID {spyGuid} not found");

        }

        [HttpPost("new-spy")]
         public IActionResult AddSpy(Spy newSpy)
        {
            if (string.IsNullOrEmpty(newSpy.Name))
            {
                return BadRequest("Name needed");
            }

            _repo.Add(newSpy);

            return Created("/api/spies/1", newSpy);
        }

        [HttpGet("skills/{skill}")]
        public IEnumerable<Spy> GetSpiesBySkill(string skill)
        {
            return _repo.GetBySkills(skill);
        }

    }
}
