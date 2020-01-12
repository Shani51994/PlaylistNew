using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListNew.DB
{
    class queries
    {
        //******************* creat playlist section*******************************************************************************
        
            /*
        public static string creartPlaylistAllOptionsTest = @" SELECT songs.id FROM playlistgame.songs
                            WHERE songs.tempo >= 0 AND songs.tempo <= 200
                            AND songs.loudness >= -52 AND songs.loudness <= 1
                            AND songs.hotness >= 0 and songs.hotness <=0.5
                            AND (songs.year >= 1970 AND songs.year < 1990)
                            AND (songs.duration >= 0 AND songs.duration < 300)
                            LIMIT 200";

       
        public static string creartPlaylistAllOptionsSmall = @" SELECT songs.id FROM playlistgame.songs
                            WHERE songs.tempo >= '{0}' AND songs.tempo <= '{1}'
                            AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                            AND songs.hotness >= 0 AND songs.hotness <= '{4}'
                            AND (songs.year >= '{5}' AND songs.year < '{6} + 10')
                            AND (songs.duration >= 0 AND songs.duration < '{7} * 60')
                            LIMIT 5";

    */

        // insert playlist name from user to the playlists table
        public static string insertNewPlaylist = @"INSERT INTO playlistgame.playlists(playlists.playlist_name) VALUES ('{0}')";

        // get playlist id by playlist name
        public static string getPlaylistIdByName = @"SELECT playlists.playlist_id FROM playlistgame.playlists
                    WHERE playlists.playlist_name = '{0}'";

        // get songs ids according to the user requests, when user choosed all options
        public  static string getSongsIdsAllOptionsOneDec = @"SELECT songs.id FROM playlistgame.songs
                    WHERE songs.tempo >= '{0}' AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND songs.hotness >= 0 AND songs.hotness <= '{4}'
                    AND (songs.year >= '{5}' AND songs.year< '{6}')
                    AND(songs.duration >= 0 AND songs.duration< '{7}')
                    LIMIT {8}";

        // get songs ids according to the user requests, when user choosed all options
        public static string getSongsIdsAllOptionsTwoDec = @"SELECT songs.id FROM playlistgame.songs
                    WHERE songs.tempo >= '{0}' AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND songs.hotness >= 0 AND songs.hotness <= '{4}'
                    AND (songs.year >= '{5}' AND songs.year< '{6}')
                    AND (songs.year >= '{7}' AND songs.year < '{8}')
                    AND (songs.duration >= 0 AND songs.duration < '{9}')
                    LIMIT '{10}'";

        // get songs ids according to the user requests, when user choosed all options
        public static string getSongsIdsAllOptionsThreeDec = @"SELECT songs.id FROM playlistgame.songs
                    WHERE songs.tempo >= '{0}' AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND songs.hotness >= 0 AND songs.hotness <= '{4}'
                    AND (songs.year >= '{5}' AND songs.year< '{6}')
                    AND (songs.year >= '{7}' AND songs.year < '{8}')
                    AND (songs.year >= '{9}' AND songs.year < '{10}')
                    AND (songs.duration >= 0 AND songs.duration < '{11}')
                    LIMIT '{12}'";

        // get songs ids according to the user requests, when user choosed all options
        public static string getSongsIdsAllOptionsFourDec = @"SELECT songs.id FROM playlistgame.songs
                    WHERE songs.tempo >= '{0}' AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND songs.hotness >= 0 AND songs.hotness <= '{4}'
                    AND (songs.year >= '{5}' AND songs.year< '{6}')
                    AND (songs.year >= '{7}' AND songs.year < '{8}')
                    AND (songs.year >= '{9}' AND songs.year < '{10}')
                    AND (songs.year >= '{11}' AND songs.year < '{12}')
                    AND (songs.duration >= 0 AND songs.duration < '{13}')
                    LIMIT '{14}'";


        // get songs ids according to the user requests, when user didnt choose specific popularity
        // and specific decade
        public static string getSongsIdsWithoutPopAndDec = @"SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{0} AND songs.tempo <= '{1}'                    
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND (songs.duration >= 0 AND songs.duration < '{4}')
                    LIMIT '{5}'";

        // get songs ids according to the user requests, when user didnt choose specific decade
        public static string getSongsIdsWithoutDec = @"SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{0} AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND songs.hotness >= 0 AND songs.hotness <= '{4}'
                    AND (songs.duration >= 0 AND songs.duration < '{5}')
                    LIMIT '{6}'";

        // get songs ids according to the user requests, when user didnt choose all specific popularity
        public static string getSongsIdsWithoutPopOneDec = @"SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{0} AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND (songs.year >= '{4}' AND songs.year < '{5}')
                    AND (songs.duration >= 0 AND songs.duration < '{6}')
                    LIMIT '{7}'";

        // get songs ids according to the user requests, when user didnt choose all specific popularity
        public static string getSongsIdsWithoutPopTwoDec = @"SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{0} AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND (songs.year >= '{4}' AND songs.year < '{5}')
                    AND (songs.year >= '{6}' AND songs.year < '{7}')
                    AND (songs.duration >= 0 AND songs.duration < '{8}')
                    LIMIT '{9}'";

        // get songs ids according to the user requests, when user didnt choose all specific popularity
        public static string getSongsIdsWithoutPopThreeDec = @"SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{0} AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND (songs.year >= '{4}' AND songs.year < '{5}')
                    AND (songs.year >= '{6}' AND songs.year < '{7}')
                    AND (songs.year >= '{8}' AND songs.year < '{9}')
                    AND (songs.duration >= 0 AND songs.duration < '{10}')
                    LIMIT '{11}'";

        // get songs ids according to the user requests, when user didnt choose all specific popularity
        public static string getSongsIdsWithoutPopFourDec = @"SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{0} AND songs.tempo <= '{1}'
                    AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                    AND (songs.year >= '{4}' AND songs.year < '{5}')
                    AND (songs.year >= '{6}' AND songs.year < '{7}')
                    AND (songs.year >= '{8}' AND songs.year < '{9}')
                    AND (songs.year >= '{10}' AND songs.year < '{11}')
                    AND (songs.duration >= 0 AND songs.duration < '{12}')
                    LIMIT '{13}'";

        // create playlist and insert all songs choosed
        public static string creartPlaylist = @"INSERT INTO playlistgame.songs_to_playlist(songs_to_playlist.playlist_id, songs_to_playlist.song_id)
                    VALUES ('{0}', '{1}')";


        //******************* END of creat playlist section*******************************************************************************




        //******************* Users section**************************************************************************************
       
            // get user id when user logged in
        public static string getUserIdAndName = @"SELECT users.user_id, users.full_name
                    FROM playlistgame.users
                    WHERE name = '{0}';";

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
        public static string getAllplaylistSongs = @"SELECT songs.name, artists.name, albums.name
                                                    FROM playlistgame.songs
                                                    INNER JOIN playlistgame.albums ON albums.id = songs.albumId
                                                    INNER JOIN playlistgame.artists ON artists.id = songs.artistId
                                                    Where songs.id IN (SELECT song_to_playlist.song_id
                                                                     From playlistgame.song_to_playlist
                                                                     WHERE song_to_playlist.playlist_id= '{0}');";

        //******************* show song section**************************************************************************************







        //******************* show playlist section**************************************************************************************

        // show all playlists of a specific user on the screen - sort it by date!
        public static string getAllUserPlaylists = @"SELECT playlists.playlist_name, playlists.playlist_id
                    FROM playlistgame.playlists, playlistgame.user_to_playlists
                    WHERE user_id='{0}' AND playlists.playlist_id=user_to_playlists.playlist_id;";
                   
                     // maybe add:
                    // ORDER BY playlists.creation_time DESC"

        //******************* show playlist section**************************************************************************************

         
    }

}
