using System;
using System.Collections.Generic;
using System.Text;
using NAudio.Wave;

namespace MyPersonalDj
{
    internal class audioplayer
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        public void PlayMusic(string filepath)
        {
            if(outputDevice != null)
            {
                outputDevice.Stop();
            }

            audioFile = new AudioFileReader(filepath);
            outputDevice = new WaveOutEvent();

            outputDevice.Init(audioFile);
            outputDevice.Play();

            Console.WriteLine("Now Playing: "+filepath);
        }
        public void StopMusic()
        {
            if(outputDevice != null)
            {
                    outputDevice.Stop();
                    Console.WriteLine("Stop Playing");
            }
        }
    }
}
