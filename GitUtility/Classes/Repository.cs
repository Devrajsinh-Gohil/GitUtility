using System;
using System.Collections.Concurrent;

namespace GitUtility.Classes
{
	public class Repository
	{
        public string RepositoryName { get; set; }
        public string RepositoryPath { get; set; }
        public List<Commit> Commits { get; set; }
        public Repository(string path)
        {
            RepositoryName = Path.GetFileName(path);
            RepositoryPath = path;
            Commits = new();
        }

        public void GetCommits()
        {
            var repository = new LibGit2Sharp.Repository(RepositoryPath);
            var seenCommits = new ConcurrentDictionary<string, bool>();

            Commits.AddRange(
                repository.Commits
                    .AsParallel()
                    .Select(
                        commit => new Commit(
                        new Author(commit.Author.Name, commit.Author.Email),
                        commit.Message,
                        commit.Author.When.Date,
                        commit.Sha)
                    )
                    .ToList()
            );
        }
    }
}

