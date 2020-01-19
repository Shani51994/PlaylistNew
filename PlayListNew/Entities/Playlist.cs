using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListNew.Entities
{
    public class Playlist
    {
       
        public string PlaylistName
        {
            get; set;
        }

        public int PlaylistNumOfSongs
        {
            set; get;
        }

        
        public int PlaylistId
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

    }
}
