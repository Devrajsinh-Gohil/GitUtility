
using RazorLight;

namespace GitUtility.Classes
{
    /**
     * <summary>
     * Class with methods to generate HTML outputs of Repository Stats.
     * </summary>
     */
    public class HTMLGenerator
    {
        /**
         * <summary>
         * Class with methods to generate HTML outputs of Repository Stats.
         * </summary>
         */
        private string WorkingPath { get; set; }

        /**
         * <summary>
         * Class with methods to generate HTML outputs of Repository Stats.
         * </summary>
         */
        public HTMLGenerator(RootDirectory rootDirectory)
        {
            WorkingPath = rootDirectory.RootPath;
        }

        public async void GenerateRepositoryStats(List<RepositoryStats> repositoryStats)
        {
            string templatePath = Path.Combine(WorkingPath, "RazorTemplates", "RepositoryStats.cshtml");
            if (!File.Exists(templatePath))
            {
                Console.WriteLine($"Template file '{templatePath}' not found.");
                return;
            }
            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(WorkingPath) // Ensure the path is correctly set
                .UseMemoryCachingProvider()
                .EnableDebugMode() // Enable debug mode for detailed errors
                .Build();
            try
            {
                foreach (RepositoryStats repositoryStat in repositoryStats)
                {
                    string filePath = Path.Combine(WorkingPath, "Output", repositoryStat.Repository.RepoName + ".html");
                    string htmlContent = await engine.CompileRenderAsync(templatePath, repositoryStats);
                    File.WriteAllText(filePath, htmlContent);
                    Console.WriteLine($"HTML file '{filePath}' generated successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public async void GenerateOverallStats(OverallStats overallStats)
        {
            string templatePath = Path.Combine(WorkingPath, "RazorTemplates", "OverAllStates.cshtml");
            if (!File.Exists(templatePath))
            {
                Console.WriteLine($"Template file '{templatePath}' not found.");
                return;
            }
            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(WorkingPath) // Ensure the path is correctly set
                .UseMemoryCachingProvider()
                .EnableDebugMode() // Enable debug mode for detailed errors
            .Build();

            try
            {
                string filePath = Path.Combine(WorkingPath, "Output", "OverallStats.html");
                string htmlContent = await engine.CompileRenderAsync(templatePath, overallStats);
                File.WriteAllText(filePath, htmlContent);
                Console.WriteLine($"HTML file '{filePath}' generated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}

