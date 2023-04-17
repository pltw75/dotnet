using FluentAssertions;
using Moq;
using PeterWongClientRestApi.Controllers;
using PeterWongClientRestApi.Data;
using PeterWongClientRestApi.Models;
using PeterWongClientRestApi.Services;

namespace PeterWongUnitTestProject
{
    public class UnitTestController
    {
        private readonly Mock<IClientService> clientService;
        public UnitTestController()
        {
            clientService = new Mock<IClientService>();
        }

        // CRUD tests
        // Asserts are written using Fluent Assertions to be more readable
        // https://fluentassertions.com/
        [Fact]
        public void CreateClientAsync_ClientResponseModel()
        {
            // arrange
            var clientList = ClientDbInitializer.GetClientModels();

            for (int i = 0; i < clientList.Count(); ++i)
            {
                var clientResponseModel = new ClientResponseModel()
                {
                    clientModel = clientList[i],
                    IsOk = true,
                    StatusOrError = "Ok"
                };

                // Moq here
                // .Result can deadlock in production but it is safe for testing
                clientService.Setup(x => x.CreateClientAsync(clientList[i]).Result)
                    .Returns(clientResponseModel);
                var clientController = new ClientController(clientService.Object);

                // act
                var clientResult = clientController.CreateClientAsync(clientList[i]).Result;

                // Fluent Assertions
                clientResult.Should().NotBeNull();
                clientResult.IsOk.Should().BeTrue();
                clientResult.clientModel.UniqueId.Should().NotBe(new Guid());
            }
        }

        [Fact]
        public void ReadClientPage_ClientPage()
        {
            // arrange
            var clientList = ClientDbInitializer.GetClientModels(); 

            // Moq paging here
            clientService.Setup(x => x.ReadClientPage(0))
                .Returns(clientList.Take(ClientService.PAGE_SIZE));

            var clientController = new ClientController(clientService.Object);

            // act
            var clientResult = clientController.ReadClientPage(0);

            // Fluent Assertions
            clientResult.Should().NotBeNull();
            clientResult.Count().Should().Be(ClientService.PAGE_SIZE);

            for (int i = 0; i < clientResult.Count(); ++i)
            {
                clientResult.ToList()[i].Should().NotBeNull();
                clientResult.ToList()[i].IsOk.Should().BeTrue();
                clientResult.ToList()[i].clientModel.Should().NotBeNull();
                clientResult.ToList()[i].clientModel.UniqueId.Should().NotBe(new Guid());
                clientResult.ToList()[i].clientModel.UniqueId.Should().Be(clientList[i].UniqueId);
            }
        }

        [Fact]
        public void ReadClientByIdAsync_ClientResponseModel()
        {
            // arrange
            var clientList = ClientDbInitializer.GetClientModels();

            for (int i=0; i<clientList.Count(); ++i)
            {
                var clientResponseModel = new ClientResponseModel()
                {
                    clientModel = clientList[i],
                    IsOk = true,
                    StatusOrError = "Ok"
                };

                // Moq here
                // .Result can deadlock in production but it is safe for testing
                clientService.Setup(x => x.ReadClientByIdAsync(clientResponseModel.clientModel.ID).Result)
                    .Returns(clientResponseModel);

                var clientController = new ClientController(clientService.Object);

                // act
                // .Result can deadlock in production but it is safe for testing
                var clientResult = clientController.ReadClientByIdAsync(clientResponseModel.clientModel.ID).Result;

                // Fluent Assertions
                clientResult.Should().NotBeNull();
                clientResult.IsOk.Should().BeTrue();
                clientResult.clientModel.UniqueId.Should().Be(clientResponseModel.clientModel.UniqueId);
                clientResult.clientModel.UniqueId.Should().NotBe(new Guid());
            }
        }

        [Theory]
        [InlineData("Meredith@Alonso.com")]
        public void CheckClientExistOrNotByContactEmailAddress_ClientResponseModel(string contactEmaiAddress)
        {
            // arrange
            var clientList = ClientDbInitializer.GetClientModels();

            // Moq here
            clientService.Setup(x => x.ReadClientPage(0))
                .Returns(clientList.Take(ClientService.PAGE_SIZE));

            var clientController = new ClientController(clientService.Object);

            // act
            var clientResult = clientController.ReadClientPage(0);
            var expectedContactEmailAddress = clientResult.ToList()[1].clientModel.ContactEmailAddress;

            // Fluent Assertions
            clientResult.Should().NotBeNull();
            clientResult.ToList()[1].IsOk.Should().BeTrue();
            clientResult.ToList()[1].clientModel.Should().NotBeNull();
            clientResult.ToList()[1].clientModel.UniqueId.Should().NotBe(new Guid());
            expectedContactEmailAddress.Should().Be(contactEmaiAddress);
        }

        [Fact]
        public void UpdateClientAsync_ClientResponseModel()
        {
            // arrange
            var clientList = ClientDbInitializer.GetClientModels();

            for (int i = 0; i < clientList.Count(); ++i)
            {
                var clientResponseModel = new ClientResponseModel()
                {
                    clientModel = clientList[i],
                    IsOk = true,
                    StatusOrError = "Ok"
                };

                // Moq here
                // .Result can deadlock in production but it is safe for testing
                clientService.Setup(x => x.UpdateClientAsync(clientList[i]).Result)
                    .Returns(clientResponseModel);
                var clientController = new ClientController(clientService.Object);

                // act
                var clientResult = clientController.UpdateClientAsync(clientList[i]).Result;

                // Fluent Assertions
                clientResult.Should().NotBeNull();
                clientResult.IsOk.Should().BeTrue();
                clientResult.clientModel.UniqueId.Should().NotBe(new Guid());
            }
        }

        [Fact]
        public void DeleteClientAsync_ClientResponseModel()
        {
            // arrange
            var clientList = ClientDbInitializer.GetClientModels();

            for (int i = 0; i < clientList.Count(); ++i)
            {
                var clientResponseModel = new ClientResponseModel()
                {
                    clientModel = clientList[i],
                    IsOk = true,
                    StatusOrError = "Ok"
                };

                // Moq here
                // .Result can deadlock in production but it is safe for testing
                clientService.Setup(x => x.DeleteClientAsync(clientList[i].ID).Result)
                    .Returns(clientResponseModel);
                var clientController = new ClientController(clientService.Object);

                // act
                var clientResult = clientController.DeleteClientAsync(clientList[i].ID).Result;

                // Fluent Assertions
                clientResult.Should().NotBeNull();
                clientResult.IsOk.Should().BeTrue();
                clientResult.clientModel.UniqueId.Should().NotBe(new Guid());
            }
        }
    }
}