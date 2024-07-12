using System;
namespace GitUtility.Classes
{
	public class MapCell
	{
        public DateTime Date { get; set; }
        public int CommitCount { get; set; }

        public MapCell(DateTime date, int commitCount)
        {
            Date = date;
            CommitCount = commitCount;
        }
    }
}

