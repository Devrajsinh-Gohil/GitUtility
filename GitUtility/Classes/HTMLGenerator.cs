using System;
using RazorLight;

namespace GitUtility.Classes
{
	public class HTMLGenerator
	{
        private string RootPath { get; set; }
        private readonly List<Statistics> Statistics;
        public HTMLGenerator(string rootPath, List<Statistics> statistics)
        {
            RootPath = rootPath;
            Statistics = statistics;
        }
        public async void GenerateRepositoryStats()
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "RazorTemplates", "RepositoryStats.cshtml");
            if (!File.Exists(templatePath))
            {
                Console.WriteLine($"Template file '{templatePath}' not found.");
                return;
            }
            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "RazorTemplates"))
                .UseMemoryCachingProvider()
                .Build();

            foreach (Statistics stats in Statistics)
            {
                string filePath = Path.Combine(RootPath, "Output", stats.RepositoryName + ".html");
                string htmlContent = await engine.CompileRenderAsync(templatePath, stats);
                File.WriteAllText(filePath, htmlContent);
                Console.WriteLine($"HTML file '{filePath}' generated successfully.");
            }
        }
    }
}

