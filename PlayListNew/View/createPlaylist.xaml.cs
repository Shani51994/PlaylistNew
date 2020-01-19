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

            checkBox70.IsChecked = false;
            checkBox80.IsChecked = false;
            checkBox90.IsChecked = false;
            checkBox00.IsChecked = false;
            dontCareDec.IsChecked = false;
            dontCarePop.IsChecked = false;
            this.isDontCareDecChoosed = false;
            this.isDontCarePopChoosed = false;
            this.decadesRanges.Clear();
            this.numOfDecChoosed = 0;

            this.message.Text = "";
        }

        public string createQuery(DataBaseHandler dbhandler)
        {
            
            // get all values inserted by the user
            this.duration = (int)durationSlider.Value * 60;
            this.playlistName = playListName.Text;

            if (songsNum.Text == "" || tempoMin.Text == "" || tempoMax.Text == "" ||
                loudnessMin.Text == "" || loudnessMax.Text == "")
            {
                this.clearScreen();
                message.Text = "One or more fields are incorrect. Please try again.";
                return "0";
            }
            else
            {
                this.minTempoRange = double.Parse(tempoMin.Text);
                this.maxTempoRange = double.Parse(tempoMax.Text);
                this.minLoudnessRange = double.Parse(loudnessMin.Text);
                this.maxLoudnessRange = double.Parse(loudnessMax.Text);
                this.numOfSongs = int.Parse(songsNum.Text);
            }
            

            // if user choosed zero songs in playlist
            if (this.numOfSongs == 0)
            {
                message.Text = "choose one or more songs in Song num field";
                return "0";
            }

            if (dbhandler.checkIfPlNameExsistToUser(playlistName) == 1)
            {
                this.clearScreen();
                message.Text = "you have playlist with the same name already";
                return "0";
            }

            // prints appropriate message if user didnt choose all fields or choosed wrong values (out of range)
            if (this.minLoudnessRange < -52 || this.maxLoudnessRange > 1 || this.minTempoRange < 0 ||
                this.maxTempoRange > 262 || this.numOfSongs < 0 || this.numOfSongs > 30 ||
                this.playlistName == "" || (this.numOfDecChoosed == 0 && !this.isDontCareDecChoosed))
            {
                this.clearScreen();
                message.Text = "One or more fields are incorrect. Please try again.";
                return "0";
            }


            query = "SELECT songs.id FROM playlistgame.songs " +
                    "WHERE songs.tempo >= " + this.minTempoRange.ToString() +
                    " AND songs.tempo <= " + this.maxTempoRange.ToString() +
                    " AND songs.loudness >= " + this.minLoudnessRange.ToString() +
                    " AND songs.loudness <= " + this.maxLoudnessRange.ToString();

            if (!this.isDontCarePopChoosed)
            {
                this.popularity = (double)popularitySlider.Value;
                query += " AND songs.hotness >= 0 AND songs.hotness <= " + this.popularity.ToString();
            }

            // deals with all variations of options in order to know which query to run
            if (!this.isDontCareDecChoosed)
            {
                if (this.numOfDecChoosed >= 1)
                {
                    query += " AND (songs.year >= " + this.decadesRanges[0].ToString() +
                             " AND songs.year < " + this.decadesRanges[1].ToString() + ")";

                }
                if (this.numOfDecChoosed >= 2)
                {
                    query += " OR(songs.year >= " + this.decadesRanges[2].ToString() +
                             " AND songs.year < " + this.decadesRanges[3].ToString() + ")";
                }
                if (this.numOfDecChoosed >= 3)
                {
                    query += " OR(songs.year >= " + this.decadesRanges[4].ToString() +
                             " AND songs.year < " + this.decadesRanges[5].ToString() + ")";
                }
                if (this.numOfDecChoosed == 4)
                {
                    query += " OR(songs.year >= " + this.decadesRanges[6].ToString() +
                             " AND songs.year < " + this.decadesRanges[7].ToString() + ")";
                }
            }

            query += " AND (songs.duration >= 0 AND songs.duration < " + this.duration.ToString() + ")" +
                     " ORDER BY RAND()" +
                     " LIMIT " + this.numOfSongs.ToString();

            return query;

        }

        // function for create a playlist according to the user's requests
        public void pressCreate(object sender, RoutedEventArgs e)
        {
            DataBaseHandler dbhandler = DataBaseHandler.Instance;
            string query = createQuery(dbhandler); 
            // if query equals "0" then user filter wasnt good 
            if (query == "0")
            {
                return;
            }
            

            // get all songs ids according to the user's request
            List<string> songsIds = dbhandler.getSongsIds(query);

            // if no songs found
            if (songsIds.Count == 0)
            {
                message.Text = "No songs where found for choices!";
                return;
            }
            
            // insert the playlist name into the playlists table
            dbhandler.saveNewPlaylistName(this.playlistName);


            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! not good !!!!!!

            // get the id of the new playlist
            string playlistId = dbhandler.getPlaylistId(this.playlistName);


            dbhandler.createPlaylist(songsIds, playlistId);


            // get the current user id
            string userId = User.Instance.Id.ToString();

            // insert the playlist id and the current user id into the user_to_playlists table
            dbhandler.saveNewPlaylisUser(playlistId, userId);

            this.clearScreen();
            
            // move to pop window!
           /// message.Text = "Your playlist succesfully created!";
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