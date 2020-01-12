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


            // checks if user wants specific decades
            if (this.isDontCareDecChoosed)
            {
                this.isDontCareDecChoosed = true;
            }

            // checks if user wants popularity
            if (this.isDontCarePopChoosed)
            {
                this.isDontCarePopChoosed = true;
            }
            else
            {
                this.popularity = (double)popularitySlider.Value;
            }

            // deals with all variations of options in order to know which query to run
            if (this.isDontCarePopChoosed && this.isDontCareDecChoosed)
            {
                query = string.Format(queries.getSongsIdsWithoutPopAndDec, this.minTempoRange, this.maxTempoRange,
                        this.minLoudnessRange, this.maxLoudnessRange, this.duration, this.numOfSongs);
            } else if (this.isDontCarePopChoosed && !this.isDontCareDecChoosed)
            {
                if (this.numOfDecChoosed == 1)
                {
                    query = string.Format(queries.getSongsIdsWithoutPopOneDec, this.minTempoRange, this.maxTempoRange,
                            this.minLoudnessRange, this.maxLoudnessRange, this.decadesRanges[0],
                            this.decadesRanges[1], this.duration, this.numOfSongs);
                } else if (this.numOfDecChoosed == 2)
                {
                    query = string.Format(queries.getSongsIdsWithoutPopTwoDec, this.minTempoRange, this.maxTempoRange,
                            this.minLoudnessRange, this.maxLoudnessRange, this.decadesRanges[0],
                            this.decadesRanges[1], this.decadesRanges[2],
                            this.decadesRanges[3], this.duration, this.numOfSongs);
                }
                else if (this.numOfDecChoosed == 3)
                {
                    query = string.Format(queries.getSongsIdsWithoutPopThreeDec, this.minTempoRange, this.maxTempoRange,
                            this.minLoudnessRange, this.maxLoudnessRange, this.decadesRanges[0],
                            this.decadesRanges[1], this.decadesRanges[2], this.decadesRanges[3],
                            this.decadesRanges[4], this.decadesRanges[5], this.duration, this.numOfSongs);
                }
                else
                {
                    query = string.Format(queries.getSongsIdsWithoutPopFourDec, this.minTempoRange, this.maxTempoRange,
                            this.minLoudnessRange, this.maxLoudnessRange, this.decadesRanges[0],
                            this.decadesRanges[1], this.decadesRanges[2], this.decadesRanges[3],
                            this.decadesRanges[4], this.decadesRanges[5], this.decadesRanges[6],
                            this.decadesRanges[7], this.duration, this.numOfSongs);
                }
            } else if (!this.isDontCarePopChoosed && this.isDontCareDecChoosed)
            {
                query = string.Format(queries.getSongsIdsWithoutDec, this.minTempoRange, this.maxTempoRange,
                        this.minLoudnessRange, this.maxLoudnessRange, this.popularity,
                        this.duration, this.numOfSongs);
            }
            else
            {
                if (this.numOfDecChoosed == 1)
                {
                    query = string.Format(queries.getSongsIdsAllOptionsOneDec, this.minTempoRange, this.maxTempoRange,
                            this.minLoudnessRange, this.maxLoudnessRange, this.popularity,
                            this.decadesRanges[0], this.decadesRanges[1], this.duration, this.numOfSongs);
                }
                else if (this.numOfDecChoosed == 2)
                {
                    query = string.Format(queries.getSongsIdsAllOptionsTwoDec, this.minTempoRange, this.maxTempoRange,
                            this.minLoudnessRange, this.maxLoudnessRange, this.popularity,
                            this.decadesRanges[0], this.decadesRanges[1], this.decadesRanges[2],
                            this.decadesRanges[3], this.duration, this.numOfSongs);
                }
                else if (this.numOfDecChoosed == 3)
                {
                    query = string.Format(queries.getSongsIdsAllOptionsThreeDec, this.minTempoRange, this.maxTempoRange,
                            this.minLoudnessRange, this.maxLoudnessRange, this.popularity,
                            this.decadesRanges[0], this.decadesRanges[1], this.decadesRanges[2],
                            this.decadesRanges[3], this.decadesRanges[4], this.decadesRanges[5],
                            this.duration, this.numOfSongs);
                }
                else
                {
                    query = string.Format(queries.getSongsIdsAllOptionsFourDec, this.minTempoRange, this.maxTempoRange,
                            this.minLoudnessRange, this.maxLoudnessRange, this.popularity,
                            this.decadesRanges[0], this.decadesRanges[1], this.decadesRanges[2],
                            this.decadesRanges[3], this.decadesRanges[4], this.decadesRanges[5], this.decadesRanges[6],
                            this.decadesRanges[7], this.duration, this.numOfSongs);
                }
            }

            // move all to DataBaseHandler.cs

            DataBaseHandler dbhandler = DataBaseHandler.Instance;
            
            // insert the playlist name into the playlists table
            dbhandler.saveNewPlaylistName(this.playlistName);

            // get the id of the new playlist
            string playlistId = dbhandler.getPlaylistId(this.playlistName);

            // get the current user id
            string userId = 15.ToString();

            // insert the playlist id and the current user id into the user_to_playlists table
            dbhandler.saveNewPlaylisUser(playlistId, userId);

            // get all songs ids according to the user's request
            List<string> songsIds = dbhandler.getSongsIds(query);
            /*query = string.Format(queries.getSongsIdsAllOptionsOneDec, this.minTempoRange, this.maxTempoRange,
                this.minLoudnessRange, this.maxLoudnessRange, this.popularity, this.minDecadeRange,
                this.maxDecadeRange, this.duration);
                */

            // move it to DataBaseHandler*************
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