using System;
namespace GitUtility.Classes
{
    public class RootDirectory : Directory
    {
        public List<GitRepository> GitRepositories { get; set; }
        public int GitRepositoriesCount { get; set; }

        public RootDirectory(string path) : base(path)
        {
            GitRepositories = new List<GitRepository>();
            GitRepositoriesCount = getGitRepositoriesCount();
        }

        private List<GitRepository> getGitRepositories()
        {
            List<GitRepository> gitRepositories = new List<GitRepository>();
            return gitRepositories;
        }

        private int getGitRepositoriesCount()
        {
            try
            {
                return GitRepositories.Count;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

    }
}

