namespace Prisma.Models
{
    public class AnalysisResult
    {
        public string ArticleId { get; set; }
        public string Category { get; set; }
        public string PromptUsed { get; set; }
        public DateTime AnalysisDate { get; set; }
        public string RawAnalysisContent { get; set; }

        // Sezioni strutturate dell'analisi (verranno compilate manualmente in questa versione)
        public List<BiasDetection> DetectedBiases { get; set; } = new();
        public List<RhetoricalTechnique> RhetoricalTechniques { get; set; } = new();
        public List<string> KeyTakeaways { get; set; } = new();
        public string ToneSummary { get; set; }
        public int ObjectivityScore { get; set; } // 0-100
        public string AlternativePerspective { get; set; }
        public Dictionary<string, int> EmotionalToneAnalysis { get; set; } = new();
        public List<string> MissingContext { get; set; } = new();
    }

    public class BiasDetection
    {
        public string BiasType { get; set; }
        public string Description { get; set; }
        public List<string> Examples { get; set; } = new();
        public string Impact { get; set; }
        public string Severity { get; set; } // Low, Medium, High
    }

    public class RhetoricalTechnique
    {
        public string Technique { get; set; }
        public string Description { get; set; }
        public List<string> Examples { get; set; } = new();
        public string Purpose { get; set; }
        public string Effect { get; set; }
    }

    public class AnalysisMetadata
    {
        public string Category { get; set; }
        public string IconClass { get; set; }
        public string ThemeColor { get; set; }
        public string Description { get; set; }
        public List<string> KeyQuestions { get; set; } = new();
        public List<BiasType> CommonBiases { get; set; } = new();

        public static Dictionary<string, AnalysisMetadata> CategoryMetadata = new()
        {
            ["politics"] = new AnalysisMetadata
            {
                Category = "Politics",
                IconClass = "bi-award",
                ThemeColor = "#dc3545",
                Description = "Analisi del discorso politico e delle sue implicazioni",
                KeyQuestions = new List<string>
                {
                    "Quali ideologie politiche sono implicitamente favorite?",
                    "Come vengono rappresentate le diverse fazioni politiche?",
                    "Quale linguaggio emotivo o polarizzante viene utilizzato?"
                },
                CommonBiases = new List<BiasType>
                {
                    new BiasType { Name = "Bias ideologico", Description = "Favorisce implicitamente una visione politica" },
                    new BiasType { Name = "Framing partitico", Description = "Inquadra gli eventi in modo favorevole a un partito" },
                    new BiasType { Name = "Linguaggio conflittuale", Description = "Usa termini che accentuano il conflitto politico" }
                }
            },
            // Aggiungi altre categorie qui
        };
    }

    public class BiasType
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}