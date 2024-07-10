namespace GitUtility.Classes
{
    /**
     * <summary>
     * Class to represent Git stats with respect to RootDirecotry.
     * </summary>
     */
    public class OverallStats
    {
        /**
         * <summary>
         * Properties of OverallStats class 
         * </summary>
         */
        public RootDirectory RootDirectory { get; private set; }
        public int TotalRepositories { get; private set; }
        public int TotalCommits { get; private set; }
        public List<Contributor> AllContributors { get; private set; }
        public List<Contributor> LastYearContributors { get; private set; }
        public List<GraphCell> ContributionGraph { get; private set; }
        public List<Contributor> LifetimeTopContributors { get; private set; }
        public List<int> LifetimeTopContributorCommits { get; private set; }
        public List<Contributor> LastYearTopContributors { get; private set; }
        public List<int> LastYearTopContributorCommits { get; private set; }
        public Contributor CurrentContributor { get; private set; }
        public int LifetimeRank { get; private set; }
        public int LastYearRank { get; private set; }


        /**
         * <summary>
         * Constructor for class OverallStats
         * </summary>
         * <param name="path">Path of root directory.</param>
         * <param name="currentContributorEmail">Email of current contributor</param>
         */
        public OverallStats(string path, string currentContributorEmail)
        {
            RootDirectory = new(path, currentContributorEmail);
            AllContributors = new();
            LastYearContributors = new();
            ContributionGraph = new();
            LifetimeTopContributors = new();
            LifetimeTopContributorCommits = new();
            LastYearTopContributors = new();
            LastYearTopContributorCommits = new();

            CalculateOverallStats(RootDirectory);
            CalculateTopContributors();
            CurrentContributor = AllContributors.FirstOrDefault(c => c.Email == currentContributorEmail);
            if (CurrentContributor != null)
            {
                GenerateCurrentContributorGraph();
            }
            CalculateContributorRanks();
        }


        /**
         *<summary>
         * Method to calculate overall stats from each repository.
         *</summary>
         *<param name="rootDirectory">Object of RootDirectory</param>
         */
        private void CalculateOverallStats(RootDirectory rootDirectory)
        {
            try
            {
                foreach (var repository in rootDirectory.GitRepositories)
                {
                    TotalRepositories++;
                    TotalCommits += repository.Commits.Count;

                    foreach (var contributor in repository.Contributors)
                    {
                        var existingContributor = AllContributors.FirstOrDefault(c => c.Email == contributor.Email);
                        if (existingContributor == null)
                        {
                            AllContributors.Add(contributor);
                        }
                        else
                        {
                            existingContributor.TotalContributions.AddRange(contributor.TotalContributions);
                        }
                    }
                }
                LastYearContributors = AllContributors
                                        .Where(c => c.TotalContributions.Any(tc => tc.Author.When.Date >= DateTime.Now.AddYears(-1)))
                                        .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /**
         * <summary>
         * Method to get overall contribution graph for the current contributor.
         * </summary>
         */
        private void GenerateCurrentContributorGraph()
        {
            ContributionGraphGenerator generator = new(CurrentContributor);
            ContributionGraph = generator.ContributionGraph;
        }


        /**
         * <summary>
         * Method to calculate top contributors, both lifetime and for the last year.
         * </summary>
         */
        private void CalculateTopContributors()
        {
            try
            {
                DateTime oneYearAgo = DateTime.Now.AddYears(-1);

                // Lifetime top contributors
                var lifetimeContributors = AllContributors
                    .Select(c => new { Contributor = c, Commits = c.TotalContributions.Count })
                    .OrderByDescending(c => c.Commits)
                    .Take(2)
                    .ToList();

                foreach (var contributor in lifetimeContributors)
                {
                    LifetimeTopContributors.Add(contributor.Contributor);
                    LifetimeTopContributorCommits.Add(contributor.Commits);
                }

                // Last year top contributors
                var lastYearContributors = AllContributors
                    .Select(c => new { Contributor = c, Commits = c.TotalContributions.Count(tc => tc.Author.When.Date >= oneYearAgo) })
                    .OrderByDescending(c => c.Commits)
                    .Take(2)
                    .ToList();

                foreach (var contributor in lastYearContributors)
                {
                    LastYearTopContributors.Add(contributor.Contributor);
                    LastYearTopContributorCommits.Add(contributor.Commits);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /**
         * <summary>
         * Method to calculate ranks of the current contributor.
         * </summary>
         */
        private void CalculateContributorRanks()
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}