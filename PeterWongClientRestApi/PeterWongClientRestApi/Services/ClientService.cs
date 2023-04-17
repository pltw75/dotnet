using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeterWongClientRestApi.Data;
using PeterWongClientRestApi.Models;
using System.Drawing;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PeterWongClientRestApi.Services
{
    public class ClientService : IClientService
    {
        // I only have 8 test clients, the page size can be changed for a larger database.
        const int PAGE_SIZE = 5;
        private readonly ClientContext _context;

        public ClientService(ClientContext context)
        {
            _context = context;
        }

        // CRUD implementations
        // I have worked with hardware terminal clients that have problems with Http error codes.
        // so for failed lookups e.g. for Read/Update/Delete, or Create conflicts,
        // I prefer to return 200 success codes with a extra info in the return object,
        // but that means my API users have to check the extra info in the return object
        public async Task<ClientResponseModel> CreateClientAsync(ClientModel clientModel)
        {
            try
            {
                var checkForClientModelID = await ReadClientByIdAsync(clientModel.ID);

                if (checkForClientModelID.clientModel.ID != 0)
                {
                    return new ClientResponseModel()
                    {
                        clientModel = new ClientModel(),
                        IsOk = false,
                        StatusOrError = "Error - Client already exists"
                    };
                }

                var hasConflict = await _context.Clients.FirstOrDefaultAsync(
                    x => x.UniqueId == clientModel.UniqueId
                    || (x.ClientName == clientModel.ClientName
                    && x.ContactEmailAddress == clientModel.ContactEmailAddress));

                if (hasConflict != null)
                {
                    return new ClientResponseModel()
                    {
                        clientModel = new ClientModel(),
                        IsOk = false,
                        StatusOrError = "Error - Client already exists"
                    };
                }

                clientModel.ID = 0;

                var result = await _context.Clients.AddAsync(clientModel);

                await _context.SaveChangesAsync();

                return new ClientResponseModel()
                {
                    clientModel = result.Entity,
                    IsOk = true,
                    StatusOrError = "Ok"
                };
            }
            catch (Exception ex)
            {
                return new ClientResponseModel()
                {
                    clientModel = new ClientModel(),
                    IsOk = false,
                    StatusOrError = ex.Message
                };
            }
        }

        public IEnumerable<ClientModel> ReadClientPage(int? pageNum)
        {
            if (pageNum.HasValue && (pageNum.GetValueOrDefault() > 1))
            {
                var skipRows = PAGE_SIZE * (pageNum.GetValueOrDefault() - 1);

                return _context.Clients.Skip(skipRows).Take(PAGE_SIZE).ToList();
            }

            return _context.Clients.Take(PAGE_SIZE).ToList();
        }

        public async Task<ClientResponseModel> ReadClientByIdAsync(int id)
        {
            try
            {
                var readClient = await _context.Clients.FirstOrDefaultAsync(x => x.ID == id);

                return new ClientResponseModel()
                {
                    clientModel = readClient ?? new ClientModel(),
                    IsOk = readClient != null,
                    StatusOrError = readClient != null ? "Ok" : $@"Ërror - Client ID {id} not found"
                };
            }
            catch (Exception ex)
            {
                return new ClientResponseModel()
                {
                    clientModel = new ClientModel(),
                    IsOk = false,
                    StatusOrError = ex.Message
                };
            }
        }

        public async Task<ClientResponseModel> UpdateClientAsync(ClientModel clientModel)
        {
            try
            {
                var foundClient = await _context.Clients.FirstOrDefaultAsync(x => x.ID == clientModel.ID);

                if (foundClient == null)
                {
                    return new ClientResponseModel()
                    {
                        clientModel = new ClientModel(),
                        IsOk = false,
                        StatusOrError = "Error - Client not found, cannot update"
                    };
                }

                var hasConflict = await _context.Clients.FirstOrDefaultAsync(
                    x => x.UniqueId == clientModel.UniqueId
                    || (x.ClientName == clientModel.ClientName
                    && x.ContactEmailAddress == clientModel.ContactEmailAddress));

                if (hasConflict != null && hasConflict.ID != clientModel.ID)
                {
                    return new ClientResponseModel()
                    {
                        clientModel = new ClientModel(),
                        IsOk = false,
                        StatusOrError = "Error - Client already exists, cannot create a duplicate"
                    };
                }

                _context.ChangeTracker.Clear();

                var result = _context.Clients.Update(clientModel);

                await _context.SaveChangesAsync();

                return new ClientResponseModel()
                {
                    clientModel = result.Entity,
                    IsOk = true,
                    StatusOrError = $@"Ok - Client ID {clientModel.ID} updated"
                };
            }
            catch (Exception ex)
            {
                return new ClientResponseModel()
                {
                    clientModel = new ClientModel(),
                    IsOk = false,
                    StatusOrError = ex.Message
                };
            }
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var foundClient = await _context.Clients.FirstOrDefaultAsync(x => x.ID == id);

            if (foundClient == null)
            {
                return false;
            }

            var result = _context.Remove(foundClient);

            await _context.SaveChangesAsync();

            return result != null ? true : false;
        }
    }
}
