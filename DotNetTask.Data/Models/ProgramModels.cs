namespace DotNetTask.Data.Models
{
    public class ProgramModels
    {
        public class CreateProgramRequest
        {
            public string ProgramTitle { get; set; }
            public string ProgrammeDescription { get; set; }
            public List<QuestionModel> Questions { get; set; }
        }

        public class QuestionModel
        {
            public string QuestionText { get; set; }
            public string Type { get; set; }
            public List<string>? Options { get; set; } // Only applicable for Dropdown and MultipleChoice types
            public bool IncludeOtherOption { get; set; } // Only applicable for Dropdown type
            public int? MaxOptions { get; set; } // Only applicable for MultipleChoice type
        }

        public class UpdateProgramRequest
        {
            public string ProgramTitle { get; set; }
            public string ProgrammeDescription { get; set; }
            public List<QuestionModel> Questions { get; set; }
        }

        public class ProgramResponse : BaseResponse
        {
            public string Id { get; set; }
            public string ProgramTitle { get; set; }
            public string ProgrammeDescription { get; set; }
            public List<QuestionResponse> Questions { get; set; }
        }

        public class QuestionResponse
        {
            public string Id { get; set; }
            public string QuestionText { get; set; }
            public string Type { get; set; }
            public List<string> Options { get; set; }
            public bool IncludeOtherOption { get; set; }
            public int? MaxOptions { get; set; }
        }
    }
}
