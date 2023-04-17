using PeterWongClientRestApi.Models;

namespace PeterWongClientRestApi.Data
{
    public static class ClientDbInitializer
    {
        public static ClientModel[] GetClientModels()
        {
            var clients = new ClientModel[]
            {
                new ClientModel
                {
                    UniqueId = Guid.NewGuid(),
                    ClientName = "Carson",
                    ContactEmailAddress = "Carson@Alexander.com",
                    DateBecameCustomer = DateTime.Parse("2019-09-01")
                },
                new ClientModel
                {
                    UniqueId = Guid.NewGuid(),
                    ClientName = "Meredith",
                    ContactEmailAddress = "Meredith@Alonso.com",
                    DateBecameCustomer =
                    DateTime.Parse("2017-09-01")
                },
                new ClientModel
                {
                    UniqueId = Guid.NewGuid(),
                    ClientName = "Arturo",
                    ContactEmailAddress = "Arturo@Anand.com",
                    DateBecameCustomer = DateTime.Parse("2018-09-01")
                },
                new ClientModel
                {
                    UniqueId = Guid.NewGuid(),
                    ClientName = "Gytis",
                    ContactEmailAddress = "Gytis@Barzdukas.com",
                    DateBecameCustomer = DateTime.Parse("2017-09-01")
                },
                new ClientModel
                {
                    UniqueId = Guid.NewGuid(),
                    ClientName = "Yan",
                    ContactEmailAddress = "Yan@Li.com",
                    DateBecameCustomer = DateTime.Parse("2017-09-01")
                },
                new ClientModel
                {
                    UniqueId = Guid.NewGuid(),
                    ClientName = "Peggy",
                    ContactEmailAddress = "Peggy@Justice.com",
                    DateBecameCustomer = DateTime.Parse("2016-09-01")
                },
                new ClientModel
                {
                    UniqueId = Guid.NewGuid(),
                    ClientName = "Laura",
                    ContactEmailAddress = "Laura@Norman.com",
                    DateBecameCustomer = DateTime.Parse("2018-09-01")
                },
                new ClientModel
                {
                    UniqueId = Guid.NewGuid(),
                    ClientName = "Nino",
                    ContactEmailAddress = "Nino@Olivetto.com",
                    DateBecameCustomer = DateTime.Parse("2019-09-01")
                }
            };

            return clients;
        }

        public static void Initialize(ClientContext context)
        {
            // Look for any Clients.
            if (context.Clients.Any())
            {
                return; // DB has been seeded
            }

            var clients = GetClientModels();

            context.Clients.AddRange(clients);
            context.SaveChanges();
        }
    }
}
