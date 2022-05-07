using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace ParkoMatikDB.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public int PassID { get; set; }
        public virtual Pass Pass { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public bool Parked { get; set; }
    }

    public class Pass //capacity determines how many vehicles can be list up for parking
    {
        public int ID { get; set; }
        public string Purchaser { get; set; }
        public bool Premium { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }

    public class ParkingSpot
    {
        public int ID { get; set; }
        public bool Occupied { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }

    public class Reservation
    {
        public int ID { get; set; }
        public int ParkingSpotID { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }
        public int VehicleID { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsCurrent { get; set; }
    }

    public class ParkingContext : DbContext
    {
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Pass> Passes { get; set; }
        public virtual DbSet<ParkingSpot> ParkingSpots { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
    }

    public class ParkingHelper
    {
        private ParkingContext parkingContext;
        public ParkingHelper(ParkingContext context)
        {
            this.parkingContext = context;
        }

        public Pass CreatePass(string purchaser, bool premium, int capacity)
        {
            if(purchaser.Length > 20 || purchaser.Length < 3)
            {
                throw new Exception("Purchaser Name should have characters between 3 and 20 only");
            }
            if(capacity <= 0)
            {
                throw new Exception("Capacity cannot be less than or equal to 0");
            }
            Pass newPass = new Pass();
            newPass.Purchaser = purchaser;
            newPass.Premium = premium;
            newPass.Capacity = capacity;

            parkingContext.Passes.Add(newPass);
            parkingContext.SaveChanges();

            return newPass;
        }

        public ParkingSpot CreateParkingSpot()
        {
            ParkingSpot newSpot = new ParkingSpot();
            newSpot.Occupied = false;

            parkingContext.ParkingSpots.Add(newSpot);
            return newSpot;
        }
    }

}
