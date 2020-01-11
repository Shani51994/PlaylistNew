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
using MySql.Data.MySqlClient;
using PlayListNew.Entities;
using PlayListNew.DB;

namespace PlayListNew.View
{
    
    
    /// <summary>
    /// Interaction logic for createPlaylist.xaml
    /// </summary>
    public partial class createPlaylist : Window
    {
        // variables for all user's selections
        private double popularity;
        private double minTempoRange;
        private double maxTempoRange;
        private double minLoudnessRange;
        private double maxLoudnessRange;
        private int minDecadeRange;
        private int maxDecadeRange;
        private int duration;
        private int numOfSongs;
        private string playlistName;
        private bool isDontCareDecChoosed;
        private bool isDontCarePopChoosed;
        private int numOfDecChoosed = 0;
        private List<int> decadesRanges = new List<int>();
        private string query;

        public createPlaylist()
        {
            InitializeComponent();
        }

        // function for create a playlist according to the user's requests
        public void pressCreate(object sender, RoutedEventArgs e)
        {

            // get all values inserted by the user
            this.minTempoRange = double.Parse(tempoMin.Text);
            this.maxTempoRange = double.Parse(tempoMax.Text);
            this.minLoudnessRange = double.Parse(loudnessMin.Text);
            this.maxLoudnessRange = double.Parse(loudnessMax.Text);
            this.duration = (int)durationSlider.Value * 60;
            this.numOfSongs = int.Parse(songsNum.Text);
            this.playlistName = playListName.Text;

            // if user choosed zero songs in playlist
            if (this.numOfSongs == 0)
            {
                return;
            }

            // if user insert invalis values (out of the valid range), fix it
            // to be the minimum or maximum range
            if (this.minTempoRange < 0)
            {
                this.minTempoRange = 0;
            }

            if (this.maxTempoRange > 262)
            {
                this.maxTempoRange = 262;
            }

            if (this.minLoudnessRange < -52)
            {
                this.minLoudnessRange = -52;
            }

            if (this.maxLoudnessRange > 1)
            {
                this.maxLoudnessRange = 1;
            }

            if (this.numOfSongs < 0)
            {
                this.numOfSongs = 30;
            }

            if (this.numOfSongs > 30)
            {
                this.numOfSongs = 30;
            }


            // insert the playlist name into the playlists table

            // get the id of the new playlist


            // checks how many and which checkboxes of decades user choosed
            if (this.isDontCareDecChoosed)
            {
                this.minDecadeRange = 1970;
                this.maxDecadeRange = 2009;
            } else
            {
                // send what in the list
            }

            // checks if user wants popularity
            if (this.isDontCarePopChoosed)
            {
                this.popularity = 1.0;
            }
            else
            {
                this.popularity = (double)popularitySlider.Value;
            }


            DataBaseHandler dbhandler = DataBaseHandler.Instance;

            query = string.Format(queries.insertNewPlaylist, this.playlistName);

            dbhandler.saveNewPlaylistName(query);

            query = string.Format(queries.getPlaylistIdByName, this.playlistName);

            string playlistId = dbhandler.getPlaylistId(query);

            query = string.Format(queries.getSongsIds, this.minTempoRange, this.maxTempoRange,
                this.minLoudnessRange, this.maxLoudnessRange, this.popularity, this.minDecadeRange,
                this.maxDecadeRange, this.duration);

            List<string> songsIds = dbhandler.getSongsIds(query);

            for (int i = 0; i < songsIds.Count; i++)
            {
                query = string.Format(queries.creartPlaylist, playlistId, songsIds[i]);
                dbhandler.createPlaylist(query);
            }

        }

        // function for check if user choosed songs from '70 decade
        private void choose70(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1970);
            this.decadesRanges.Add(1979);
            this.numOfDecChoosed++;

        }

        // function for check if user choosed songs from '80 decade
        private void choose80(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1980);
            this.decadesRanges.Add(1989);
            this.numOfDecChoosed++;
            
        }

        // function for check if user choosed songs from '90 decade
        private void choose90(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1990);
            this.decadesRanges.Add(1999);
            this.numOfDecChoosed++;
        }

        // function for check if user choosed songs from '00 decade
        private void choose00(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(2000);
            this.decadesRanges.Add(2009);
            this.numOfDecChoosed++;
        }

        // function for check if user choosed songs from all decades
        private void chooseAllDec(object sender, RoutedEventArgs e)
        {
            this.isDontCareDecChoosed = true;
            numOfDecChoosed = 0;
        }

        // function for check if user choosed songs without popularity priority
        private void chooseAllPop(object sender, RoutedEventArgs e)
        {
            this.isDontCarePopChoosed = true;
        }

    }
}