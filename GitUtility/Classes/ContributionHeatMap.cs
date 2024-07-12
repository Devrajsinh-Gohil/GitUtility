using System;
namespace GitUtility.Classes
{
    public class ContributionHeatMap
    {
        public List<MapCell> HeatMap { get; set; }
        public ContributionHeatMap()
        {
            HeatMap = new();
        }

        public void GenerateHeatMap(List<Commit> commits, string email)
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.AddYears(-1);

            var commitsByCurrentAuthor = commits
                .Where(commit => commit.Author.Email == email && commit.Date >= startDate && commit.Date <= endDate)
                .ToList();

            int totalDays = (int)(endDate - startDate).TotalDays;
            Console.WriteLine(totalDays);
            for (int i = 0; i < totalDays; i++)
            {
                DateTime date = startDate.AddDays(i);

                int commitCount = commitsByCurrentAuthor
                    .Where(commit => commit.Date.Date == date.Date)
                    .Count();

                HeatMap.Add(new MapCell(date, commitCount));
            }
        }
    }
}

