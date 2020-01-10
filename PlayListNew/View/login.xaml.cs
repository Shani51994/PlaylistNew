using PlayListNew.DB;
using PlayListNew.Entities;
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
using PlayListNew.View;


namespace PlayListNew.View
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
            DataBaseHandler dbhandler = DataBaseHandler.Instance;
            int ans = dbhandler.checkIfUserExistAndPasswordRight(emailText.Text, passwordText.Text);

            if (ans == 1)
            {
                User currentUser = User.Instance;
                currentUser.Email = emailText.Text;
                
                foreach (Window window in Application.Current.Windows.OfType<Home>())
                    ((Home)window).Close();

                playlistOptions options = new playlistOptions();

                this.Close();
                options.Show();
            }
            else
            {
                emailText.Text = "";
                passwordText.Text = "";
                this.wrongDetails.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// Handles the Click event of the Cancel button.
        /// </summary>
        public void Clear_Click(object sender, RoutedEventArgs e)
        {
            emailText.Text = "";
            passwordText.Text = "";
        }
    }
    
}
