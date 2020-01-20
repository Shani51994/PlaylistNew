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

namespace PlayListNew.View
{
    /// <summary>
    /// Interaction logic for createPlSucceed.xaml
    /// </summary>
    public partial class createPlSucceed : Window
    {
        int playlistID;
        public createPlSucceed(int playlistId)
        {
            InitializeComponent();
            playlistID = playlistId;
        }

        public void pressBackToOptions(object sender, RoutedEventArgs e)
        {

            foreach (Window window in Application.Current.Windows.OfType<createPlaylist>())
                ((createPlaylist)window).Close();

            playlistOptions options = new playlistOptions();

            this.Close();
            options.Show();
        }

        public void pressShowSongs(object sender, RoutedEventArgs e)
        {
            showPlaylistSongs playlistSongs = new showPlaylistSongs(playlistID);
            this.Close();
            playlistSongs.Show();
        }
    }
}