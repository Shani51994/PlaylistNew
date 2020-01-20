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


namespace PlayListNew.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class playlistOptions : Window
    {

        public playlistOptions()
        {
           InitializeComponent();
            connectionStr.Text = User.Instance.Email + " is connected";
        }
       

        private void createPlaylistWin(object sender, RoutedEventArgs e)
        {
            createPlaylist createPl = new createPlaylist();
            this.Close();
            createPl.Show();
           
        }

        private void ShowAllPlaylistWin(object sender, RoutedEventArgs e)
        {
            ShowPlaylists showAllPls = new ShowPlaylists();
            this.Close();
            showAllPls.Show();
        }


        private void backToHomePage(object sender, RoutedEventArgs e)
        {
            Home homePage = new Home();
            this.Close();
            homePage.Show();
        }


       
        private void ShowChooseFriends(object sender, RoutedEventArgs e)
        {
            chooseFriend friends = new chooseFriend();
            this.Close();
            friends.Show();
        }
        
    }
}
