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
        
        public static string creartPlaylistAllOptionsTest = @" SELECT songs.id FROM playlistgame.songs
                                        WHERE songs.tempo >= 0 AND songs.tempo <= 200
                                        AND songs.loudness >= -52 AND songs.loudness <= 1
                                        AND songs.hotness >= 0 and songs.hotness <=0.5
                                        AND (songs.year >= 1970 AND songs.year < 1990)
                                        AND (songs.duration >= 0 AND songs.duration < 300)
                                        LIMIT 200;";

       
        public static string creartPlaylistAllOptionsSmall = @" SELECT songs.id FROM playlistgame.songs
                            WHERE songs.tempo >= '{0} AND songs.tempo <= '{1}'
                            AND songs.loudness >= '{2}' AND songs.loudness <= '{3}'
                            AND songs.hotness >= 0 AND songs.hotness <= '{4}'
                            AND (songs.year >= '{5}' AND songs.year < '{6} + 10')
                            AND (songs.duration >= 0 AND songs.duration < '{7} + 60')
                            LIMIT 5;";


        // insert playlist name from user to the playlists table, check the SELECT LAST_INSERT_ID and if ID
        // automatically increased
        public static string insertNewPlaylist = @"INSERT INTO playlistgame.playlists(playlists.playlist_name) VALUES ('{0}');";

        // create playlist if user choose all options, and insert all songs choosed
        // insert song id and playlist id to the songs_to_playlist table
        public static string creartPlaylistAllOptions = @"INSERT INTO playlistgame.songs_to_playlist(songs_to_playlist.song_id, songs_to_playlist. playlist_id)
                    VALUES (
                    (SELECT playlists.playlist_id FROM playlistgame.playlists
                    WHERE playlists.playlist_name = '{0}'),
                    (SELECT songs.id FROM playlistgame.songs
                    WHERE songs.tempo >= '{1} AND songs.tempo <= '{2}'
                    AND songs.loudness >= '{3}' AND songs.loudness <= '{4}'
                    AND songs.hotness >= 0 AND songs.hotness <= '{5}'
                    AND (songs.year >= '{6}' AND songs.year < '{7} + 10')
                    AND (songs.duration >= 0 AND songs.duration < '{8} + 60')
                    LIMIT '{9}'));";

        // when user choose i dont care pop and i dont care decade - check that numbers are ok!
        public static string creartPlaylistWithoutPopAndDec = @"INSERT INTO playlistgame.songs_to_playlist(songs_to_playlist.song_id, songs_to_playlist. playlist_id)
                    VALUES (
                    (SELECT playlists.playlist_id FROM playlistgame.playlists
                    WHERE playlists.playlist_name = '{0}'),
                    (SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{1} AND songs.tempo <= '{2}'                    
                            AND songs.loudness >= '{3}' AND songs.loudness <= '{4}'
                    AND (songs.duration >= 0 AND songs.duration < '{5} + 60')
                    LIMIT '{6}');";

        // when user choose only i dont care decade - check that numbers are ok!
        public static string creartPlaylistWithoutDec = @"INSERT INTO playlistgame.songs_to_playlist(songs_to_playlist.song_id, songs_to_playlist. playlist_id)
                    VALUES (
                    (SELECT playlists.playlist_id FROM playlistgame.playlists
                    WHERE playlists.playlist_name = '{0}'),
                    (SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{1} AND songs.tempo <= '{2}'
                    AND songs.loudness >= '{3}' AND songs.loudness <= '{4}'
                    AND songs.hotness >= 0 AND songs.hotness <= '{5}'
                    AND (songs.duration >= 0 AND songs.duration < '{6} + 60')
                    LIMIT '{7}'));";

        // when user choose i dont care pop - check that numbers are ok!
        public static string creartPlaylistWithoutPop = @"INSERT INTO playlistgame.songs_to_playlist(songs_to_playlist.song_id, songs_to_playlist. playlist_id)
                    VALUES (
                    (SELECT playlists.playlist_id FROM playlistgame.playlists
                    WHERE playlists.playlist_name = '{0}'),
                    (SELECT * FROM playlistgame.songs
                    WHERE songs.tempo >= '{1} AND songs.tempo <= '{2}'
                    AND songs.loudness >= '{3}' AND songs.loudness <= '{4}'
                    AND (songs.year >= '{5}' AND songs.year < '{6} + 10')
                    AND (songs.duration >= 0 AND songs.duration < '{7} + 60')
                    LIMIT '{8}'));";

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
