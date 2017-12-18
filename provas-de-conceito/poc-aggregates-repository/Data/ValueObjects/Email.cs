namespace poc_aggregates_repository.Data
{
    public class Email
    {
        public string Address { get; private set; }

        public Email(string address)
        {
            Address = address;
        }

        private Email()
        {
        }
    }
}