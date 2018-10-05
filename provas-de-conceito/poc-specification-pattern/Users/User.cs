namespace poc_specification_pattern.Users
{
    public class User
    {
        public int Id { get; set; }
        public GenderType Gender { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Gender} - Country ({CountryId})";
        }
    }

    public enum GenderType
    {
        Male,
        Female
    }
}