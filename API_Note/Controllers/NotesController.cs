using System.Xml.Linq;
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
    public class NotesController : Controller
    {
        private readonly INoteRep _noteRep;
        private readonly IOwnerRep _ownerRep;
        private readonly IMapper _mapper;

        public NotesController(INoteRep noteRep,IOwnerRep ownerRep, IMapper mapper)
        {
            _noteRep = noteRep;
            _ownerRep = ownerRep;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Note>))]
        public IActionResult GetNoted()
        {
            var notes = _mapper.Map<List<Note>>(_noteRep.GetNotes());

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(notes);
        }

        [HttpGet("Id")]
        [ProducesResponseType(200, Type = typeof(Note))]
        [ProducesResponseType(400)]
        public IActionResult GetNote(int id) 
        {
            if (!_noteRep.NoteExist(id))
            {
                return NotFound();
            }

            var note = _mapper.Map<NoteDto>(_noteRep.GetNote(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(note);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNote([FromQuery] int ownerId, [FromBody] NoteDto newNote)
        {
            if(newNote == null)
            {
                return BadRequest(ModelState);
            }

            //todo async
            var notes = _noteRep.GetNotes().Where(c => c.Id == newNote.Id).FirstOrDefault();

            if (notes != null)
            {
                ModelState.AddModelError("", "ID already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var noteMap = _mapper.Map<Note>(newNote);

            if (!_noteRep.CreateNote(ownerId,noteMap))
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
        public IActionResult UpdateNote([FromQuery] int ownerId, int idNote, [FromBody] NoteDto updateNote)
        {
            if (updateNote == null)
            {
                return BadRequest(ModelState);
            }

            if(idNote!= updateNote.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_noteRep.NoteExist(idNote))
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newNoteMap = _mapper.Map<Note>(updateNote);

            if (!_noteRep.UpdateNote(ownerId, newNoteMap))
            {
                ModelState.AddModelError("", "Somthing went wrong with updating!");
                return StatusCode(500, ModelState);
            }

            return Ok("Success!");
        }

        [HttpDelete("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeliteNote(int noteId)
        {
            if (!_noteRep.NoteExist(noteId))
            {
                return NotFound();
            }           

            var delNote = _noteRep.GetNote(noteId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_noteRep.DeleteNote(delNote))
            {
                ModelState.AddModelError("", "Somthing went wrong with deliting!");
                return StatusCode(500, ModelState);
            }

            return Ok("Success!");
        }
    }    
}
