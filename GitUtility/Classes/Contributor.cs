using LibGit2Sharp;

namespace GitUtility.Classes
{
    /**
     * <summary>
     * Class to represent a Contributor in a Git Repository.
     * </summary>
     */
    public class Contributor
    {
        /**
         * <summary>
         * Properties of a Contributor.
         * </summary>
         */
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Commit> TotalContributions { get; set; }
        public List<Commit> AnnualContributions { get; set; }

        /**
         * <summary>
         * Constructor for the Contributor.
         * </summary>
         * <param name="auhtor">Signature from Commit.Author</param>
         * <param name="totalContributions">List of total LibGit2Sharp.Commits done by the Contributor </param>
         */
        public Contributor(Signature auhtor, List<Commit> totalContributions)
        {
            Name = auhtor.Name;
            Email = auhtor.Email;
            TotalContributions = totalContributions;
            AnnualContributions = GetAnnualContributions();
        }

        /**
         * <summary>
         * Method to get annual countributions of the contributor.
         * </summary>
         * <returns>A List of LibGit2Sharp.Commit objects</returns>
         */
        List<Commit> GetAnnualContributions()
        {
            List<Commit> annualContributions = new();
            try
            {
                DateTime startDate = DateTime.Now.AddYears(-1);
                DateTime endDate = DateTime.Now;
                foreach (Commit commit in TotalContributions)
                {
                    if (endDate.Date >= commit.Author.When.Date && commit.Author.When.Date >= startDate.Date)
                    {
                        annualContributions.Add(commit);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return annualContributions;
        }
    }
}