using LibGit2Sharp;
namespace GitUtility.Classes
{
    /**
	* <summary>
	* Class to represent the contribution graph of the contributor
	* </summary>
	*/
    public class ContributionGraphGenerator
    {
        /**
		 * <summary>
		 * Properties of a ContributionGraph
		 * </summary>
		 */
        private Contributor? Contributor { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
        public List<GraphCell> ContributionGraph { get; set; }
        public int ContributionCount { get; set; }

        /**
		 * <summary>
		 * Constructor for ContributionGraphGenerator
		 * </summary>
		 * <param name="contributor">A Contributor object to create the graph</param>
		 */
        public ContributionGraphGenerator(Contributor contributor)
        {
            Contributor = contributor;
            StartDate = DateTime.Now.AddYears(-1);
            EndDate = DateTime.Now;
            if(contributor != null)
            {
                ContributionGraph = CreateGraph();
            }
            else
            {
                ContributionGraph = CreateEmptyGraph();
            }
            ContributionCount = 0;
        }

        /**
         * <summary>
         * Function to create the ContributionGraph
         * </summary>
         * <returns>List of GraphCell</returns>
         */
        List<GraphCell> CreateGraph()
        {
            List<GraphCell> graph = new();
            try
            {
                int totalDays = (int)(EndDate - StartDate).TotalDays;
                for (int i = 0; i < totalDays; i++)
                {
                    DateTime date = StartDate.AddDays(i);
                    int commitCountOnDate = GetCommitCountOnDate(date);
                    graph.Add(new GraphCell(date, commitCountOnDate));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return graph;
        }


        /**
         * <summary>
         * Function to get number of commits on particular date.
         * </summary>
         * <param name="date">a DateTime object of date</param>
         * <returns>Count of commits by contributor on the given date</returns>
         */
        int GetCommitCountOnDate(DateTime date)
        {
            int count = 0;
            try
            {
                foreach (Commit commit in Contributor.TotalContributions)
                {
                    if (commit.Author.When.Date == date.Date)
                    {
                        count++;
                        ContributionCount++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return count;
        }


        /**
         * <summary>
         * Method to create an empty graph if no Contributor is null.
         * </summary>
         * <returns>List of GraphCell objects</returns>
         */
        List<GraphCell> CreateEmptyGraph()
        {
            List<GraphCell> graph = new();
            try
            {
                int totalDays = (int)(EndDate - StartDate).TotalDays;
                for (int i = 0; i < totalDays; i++)
                {
                    DateTime date = StartDate.AddDays(i);
                    graph.Add(new GraphCell(date, 0));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return graph;
        }
    }
}