using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PeterWongClientRestApi.Data;
using PeterWongClientRestApi.Models;
using PeterWongClientRestApi.Services;

// FULL DISCLOSURE
// code from the tutorial examples below was used, I didn't just copy/paste and change var names,
// I followed the principles from the tutorial examples and extended them.
// this is routine research that I would normally be doing in my work as well
// https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-7.0&tabs=visual-studio
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio
// https://www.c-sharpcorner.com/blogs/implementation-of-unit-test-using-xunit-and-moq-in-net-core-6-web-api

/*
To confirm, I am using Entity Framework Core 6, which aligns with .Net 6

https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/plan
EF Core 6.0 will align with .NET 6 as a long-term support (LTS) release

I implemented this following the tutorial examples for .Net 6 and Entity Framework CORE
I only have 8 test rows for clients, but I implemented API response paging with a page size of 5 rows, 
this can be changed for a larger database.
the page number is optional and will default to page 1 (page 1 is the first page)
I have worked with hardware terminal clients that have problems with Http error codes.
so for failed lookups e.g. for Read/Update/Delete, or Create conflicts,
I prefer to return 200 success codes with extra info in the return object,
but that means my API users have to check the extra info in the return object
The Database Connection string is in appsettings.json

CRUD unit tests are implemented with Moq,
Asserts are written using Fluent Assertions to be more readable
https://fluentassertions.com/

To protect from overposting attacks, I only enable the specific properties I want to bind to
http://go.microsoft.com/fwlink/?LinkId=317598.

For production, I would also consider going over this checklist and guide
https://cheatsheetseries.owasp.org/cheatsheets/Cross_Site_Scripting_Prevention_Cheat_Sheet.html
https://owasp.org/www-project-code-review-guide/
*/

namespace PeterWongClientRestApi.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // CRUD implementations
        // To protect from overposting attacks, I only enable the specific properties I want to bind to
        // http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("CreateClient")]
        public async Task<ClientResponseModel> CreateClientAsync([Bind("UniqueId,ClientName,ContactEmailAddress,DateBecameCustomer")] ClientModel clientModel)
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

        // To protect from overposting attacks, I only enable the specific properties I want to bind to
        // http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("UpdateClient")]
        public async Task<ClientResponseModel> UpdateClientAsync([Bind("ID,UniqueId,ClientName,ContactEmailAddress,DateBecameCustomer")] ClientModel clientModel)
        {
            return await _clientService.UpdateClientAsync(clientModel);
        }

        [HttpDelete("DeleteClient")]
        public async Task<ClientResponseModel> DeleteClientAsync(int id)
        {
            return await _clientService.DeleteClientAsync(id);
        }
    }
}