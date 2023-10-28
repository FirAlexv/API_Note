using API_Note.Dto;
using API_Note.Imterface;
using API_Note.Models;
using API_Note.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_Note.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : Controller
    {
        private readonly IOwnerRep _ownerRep;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerRep ownerRep, IMapper mapper)
        {
            _ownerRep = ownerRep;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwner()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRep.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owners);
        }

        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetNote(int id)
        {
            if (!_ownerRep.OwnerExists(id))
            {
                return NotFound();
            }

            var owner = _mapper.Map<OwnerDto>(_ownerRep.GetOwner(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromBody] OwnerDto newOwner)
        {
            if (newOwner == null)
            {
                return BadRequest(ModelState);
            }

            //todo async
            var owners = _ownerRep.GetOwners().Where(c => c.Id == newOwner.Id).FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "ID already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = _mapper.Map<Owner>(newOwner);

            if (!_ownerRep.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Somthing went wrong with saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Success!");
        }

        [HttpPut("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner([FromQuery] int ownerId, [FromBody] OwnerDto updateOwner)
        {
            if (updateOwner == null)
            {
                return BadRequest(ModelState);
            }

            if (ownerId != updateOwner.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_ownerRep.OwnerExists(ownerId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newOwnerMap = _mapper.Map<Owner>(updateOwner);

            if (!_ownerRep.UpdateOwner( newOwnerMap))
            {
                ModelState.AddModelError("", "Somthing went wrong with updating!");
                return StatusCode(500, ModelState);
            }

            return Ok("Success!");
        }        
    }
}
