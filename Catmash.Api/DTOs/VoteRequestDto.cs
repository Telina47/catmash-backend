namespace Catmash.Api.DTOs
{
    public class VoteRequestDto
    {
        public string WinnerCatId { get; set; } = null!;
        public string LoserCatId { get; set; } = null!;
    }
}
