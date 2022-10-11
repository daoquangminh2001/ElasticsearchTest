namespace ElasticsearchTest.Models
{
    public class City
    {
        public int City_Id { get; set; }
        public string CITY_NAME { get; set; }
        public double CITY_LONGITUDE { get; set; }
        public double CITY_LATITUDE { get; set; }
        public int Country_Id { get; set; }
    }
}