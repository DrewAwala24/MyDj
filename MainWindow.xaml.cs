using MyPersonalDj;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyPersonalDjGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        songmenu myMenu = new songmenu();
        private System.Threading.CancellationTokenSource _logWatcherCts;
        public MainWindow()
        {
            InitializeComponent();

            // Load songs from the user's Music folder before binding the list
            try
            {
                var musicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                myMenu.LoadSongs(musicPath);
            }
            catch
            {
                // ignore failures to load; UI will simply show an empty list
            }

            LoadSongs();

            // start watching the shared console log and update the UI immediately
            _logWatcherCts = new System.Threading.CancellationTokenSource();
            StartLogWatcher(_logWatcherCts.Token);

            // update status and show a debug message to confirm loading
            try
            {
                var count = myMenu.GetSongCount();
                StatusText.Text = $"Songs loaded: {count}";
                DebugBanner.Text = $"MainWindow initialized - Songs: {count}";
                if (count > 0)
                {
                    SongListBox.SelectedIndex = 0;
                }
                else
                {
                    // No songs found in Music folder — try the configured ConsoleSongsFolder before launching console
                    var consoleFolder = Config.ConsoleSongsFolder;
                    StatusText.Text = $"No songs found in Music folder — trying configured folder: {consoleFolder}";
                    try
                    {
                        myMenu.LoadSongs(consoleFolder);
                        LoadSongs();
                    }
                    catch { }

                    var newCount = myMenu.GetSongCount();
                    if (newCount > 0)
                    {
                        StatusText.Text = $"Songs loaded from configured folder: {consoleFolder} ({newCount})";
                        SongListBox.SelectedIndex = 0;
                    }
                    else
                    {
                        // still no songs — run the console program logic on a background thread
                        StatusText.Text = $"No songs found in Music or configured folder ({consoleFolder}) — launching console mode.";
                        Task.Run(() =>
                        {
                            try
                            {
                                try { System.IO.File.AppendAllText(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "MyPersonalDj_console.log"), $"Allocating console at {DateTime.Now}\n"); } catch { }
                                AllocConsole();
                                // ensure console IO is available to the managed Console APIs
                                var standardOut = System.Console.OpenStandardOutput();
                                var standardIn = System.Console.OpenStandardInput();
                                using (var writer = new System.IO.StreamWriter(standardOut) { AutoFlush = true })
                                using (var reader = new System.IO.StreamReader(standardIn))
                                {
                                    System.Console.SetOut(writer);
                                    System.Console.SetIn(reader);
                                    Program.RunConsole(new string[0]);
                                }
                            }
                            catch
                            {
                                // ignore errors from console mode
                            }
                        });

                        MessageBox.Show($"No songs found in Music or configured folder ({consoleFolder}). Console mode has been started in the background.", "Debug");
                    }
                }
            }
            catch { }
        }
        private void LoadSongs()
        {
            var songs = myMenu.getLibrary();
            SongListBox.ItemsSource = songs;
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            try { _logWatcherCts?.Cancel(); } catch { }
        }

        private void StartLogWatcher(System.Threading.CancellationToken token)
        {
            var logPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "MyPersonalDj_console.log");
            Task.Run(async () =>
            {
                long lastLen = 0;
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        if (System.IO.File.Exists(logPath))
                        {
                            var fi = new System.IO.FileInfo(logPath);
                            if (fi.Length > lastLen)
                            {
                                using (var fs = System.IO.File.Open(logPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
                                using (var sr = new System.IO.StreamReader(fs))
                                {
                                    fs.Seek(lastLen, System.IO.SeekOrigin.Begin);
                                    var newText = await sr.ReadToEndAsync();
                                    lastLen = fs.Length;
                                    if (!string.IsNullOrEmpty(newText))
                                    {
                                        Dispatcher.Invoke(() =>
                                        {
                                            ConsoleLogText.AppendText(newText);
                                            ConsoleLogText.ScrollToEnd();
                                        });
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                    await Task.Delay(500, token).ContinueWith(_ => { });
                }
            }, token);
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if(SongListBox.SelectedItem != null)
            {
                string selected = SongListBox.SelectedItem.ToString();
                myMenu.playSong(selected);
            }
        }
    }

}