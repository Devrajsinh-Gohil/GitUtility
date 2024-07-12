using System;
namespace GitUtility.Classes
{
	public class Statistics
	{
        public AuthorCommitInfo? CurrentAuhtorInfo { get; set; }
        public string RepositoryName { get; set; }
        public string RepositoryPath { get; set; }
        public List<Commit> Commits { get; set; }
        public string AuthorEmail { get; set; }
        public string? TotalContributors { get; set; }
        public string? TotalContributorsThisYear { get; set; }
        public string? CurrentAuthorRank { get; set; }
        public string? CurrentAuthorRankThisYear { get; set; }
        public List<AuthorCommitInfo> Top2Authors { get; set; }
        public List<AuthorCommitInfo> Top2AuthorsThisYear { get; set; }
        public ContributionHeatMap HeatMap { get; set; }

        public Statistics(string repositoryName, string repositoryPath, List<Commit> commits, string email)
        {
            RepositoryName = repositoryName;
            RepositoryPath = repositoryPath;
            Commits = commits;
            AuthorEmail = email;
            Top2Authors = new();
            Top2AuthorsThisYear = new();
            HeatMap = new();
            CalculateStatistics();
        }

        public void CalculateStatistics()
        {

            var currentDate = DateTime.Now;
            var lastYearDate = currentDate.AddYears(-1);

            var authorCommitInfos = Commits
                .GroupBy(commit => commit.Author.Email)
                .Select(group => new AuthorCommitInfo
                (
                    group.First().Author,
                    group.Count(),
                    group.Count(commit => commit.Date >= lastYearDate && commit.Date <= currentDate)
                ))
                .ToList();

            CurrentAuhtorInfo = authorCommitInfos.FirstOrDefault(authorCommitInfo => authorCommitInfo.Author.Email == AuthorEmail);

            TotalContributors = authorCommitInfos.Count.ToString();
            TotalContributorsThisYear = authorCommitInfos.Count(info => info.CommitCountThisYear > 0).ToString();

            Top2Authors.AddRange(
                authorCommitInfos
                .OrderByDescending(commitInfo => commitInfo.TotalCommitCount)
                .Take(2)
                .ToList()
            );

            Top2AuthorsThisYear.AddRange(
                authorCommitInfos
                .OrderByDescending(commitInfo => commitInfo.CommitCountThisYear)
                .Take(2)
                .ToList()
                );

            var sortedByTotalCommits = authorCommitInfos
                .OrderByDescending(commitInfo => commitInfo.TotalCommitCount)
                .ToList();

            CurrentAuthorRank = (sortedByTotalCommits
                .FindIndex(commitInfo => commitInfo.Author.Email == AuthorEmail)+1).ToString();

            if (string.IsNullOrEmpty(CurrentAuthorRank))
            {
                CurrentAuthorRank = "no contributions";
            }

            var sortedByCommitsThisYear = authorCommitInfos
                .Where(commitInfo => commitInfo.CommitCountThisYear > 0)
                .OrderByDescending(commitInfo => commitInfo.CommitCountThisYear)
                .ToList();

            CurrentAuthorRankThisYear = (sortedByCommitsThisYear
                .FindIndex(commitInfo => commitInfo.Author.Email == AuthorEmail) + 1).ToString();

            if (string.IsNullOrEmpty(CurrentAuthorRankThisYear))
            {
                CurrentAuthorRank = "no contributions this year.";
            }

            HeatMap.GenerateHeatMap(Commits, AuthorEmail);

        }
    }
}