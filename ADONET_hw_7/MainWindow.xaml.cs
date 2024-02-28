using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using MusicDb;

namespace ADONET_hw_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Music2Context db = new();

        ICollection<SongArtist> songArtists = null;
        public MainWindow()
        {
            InitializeComponent();

            ICollection<Song> songs = db.Songs.ToArray();
            ICollection<Artist> artists = db.Artists.ToArray();

            foreach (Song song in songs)
            {
                cb_Song.Items.Add(song);
            }
            foreach (Artist artist in artists)
            {
                cb_Artist.Items.Add(artist);
            }

            songArtists = db.SongArtists
                .OrderBy(songArtist => songArtist.Id)
                .ToArray();

            DataContext = songArtists;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Song song = (Song)cb_Song.SelectedItem;
            Artist artist = (Artist)cb_Artist.SelectedItem;

            SongArtist songArtist = new SongArtist();
            songArtist.SongId = song.Id;
            songArtist.ArtistId = artist.Id;

            db.SongArtists.Add(songArtist);
            db.SaveChanges();

            songArtists = db.SongArtists
                .OrderBy(songArtist => songArtist.Id)
                .ToArray();

            DataContext = songArtists;

        }
    }
}