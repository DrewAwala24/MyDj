using MyPersonalDj;
using System.Text;
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
        songmenu myMenu = new songmenu();
        public MainWindow()
        {
            InitializeComponent();

            SongListBox.ItemsSource = (System.Collections.IEnumerable)myMenu.getLibrary();
        }

    }

}