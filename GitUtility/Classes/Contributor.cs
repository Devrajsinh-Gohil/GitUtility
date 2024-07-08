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
        public List<Commit> Contributions { get; set; }

        /**
         * <summary>
         * Constructor for the Contributor.
         * </summary>
         * <param name="auhtor">Signature from Commit.Author</param>
         * <param name="commits">List of LibGit2Sharp.Commits done by the Contributor </param>
         */
        public Contributor(Signature auhtor, List<Commit> commits)
        {
            Name = auhtor.Name;
            Email = auhtor.Email;
            Contributions = commits;
        }
    }
}