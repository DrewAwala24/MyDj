using System;
using System.Collections.Generic;
using System.Text;

namespace MyPersonalDjGui
{
    internal class playlist
    {
        private string songTitle;
        private string filepath;

        public playlist(string v, string file)
        {
            songTitle = v;
            filepath = file;
        }

        public void Songs(string songTitle, string filepath)
        {
            this.songTitle = songTitle;
            this.filepath = filepath;
        }
        public string getSongTitle()
        {
            return songTitle;
        }
        public string getFilePath()
        {
            return filepath;
        }
    }
}
