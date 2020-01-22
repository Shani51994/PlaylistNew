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

namespace PlayListNew.View
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
            // checks fields are not empty
            if (emailText.Text == "" || passwordText.Text=="" || fullNameText.Text == "")
            {
                this.message.Text = "fill all fileds please";
                return;
            }
            
            DataBaseHandler dbhandler = DataBaseHandler.Instance;
            int ans = dbhandler.checkIfUserExist(emailText.Text);

            // if ans ==0 then user email not used in past 
            if (ans == 0)
            {
                dbhandler.SaveUserData(emailText.Text, passwordText.Text, fullNameText.Text);
                this.Close();
            }
            else
            {
                emailText.Text = "";
                passwordText.Text = "";
                fullNameText.Text = "";
                this.message.Text = "there is a user with this email already";
            }
        }

        /// <summary>
        /// Handles the Click event of the Cancel button.
        /// </summary>
        public void Clear_Click(object sender, RoutedEventArgs e)
        {
            emailText.Text = "";
            passwordText.Text = "";
            fullNameText.Text = "";
        }
    }

}

