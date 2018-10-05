using poc_specification_pattern.Shared;

namespace poc_specification_pattern.Users
{
    public class User : Entity
    {
        public GenderType Gender { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Email} - {Gender} - Country ({CountryId})";
        }
    }

    public enum GenderType
    {
        Male,
        Female
    }
}