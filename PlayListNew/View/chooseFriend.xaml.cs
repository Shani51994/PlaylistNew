using PlayListNew.DB;
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
    /// Interaction logic for chooseFriend.xaml
    /// </summary>
    public partial class chooseFriend : Window
    {
        public chooseFriend()
        {
            InitializeComponent();
        }

        public void pressGoBack(object sender, RoutedEventArgs e)
        {
            playlistOptions options = new playlistOptions();
            this.Close();
            options.Show();
        }

        public void pressCreate(object sender, RoutedEventArgs e)
        {
            //List<string> NameArr = ["firstEmail", "secEmail", "thirdEmail", "fourEmail", "fifthEmail"];

            List<string> friendEmails = new List<string>();


            // try to fix!!! -put in array and then check this condition-
            if(this.firstEmail.Text != "")
            {
                friendEmails.Add(this.firstEmail.Text);
            }

            if (this.secEmail.Text != "")
            {
                friendEmails.Add(this.secEmail.Text);
            }

            if (this.thirdEmail.Text != "")
            {
                friendEmails.Add(this.thirdEmail.Text);
            }


            if (this.fourEmail.Text != "")
            {
                friendEmails.Add(this.fourEmail.Text);
            }


            if (this.fifthEmail.Text != "")
            {
                friendEmails.Add(this.fifthEmail.Text);
            }


            DataBaseHandler dbHandler = DataBaseHandler.Instance;
            int numOfFriendsPlaylists = dbHandler.countFriendPlaylist(friendEmails);


            if (numOfFriendsPlaylists == 0)
            {
                // show error that no playlist found
                this.message.Text = "no plalists to show";

            }
            else
            {
                // show friends playlist window
                ShowFriendsPlaylists showAllPls = new ShowFriendsPlaylists(friendEmails);
                this.Close();
                showAllPls.Show();

            }



        }
    }
}
