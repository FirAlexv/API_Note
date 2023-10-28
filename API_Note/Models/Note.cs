namespace API_Note.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }


        public int OwnerId { get; set; }

        public Owner Owner { get; set; }
    }
}
