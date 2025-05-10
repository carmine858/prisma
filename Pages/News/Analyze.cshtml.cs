using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Prisma.Models;
using Prisma.Services;

namespace Prisma.Pages.News
{
    public class AnalyzeModel : PageModel
    {
        private readonly ILogger<AnalyzeModel> _logger;
        private readonly INewsService _newsService;
        private readonly AnalysisService _analysisService;

        public AnalyzeModel(
            ILogger<AnalyzeModel> logger,
            INewsService newsService,
            AnalysisService analysisService)
        {
            _logger = logger;
            _newsService = newsService;
            _analysisService = analysisService;
        }

        public NewsArticle Article { get; private set; }
        public AnalysisResult Analysis { get; private set; }
        public AnalysisMetadata CategoryMeta { get; private set; }
        public bool IsLoading { get; private set; }
        public bool HasError { get; private set; }
        public string ErrorMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Index");
            }

            IsLoading = true;
            HasError = false;

            try
            {
                // Recupera l'articolo
                Article = await _newsService.GetArticleByIdAsync(id);

                if (Article == null)
                {
                    HasError = true;
                    ErrorMessage = "Articolo non trovato. Verifica l'ID o prova un altro articolo.";
                    return Page();
                }

                // Ottieni i metadati della categoria
                var category = Article.Section?.ToLower() ?? "default";
                if (AnalysisMetadata.CategoryMetadata.ContainsKey(category))
                {
                    CategoryMeta = AnalysisMetadata.CategoryMetadata[category];
                }
                else
                {
                    CategoryMeta = new AnalysisMetadata
                    {
                        Category = Article.Section ?? "General",
                        IconClass = "bi-newspaper",
                        ThemeColor = "#6c757d",
                        Description = "Analisi dell'articolo",
                        KeyQuestions = new List<string>(),
                        CommonBiases = new List<BiasType>()
                    };
                }

                // Analizza l'articolo
                Analysis = await _analysisService.AnalyzeArticleAsync(Article);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing article {Id}", id);
                HasError = true;
                ErrorMessage = $"Si è verificato un errore durante l'analisi dell'articolo: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }

            return Page();
        }
    }
}