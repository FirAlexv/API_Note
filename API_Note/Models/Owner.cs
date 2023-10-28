namespace API_Note.Models
{
    public class Owner
    {
        public int Id{ get; set; }

        public string Login { get; set; }

        public string? Password { get; set; }


        public ICollection<Note> Note { get; }
    }
}
