using PeterWongClientRestApi.Models;

namespace PeterWongClientRestApi.Services
{
    public interface IClientService
    {
        // CRUD implementations
        public Task<ClientResponseModel> CreateClientAsync(ClientModel clientModel);

        public IEnumerable<ClientModel> ReadClientPage(int? pageNum);

        public Task<ClientResponseModel> ReadClientByIdAsync(int id);

        public Task<ClientResponseModel> UpdateClientAsync(ClientModel clientModel);

        public Task<bool> DeleteClientAsync(int id);
    }
}
