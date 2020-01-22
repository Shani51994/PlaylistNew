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
        public Boolean belongToPlayerG;

        // make query of all songs in playlist and update the dataGrid in the window
        public void showCurrentSongs()
        {
            DataBaseHandler dbHandler = DataBaseHandler.Instance;
            ObservableCollection<Song> songList = dbHandler.GetPlaylistSongs(playlistIdG, belongToPlayerG);
            dataGrid1.ItemsSource = songList;
        }


        public showPlaylistSongs(int playlistId, Boolean belongToPlayer)
        {
            InitializeComponent();
            playlistIdG = playlistId;
            belongToPlayerG = belongToPlayer;
            showCurrentSongs();
        }


        private void pressDeleteSong(object sender, RoutedEventArgs e)
        {
            
            Button button = (Button)sender;
            int songId = (int)button.CommandParameter;
            // delete the song 
            DataBaseHandler.Instance.deleteSong(songId, playlistIdG);

            // check if num of songs in playlist equalls to zero, if yes - delete the playlist
            DataBaseHandler.Instance.checksAfterdeletingSongs(playlistIdG);

            // refresh - the song list
            showCurrentSongs();
        }



        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
