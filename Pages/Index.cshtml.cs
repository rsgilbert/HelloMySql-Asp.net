using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelloMySql.Models;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace HelloMySql.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MusicStoreContext _musicStoreContext;

        public IndexModel(ILogger<IndexModel> logger, MusicStoreContext musicStoreContext)
        {
            _logger = logger;
            _musicStoreContext = musicStoreContext;
        }

        public void OnGet()
        {
             var albums = GetAlbums();
            Console.WriteLine(albums);
            Console.WriteLine($"count is {albums.Count()}");
            foreach(var album in albums)
            {
                Console.WriteLine(album.Name);
            }
        }

        public List<Album> GetAlbums()
        {
            List<Album> list = new List<Album>();
            using(MySqlConnection conn = _musicStoreContext.GetConnection())
            {
               conn.Open();
               Console.WriteLine($"****\nConn state: {conn.State}\n****");
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Album", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Album()
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
            return list;
        }
    }
}