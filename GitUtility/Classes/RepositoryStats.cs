namespace GitUtility.Classes
{
    /**
	 * <summary>
	 * Class to represent the stats of a Repository;
	 * </summary>
	 */
    public class RepositoryStats
    {
        /**
		 * <summary>
		 * Properties for RepositoryStats.
		 * </summary>
		 */
        public GitRepository Repository { get; set; }
        public Contributor CurrentContributor { get; set; }
        public ContributionGraphGenerator ContributionGraph { get; set; }
        public int ContributorCount { get; set; }
        public List<Contributor> Top2Contributors { get; set; }
        public List<Contributor> AnnualTop2Contributors { get; set; }
        public List<int> CurrentContributorRanks { get; set; }
    
        /**
         * <summary>
         * Constrtuctor for RepositoryStats.
         * </summary>
         * <param name="repository">GitRepository object to get stats for</param>
         * <param name="contributor">Contributor object to get stats of the given contributor</param>
         */
        public RepositoryStats(GitRepository repository, string contributorEmail)
        {
            Repository = repository;
            CurrentContributor = FindContributorWithEmail(contributorEmail);
            ContributionGraph = new ContributionGraphGenerator(CurrentContributor);
            ContributorCount = repository.Contributors.Count;
            Top2Contributors = GetTop2Contributor();
            AnnualTop2Contributors = GetTop2AnnualContributor();
            CurrentContributorRanks = GetRanks();
        }

        Contributor FindContributorWithEmail(string email)
        {
            Contributor? contributor = Repository.Contributors.Find(c => c.Email == email) ?? throw new InvalidOperationException("Contributor with the given email was not found in: " + Repository.RepoName );
            return contributor;
        }


        /**
         * <summary>
         * Method to get top 2 contributors in the Repository.
         * </summary>
         * <returns>List of Emails of top contributors</returns>
         */
        List<Contributor> GetTop2Contributor()
        {
            List<Contributor> top2 = new();
            try
            {
                if(Repository.Contributors.Count > 1)
                {
                    top2.Add(Repository.Contributors[0]);
                    top2.Add(Repository.Contributors[1]);
                }
                else
                {
                    top2.Add(Repository.Contributors[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return top2;
        }

        /**
         * <summary>
         * Method to get annual top 2 contributors in the Repository.
         * </summary>
         * <returns>List of Emails of annual top contributors.</returns>
         */
        List<Contributor> GetTop2AnnualContributor()
        {
            List<Contributor> annualTop2 = new();
            try
            {
                List<Contributor> contributors = Repository.Contributors.OrderByDescending(c => c.AnnualContributions.Count).ToList();
                if(contributors.Count > 1)
                {
                    annualTop2.Add(contributors[0]);
                    annualTop2.Add(contributors[1]);
                }
                else
                {
                    annualTop2.Add(contributors[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return annualTop2;
        }

        /**
         * <summary>
         * Method to get overall and annual ranks of current contributor.
         * </summary>
         * <returns>List ranks of size 2, index 0 is overall rank and index 1 is annual rank.</returns>
         */
        List<int> GetRanks()
        {
            List<int> ranks = new(2);
            try
            {
                // Overall Rank
                ranks.Add(Repository.Contributors.FindIndex(c => c.Email == CurrentContributor.Email)+1);

                // Annual Rank
                List<Contributor> contributors = Repository.Contributors.OrderByDescending(c => c.AnnualContributions.Count).ToList();
                ranks.Add(contributors.FindIndex(c => c.Email == CurrentContributor.Email) + 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ranks;
        }
    }
}

