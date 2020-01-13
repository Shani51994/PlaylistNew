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



        /*
          private float duration;

        public int SongId { set; get; }

        public string SongName { set; get; }

        public string AlbumName { set; get; }

        public Artist SongArtist { set; get; }

        public float SongHotness { set; get; }

        public float Tempo { set; get; }

		/// <summary>
		/// Gets or sets the duration for common people.
		/// </summary>
		/// <value>
		/// The duration of the for common people.
		/// </value>
		public float RealDuration
        {
            set
            {
                this.duration = value;
                this.Duration = TimeSpan.FromSeconds(Convert.ToInt32(this.duration)).ToString(@"mm\:ss"); ;
            }
            get
            {
                return this.duration;
            }
        }

        public string Duration { get; set; }

        public float Loudness { set; get; } 
          
         
         */
    }
}
