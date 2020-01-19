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


        /*
         * Get friends emails, if there is no plailist to show - pop message, else show list of friends playlist
         */
        public void pressCreate(object sender, RoutedEventArgs e)
        {
            List<string> NameArr = new List<string>(new string[] { "firstEmail", "secEmail", "thirdEmail", "fourEmail", "fifthEmail" });
            List<string> friendEmails = new List<string>();

            TextBox textbox;

            foreach (string fieldName in NameArr)
            {
                textbox = (TextBox)FindName(fieldName);
                if (textbox.Text != "")
                {
                    friendEmails.Add(textbox.Text);
                }
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
