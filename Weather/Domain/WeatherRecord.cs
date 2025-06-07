using System.ComponentModel.DataAnnotations;

namespace Weather.Domain
{
    public class WeatherRecord
    {
        [Key] public int Id { get; set; }
        public string Location { get; set; }
        public DateTime FetchedAt { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}

