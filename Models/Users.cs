namespace ElasticsearchTest.Models
{
    public class Users
    {
        public int User_ID { get; set; }
        public string User_Name { get; set; }
        public bool gender { get; set; }
        public int age { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? role { get; set; }
        public int? City_ID { get; set; }
    }
}