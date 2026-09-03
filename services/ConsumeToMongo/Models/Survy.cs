using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeToMongo.Models
{
    public class Survy
    {
        public int ResponseId { get; set; }
        public string Age { get; set; } = string.Empty;
        public int? YearsCode { get; set; }
        public string? DevType { get; set; } = string.Empty;
        public string? LearnCodeChoose { get; set; } = string.Empty;
        public IEnumerable<string>? LearningMethods { get; set; }
        public string LearnCodeAI { get; set; } = string.Empty;
        public IEnumerable<string> AiLearningMethods { get; set; } = new List<string>();
        public string? AiUsage { get; set; }
        public string? AiTrust { get; set; }
        public string? AiSentiment { get; set; }
        public string ExperienceLevel { get; set; } = string.Empty;
        public bool UsesDocumentation { get; set; }
        public bool UsesAIForLearning { get; set; }
        public bool UsesStackOverflow { get; set; }

    }
}
