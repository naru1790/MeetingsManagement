namespace Meetings.Dtos
{
    public class ResponseDto
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Response { get; set; }
    }
}
