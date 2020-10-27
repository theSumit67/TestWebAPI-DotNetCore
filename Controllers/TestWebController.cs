using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebAPI.Models;
using TestWebAPI.Models.DTOs;
using TestWebAPI.Repository.IRepository;

namespace TestWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NationalParksController : Controller
    {
        private INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {

            var objList = _npRepo.GetNationalParks();
            // never expose domain model; convert to DTOs
            var objDTO = new List<NationalParkDTO>();
            foreach (var obj in objList)
            {
                objDTO.Add(_mapper.Map<NationalParkDTO>(obj));
            }

            return Ok(objDTO);
        }

        // need routeParam template to eliminate ambiguous routes
        [HttpGet("{parkId:int}")] // dont add space in value
        public IActionResult GetNationalPark(int parkId)
        {
            var obj = _npRepo.GetNationalPark(parkId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<NationalParkDTO>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDTO parkDto)
        {
            if (parkDto == null)
            {
                // model state contans error encountered
                return BadRequest(ModelState);
            }

            if (_npRepo.NationalParkExists(parkDto.Name))
            {
                ModelState.AddModelError("", "Duplicate Name / Already exist");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parkObj = _mapper.Map<NationalPark>(parkDto);
            if (!_npRepo.CreateNationalPark(parkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving     {parkObj.Name }");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}