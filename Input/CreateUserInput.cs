namespace ElasticsearchTest.Input
{
    public class CreateUserInput
    {
        public string User_Name { get; set; }
        public bool gender { get; set; }
        public int age { get; set; }
        public string Password { get; set; }
        public bool role { get; set; }
        public string? City_ID { get; set; }
    }
}