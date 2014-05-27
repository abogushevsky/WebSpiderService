namespace WebSpiderService.Common.SocialNetworks.Entities
{
    /// <summary>
    /// Represents a social network user
    /// </summary>
    public class Person
    {
        /// <summary>
        /// User id in social network
        /// </summary>
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Some page describtion or status
        /// </summary>
        public string About { get; set; }

        public int Age { get; set; }
    }
}