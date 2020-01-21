using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListNew.Entities
{
    public class Song
    {
        public string SongName
        {
            get; set;
        }
        
        public int SongId
        {
            get; set;
        }

        public string ArtistName
        {
            get; set;
        }

        public string AlbumName
        {
            get; set;
        }


        public string Duration
        {
            get; set;
        }


        public Boolean BelongToPlayer
        {
            get; set;
        }

    }
}
