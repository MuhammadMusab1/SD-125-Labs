using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkoMatikDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkoMatikDBTesting
{
    [TestClass]
    public class ParkingHelperTesting
    {
        private Mock<ParkingContext> MockParkingContext { get; set; }
        private Mock<DbSet<ParkingSpot>> MockParkingSpotDbSet { get; set; }
        [TestInitialize]
        public void Initialize()
        {
            //Set up fake database

            Pass mockPass = new Pass() { ID = 1, Capacity= 3, Premium = false, Purchaser = "Mohid", Vehicles = null };

            Vehicle mockVehicle1 = new Vehicle() { ID = 1, Parked = false, Pass = mockPass, PassID = 1,Reservations = null };
            Vehicle mockVehicle2 = new Vehicle() { ID = 1, Parked = false, Pass = mockPass, PassID = 1, Reservations = null };
            Vehicle mockVehicle3 = new Vehicle() { ID = 1, Parked = false, Pass = mockPass, PassID = 1, Reservations = null };
            Vehicle mockVehicle4 = new Vehicle() { ID = 1, Parked = false, Pass = null, Reservations = null };

            ParkingSpot mockParkingSpot1 = new ParkingSpot() { ID = 1, Occupied = false, Reservations = null };
            ParkingSpot mockParkingSpot2 = new ParkingSpot() { ID = 2, Occupied = false, Reservations = null };

            List<Vehicle> passVehicle = new List<Vehicle>()
            {
                mockVehicle1, mockVehicle2, mockVehicle3
            };
            mockPass.Vehicles = passVehicle;

            Reservation reservation1 = new Reservation() { 
                ID = 1, 
                IsCurrent = true, 
                Expiry = DateTime.Now, 
                ParkingSpot = mockParkingSpot1, 
                ParkingSpotID= 1, 
                Vehicle= mockVehicle1, 
                VehicleID= 1 };

            mockVehicle1.Reservations = new List<Reservation>() { reservation1 };
            mockVehicle1.Parked = true;
            mockParkingSpot1.Reservations = new List<Reservation>() { reservation1 };
            mockParkingSpot1.Occupied = true;

            var AllVehicles = new List<Vehicle>()
            {
                mockVehicle1,
                mockVehicle2,
                mockVehicle3,
                mockVehicle4
            }.AsQueryable();

            Mock<DbSet<Vehicle>> mockVehicleSet = new Mock<DbSet<Vehicle>>();
            mockVehicleSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(AllVehicles.Provider);
            mockVehicleSet.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(AllVehicles.Expression);
            mockVehicleSet.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(AllVehicles.ElementType);
            mockVehicleSet.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(AllVehicles.GetEnumerator());

            var AllPasses = new List<Pass>()
            {
                mockPass
            }.AsQueryable();

            Mock<DbSet<Pass>> mockPassSet = new Mock<DbSet<Pass>>();
            mockPassSet.As<IQueryable<Pass>>().Setup(m => m.Provider).Returns(AllPasses.Provider);
            mockPassSet.As<IQueryable<Pass>>().Setup(m => m.Expression).Returns(AllPasses.Expression);
            mockPassSet.As<IQueryable<Pass>>().Setup(m => m.ElementType).Returns(AllPasses.ElementType);
            mockPassSet.As<IQueryable<Pass>>().Setup(m => m.GetEnumerator()).Returns(AllPasses.GetEnumerator());

            var AllParkingSpots = new List<ParkingSpot>()
            {
                mockParkingSpot1,
                mockParkingSpot2,
            }.AsQueryable();

            MockParkingSpotDbSet = new Mock<DbSet<ParkingSpot>>();
            MockParkingSpotDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.Provider).Returns(AllParkingSpots.Provider);
            MockParkingSpotDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.Expression).Returns(AllParkingSpots.Expression);
            MockParkingSpotDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.ElementType).Returns(AllParkingSpots.ElementType);
            MockParkingSpotDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.GetEnumerator()).Returns(AllParkingSpots.GetEnumerator());


            MockParkingContext = new Mock<ParkingContext>();

            MockParkingContext.Setup(context => context.Vehicles).Returns(mockVehicleSet.Object);
            MockParkingContext.Setup(context => context.Passes).Returns(mockPassSet.Object);
            MockParkingContext.Setup(context => context.ParkingSpots).Returns(MockParkingSpotDbSet.Object);


            //If there are no “Current” Reservation relationships on the ParkingSpot and Vehicle, the system creates a new “Current” one.
            //Both the Vehicle and ParkingSpot have their Parked properties set as True.
        }
        [TestMethod]
        public void CreatePassThrowsExceptionIfPurchaserIsNotTheCorrectStringLength()
        {
            //Arrange
            ParkingHelper helper = new ParkingHelper(MockParkingContext.Object);

            //Act and Assert
            Assert.ThrowsException<Exception>(() =>
            helper.CreatePass("a", false, 2)); //less than 3

            Assert.ThrowsException<Exception>(() =>
            helper.CreatePass("osdkdsbfjskdfkbsdfkjsdbfjbdsfbsdkfbjs", false, 2)); //more than 20

        }
        [TestMethod]
        public void CreatePassThrowsExceptionIfCapacityIsZero()
        {
            //Arrange
            ParkingHelper helper = new ParkingHelper(MockParkingContext.Object);

            //Act and Assert
            Assert.ThrowsException<Exception>(() =>
            helper.CreatePass("aman", false, 0)); //capacity is zero

            Assert.ThrowsException<Exception>(() =>
            helper.CreatePass("aman", false, -10)); //capacity is zero

        }
        [TestMethod]
        public void CreateParkingSpotMethodWorks()
        {
            //Arrange
            ParkingHelper helper = new ParkingHelper(MockParkingContext.Object);

            //Act and Assert
            ParkingSpot newSpot = helper.CreateParkingSpot();
            MockParkingSpotDbSet.Verify(mockSet => mockSet.Add(It.IsAny<ParkingSpot>()), Times.Once());
            MockParkingContext.Verify(context => context.SaveChanges(), Times.Once());
        }

    }
}