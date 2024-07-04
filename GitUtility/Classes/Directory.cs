using System;

namespace GitUtility.Classes
{
	public class Directory
	{

		public string? Name { get; set; }
		public string Path { get; set; }

		public Directory(string path)
		{
			Path = path;
			getName(path);
		}

		private void getName(string path)
		{
			try
			{
				Name = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(path));
			}
			catch (ArgumentException e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}