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
        private int duration;
        private int numOfSongs;
        private string playlistName;
        private bool isDontCareDecChoosed = false;
        private bool isDontCarePopChoosed = false;
        private int numOfDecChoosed = 0;
        private List<int> decadesRanges = new List<int>();
        private string query;
        private bool isChoosed70 = false;
        private bool isChoosed80 = false;
        private bool isChoosed90 = false;
        private bool isChoosed00 = false;

        public createPlaylist()
        {
            InitializeComponent();
        }

        // function for clear screen from user's reqauests
        public void clearScreen()
        {
            // initial all values inserted by the user
            tempoMin.Text = "0";
            tempoMax.Text = "262";
            loudnessMin.Text = "-52";
            loudnessMax.Text = "1";
            this.duration = (int)durationSlider.Value * 60;
            songsNum.Clear();
            playListName.Clear();
            durationSlider.Value = 2;
            popularitySlider.Value = 0;

            if (this.isChoosed70)
            {
                checkBox70.IsChecked = false;
            }

            if (this.isChoosed80)
            {
                checkBox80.IsChecked = false;
            }

            if (this.isChoosed90)
            {
                checkBox90.IsChecked = false;
            }

            if (this.isChoosed00)
            {
                checkBox00.IsChecked = false;
            }

            if (this.isDontCareDecChoosed)
            {
                dontCareDec.IsChecked = false;
            }

            if (this.isDontCarePopChoosed)
            {
                dontCarePop.IsChecked = false;
            }

            // initial all variables
            this.isChoosed70 = false;
            this.isChoosed80 = false;
            this.isChoosed90 = false;
            this.isChoosed00 = false;
            this.isDontCareDecChoosed = false;
            this.isDontCarePopChoosed = false;
            this.decadesRanges.Clear();
            this.numOfDecChoosed = 0;
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

            query = "SELECT songs.id FROM playlistgame.songs " +
                    "WHERE songs.tempo >= " + this.minTempoRange.ToString() +
                    " AND songs.tempo <= " + this.maxTempoRange.ToString() +
                    " AND songs.loudness >= " + this.minLoudnessRange.ToString() +
                    " AND songs.loudness <= " + this.maxLoudnessRange.ToString();

            // checks if user wants specific decades
            //if (this.isDontCareDecChoosed)
            //{
            //    this.isDontCareDecChoosed = true;
            //}

            // checks if user wants popularity
            //if (this.isDontCarePopChoosed)
            //{
            //    this.isDontCarePopChoosed = true;
            //}
            //else
            if (!this.isDontCarePopChoosed)
            {
                this.popularity = (double)popularitySlider.Value;
                query += " AND songs.hotness >= 0 AND songs.hotness <= " + this.popularity.ToString();
            }

            // deals with all variations of options in order to know which query to run
            if (!this.isDontCareDecChoosed)
            {
                if (this.numOfDecChoosed == 1)
                {
                    query += " AND(songs.year >= " + this.decadesRanges[0].ToString() +
                             " AND songs.year < " + this.decadesRanges[1].ToString() + ")";

                }
                else if (this.numOfDecChoosed == 2)
                {
                    query += " AND((songs.year >= " + this.decadesRanges[0].ToString() +
                             " AND songs.year < " + this.decadesRanges[1].ToString() + ")" +
                             " OR(songs.year >= " + this.decadesRanges[2].ToString() +
                             " AND songs.year < " + this.decadesRanges[3].ToString() + "))";
                }
                else if (this.numOfDecChoosed == 3)
                {
                    query += " AND((songs.year >= " + this.decadesRanges[0].ToString() +
                             " AND songs.year < " + this.decadesRanges[1].ToString() + ")" +
                             " OR(songs.year >= " + this.decadesRanges[2].ToString() +
                             " AND songs.year < " + this.decadesRanges[3].ToString() + ")" +
                             " OR(songs.year >= " + this.decadesRanges[4].ToString() +
                             " AND songs.year < " + this.decadesRanges[5].ToString() + "))";
                }
                else if (this.numOfDecChoosed == 4)
                {
                    query += " AND((songs.year >= " + this.decadesRanges[0].ToString() +
                             " AND songs.year < " + this.decadesRanges[1].ToString() + ")" +
                             " OR(songs.year >= " + this.decadesRanges[2].ToString() +
                             " AND songs.year < " + this.decadesRanges[3].ToString() + ")" +
                             " OR(songs.year >= " + this.decadesRanges[4].ToString() +
                             " AND songs.year < " + this.decadesRanges[5].ToString() + ")" +
                             " OR(songs.year >= " + this.decadesRanges[6].ToString() +
                             " AND songs.year < " + this.decadesRanges[7].ToString() + "))";
                }
            }

            query += " AND (songs.duration >= 0 AND songs.duration < " + this.duration.ToString() + ")" +
                     " ORDER BY RAND()" +
                     " LIMIT " + this.numOfSongs.ToString();

            DataBaseHandler dbhandler = DataBaseHandler.Instance;

            // insert the playlist name into the playlists table
            dbhandler.saveNewPlaylistName(this.playlistName);

            // get the id of the new playlist
            string playlistId = dbhandler.getPlaylistId(this.playlistName);

            // get all songs ids according to the user's request
            List<string> songsIds = dbhandler.getSongsIds(query);

            // insert every song to the platlists table
            for (int i = 0; i < songsIds.Count; i++)
            {
                query = string.Format(queries.creartPlaylist, playlistId, songsIds[i]);
                dbhandler.createPlaylist(query);
            }

            // if no songs found
            if (songsIds.Count == 0)
            {
                dbhandler.deletePlaylist(int.Parse(playlistId));
                return;
            }

            // get the current user id
            string userId = User.Instance.Id.ToString();

            // insert the playlist id and the current user id into the user_to_playlists table
            dbhandler.saveNewPlaylisUser(playlistId, userId);

            this.clearScreen();

            message.Text = "Your playlist succesfully created!";

            //System.Threading.Thread.Sleep(5000);

        }

        // function for check if user choosed songs from '70 decade
        private void choose70(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1970);
            this.decadesRanges.Add(1979);
            this.numOfDecChoosed++;
            this.isChoosed70 = true;
        }

        // function for check if user choosed songs from '80 decade
        private void choose80(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1980);
            this.decadesRanges.Add(1989);
            this.numOfDecChoosed++;
            this.isChoosed80 = true;
        }

        // function for check if user choosed songs from '90 decade
        private void choose90(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1990);
            this.decadesRanges.Add(1999);
            this.numOfDecChoosed++;
            this.isChoosed90 = true;
        }

        // function for check if user choosed songs from '00 decade
        private void choose00(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(2000);
            this.decadesRanges.Add(2009);
            this.numOfDecChoosed++;
            this.isChoosed00 = true;
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

        public void pressGoBack(object sender, RoutedEventArgs e)
        {
            playlistOptions options = new playlistOptions();
            this.Close();
            options.Show();
        }

        public void pressClear(object sender, RoutedEventArgs e)
        {
            this.clearScreen();
        }

    }
}