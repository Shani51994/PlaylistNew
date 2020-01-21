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
    /// Interaction logic for ShowFriendsPlaylists.xaml
    /// </summary>
    /// 


    public partial class ShowFriendsPlaylists : Window
    {

        List<string> emails;
        public ShowFriendsPlaylists(List<string> friendEmails)
        {
            emails = friendEmails;
            InitializeComponent();
            showCurrentPlaylist(friendEmails);
        }

        /// <summary>
        /// Handles the Click event of the Show songs button.
        /// </summary>
        private void pressOpenPlaylist(object sender, RoutedEventArgs e)
        {
           
            Button button = (Button)sender;
            int playlistId = (int)button.CommandParameter;
            showPlaylistSongs playlistSongs = new showPlaylistSongs(playlistId, false);
            playlistSongs.Show();
           
        }

        public void pressGoBack(object sender, RoutedEventArgs e)
        {
            playlistOptions options = new playlistOptions();
            this.Close();
            options.Show();
        }


        public void showCurrentPlaylist(List<string> friendEmails)
        {
           
            DataBaseHandler dbHandler = DataBaseHandler.Instance;
            ObservableCollection<Playlist> playlistList = dbHandler.GetFriendsPlaylist(friendEmails);
            dataGrid1.ItemsSource = playlistList;
        }



        //needToFix!!!!!!!!
        private void pressCopyPlaylist(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            int playlistId = (int)button.CommandParameter;

            DataBaseHandler.Instance.copyPlaylist(playlistId);
        }

    }
}
