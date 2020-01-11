using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GuessTheSongServer.DM;
using MySql.Data;
using MySql.Data.MySqlClient;
using PlayListNew.Entities;

namespace PlayListNew.DB
{
    public class DataBaseHandler
    {
        static private DBConnection DBConnection;
        static private DataBaseHandler instance = null;
        
        string path = @"PlayListNewLog.txt";

        private string Server = "localhost";
        //private string DatabaseName = "playlistGame";
        //private string Password = "Mm1614113";
        private string DatabaseName = "playlistgame";
        private string Password = "Sql1234pass1234Sql";
        private string User = "root";

        public DataBaseHandler()
        {
            DBConnection = DBConnection.Instance();
            DBConnection.DatabaseName = DatabaseName;
            DBConnection.Password = Password;
            DBConnection.Server = Server;
            DBConnection.User = User;

            DBConnection.Start();
        }


        public static DataBaseHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBaseHandler();
                }
                return instance;
            }

        }

        private string ConnectionString { get; set; }
        private MySqlConnection ConnectionHandler { get; set; }

        public void EndDBConnection()
        {
            DBConnection.Close();
        }


        public int checkIfUserExist(string email)
        {
            string query = string.Format(queries.queryToCheckIfUserExist, email);
            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);
        
            //*******************
            var reader = command.ExecuteReader();
            if (reader.HasRows){
                reader.Close();
                return 1;
            } else {
                reader.Close();
                return 0;
            }
        }


        public int checkIfUserExistAndPasswordRight(string email, string password)
        {
            string query = string.Format(queries.toCheckPassword, email);
            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);
       
            string DBpassword = "";
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                DBpassword = reader.GetString(0);
            }
            
            if (DBpassword == password){
                reader.Close();
                return 1;
            }else{
                reader.Close();
                return 0;
            }
        }

        public void SaveUserData(string email, string password, string fullName)
        {
            string query = string.Format(queries.insertNewUser, email, password, fullName);
            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);
            
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at RunQuery function" + ex.Message + Environment.NewLine);
            }
        }


        public void saveNewPlaylistName(string playlistName)
        {
            string query = string.Format(queries.insertNewPlaylist, playlistName);

            try
            {
                if (DBConnection.IsConnect())
                {
                    var command = new MySqlCommand(query, DBConnection.Connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }
        }


        public string getPlaylistId(string playlistName)
        {
            string playlistId = "";
            string query = string.Format(queries.getPlaylistIdByName, playlistName);
            try
            {
                if (DBConnection.IsConnect())
                {

                    var command = new MySqlCommand(query, DBConnection.Connection);

                    var reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
                        playlistId = reader.GetString(0);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }
            return playlistId;

        }


        public List<string> getSongsIds(string query)
        {
            List<string> songsIds = new List<string>();

            try
            {
                if (DBConnection.IsConnect())
                {

                    var command = new MySqlCommand(query, DBConnection.Connection);

                    var reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
                        songsIds.Add(reader.GetString(0));
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }
            return songsIds;

        }


        public void createPlaylist(string query)
        {
            try
            {
                if (DBConnection.IsConnect())
                {
                    //        public void createPlaylist(string query)
                    //string connstring = string.Format(queries.creartPlaylistAllOptions, Server, DatabaseName, User, Password);

                    //string query = queries.creartPlaylistAllOptions;
                    var command = new MySqlCommand(query, DBConnection.Connection);


                    command.ExecuteNonQuery();

                }
            } catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }
        }


        public ObservableCollection<Song> GetPlaylistSongs(int playlistID)
        {
            ObservableCollection<Song> songs = new ObservableCollection<Song>();
            
            try
            {
                if (DBConnection.IsConnect())
                {
                    string query = string.Format(queries.getAllplaylistSongs, playlistID);

                    var cmd = new MySqlCommand(query, DBConnection.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string songName = reader.GetString(0);
                        string artistName = reader.GetString(1);
                        string albumName = reader.GetString(2);
                        Song song = new Song() { SongName = songName, ArtistName = artistName, AlbumName = albumName };
                        songs.Add(song);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }
            
            return songs;

        }


        public ObservableCollection<Playlist> GetAllUserPlaylist()
        {
            ObservableCollection<Playlist> playlists = new ObservableCollection<Playlist>();

            try
            {
                if (DBConnection.IsConnect())
                {
                    //DOTO !!!!!!!!!!!!!!!!!!!!
                    // change when program ready
                    //string query = string.Format(queries.getAllUserPlaylists, Entities.User.Instance.Email);

                    //only for checks:
                    string query = string.Format(queries.getAllUserPlaylists, 1);

                    var cmd = new MySqlCommand(query, DBConnection.Connection);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string playlistName = reader.GetString(0);
                        int playlistId = reader.GetInt32(1);
                        Playlist plist = new Playlist() { PlaylistName = playlistName, PlaylistId = playlistId };
                        playlists.Add(plist);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }

            return playlists;

        }



    }
}