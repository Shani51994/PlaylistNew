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
            createPl.Show();
          //  this.Close();
        }

        private void ShowAllPlaylistWin(object sender, RoutedEventArgs e)
        {
            showAllUserPlaylist showAllPls = new showAllUserPlaylist();
            showAllPls.Show();
        }



    }
}
