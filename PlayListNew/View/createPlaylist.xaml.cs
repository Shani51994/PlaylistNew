using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using PlayListNew.DB;

namespace PlayListNew.View
{
    
    
    /// <summary>
    /// Interaction logic for createPlaylist.xaml
    /// </summary>
    public partial class createPlaylist : Window
    {
        private double popularity;
        private double minTempoRange;
        private double maxTempoRange;
        private double minLoudnessRange;
        private double maxLoudnessRange;
        private int minDecadeRange;
        private int maxDecadeRange;
        private int duration;
        private int numOfSongs;
        private string playlistName;
        private bool isDontCareDecChoosed;
        private int numOfDecChoosed = 0;
        private List<int> decadesRanges = new List<int>();

        public createPlaylist()
        {
            InitializeComponent();
        }

        public void pressCreate(object sender, RoutedEventArgs e)
        {

            // get all values inserted by the user
            this.popularity = (double)popularitySlider.Value;
            this.minTempoRange = double.Parse(tempoMin.Text);
            this.maxTempoRange = double.Parse(tempoMax.Text);
            this.minLoudnessRange = double.Parse(loudnessMin.Text);
            this.maxLoudnessRange = double.Parse(loudnessMax.Text);
            this.duration = (int)durationSlider.Value;
            this.numOfSongs = int.Parse(songsNum.Text);
            this.playlistName = playListName.Text;


            // checks hoe many checkboxes of decades user choosed
            if (this.isDontCareDecChoosed)
            {

            }

            DataBaseHandler dbhandler = DataBaseHandler.Instance;
            dbhandler.createPlaylist();
        }


        private void choose70(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1970);
            this.decadesRanges.Add(1979);
            this.numOfDecChoosed++;

        }

        private void choose80(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1980);
            this.decadesRanges.Add(1989);
            this.numOfDecChoosed++;
            
        }

        private void choose90(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(1990);
            this.decadesRanges.Add(1999);
            this.numOfDecChoosed++;
        }

        private void choose00(object sender, RoutedEventArgs e)
        {
            this.decadesRanges.Add(2000);
            this.decadesRanges.Add(2009);
            this.numOfDecChoosed++;
        }

        private void chooseAllDec(object sender, RoutedEventArgs e)
        {
            this.isDontCareDecChoosed = true;
            numOfDecChoosed = 0;
        }

    }
}