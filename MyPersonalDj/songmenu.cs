using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyPersonalDj
{
    internal class songmenu
    {
        private List<playlist> myRecord = new List<playlist>();

        public void LoadSongs(string folderpath)
        {
            string[] files = Directory.GetFiles(folderpath, "*.mp3");
            foreach (string file in files)
            {
                playlist newSong = new playlist(Path.GetFileNameWithoutExtension(file), file);

                myRecord.Add(newSong);
            }
        }
        public void displayLibrary()
        {
            Console.WriteLine("---------YOUR SONGS--------");
            for (int i = 0; i < myRecord.Count; i++)
            {
                Console.WriteLine($"{i}: {myRecord[i].getSongTitle()}");
            }
        }
        public playlist GetPlaylist(int choice)
        {
            if (choice >= 0 && choice < myRecord.Count)
            {
                return myRecord[choice];
            }
            return null;
        }

        internal int GetSongCount()
        {
            return myRecord.Count();
        }
    }
    
}
