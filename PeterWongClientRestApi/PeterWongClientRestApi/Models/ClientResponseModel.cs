namespace PeterWongClientRestApi.Models
{
    public class ClientResponseModel
    {
        public ClientModel clientModel { get; set; }

        public bool IsOk { get; set; }

        public string StatusOrError { get; set; }
    }
}