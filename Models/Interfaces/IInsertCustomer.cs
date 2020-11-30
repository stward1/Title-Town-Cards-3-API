namespace API.Models.Interfaces
{
    public interface IInsertCustomer
    {
        void SaveCustomer(string FirstName, string LastName, string Email, string Password, string PhoneNumber, int RewardsPoints);

        /*
                public string FirstName{ get; set; }
        public string LastName { get; set; }
        //email is the primary key for customers; this obviously cannot be auto-incremented
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int RewardsPoints { get; set; }
        
        */
    }
}