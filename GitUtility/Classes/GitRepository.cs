using LibGit2Sharp;

namespace GitUtility.Classes
{
    /**
     * <summary>
     * Class to represent a Git Repository.
     * </summary>
     */ 
	public class GitRepository
	{

        /**
         * <summary>
         * Properties of a Git Repository.
         * </summary>
         */
        public string RepoPath { get; set; }
        public string RepoName { get; set; }
        public List<Commit> Commits { get; set; }
		public List<Contributor> Contributors { get; set; }


        /**
         * <summary>
         * Constructor for GitRepository.
         * </summary>
         * <param name="path">Takes a string input for Repository path</param>
         */ 
        public GitRepository(string path)
		{
            RepoPath = path;
            RepoName = Path.GetFileName(RepoPath);
			Commits = GetCommits();
            Contributors = GetContributors();
        }

        /**
         * <summary>
         * Method to get List of all the commits for current GitRepository.
         * </summary>
         * <returns>Returns List of LibGit2Sharp.Commit objects</returns>
         */
        List<Commit> GetCommits()
        {
            List<Commit> commits = new();
            var repository = new Repository(RepoPath);
            foreach (Commit commit in repository.Commits)
            {
                commits.Add(commit);
            }
            return commits;
        }

        /**
         * <summary>
         * Method to get List of all the Contributors of this GitRepository.
         * </summary>
         * <returns>Retruns List of Contributor objects</returns>
         */
        List<Contributor> GetContributors()
        {
            List<Contributor> contributors = new();
            var uniqueAuthors = Commits.Select(c => c.Author).Distinct(new AuthorComparer());

            foreach (var author in uniqueAuthors)
            {
                contributors.Add(new Contributor (author, GetContributorCommits(author.Email)));
            }

            return contributors.OrderByDescending(c => c.Contributions.Count).ToList();
        }

        /**
         * <summary>
         * Method to get List of all the commits of a particular Contributor.
         * </summary>
         * <returns>Returns List of LibGit2Sharp.Commit objects</returns>
         */
        List<Commit> GetContributorCommits(string email)
        {
            var commits = Commits.Where(c => c.Author.Email == email).ToList();
            return commits;
        }
    }

    /**
     * <summary>
     * A helper class to compare to Contributors with their Email
     * to get non-repeating list of contributors from the GitRepository.
     * </summary>
     */
    public class AuthorComparer : IEqualityComparer<Signature>
    {
        public bool Equals(Signature x, Signature y)
        {
            return x.Email == y.Email;
        }

        public int GetHashCode(Signature obj)
        {
            return obj.Email.GetHashCode();
        }
    }
}