namespace TNWalks.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}