using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;
using LoafAndStranger.DataAccess;

namespace LoafAndStranger.Controller
{
    [Route("api/strangers")]
    [ApiController]
    public class StrangersController : ControllerBase
    {
        StrangersRepository _repo;

        public StrangersController(StrangersRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllStrangers()
        {
            var strangers = _repo.GetAll();
            return Ok(strangers);
        }

        
    }
}
