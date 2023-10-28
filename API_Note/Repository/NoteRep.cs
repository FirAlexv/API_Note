using API_Note.Data;
using API_Note.Imterface;
using API_Note.Models;

namespace API_Note.Repository
{
    public class NoteRep : INoteRep
    {
        private readonly DataContext _context;

        public NoteRep(DataContext context)
        {
            _context = context;
        }

        public bool CreateNote(int ownerId, Note newNote)
        {
            newNote.OwnerId = ownerId;
            _context.Add(newNote);

            return Save();
        }

        public bool DeleteNote(Note note)
        {
            _context.Remove(note);

            return Save();
        }

        public Note GetNote(int id)
        {
            return _context.Note.Where(p => p.Id == id).FirstOrDefault();
        }

        public Note GetNote(string text)
        {
            return _context.Note.Where(p => p.Text == text).FirstOrDefault();
        }

        public ICollection<Note> GetNotes()
        {
            return _context.Note.OrderBy(p => p.Id).ToList();
        }

        public bool NoteExist(int id)
        {
            return _context.Note.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateNote(int ownerId, Note note)
        {
            note.OwnerId = ownerId;
            _context.Update(note);

            return Save();
        }
    }
}
