using System;
using System.Collections.Generic;
using System.Text;

namespace MyPersonalDj
{
    internal class playlist
    {
        private string songTitle;
        private string filepath;

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
