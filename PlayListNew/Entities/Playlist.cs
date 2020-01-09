using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListNew.Entities
{
    class Playlist
    {
        private string playlistName;
        private int playlistId;
        public List<Song> playlistSongs = new List<Song>();

        private Playlist() { }
       
        public string PlaylistName
        {
            get { return playlistName; }
            set { playlistName = value; }
        }

        public int PlaylistId
        {
            get { return playlistId; }
            set { playlistId = value; }
        }

        
        public List<Song> PlaylistSongs {
            set; get; }

        /*
        public string TotalDuration
        {
            get; set;
        }*/

    }
}
