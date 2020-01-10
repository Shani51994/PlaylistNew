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
    public partial class ShowPlaylists : Window
    {
        public ShowPlaylists()
        {
            InitializeComponent();
            DataBaseHandler dbHandler = DataBaseHandler.Instance;
            
            ObservableCollection<Playlist> playlistList = dbHandler.GetAllUserPlaylist();

            //ObservableCollection<Song> songList = new ObservableCollection<Song>();
            //songList.Add(new Song() { SongName = "maa", ArtistName = "pp", AlbumName = "sh" });

            dataGrid1.ItemsSource = playlistList;
        }

        /// <summary>
        /// Handles the Click event of the Show songs button.
        /// </summary>
        private void OpenPlaylist_Click(object sender, RoutedEventArgs e)
        {
           
            Button button = (Button)sender;
            int playlistId = (int)button.CommandParameter;
            showPlaylistSongs playlistSongs = new showPlaylistSongs(playlistId);
            playlistSongs.Show();
           
        }
    }
}
