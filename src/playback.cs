using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPersonalDj
{
    internal class playback
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        public void start(String filepath)
        {
            Stop();
            audioFile = new AudioFileReader(filepath);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }

        public void pause()
        {
            if(outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Pause();
                Console.WriteLine("⏸️ Paused");
            }
            else if(outputDevice != null && outputDevice.PlaybackState == PlaybackState.Paused)
            {
                outputDevice.Play();
                Console.WriteLine("▶️ Playing");
            }
        }
        public void Stop()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();
            audioFile?.Dispose();
        }
        public int GetShuffleIndex(int totalSongs)
        {
            Random random = new Random();
            return random.Next(0, totalSongs);
        }
    }
}
