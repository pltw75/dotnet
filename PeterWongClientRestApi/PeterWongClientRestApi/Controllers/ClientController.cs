using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeterWongClientRestApi.Data;
using PeterWongClientRestApi.Models;
using PeterWongClientRestApi.Services;

// FULL DISCLOSURE
// code from the tutorial examples below was used
// https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-7.0&tabs=visual-studio
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio
// https://www.c-sharpcorner.com/blogs/implementation-of-unit-test-using-xunit-and-moq-in-net-core-6-web-api
// https://medium.com/@niteshsinghal85/end-to-end-unit-testing-for-net-6-web-api-58883d1b2fe4
// https://learn.microsoft.com/en-gb/ef/ef6/what-is-new/past-releases

/*
I implemented this following the tutorial examples for .Net 6 and Entity Framework CORE
I only have 8 test clients, but I implemented paging with a page size of 5 rows, this can be changed for a larger database.
the page number is optional and will default to page 1 (page 1 is the first page)
I have worked with hardware terminal clients that have problems with Http error codes.
so for failed lookups e.g. for Read/Update/Delete, or Create conflicts,
I prefer to return 200 success codes with a extra info in the return object,
but that means my API users have to check the extra info in the return object
The Database Connection string is in appsettings.json
To confirm, I am using Entity Framework Core 6, which aligns with .Net 6
https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/plan
EF Core 6.0 will align with .NET 6 as a long-term support (LTS) release
*/

namespace PeterWongClientRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // CRUD implementations
        [HttpPost("CreateClient")]
        public async Task<ClientResponseModel> CreateClientAsync(ClientModel clientModel)
        {
            return await _clientService.CreateClientAsync(clientModel);
        }

        [HttpGet("ReadClientPage")]
        public IEnumerable<ClientResponseModel> ReadClientPage(int? pageNum)
        {
            var clientList = _clientService.ReadClientPage(pageNum);

            foreach (var client in clientList)
            {
                yield return new ClientResponseModel()
                {
                    clientModel = client,
                    IsOk = true,
                    StatusOrError = "Ok"
                };
            }
        }

        [HttpGet("ReadClientById")]
        public async Task<ClientResponseModel> ReadClientByIdAsync(int id)
        {
            return await _clientService.ReadClientByIdAsync(id);
        }

        [HttpPut("UpdateClient")]
        public async Task<ClientResponseModel> UpdateClientAsync(ClientModel clientModel)
        {
            return await _clientService.UpdateClientAsync(clientModel);
        }

        [HttpDelete("DeleteClient")]
        public async Task<bool> DeleteClientAsync(int id)
        {
            return await _clientService.DeleteClientAsync(id);
        }
    }
}