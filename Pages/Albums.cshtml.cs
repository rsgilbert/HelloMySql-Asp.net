using HelloMySql.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace HelloMySql.Pages
{
    public class AlbumsModel : PageModel
    {
        private readonly ILogger<AlbumsModel> _logger;
        private readonly MusicStoreContext _musicStoreContext;
        public List<Album> albums = new List<Album>();

        public AlbumsModel(ILogger<AlbumsModel> logger, MusicStoreContext musicStoreContext)
        {
            _logger = logger;
            _musicStoreContext = musicStoreContext;
        }

        public void OnGet()
        {
            Console.WriteLine("called get");
            albums = GetAlbums();
        }

        public List<Album> GetAlbums()
        {
            List<Album> albumList = new List<Album>();
            Console.WriteLine("Getting albums");
            using (MySqlConnection conn = _musicStoreContext.GetConnection())
            {
                conn.Open();
                Console.WriteLine($"****\nConn state: {conn.State}\n****");
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Album", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        albumList.Add(new Album()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            ArtistName = reader["ArtistName"].ToString(),
                            Price = Convert.ToInt32(reader["Price"]),
                            Genre = reader["Genre"].ToString()
                        });
                    }
                }
            }
            return albumList;
        }
    }
}