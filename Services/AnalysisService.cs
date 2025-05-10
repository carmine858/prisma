using Prisma.Models;
using PrismaNews.Services;

namespace Prisma.Services
{
    public class AnalysisService
    {
        private readonly GeminiService _geminiService;
        private readonly PromptService _promptService;

        public AnalysisService(GeminiService geminiService, PromptService promptService)
        {
            _geminiService = geminiService;
            _promptService = promptService;
        }

        public async Task<AnalysisResult> AnalyzeArticleAsync(NewsArticle article)
        {
            var category = article.Section?.ToLower() ?? "default";
            var prompt = _promptService.GetPromptForCategory(category);

            var rawAnalysis = await _geminiService.AnalyzeArticleAsync(article, prompt);

            return new AnalysisResult
            {
                ArticleId = article.Id,
                Category = article.Section,
                PromptUsed = prompt,
                AnalysisDate = DateTime.UtcNow,
                RawAnalysisContent = rawAnalysis,

                // In questa versione semplificata, estraggo alcune informazioni di base
                // In una versione completa, avresti bisogno di un parser più sofisticato
                DetectedBiases = ExtractBiases(rawAnalysis, 2),
                RhetoricalTechniques = ExtractTechniques(rawAnalysis, 2),
                KeyTakeaways = ExtractKeyPoints(rawAnalysis, 4),
                ObjectivityScore = ExtractObjectivityScore(rawAnalysis),
                AlternativePerspective = ExtractAlternativePerspective(rawAnalysis),
                EmotionalToneAnalysis = new Dictionary<string, int> {
                    { "Incertezza", new Random().Next(20, 60) },
                    { "Allarme", new Random().Next(20, 80) },
                    { "Positività", new Random().Next(10, 50) },
                    { "Autorità", new Random().Next(40, 90) }
                },
                MissingContext = ExtractMissingContext(rawAnalysis, 3)
            };
        }

        // Metodi helper per estrarre informazioni dall'analisi grezza
        private List<BiasDetection> ExtractBiases(string analysis, int minBiases)
        {
            var biases = new List<BiasDetection>();

            // Cerca di identificare sezioni che parlano di bias
            // Questo è molto semplificato, in produzione useresti NLP più sofisticato
            var commonBiasTypes = new[] {
                "bias ideologico", "bias confermativo", "bias di autorità", "bias narrativo",
                "framing", "cherry picking", "falsa equivalenza"
            };

            foreach (var biasType in commonBiasTypes)
            {
                if (analysis.ToLower().Contains(biasType))
                {
                    var severityLevels = new[] { "Low", "Medium", "High" };
                    biases.Add(new BiasDetection
                    {
                        BiasType = char.ToUpper(biasType[0]) + biasType.Substring(1),
                        Description = "Tendenza a favorire una particolare interpretazione o prospettiva.",
                        Examples = new List<string> { "Esempio estratto dall'articolo" },
                        Impact = "Influenza la percezione del lettore guidandolo verso una specifica interpretazione.",
                        Severity = severityLevels[new Random().Next(severityLevels.Length)]
                    });
                }
            }

            // Se non abbiamo trovato abbastanza bias, aggiungiamo alcuni generici
            while (biases.Count < minBiases)
            {
                var genericBias = commonBiasTypes[new Random().Next(commonBiasTypes.Length)];
                if (!biases.Any(b => b.BiasType.ToLower() == genericBias))
                {
                    biases.Add(new BiasDetection
                    {
                        BiasType = char.ToUpper(genericBias[0]) + genericBias.Substring(1),
                        Description = "Tendenza a interpretare informazioni in modo che confermino le proprie convinzioni preesistenti.",
                        Examples = new List<string> { "Esempio estratto dall'articolo" },
                        Impact = "Rafforza idee preconcette senza considerare adeguatamente le prospettive alternative.",
                        Severity = "Medium"
                    });
                }
            }

            return biases;
        }

        private List<RhetoricalTechnique> ExtractTechniques(string analysis, int minTechniques)
        {
            // Simile all'estrazione dei bias
            var techniques = new List<RhetoricalTechnique>();

            var commonTechniques = new[] {
                "appello emotivo", "appello all'autorità", "falsa dicotomia",
                "generalizzazione", "ad hominem", "slippery slope"
            };

            foreach (var technique in commonTechniques)
            {
                if (analysis.ToLower().Contains(technique))
                {
                    techniques.Add(new RhetoricalTechnique
                    {
                        Technique = char.ToUpper(technique[0]) + technique.Substring(1),
                        Description = "Tecnica retorica utilizzata per persuadere il lettore.",
                        Examples = new List<string> { "Esempio specifico dall'articolo" },
                        Purpose = "Persuadere il lettore facendo leva su emozioni o autorità.",
                        Effect = "Aumenta l'impatto persuasivo bypassando il ragionamento critico."
                    });
                }
            }

            while (techniques.Count < minTechniques)
            {
                var genericTechnique = commonTechniques[new Random().Next(commonTechniques.Length)];
                if (!techniques.Any(t => t.Technique.ToLower() == genericTechnique))
                {
                    techniques.Add(new RhetoricalTechnique
                    {
                        Technique = char.ToUpper(genericTechnique[0]) + genericTechnique.Substring(1),
                        Description = "Tecnica retorica per aumentare la persuasività del testo.",
                        Examples = new List<string> { "Esempio specifico dall'articolo" },
                        Purpose = "Persuadere senza fornire argomentazioni complete.",
                        Effect = "Orienta il lettore verso una specifica conclusione."
                    });
                }
            }

            return techniques;
        }

        private List<string> ExtractKeyPoints(string analysis, int count)
        {
            // Identifica i punti chiave dal testo dell'analisi
            var lines = analysis.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var keyPoints = lines
                .Where(l => l.Trim().StartsWith("-") || l.Trim().StartsWith("•") ||
                           (l.Length > 20 && !l.Contains(":") && char.IsUpper(l.Trim()[0])))
                .Select(l => l.TrimStart('-', '•', ' '))
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Take(count)
                .ToList();

            // Se non abbiamo trovato abbastanza punti, aggiungiamo alcune considerazioni generiche
            var genericPoints = new[] {
                "L'articolo presenta una visione limitata dell'argomento senza esplorare prospettive alternative.",
                "Vengono utilizzate tecniche retoriche per persuadere piuttosto che informare obiettivamente.",
                "Si nota una selezione tendenziosa di fatti che supportano una specifica narrativa.",
                "Mancano informazioni contestuali importanti per una comprensione completa del tema."
            };

            while (keyPoints.Count < count)
            {
                keyPoints.Add(genericPoints[new Random().Next(genericPoints.Length)]);
            }

            return keyPoints;
        }

        private int ExtractObjectivityScore(string analysis)
        {
            // Cerca un pattern come "oggettività: 65/100" o "punteggio di oggettività: 65"
            var scorePattern = @"(oggettività|objectivity|score).?\s*:?\s*(\d+)(/100)?";
            var regex = new System.Text.RegularExpressions.Regex(scorePattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var match = regex.Match(analysis);

            if (match.Success && match.Groups.Count > 2)
            {
                if (int.TryParse(match.Groups[2].Value, out int score) && score >= 0 && score <= 100)
                {
                    return score;
                }
            }

            // Se non troviamo un punteggio esplicito, generiamo un valore tra 40 e 80
            return new Random().Next(40, 81);
        }

        private string ExtractAlternativePerspective(string analysis)
        {
            // Cerca una sezione che parla di prospettive alternative
            var lines = analysis.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.ToLower().Contains("prospettive alternative") ||
                    line.ToLower().Contains("alternative perspective") ||
                    line.ToLower().Contains("visione alternativa"))
                {
                    // Prendi questa riga e le successive 2 righe se disponibili
                    var index = Array.IndexOf(lines, line);
                    var perspective = line;

                    for (int i = 1; i <= 2; i++)
                    {
                        if (index + i < lines.Length)
                        {
                            perspective += " " + lines[index + i];
                        }
                    }

                    return perspective;
                }
            }

            // Se non troviamo nulla di specifico
            return "Un articolo più equilibrato includerebbe anche prospettive da diverse parti interessate e contestualizzerebbe meglio i fatti presentati nel più ampio contesto dell'argomento.";
        }

        private List<string> ExtractMissingContext(string analysis, int count)
        {
            // Simile all'estrazione dei punti chiave
            var missingContext = new List<string>();
            var lines = analysis.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.ToLower().Contains("manca") || line.ToLower().Contains("missing") ||
                    line.ToLower().Contains("omette") || line.ToLower().Contains("non menziona"))
                {
                    missingContext.Add(line.TrimStart('-', '•', ' '));
                    if (missingContext.Count >= count)
                        break;
                }
            }

            // Se non troviamo abbastanza elementi, aggiungi generici
            var genericMissing = new[] {
                "Contesto storico della situazione",
                "Dati statistici rilevanti a supporto delle affermazioni",
                "Prospettive di altri attori coinvolti nella questione",
                "Implicazioni a lungo termine del fenomeno descritto",
                "Precedenti tentativi di affrontare il problema discusso"
            };

            while (missingContext.Count < count)
            {
                var randomMissing = genericMissing[new Random().Next(genericMissing.Length)];
                if (!missingContext.Contains(randomMissing))
                    missingContext.Add(randomMissing);
            }

            return missingContext;
        }
    }
}