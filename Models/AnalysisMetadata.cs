namespace PrismaNews.Models
{
    public class AnalysisMetadata
    {
        public string Category { get; set; }
        public string IconClass { get; set; }
        public string ThemeColor { get; set; }
        public string Description { get; set; }
        public List<string> KeyQuestions { get; set; }
        public List<BiasType> CommonBiases { get; set; }

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
            ["environment"] = new AnalysisMetadata
            {
                Category = "Environment",
                IconClass = "bi-tree",
                ThemeColor = "#198754",
                Description = "Impatto ambientale e sostenibilità in primo piano",
                KeyQuestions = new List<string>
                {
                    "L'articolo utilizza toni allarmistici o equilibrati?",
                    "Sono presenti dati scientifici a supporto delle affermazioni?",
                    "Vengono presentate diverse prospettive sul tema?"
                },
                CommonBiases = new List<BiasType>
                {
                    new BiasType { Name = "Catastrofismo", Description = "Enfatizza scenari apocalittici senza contesto" },
                    new BiasType { Name = "Cherry-picking", Description = "Seleziona solo dati che supportano una tesi" },
                    new BiasType { Name = "Falsa equivalenza", Description = "Dà uguale peso a posizioni scientifiche e non" }
                }
            },
            ["society"] = new AnalysisMetadata
            {
                Category = "Society",
                IconClass = "bi-people",
                ThemeColor = "#20c997",
                Description = "Tendenze sociali e culturali",
                KeyQuestions = new List<string>
                {
                    "Sono presenti stereotipi o generalizzazioni?",
                    "Il tono è moralistico o descrittivo?",
                    "Si presenta un fenomeno sociale in modo polarizzante?"
                },
                CommonBiases = new List<BiasType>
                {
                    new BiasType { Name = "Stereotipizzazione", Description = "Generalizza caratteristiche su gruppi sociali" },
                    new BiasType { Name = "Moralismo", Description = "Impone giudizi morali impliciti" },
                    new BiasType { Name = "Polarizzazione", Description = "Presenta questioni complesse come binarie" }
                }
            },
            ["business"] = new AnalysisMetadata
            {
                Category = "Business",
                IconClass = "bi-cash-coin",
                ThemeColor = "#fd7e14",
                Description = "Dinamiche economiche e finanziarie",
                KeyQuestions = new List<string>
                {
                    "I fenomeni economici sono semplificati eccessivamente?",
                    "C'è una selezione tendenziosa dei dati economici?",
                    "Si presentano correlazioni come causazioni?"
                },
                CommonBiases = new List<BiasType>
                {
                    new BiasType { Name = "Ipersemplificazione", Description = "Riduce fenomeni complessi a singoli fattori" },
                    new BiasType { Name = "Bias di conferma", Description = "Seleziona solo dati che confermano una narrativa" },
                    new BiasType { Name = "Falsa causazione", Description = "Confonde correlazione con relazione causa-effetto" }
                }
            },
            ["technology"] = new AnalysisMetadata
            {
                Category = "Technology",
                IconClass = "bi-cpu",
                ThemeColor = "#6610f2",
                Description = "Innovazioni tecnologiche e loro impatti",
                KeyQuestions = new List<string>
                {
                    "La tecnologia è presentata in modo tecno-ottimista o tecno-pessimista?",
                    "Vengono considerate le implicazioni etiche e sociali?",
                    "Si valutano rischi e benefici in modo equilibrato?"
                },
                CommonBiases = new List<BiasType>
                {
                    new BiasType { Name = "Determinismo tecnologico", Description = "Presenta la tecnologia come inevitabile" },
                    new BiasType { Name = "Bias di novità", Description = "Sopravvaluta l'impatto di nuove tecnologie" },
                    new BiasType { Name = "Tecno-utopismo/distopismo", Description = "Estremizza visioni utopiche o distopiche" }
                }
            },
            ["world"] = new AnalysisMetadata
            {
                Category = "World",
                IconClass = "bi-globe",
                ThemeColor = "#0d6efd",
                Description = "Eventi globali e analisi geopolitica",
                KeyQuestions = new List<string>
                {
                    "Si considera il contesto storico e culturale locale?",
                    "L'articolo mostra bias occidentali o etnocentrici?",
                    "Si presentano stereotipi regionali o nazionali?"
                },
                CommonBiases = new List<BiasType>
                {
                    new BiasType { Name = "Etnocentrismo", Description = "Valuta culture diverse secondo standard occidentali" },
                    new BiasType { Name = "Orientalismo", Description = "Rappresenta culture non-occidentali come esotiche o arretrate" },
                    new BiasType { Name = "Bias geopolitico", Description = "Favorisce interessi geopolitici specifici" }
                }
            },
            // Aggiungi altre categorie se necessario
        };
    }

    public class BiasType
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}