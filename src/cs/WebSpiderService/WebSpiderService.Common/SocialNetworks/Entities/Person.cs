namespace WebSpiderService.Common.SocialNetworks.Entities
{
    /// <summary>
    /// Represents a social network user
    /// </summary>
    public class Person
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}