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

namespace PlaylistNew.View
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// Handles the Click event of the Submit Button.
        /// </summary>
        public void clickSubmit(object sender, RoutedEventArgs e)
        {

            DataBaseHandler dbhandler = new DataBaseHandler("localhost", "playlistGame", "Mm1614113", "root");
            /*
            int ans = dbhandler.checkIfUserExistAndPasswordRight(emailText.Text);

            if (ans == 0)
            {
                dbhandler.SaveUserData(emailText.Text, passwordText.Text, fullNameText.Text);
            }
            else
            {
                this.userExist.Visibility = Visibility.Visible;
            }
            */

        }

        /// <summary>
        /// Handles the Click event of the Cancel button.
        /// </summary>
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
        }
    }
    
}
