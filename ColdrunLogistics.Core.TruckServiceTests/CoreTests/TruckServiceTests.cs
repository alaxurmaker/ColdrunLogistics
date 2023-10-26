using NUnit.Framework;
using Moq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColdrunLogistics.Data.Models.Trucks;
using ColdrunLogistics.Data;
using ColdrunLogistics.Core.Services;
using ErrorOr;
using ColdrunLogistics.Models.Utilities.Errors;
using ColdrunLogistics.Api.Models.Enums;

namespace ColdrunLogistics.UnitTests.CoreTests
{
    [TestFixture]
    public class TruckServiceTests
    {
        [Test]
        public void CreateTruck_ShouldReturnCreated()
        {
            // Arrange
            var truck = Truck.CreateTruck("Ciężarówka 1", "ABC123", "Opis 1", new Guid("12345678-1234-1234-1234-1234567890AB"), TruckStatusType.OutOfService);
            var truckRepositoryMock = new Mock<ITruckRepository>();
            truckRepositoryMock.Setup(repo => repo.CreateTruck(truck));
            var truckService = new TruckService(truckRepositoryMock.Object);

            // Act
            var result = truckService.CreateTruck(truck);

            // Assert
            Assert.AreEqual(Result.Created, result.Value);
            truckRepositoryMock.Verify(repo => repo.CreateTruck(truck), Times.Once);
        }

        [Test]
        public void GetTruckById_ShouldReturnTruck_WhenTruckExists()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var existingTruck = Truck.CreateTruck("Ciężarówka 1", "ABC123", "Opis 1", truckId, TruckStatusType.OutOfService);
            var truckRepositoryMock = new Mock<ITruckRepository>();
            truckRepositoryMock.Setup(repo => repo.GetTruckById(truckId)).Returns(existingTruck);
            var truckService = new TruckService(truckRepositoryMock.Object);

            // Act
            var result = truckService.GetTruckById(truckId);

            // Assert
            Assert.AreEqual(existingTruck, result.Value);
        }

        [Test]
        public void GetTruckById_ShouldReturnNotFoundError_WhenTruckNotFound()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truckRepositoryMock = new Mock<ITruckRepository>();
            var truckService = new TruckService(truckRepositoryMock.Object);

            // Act
            var result = truckService.GetTruckById(truckId);

            // Assert
            Assert.AreEqual(TruckErrors.NotFound, result.FirstError);
        }

        [Test]
        public void UpdateTruck_ShouldReturnUpdated_WhenTruckExistsAndStatusAllowed()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            
            var existingTruck = Truck.CreateTruck("Ciężarówka 1", "ABC123", "Opis 1", truckId, TruckStatusType.Loading);
            var newTruck = Truck.CreateTruck("Ciężarówka 1", "ABC123", "Opis 1", truckId, TruckStatusType.ToJob);
            var truckRepositoryMock = new Mock<ITruckRepository>();
            truckRepositoryMock.Setup(repo => repo.GetTruckById(truckId)).Returns(existingTruck);
            var truckService = new TruckService(truckRepositoryMock.Object);

            // Act
            var result = truckService.UpdateTruck(newTruck);

            // Assert
            Assert.AreEqual(Result.Updated, result.Value);
            truckRepositoryMock.Verify(repo => repo.UpdateTruck(newTruck), Times.Once);
        }

        [Test]
        public void UpdateTruck_ShouldReturnNotFoundError_WhenTruckNotFound()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var newTruck = Truck.CreateTruck("Ciężarówka 1", "ABC123", "Opis 1", truckId, TruckStatusType.OutOfService);
            var truckRepositoryMock = new Mock<ITruckRepository>();
            var truckService = new TruckService(truckRepositoryMock.Object);

            // Act
            var result = truckService.UpdateTruck(newTruck);

            // Assert
            Assert.AreEqual(TruckErrors.NotFound, result.FirstError);
        }

        [Test]
        public void UpdateTruck_ShouldReturnInvalidStatusError_WhenStatusNotAllowed()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var existingTruck = Truck.CreateTruck("Ciężarówka 1", "ABC123", "Opis 1", truckId, TruckStatusType.Loading);
            var newTruck = Truck.CreateTruck("Ciężarówka 1", "ABC123", "Opis 1", truckId, TruckStatusType.Returning);
            var truckRepositoryMock = new Mock<ITruckRepository>();
            truckRepositoryMock.Setup(repo => repo.GetTruckById(truckId)).Returns(existingTruck);
            var truckService = new TruckService(truckRepositoryMock.Object);

            // Act
            var result = truckService.UpdateTruck(newTruck);

            // Assert
            Assert.AreEqual(TruckErrors.InvalidStatus, result.FirstError);
        }

        [Test]
        public void DeleteTruck_ShouldReturnDeleted()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truckRepositoryMock = new Mock<ITruckRepository>();
            var truckService = new TruckService(truckRepositoryMock.Object);

            // Act
            var result = truckService.DeleteTruck(truckId);

            // Assert
            Assert.AreEqual(Result.Deleted, result.Value);
            truckRepositoryMock.Verify(repo => repo.DeleteTruck(truckId), Times.Once);
        }
    }
}