
using System.Diagnostics;
using GitUtility.Classes;
using LibGit2Sharp;

namespace GitUtility
{
    internal class GitUtility
    {
        static void Main()
        {
            try
            {
                RootDirectory rootDirectory = new("/Users/devrajsinhgohil/Desktop/Test");
                string email = "safia@microsoft.com";
                List<Classes.Commit> allCommits = new();
                List<Statistics> statistics = new();
                rootDirectory.FindRepositories();
                foreach (Classes.Repository repository in rootDirectory.Repositories)
                {
                    repository.GetCommits();
                    allCommits.AddRange(repository.Commits);
                    statistics.Add(new(repository.RepositoryName, repository.RepositoryPath, repository.Commits, email));
                }
                statistics.Add(new("Overall Statistics", rootDirectory.RootPath, allCommits, email));

                HTMLGenerator hTMLGenerator = new(rootDirectory.RootPath, statistics);
                hTMLGenerator.GenerateRepositoryStats();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
