using System;
namespace GitUtility.Classes
{
    /**
     * <summary>
     * Class to represent a Cell in the ContributionGraph.
     * </summary>
     */
    public class GraphCell
	{
        /**
         * <summary>
         * Properties of a GraphCell.
         * </summary>
         */
        public DateTime Date { get; set; }
		public int ContributionCount { get; set; }

        /**
         * <summary>
         * Constructor for the GraphCell.
         * </summary>
         * <param name="date">Date object of DateTime</param>
         * <param name="contributionCount">Number of contribbuitons on a given Date as integer</param>
         */
        public GraphCell(DateTime date, int contributionCount)
		{
			Date = date;
			ContributionCount = contributionCount;
		}
	}
}

