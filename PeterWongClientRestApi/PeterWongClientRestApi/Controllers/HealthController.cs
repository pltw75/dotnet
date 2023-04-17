using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PeterWongClientRestApi.Data;
using PeterWongClientRestApi.Models;
using PeterWongClientRestApi.Services;

namespace PeterWongClientRestApi.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HealthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Heartbeat, needed by some load balancers and telemetry/analytics services to confirm your service is still alive
        // also useful to confirm your service is returning live responses not cached.
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(
                new
                {
                    RandomGuid = Guid.NewGuid().ToString(),
                    UTC = DateTime.UtcNow.ToString(),
                    MachineName = Environment.MachineName,
                    ClientContext = _configuration.GetConnectionString("ClientContext"),
                    Released = "18 Apr 2023",
                    StatusOrError = "Ok"
                });
        }
   }
}