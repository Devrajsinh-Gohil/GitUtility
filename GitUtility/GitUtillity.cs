
using GitUtility.Classes;

namespace GitUtility
{
    internal class GitUtility
    {
        private static string RootPath { get; set; }
        private static string Email { get; set; }
        private static RootDirectory RootDirectory { get; set; }
        private static List<Commit> AllCommits { get; set; }
        private static List<Statistics> Statistics { get; set; }
        private static HTMLGenerator HTMLGenerator { get; set; }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Usage: ./GitUtility <root-directory-path> <author-email>");
                }
                else
                {
                    RootPath = args[0];
                    Email = args[1];
                    RootDirectory = new(RootPath);
                    AllCommits = new();
                    Statistics = new();

                    RootDirectory.FindRepositories();

                    foreach (Repository repository in RootDirectory.Repositories)
                    {
                        repository.GetCommits();
                        AllCommits.AddRange(repository.Commits);
                        Statistics.Add(new(repository.RepositoryName, repository.RepositoryPath, repository.Commits, Email));
                    }

                    Statistics.Add(new("Overall Statistics", RootDirectory.RootPath, AllCommits, Email));

                    HTMLGenerator HTMLGenerator = new(RootDirectory.RootPath, Statistics);
                    HTMLGenerator.GenerateRepositoryStats();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
