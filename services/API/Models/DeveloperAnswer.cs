using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class DeveloperAnswer
    {
        //[BsonElement("responseId")]
        public int ResponseId { get; set; }

        //[BsonElement("age")]
        public string Age { get; set; } = string.Empty;

        //[BsonElement("yearsCode")]
        public int? YearsCode { get; set; }

        //[BsonElement("devType")]
        public string? DevType { get; set; } = string.Empty;

        //[BsonElement("learnCodeChoose")]
        public string? LearnCodeChoose { get; set; } = string.Empty;

        [BsonElement("learningMethods")]
        public IEnumerable<string>? LearningMethods { get; set; }

        [BsonElement("learnCodeAI")]
        public string LearnCodeAI { get; set; } = string.Empty;

        [BsonElement("aiLearningMethods")]
        public IEnumerable<string> AiLearningMethods { get; set; } = new List<string>();

        [BsonElement("aiUsage")]
        public string? AiUsage { get; set; }

        [BsonElement("aiTrust")]
        public string? AiTrust { get; set; }

        [BsonElement("aiSentiment")]
        public string? AiSentiment { get; set; }

        [BsonElement("experienceLevel")]
        public string ExperienceLevel { get; set; } = string.Empty;

        //[BsonElement("usesDocumentation")]
        public bool UsesDocumentation { get; set; }

        //[BsonElement("usesAIForLearning")]
        public bool UsesAIForLearning { get; set; }

        //[BsonElement("usesStackOverflow")]
        public bool UsesStackOverflow { get; set; }
    }
}
