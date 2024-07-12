using System;
namespace GitUtility.Classes
{
	public class AuthorCommitInfo
	{
        public Author Author { get; set; }
        public int TotalCommitCount { get; set; }
        public int CommitCountThisYear { get; set; }

        public AuthorCommitInfo(Author author, int totalCommitCount, int commitCountLastYear)
        {
            Author = author;
            TotalCommitCount = totalCommitCount;
            CommitCountThisYear = commitCountLastYear;
        }
    }
}

