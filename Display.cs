using System;
using System.Collections.Generic;
using System.Text;

namespace MyPersonalDjGui
{
    internal class Display
    {
        private string artistName = "";
        private string songName = "";

        public Display(string artistName, string songName)
        {
            this.artistName = artistName;
            this.songName = songName;
            
        }
        public string getArtistName()
        {
            return artistName;
        }
        public string getSong()
        {
            return songName;
        }
       

        public void displayInfo()
        {
            Console.WriteLine("==== Currently Playing====");
            Console.WriteLine("Artist: " + artistName);
            Console.WriteLine("Song: " + songName);
        }
       
    }
}
