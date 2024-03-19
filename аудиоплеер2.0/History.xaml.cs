using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace аудиоплеер2._0
{
    /// <summary>
    /// Логика взаимодействия для история.xaml
    /// </summary>
    public partial class история : Window

    {
        private IEnumerable<string> musicFiles;
        public история(List<string> musicFiles)
        {
            musicFiles = musicFiles;
            InitializeComponent();
            Tresh.ItemsSource = musicFiles;
        }

        public List<string> MusicFiles { get; private set; }
    }
}

