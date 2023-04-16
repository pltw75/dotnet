namespace PeterWongClientRestApi.Models
{
    public class ClientModel
    {        
        public int ID { get; set; }

        public Guid UniqueId { get; set; }

        public string ClientName { get; set; }

        public string ContactEmailAddress { get; set; }

        public DateTime DateBecameCustomer { get; set; }
    }
}