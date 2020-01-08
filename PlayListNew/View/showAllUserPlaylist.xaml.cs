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
    /// Interaction logic for showAllUserPlaylist.xaml
    /// </summary>
    public partial class showAllUserPlaylist : Window
    {
        public showAllUserPlaylist()
        {
            InitializeComponent();
        }
        
       /*
        /// <summary>
        /// Handles the Click event of the Show songs button.
        /// </summary>
        private void OpenPlaylist_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int playlistId = (int)button.CommandParameter;
            PlaylistView playlistView = new PlaylistView(this.viewModel.VM_PlaylistsDic, playlistId);
            playlistView.ShowDialog();
        }

        /// <summary>
        /// Handles the PreviewMouseWheel event of the ScrollViewer control.
        /// Makes the table to scroll properly.
        /// </summary>
        /// 

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer svc = (ScrollViewer)sender;
            svc.ScrollToVerticalOffset(svc.VerticalOffset - e.Delta);
            e.Handled = true;
        }

    */
   
        
    }
}




