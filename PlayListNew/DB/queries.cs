using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListNew.DB
{
    class queries
    {
        public static string creartPlaylistAllOptions = @" SELECT songs.id FROM playlistgame.songs
                                        WHERE songs.tempo >= 0 AND songs.tempo <= 200
                                        AND songs.loudness >= -52 AND songs.loudness <= 1
                                        AND songs.hotness >= 0 and songs.hotness <=0.5
                                        AND (songs.year >= 1970 AND songs.year < 1990)
                                        AND (songs.duration >= 0 AND songs.duration < 300)
                                        LIMIT 200;";
    }
}
