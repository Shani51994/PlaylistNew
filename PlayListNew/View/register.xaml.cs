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
using PlayListNew.DB;

namespace PlaylistNew.View
{
    /// <summary>
    /// Interaction logic for homeScreen.xaml
    /// </summary>
    public partial class register : Window
    {
        public register()
        {
           InitializeComponent();
        }
        

        /// <summary>
        /// Handles the Click event of the Submit button.
        /// </summary>
        public void registerClick(object sender, RoutedEventArgs e)
        {
            DataBaseHandler dbhandler = new DataBaseHandler("localhost", "playlistGame", "Mm1614113", "root");
            int ans = dbhandler.checkIfUserExist(emailText.Text);
            if (ans == 0)
            {
                dbhandler.SaveUserData(emailText.Text, passwordText.Text, fullNameText.Text);
            }
            else
            {
                this.userExist.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Handles the Click event of the Cancel button.
        /// </summary>
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            /*
            this.welcomeScreen.UserName = "";
            this.welcomeScreen.Password = "";
            this.welcomeScreen.Email = "";
            this.Close();
            */
        }
    }

}

