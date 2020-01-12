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
        public int playlistIdG;

        public void showCurrentSongs()
        {
            DataBaseHandler dbHandler = DataBaseHandler.Instance;
            ObservableCollection<Song> songList = dbHandler.GetPlaylistSongs(playlistIdG);
            dataGrid1.ItemsSource = songList;
        }


        public showPlaylistSongs(int playlistId)
        {
            InitializeComponent();
            playlistIdG = playlistId;
            showCurrentSongs();
        }


        private void pressDeletePlaylist(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int songId = (int)button.CommandParameter;

            DataBaseHandler.Instance.deleteSong(songId);

            showCurrentSongs();
        }



        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
