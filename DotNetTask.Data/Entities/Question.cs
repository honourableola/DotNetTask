using DotNetTask.Data.Enums;
using Newtonsoft.Json;

namespace DotNetTask.Data.Entities
{
    public class Question
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public List<string> Options { get; set; }
        public bool IncludeOtherOption { get; set; }
        public int? MaxOptions { get; set; }
    }
}
