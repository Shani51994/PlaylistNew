using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListNew.DB
{
    class queries
    {
        //******************* create playlist section*******************************************************************************


        // important!!
        // the query for get all songs ids according to the user,
        // is generate dynamiclly in the file: createPlaylist.xaml.cs


        // insert playlist name from user to the playlists table
        public static string insertNewPlaylist = @"INSERT INTO playlistgame.playlists(playlists.playlist_name) VALUES ('{0}')";

        // get playlist id by playlist name
        public static string getPlaylistIdByName = @"SELECT playlists.playlist_id FROM playlistgame.playlists
                    WHERE playlists.playlist_name = '{0}'";


    
        // create playlist and insert all songs choosed
        public static string creartPlaylist = @"INSERT INTO playlistgame.songs_to_playlist(songs_to_playlist.playlist_id, songs_to_playlist.song_id)
                    VALUES ('{0}', '{1}')";

        // create row for user id and playlist id and insert to user_tp_playlists table
        public static string crearteUserAndPlaylist = @"INSERT INTO playlistgame.user_to_playlists(user_to_playlists.playlist_id, user_to_playlists.user_id)
                    VALUES ('{0}', '{1}')";

        //******************* END of create playlist section*******************************************************************************



        //******************* Users section**************************************************************************************

        // get user id when user logged in
        public static string getUserId = @"SELECT users.user_id
                                            FROM playlistgame.users
                                            WHERE email = '{0}';";

        // insert new user
        public static string insertNewUser = @"INSERT INTO playlistGame.users (Email, Password, Full_name)
                                              Values('{0}','{1}', '{2}');";

        // to check if email exsits
        public static string queryToCheckIfUserExist = @"SELECT * from playlistGame.users WHERE email= '{0}';";

        // to check if password entered is correct
        public static string toCheckPassword = "SELECT users.password from playlistGame.users WHERE email='{0}';";

        //******************* END of Users section**************************************************************************************





        //******************* show song section**************************************************************************************

        // show playlist songs
        public static string getAllplaylistSongs = @"SELECT songs.name, artists.name, albums.name, songs.id, songs.duration
                                                    FROM playlistgame.songs
                                                    INNER JOIN playlistgame.albums ON albums.id = songs.albumId
                                                    INNER JOIN playlistgame.artists ON artists.id = songs.artistId
                                                    Where songs.id IN (SELECT songs_to_playlist.song_id
                                                                     From playlistgame.songs_to_playlist
                                                                     WHERE songs_to_playlist.playlist_id= '{0}');";

        //******************* show song section**************************************************************************************




        //******************* show playlist section**************************************************************************************

        public static string getAllUserPlaylists = @"SELECT playlists.playlist_name, playlists.playlist_id, COUNT(*) as Num
                                                    FROM playlistgame.playlists
                                                    INNER JOIN playlistgame.user_to_playlists ON playlists.playlist_id = user_to_playlists.playlist_id
                                                    INNER JOIN playlistgame.songs_to_playlist ON playlists.playlist_id = songs_to_playlist.playlist_id
                                                    WHERE user_id='{0}'
                                                    group by playlist_id
				                                    ORDER BY creation_date DESC;";



        // fix!!!
        public static string getAllFriendsPlaylists = @"SELECT * FROM playlistgame.users
                                                        where email IN ({0});";

        // fix!!!
        public static string countFriendPlaylistNum = @"";


        // fix!!!
        public static string copyPlaylistFromFriend = @"";


        //******************* show playlist section**************************************************************************************




        //******************* delete section**************************************************************************************

        // when delete playlist - delete the playlist from tables: playlists, songs_to_playlist, user_to_playlists
        public static string deletePlaylist = @"DELETE FROM playlists WHERE playlist_id='{0}';";
        public static string deleteSongsByPlaylist = @"DELETE FROM songs_to_playlist WHERE playlist_id='{0}';";

        // fix!!!
        public static string countSongNumIfNoSongThenDelete = @"";

        public static string deletePlaylistToUser = @"DELETE FROM user_to_playlists WHERE playlist_id='{0}';";


        public static string deleteSongFromPlaylist = @"DELETE FROM songs_to_playlist WHERE song_id='{0}';";

        //******************* end of delete section**************************************************************************************
    }

}
