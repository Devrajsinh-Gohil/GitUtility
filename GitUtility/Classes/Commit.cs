using System;
namespace GitUtility.Classes
{
	public class Commit
	{
        public Author Author { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Hash { get; set; }
        public Commit(Author author, string message, DateTime date, string hash)
        {
            Author = author;
            Message = message;
            Date = date;
            Hash = hash;
        }
    }
}

