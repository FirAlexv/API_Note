using API_Note.Models;

namespace API_Note.Imterface
{
    public interface INoteRep
    {
        ICollection<Note> GetNotes();

        Note GetNote(int id);

        /// <summary>
        /// не уверен!
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Note GetNote(string text);

        bool NoteExist(int id);

        bool CreateNote(int ownerId, Note note);

        bool UpdateNote(int ownerId, Note note);

        bool DeleteNote(Note note);

        bool Save();
    }
}
