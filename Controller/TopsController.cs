using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.DataAccess;
using LoafAndStranger.Models;

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

        [HttpPost]
        public IActionResult AddTop(AddTopCommand command)
        {
            var newTop = _repo.Add(command.NumberOfSeats);
            return Created($"/api/tops/{newTop.id}", newTop);

        }

        [HttpGet]
        public IActionResult GetAllTops()
        {
            return Ok(_repo.GetAll());
        }
    }
}
