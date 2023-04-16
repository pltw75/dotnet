using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeterWongClientRestApi.Data;
using PeterWongClientRestApi.Models;

// FULL DISCLOSURE
// code from the tutorial examples below was used
// https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-7.0&tabs=visual-studio
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio
namespace PeterWongClientRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        const int PAGE_SIZE = 10;

        private readonly ILogger<ClientController> _logger;
        private readonly ClientContext _context;

        public ClientController(ILogger<ClientController> logger, ClientContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetClient")]
        public async Task<IEnumerable<ClientModel>> Get()
        {
            var clientList = await _context.Clients.Take(PAGE_SIZE).ToListAsync();

            return clientList;
        }
    }
}