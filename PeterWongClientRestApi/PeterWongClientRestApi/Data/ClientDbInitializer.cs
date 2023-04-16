using PeterWongClientRestApi.Models;
    
namespace PeterWongClientRestApi.Data
{
    public static class ClientDbInitializer
    {
        public static void Initialize(ClientContext context)
        {
            // Look for any Clients.
            if (context.Clients.Any())
            {
                return; // DB has been seeded
            }

            var clients = new ClientModel[]
            {
                new ClientModel { UniqueId = Guid.NewGuid(), ClientName = "Carson", ContactEmailAddress = "Alexander", DateBecameCustomer = DateTime.Parse("2019-09-01") },
                new ClientModel { UniqueId = Guid.NewGuid(), ClientName = "Meredith", ContactEmailAddress = "Alonso", DateBecameCustomer = DateTime.Parse("2017-09-01") },
                new ClientModel { UniqueId = Guid.NewGuid(), ClientName = "Arturo", ContactEmailAddress = "Anand", DateBecameCustomer = DateTime.Parse("2018-09-01") },
                new ClientModel { UniqueId = Guid.NewGuid(), ClientName = "Gytis", ContactEmailAddress = "Barzdukas", DateBecameCustomer = DateTime.Parse("2017-09-01") },
                new ClientModel { UniqueId = Guid.NewGuid(), ClientName = "Yan", ContactEmailAddress = "Li", DateBecameCustomer = DateTime.Parse("2017-09-01") },
                new ClientModel { UniqueId = Guid.NewGuid(), ClientName = "Peggy", ContactEmailAddress = "Justice", DateBecameCustomer = DateTime.Parse("2016-09-01") },
                new ClientModel { UniqueId = Guid.NewGuid(), ClientName = "Laura", ContactEmailAddress = "Norman", DateBecameCustomer = DateTime.Parse("2018-09-01") },
                new ClientModel { UniqueId = Guid.NewGuid(), ClientName = "Nino", ContactEmailAddress = "Olivetto", DateBecameCustomer = DateTime.Parse("2019-09-01") }
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();
        }
    }
}
