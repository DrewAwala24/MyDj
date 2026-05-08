using System;

namespace MyPersonalDj
{
    class Program
    {
        static void Main(string[] args)
        {

            audioplayer myDj = new audioplayer();
            
            string folderpath = @"";
            string mySongs = @"C:\Users\Dr3wski\Music\PLAY\Gunna - podcast [Official Visualizer].mp3";

            Console.WriteLine("Press Z to play music");
            while(Console.ReadKey(true).Key == ConsoleKey.Z)
            {
                myDj.PlayMusic(mySongs);
                Console.WriteLine("Now Playing");
                Display display = new Display("Gunna", "Podcast");
                display.displayInfo();
            }

            Console.WriteLine("Press the SPACEBAR to stop the music");
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                Console.WriteLine("Wrong Key");
            }
            try
            {
                myDj.StopMusic();                
            }
            catch(Exception e)
            {
                Console.WriteLine("Invalid Key!");
            }
            Console.ReadKey();
          
        }
    }
}