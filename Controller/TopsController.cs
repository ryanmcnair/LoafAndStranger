using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.DataAccess;

namespace LoafAndStranger.Controller
{
    [Route("api/Tops")]
    [ApiController]
    public class TopsController : ControllerBase
    {
        TopsRepository _repo;

        public TopsController()
        {
            _repo = new TopsRepository();
        }

        [HttpGet]
        public IActionResult GetAllTops()
        {
            return Ok(_repo.GetAll());
        }
    }
}
