using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace MyPersonalDj
{
    internal class songmenu
    {
        private List<playlist> myRecord = new List<playlist>();

        public void LoadSongs(string folderpath)
        {
            if (string.IsNullOrEmpty(folderpath) || !Directory.Exists(folderpath))
            {
                return;
            }

            try
            {
                var files = Directory.EnumerateFiles(folderpath, "*.mp3", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    // avoid adding duplicates
                    if (myRecord.Any(p => string.Equals(p.getFilePath(), file, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    playlist newSong = new playlist(Path.GetFileNameWithoutExtension(file), file);
                    myRecord.Add(newSong);
                }
            }
            catch
            {
                // ignore IO errors (permissions, etc.) to keep UI responsive
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

        public int GetSongCount()
        {
            return myRecord.Count;
        }

        // Return a list of song titles for binding to UI controls
        public List<string> getLibrary()
        {
            return myRecord.Select(p => p.getSongTitle()).ToList();
        }

        // Play a song by its title. This is a minimal implementation that
        // looks up the filepath by title and launches the file using the OS
        // default handler for the media type.
        public void playSong(string songName)
        {
            if (string.IsNullOrEmpty(songName)) return;

            var item = myRecord.FirstOrDefault(p => string.Equals(p.getSongTitle(), songName, StringComparison.OrdinalIgnoreCase));
            if (item == null) return;

            var path = item.getFilePath();
            if (File.Exists(path))
            {
                var psi = new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
        }
    }
    
}
