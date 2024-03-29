using System.ComponentModel.DataAnnotations;

namespace MusicQuizAPI.Models.Database
{
    public class FavoriteAnime
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }
        
        [Required]
        public int AnimeID { get; set; }

        public int Score { get; set; } = 0;

        public Anime Anime { get; set; }
        
        public User User { get; set; }
    }
}