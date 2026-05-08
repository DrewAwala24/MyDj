using System;

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
            
            while(isRunning)
            {
              
                myMenu.displayLibrary();

                Console.WriteLine("Enter the song number to play: ");
                string input = Console.ReadLine();
                int choice = Convert.ToInt32(input);

                if(choice == -1)
                {
                    isRunning = false;
                    continue;
                }

                playlist selectedSong = myMenu.GetPlaylist(choice);

                if(selectedSong != null)
                {
                    myDj.PlayMusic(selectedSong.getFilePath());
                    Console.WriteLine($"Now Playing: {selectedSong.getSongTitle()}");
                }
                
            }

            Console.WriteLine("Press the SPACEBAR to stop the music");
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) {}

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