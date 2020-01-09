using System;
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
using PlayListNew.Entities;
using PlayListNew.DB;
using System.Collections.ObjectModel;

namespace PlayListNew.View
{
    /// <summary>
    /// Interaction logic for showPlaylistSongs.xaml
    /// </summary>
    public partial class showPlaylistSongs : Window
    {
        public showPlaylistSongs()
        {
            InitializeComponent();
            DataBaseHandler dbHandler = DataBaseHandler.Instance;

            int playlistId = 11;
            //List<Song> songList = DataBaseHandler.GetPlaylistSongs(playlistId);
            ObservableCollection<Song> songList=  new ObservableCollection<Song>();
            songList.Add(new Song() { songName = "maa", artistName = "pp", albumName = "sh" });
            
         //   dataGrid1.ItemsSource = songList;

        }

    }
}
