using System;
using LibGit2Sharp;

namespace GitUtility.Classes
{
	public class RootDirectory
	{
        public string RootName { get; set; }
        public string RootPath { get; set; }
        public List<Repository> Repositories { get; set; }

        public RootDirectory(string path)
        {
            RootName = Path.GetFileName(path);
            RootPath = path;
            Repositories = new();
        }

        public void FindRepositories()
        {
            var dotGitPaths = Directory.GetDirectories(RootPath, ".git", SearchOption.AllDirectories);

            foreach (string path in dotGitPaths)
            {
                string? repositoryPath = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(repositoryPath))
                {
                    throw new ArgumentNullException(nameof(repositoryPath), "empty");
                }
                Repositories.Add(new(repositoryPath));
            }
        }
    }
}