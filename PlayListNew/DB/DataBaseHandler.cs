using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        public DataBaseHandler()
        {
            DBConnection = DBConnection.Instance();
            DBConnection.DatabaseName = ConnectionInfo.DatabaseName;
            DBConnection.Password = ConnectionInfo.Password;
            DBConnection.Server = ConnectionInfo.Server;
            DBConnection.User = ConnectionInfo.User;

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


        /*
         * function that check if user email is already exist, if exsist return 1 (else return 0).
         */
        public int checkIfUserExist(string email)
        {
            string query = string.Format(queries.queryToCheckIfUserExist, email);
            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);
            
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                return 1;
            }
            else
            {
                reader.Close();
                return 0;
            }
        }

        


        public int checkIfPlNameExsistToUser(string name)
        {
            string query = string.Format(queries.queryTocheckIfPlNameExsistToUser, User.Instance.Id, name);
            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                return 1;
            }
            else
            {
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
            while (reader.Read())
            {
                DBpassword = reader.GetString(0);
            }

            if (DBpassword == password)
            {
                reader.Close();
                return 1;
            }
            else
            {
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

        public void saveNewPlaylisUser(int playlistId, int userId)
        {
            string query = string.Format(queries.crearteUserAndPlaylist, playlistId, userId);

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


        public int getPlaylistId(string playlistName)
        {
            int playlistId=0;
            string query = string.Format(queries.getPlaylistIdByPlaylistName, playlistName);
            try
            {
                if (DBConnection.IsConnect())
                {
                    var command = new MySqlCommand(query, DBConnection.Connection);
                    var reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        playlistId = reader.GetInt32(0);
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

        
        public int createPlaylist(string playlistName, List<string> songsIds)
        {
            // insert the playlist name into the playlists table
            saveNewPlaylistName(playlistName);
            
            // get the current user id
            int userId = User.Instance.Id;

            // get the id of the new playlist of the user
            int playlistId = getPlaylistId(playlistName);

            savePlaylistSongs(songsIds, playlistId);

            // insert the playlist id and the current user id into the user_to_playlists table
            saveNewPlaylisUser(playlistId, userId);

            return playlistId;
        }

        public void savePlaylistSongs(List<string> songsIds, int playlistId)
        {
            try
            {
                if (DBConnection.IsConnect())
                {
                    // insert every song to the platlists table
                    for (int i = 0; i < songsIds.Count; i++)
                    {
                        string query = string.Format(queries.creartPlaylist, playlistId, songsIds[i]);
                        var command = new MySqlCommand(query, DBConnection.Connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }
        }


        public ObservableCollection<Song> GetPlaylistSongs(int playlistID, Boolean doesBelongToPlayer)
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
                        int id = reader.GetInt32(3);
                        int duration = reader.GetInt32(4);
                        Boolean belongToPlayer = doesBelongToPlayer;
                        string durationS = TimeSpan.FromSeconds(Convert.ToInt32(duration)).ToString(@"mm\:ss"); ;

                        Song song = new Song() { SongName = songName, ArtistName = artistName, AlbumName = albumName, SongId=id, Duration=durationS, BelongToPlayer= belongToPlayer };
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
                    string query = string.Format(queries.getAllUserPlaylists, Entities.User.Instance.Id);

                    var cmd = new MySqlCommand(query, DBConnection.Connection);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string playlistName = reader.GetString(0);
                        int playlistId = reader.GetInt32(1);
                        int playlistNumOfSongs = reader.GetInt32(2);
                        Playlist plist = new Playlist() { PlaylistName = playlistName, PlaylistId = playlistId, PlaylistNumOfSongs = playlistNumOfSongs };
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




        public int getUserIdByEmail(string email)
        {

            string query = string.Format(queries.getUserId, email);
            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);

            int id=0;
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();

            return id;
        }

        

        public void deletePlaylist(int playlistId)
        {

            string query = string.Format(queries.deletePlaylist, playlistId);
            MySqlCommand command = new MySqlCommand(query, DBConnection.Connection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at RunQuery function" + ex.Message + Environment.NewLine);
            }


            string query2 = string.Format(queries.deleteSongsByPlaylist, playlistId);
            MySqlCommand command2 = new MySqlCommand(query2, DBConnection.Connection);

            try
            {
                command2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at RunQuery function" + ex.Message + Environment.NewLine);
            }

            
            string query3 = string.Format(queries.deletePlaylistToUser, playlistId);
            MySqlCommand command3 = new MySqlCommand(query3, DBConnection.Connection);

            try
            {
                command3.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at RunQuery function" + ex.Message + Environment.NewLine);
            }
        }


        public void deleteSong(int songId)
        {

            string query = string.Format(queries.deleteSongFromPlaylist, songId);
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


        private string convertListToString(List<string> emails)
        {

            string strEmails = "";
            int firststr = 1;
            foreach (string email in emails)
            {
                if (firststr == 1)
                {
                    strEmails += "'" + email + "'";
                    firststr = 0;
                }
                else
                {
                    strEmails += "," + "'" + email + "'";
                }
            }

            return strEmails;
        }


        public int countFriendPlaylist(List<string> emails)
        {

            string strEmails = convertListToString(emails);
            int playlistNum = 0;
            try
            {
                if (DBConnection.IsConnect())
                {
                    string query = string.Format(queries.countFriendPlaylistNum, strEmails);
                    var cmd = new MySqlCommand(query, DBConnection.Connection);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        playlistNum = reader.GetInt32(0);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }


            return (playlistNum);
        }


        
        public ObservableCollection<Playlist> GetFriendsPlaylist(List<string> emails)
        {
            string strEmails = convertListToString(emails);

            ObservableCollection<Playlist> playlists = new ObservableCollection<Playlist>();

            try
            {
                if (DBConnection.IsConnect())
                {
                    string query = string.Format(queries.getAllFriendsPlaylists, strEmails);

                    var cmd = new MySqlCommand(query, DBConnection.Connection);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string playlistName = reader.GetString(0);
                        int playlistId = reader.GetInt32(1);
                        int playlistNumOfSongs = reader.GetInt32(3);
                        string userName = reader.GetString(2);
                        Playlist plist = new Playlist() { PlaylistName = playlistName, PlaylistId = playlistId,
                            PlaylistNumOfSongs = playlistNumOfSongs, UserName=userName};
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


        public void checksAfterdeletingSongs(int playlistId)
        {

            string query = string.Format(queries.afterDeletingSongCheckPlaylistSongs, playlistId);
            string query2 = string.Format(queries.afterDeletingSongCheckUTP, playlistId);

            try
            {
                if (DBConnection.IsConnect())
                {
                    var command = new MySqlCommand(query, DBConnection.Connection);
                    command.ExecuteNonQuery();
                    var command2 = new MySqlCommand(query2, DBConnection.Connection);
                    command2.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
            }
        }

       


    public void copyPlaylist(int playlistIdToCopy)
    {
        // get name  of playlist by playlistId
        string plNewName = "";
        string plName = "";
        // name of user that had this play list
        string userName = "";
        string query = string.Format(queries.getPlaylistNameAndUserNByPlId, playlistIdToCopy);
        try
        {
            if (DBConnection.IsConnect())
            {
                var command = new MySqlCommand(query, DBConnection.Connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    plName = reader.GetString(0);
                    userName = reader.GetString(1);
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            File.AppendAllText(path, "Server DB Error at GetTopScores function" + ex.Message + Environment.NewLine);
        }

        plNewName = plName + " copy from " +userName;
       
        List<string> songsIds = new List<string>();

        query = string.Format(queries.getSongsByPlaylistId, playlistIdToCopy);
        
        // query to get the songs
        songsIds = getSongsIds(query);


        int res = createPlaylist(plNewName, songsIds);

     }


    }
}