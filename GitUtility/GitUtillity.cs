using System;

namespace GitUtility
{
    internal class GitUtility
    {
        static void Main(string[] args)
        {
            Classes.RootDirectory rd = new Classes.RootDirectory("/Users/devrajsinhgohil/Desktop/game/Maze/.git");
            Console.WriteLine(rd.Name);
       }
    }
}