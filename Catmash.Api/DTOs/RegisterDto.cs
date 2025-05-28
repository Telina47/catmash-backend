namespace Catmash.Api.DTOs
{
    public class RegisterDto
    {
        public string Pseudo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

}
