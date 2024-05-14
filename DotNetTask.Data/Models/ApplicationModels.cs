namespace DotNetTask.Data.Models
{
    public class ApplicationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IDNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public List<AnswerRequest> Responses { get; set; }
    }

    public class AnswerRequest
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public string? SingleResponse { get; set; }
        public List<string>? MultipleResponse { get; set; }
    }
}
