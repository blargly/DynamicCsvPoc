namespace CSVUploader.Models
{
    public class SurveyInputModel
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public Dictionary<string, string> Questions { get; set; } = new Dictionary<string, string>();
    }
}
