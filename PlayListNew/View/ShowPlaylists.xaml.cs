using PlayListNew.DB;
using PlayListNew.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PlayListNew.View
{
    /// <summary>
    /// Interaction logic for ShowPlaylists.xaml
    /// </summary>
    /// 

        
    public partial class ShowPlaylists : Window
    {
        public ShowPlaylists()
        {
            InitializeComponent();
            showCurrentPlaylist();
        }

        /// <summary>
        /// Handles the Click event of the Show songs button.
        /// </summary>
        private void pressOpenPlaylist(object sender, RoutedEventArgs e)
        {
           
            Button button = (Button)sender;
            int playlistId = (int)button.CommandParameter;

            // show songs of the playlist
            showPlaylistSongs playlistSongs = new showPlaylistSongs(playlistId, true);
            playlistSongs.Show();
           
        }

        public void pressGoBack(object sender, RoutedEventArgs e)
        {
            playlistOptions options = new playlistOptions();
            this.Close();
            options.Show();
        }


        // make query of all playlists in playlist and update the dataGrid in the window
        public void showCurrentPlaylist()
        {
           
            DataBaseHandler dbHandler = DataBaseHandler.Instance;
            ObservableCollection<Playlist> playlistList = dbHandler.GetAllUserPlaylist();
            dataGrid1.ItemsSource = playlistList;
        }

        private void pressDeletePlaylist(object sender, RoutedEventArgs e)
        {
            // take playlistId from CommandParameterin button 
            Button button = (Button)sender;
            int playlistId = (int)button.CommandParameter;

            // delete the playlist 
            DataBaseHandler.Instance.deletePlaylist(playlistId);

            // refresh playlist list
            showCurrentPlaylist();

        }

    }
}
