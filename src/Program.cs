using System;
using System.Threading.Channels;

namespace MyPersonalDj
{
    class Program
    {
        static void Main(string[] args)
        {

            audioplayer myDj = new audioplayer();
            songmenu myMenu = new songmenu();
            
            string folderpath = @"C:\Users\Dr3wski\Music\PLAY";
            myMenu.LoadSongs(folderpath);

            bool isRunning = true;
            playback remote = new playback();
            myMenu.displayLibrary();

            while (isRunning)
            {
                Console.WriteLine("[N] New Song, [P] Pause/Resume, [E] Exit, [S] Shuffle");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.N:
                        Console.WriteLine("Enter Song Number: ");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        playlist selected = myMenu.GetPlaylist(choice);
                        if (selected != null) remote.start(selected.getFilePath());
                        break;

                    case ConsoleKey.P:
                        remote.pause();
                        break;

                    case ConsoleKey.S:
                        int total = myMenu.GetSongCount();
                        int randomIndex = remote.GetShuffleIndex(total);
                        playlist randomSongs = myMenu.GetPlaylist(randomIndex);

                        if (randomSongs != null)
                        {
                            remote.start(randomSongs.getFilePath());
                            Console.WriteLine($"\n>>> Shuffled to: {randomSongs.getSongTitle()}");
                        }
                        break;

                    case ConsoleKey.E:
                        Console.WriteLine("Closing App.........");
                        remote.Stop();
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Unkown Command! ");
                        Console.WriteLine("Use N, P, S, E");
                        break;
                }


            }
          
        }
    }
}