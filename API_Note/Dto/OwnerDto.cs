using Microsoft.EntityFrameworkCore;

namespace API_Note.Dto
{
    [PrimaryKey("Login")]
    public class OwnerDto
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string? Password { get; set; }
    }
}
