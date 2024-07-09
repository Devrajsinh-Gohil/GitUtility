namespace GitUtility.Classes
{
    /**
     * <summary>
     * Class to represent the root directory
     * </summary>
     */
    public class RootDirectory
    {
        /**
		 * <summary>
		 * Properties for RootDirectory.
		 * </summary>
		 */
        public string RootPath { get; set; }
        public int GitRepositoriesCount { get; set; }
        public string CurrentContributorEmail { get; set; }
        public List<GitRepository> GitRepositories { get; set; }
        public List<RepositoryStats> AllRepositoryStats { get; set; }

        /**
		 * <summary>
		 * Constructor of RootDirectory.
		 * </summary>
		 * <param name="path">Path to the root directory containing git repositories.</param>
		 */
        public RootDirectory(string path, string contributorEmail)
        {
            RootPath = path;
            CurrentContributorEmail = contributorEmail;
            GitRepositories = GetGitRepositories();
            GitRepositoriesCount = GitRepositories.Count;
            AllRepositoryStats = GetRepositoryStats();
        }

        /**
		 * <summary>
		 * Method to get all the git repositories present in root directory and its sub directories.
		 * </summary>
		 * <returns>List of GitRepository objects.</returns>
		 */
        List<GitRepository> GetGitRepositories()
        {
            List<GitRepository> gitRepositories = new();
            try
            {
                var dotGitRepositories = Directory.GetDirectories(RootPath, ".git", SearchOption.AllDirectories);
                foreach (string gitRepositoryPath in dotGitRepositories)
                {
                    string? repoPath = Path.GetDirectoryName(gitRepositoryPath);
                    if (string.IsNullOrEmpty(repoPath))
                    {
                        throw new ArgumentNullException(nameof(repoPath), "empty");
                    }
                    GitRepository gitRepository = new(repoPath);
                    gitRepositories.Add(gitRepository);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return gitRepositories;
        }

        /**
		 * <summary>
		 * Method to get stats of all the git repositories present in root directory and its sub directories.
		 * </summary>
		 * <returns>List of RepositoryStats objects.</returns>
		 */
        List<RepositoryStats> GetRepositoryStats()
        {
            List<RepositoryStats> repositoryStats = new();
            try
            {
               foreach(GitRepository repository in GitRepositories)
                {
                    repositoryStats.Add(new RepositoryStats(repository, CurrentContributorEmail));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return repositoryStats;
        }
    }
}

