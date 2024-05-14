using Newtonsoft.Json;

namespace DotNetTask.Data.Entities
{
    public class ProgramData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgrammeDescription { get; set; }
        public List<Question> Questions { get; set; }
    }
}
