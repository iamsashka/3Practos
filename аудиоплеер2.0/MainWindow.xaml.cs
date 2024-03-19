using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using аудиоплеер2._0;

namespace AudioPlayer
{
    
    public partial class MainWindow : Window
    {
        private string _directoryPath;
        private int _currentIndex;
        private bool _shuffle;
        private bool _repeat;

        MediaPlayer mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            _shuffle = false;
            _repeat = false;
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _directoryPath = dialog.SelectedPath;
                List<string> files = Directory.GetFiles(_directoryPath, "*.mp3").ToList();
                files.AddRange(Directory.GetFiles(_directoryPath, "*.m4a"));
                files.AddRange(Directory.GetFiles(_directoryPath, "*.wav"));
                MediaList.ItemsSource = files;
                if (MediaList.Items.Count > 0)
                {
                    _currentIndex = 0;
                    media.Source = new Uri(MediaList.Items[_currentIndex].ToString());
                    media.Play();
                }
            }
        }

        public List<string> GetMusicFiles()
        {
            return MediaList.ItemsSource as List<string>;
        }

        private void history_click(object sender, RoutedEventArgs e)
        {
            история historyWindow = new история(GetMusicFiles());
            historyWindow.ShowDialog();
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            SliderMusic.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
            Timer.Text = TimeSpan.FromSeconds(media.Position.TotalSeconds).ToString(@"hh\:mm\:ss");
            TimerLeft.Text = TimeSpan.FromSeconds(media.NaturalDuration.TimeSpan.TotalSeconds - media.Position.TotalSeconds).ToString(@"hh\:mm\:ss");
        }

        private void SliderMusic_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Position = TimeSpan.FromSeconds(SliderMusic.Value);
            Timer.Text = TimeSpan.FromSeconds(media.Position.TotalSeconds).ToString(@"hh\:mm\:ss");
            TimerLeft.Text = TimeSpan.FromSeconds(media.NaturalDuration.TimeSpan.TotalSeconds - media.Position.TotalSeconds).ToString(@"hh\:mm\:ss");
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = SliderVolume.Value / 10;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            media.Play();

        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            SliderMusic.Value = 0;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex < MediaList.Items.Count - 1)
            {
                _currentIndex++;
                media.Source = new Uri(MediaList.Items[_currentIndex].ToString());
                media.Play();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                media.Source = new Uri(MediaList.Items[_currentIndex].ToString());
                media.Play();
            }
        }

        private void shuffleButton_Click(object sender, RoutedEventArgs e)
        {
            _shuffle = !_shuffle;
            if (_shuffle)
            {
                var files = Directory.GetFiles(_directoryPath, "*.mp3");
                var randomFiles = new System.Collections.Generic.List<string>(files);
                System.Random rnd = new System.Random();
                for (int i = randomFiles.Count - 1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    var temp = randomFiles[i];
                    randomFiles[i] = randomFiles[j];
                    randomFiles[j] = temp;
                }
                MediaList.ItemsSource = randomFiles;
                media.Source = new Uri(MediaList.Items[_currentIndex].ToString());
                media.Play();
            }
            else
            {
                MediaList.ItemsSource = Directory.GetFiles(_directoryPath, "*.mp3");
                media.Source = new Uri(MediaList.Items[_currentIndex].ToString());
                media.Play();
            }
        }

        private void repeatButton_Click(object sender, RoutedEventArgs e)
        {
            _repeat = !_repeat;
            if (_repeat)
            {
                repeatButton.Content = "Повтор";
            }
            else
            {
                repeatButton.Content = "Отмена";
            }
        }
    }
}