using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Lab1SD125UnitTesting
{
    [TestClass]
    public class VehicleTrackerTesting
    {
        private VehicleTracker VehicleTracker { get; set; }
        [TestInitialize]
        public void TestInitialize() //runs before every test
        {
            VehicleTracker = new VehicleTracker(5, "123 Fake st");
        }
        [TestMethod]
        public void OnInitializingAVehicleTrackerCheckTheGenerateSlotsMethod()
        {
            //Arrange
            Dictionary<int, Vehicle> expectedVehicleList = new Dictionary<int, Vehicle>();
            for(int i = 1; i <= 10; i++)
            {
                expectedVehicleList.Add(i, null);
            }
            //Act is done by Initializing the VehicleTracker

            //Assert(use CollectionAssert to compare two collection)
            CollectionAssert.AreEqual(expectedVehicleList, VehicleTracker.VehicleList);
        }
        [TestMethod]
        public void AddVehicleMethodAddsVehicle()
        {
            //Arrange(declare a new Vehicle with the licence place “A01 T22”, and with a parking pass (bool true))
            Vehicle testVehicle = new Vehicle("ABC-123", true);
            int expectedSlotAvailableAfterOneVehicleIsAdded = VehicleTracker.Capacity - 1;

            //Act
            VehicleTracker.AddVehicle(testVehicle);

            //Assert
            Assert.AreEqual(expectedSlotAvailableAfterOneVehicleIsAdded, VehicleTracker.SlotsAvailable);
        }

        [TestMethod]
        public void AddVehicleMethodThrowsErrorWhenNoSlotIsAvailable()
        {
            //Arrange(declare a new Vehicle with the licence place “A01 T22”, and with a parking pass (bool true))
            Vehicle testVehicle1 = new Vehicle("ABC-123", true);
            VehicleTracker.AddVehicle(testVehicle1); //capicity is 1

            //Act and Assert
            Vehicle testVehicle2 = new Vehicle("EFG-456", true);
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            VehicleTracker.AddVehicle(testVehicle2));
        }
        [TestMethod]
        public void RemoveVehicleMethodRemovesVehicleWithGivenLicenceFromSlot()
        {
            //Arrange(declare a new Vehicle with the licence place “A01 T22”, and with a parking pass (bool true))
            Vehicle testVehicle1 = new Vehicle("ABC-123", true);
            VehicleTracker.AddVehicle(testVehicle1); //capicity is 1

            //Act
            int expectedSlotsAvailabelAfterRemovingOneVehicle = VehicleTracker.SlotsAvailable + 1;
            VehicleTracker.RemoveVehicle("ABC-123");

            //Assert
            Assert.AreEqual(expectedSlotsAvailabelAfterRemovingOneVehicle, VehicleTracker.SlotsAvailable);
        }
        [TestMethod]
        public void RemoveVehicleMethodRemovesVehicleWithGivenSlotNumberFromSlot()
        {
            //Arrange(declare a new Vehicle with the licence place “A01 T22”, and with a parking pass (bool true))
            Vehicle testVehicle1 = new Vehicle("ABC-123", true);
            VehicleTracker.AddVehicle(testVehicle1); //capicity is 1

            //Act
            int expectedSlotsAvailabelAfterRemovingOneVehicle = VehicleTracker.SlotsAvailable + 1;
            VehicleTracker.RemoveVehicle(1);

            //Assert
            Assert.AreEqual(expectedSlotsAvailabelAfterRemovingOneVehicle, VehicleTracker.SlotsAvailable);
        }
        [TestMethod]
        public void RemoveVehicleMethodThrowsErrorOnBadLicenseInput()
        {
            //Arrange(declare a new Vehicle with the licence place “A01 T22”, and with a parking pass (bool true))
            Vehicle testVehicle1 = new Vehicle("ABC-123", true);
            VehicleTracker.AddVehicle(testVehicle1); //capicity is 1

            //Act and Assert
            Assert.ThrowsException<NullReferenceException>(() =>
            VehicleTracker.RemoveVehicle("Not a valid License"));
        }
        [TestMethod]
        public void RemoveVehicleMethodThrowsErrorOnBadNumberSlotInput()
        {
            //Arrange(declare a new Vehicle with the licence place “A01 T22”, and with a parking pass (bool true))
            Vehicle testVehicle1 = new Vehicle("ABC-123", true);
            VehicleTracker.AddVehicle(testVehicle1); //capicity is 1

            //Act and Assert(all assertion must be true to pass a test)
            Assert.ThrowsException<ArgumentException>(() =>
            VehicleTracker.RemoveVehicle(92));

            Assert.ThrowsException<ArgumentException>(() =>
            VehicleTracker.RemoveVehicle(-1));

            //if one of them failed the whole test will fail
        }
        [TestMethod]
        public void RemoveVehicleMethodThrowsErrorOnRemovingVehicleFromAnUnfilledSlot()
        {
            //Arrange(declare a new Vehicle with the licence place “A01 T22”, and with a parking pass (bool true))

            //Act and Assert(all assertion must be true to pass a test)
            Assert.ThrowsException<NullReferenceException>(() =>
            VehicleTracker.RemoveVehicle("A01 T22"));

            Assert.ThrowsException<NullReferenceException>(() =>
            VehicleTracker.RemoveVehicle(1));

            //if one of them failed the whole test will fail
        }
        [TestMethod]
        public void ParkedPassholdersMehtodReturnsAListOfVehiclesWithAParkingPass()
        {
            //Arrange
            Vehicle testVehicle1 = new Vehicle("123-ABC", false);
            Vehicle testVehicle2 = new Vehicle("456-DEF", true);
            Vehicle testVehicle3 = new Vehicle("789-GHI", true);

            VehicleTracker.AddVehicle(testVehicle1);
            VehicleTracker.AddVehicle(testVehicle2);
            VehicleTracker.AddVehicle(testVehicle3);

            List<Vehicle> expectedVehiclesWithAParkingPass = new List<Vehicle>()
            {
                testVehicle2,
                testVehicle3
            };

            //Act
            List<Vehicle> actualVehiclesWithAParkingPass = VehicleTracker.ParkedPassholders();

            //Assert
            CollectionAssert.AreEqual(expectedVehiclesWithAParkingPass, actualVehiclesWithAParkingPass);
        }
        [TestMethod]
        public void ParkedPassholdersPercentageShowsThePercentageOfPassholders()
        {
            //Arrange
            Vehicle testVehicle1 = new Vehicle("123-ABC", false);
            Vehicle testVehicle2 = new Vehicle("456-DEF", true);
            Vehicle testVehicle3 = new Vehicle("789-GHI", true);

            VehicleTracker.AddVehicle(testVehicle1);
            VehicleTracker.AddVehicle(testVehicle2);
            VehicleTracker.AddVehicle(testVehicle3);

            double vehiclesWithPassholders = VehicleTracker.ParkedPassholders().Count;
            double totalParkedVehicles = VehicleTracker.Capacity - VehicleTracker.SlotsAvailable;

            //Act
            double expectedPercentageOfVehiclesWithPassholder = (vehiclesWithPassholders / totalParkedVehicles) * 100;
            double actualPercentageOfVehiclesWithPassholder = VehicleTracker.PassholderPercentage();

            //Assert
            Assert.AreEqual(expectedPercentageOfVehiclesWithPassholder, actualPercentageOfVehiclesWithPassholder);
        }
    }
}