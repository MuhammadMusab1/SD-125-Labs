static void Main(string[] args)
{

}
public class Vehicle
{
    public string Licence { get; set; }
    public bool Pass { get; set; }
    public Vehicle(string licence, bool pass)
    {
        this.Licence = licence;
        this.Pass = pass;
    }
}

public class VehicleTracker
{
    //PROPERTIES
    public string Address { get; set; }
    public int Capacity { get; set; }
    public int SlotsAvailable { get; set; }
    public Dictionary<int, Vehicle> VehicleList { get; set; }

    public VehicleTracker(int capacity, string address)
    {
        this.Capacity = capacity;
        this.SlotsAvailable = capacity;
        this.Address = address;
        this.VehicleList = new Dictionary<int, Vehicle>();

        this.GenerateSlots();
    }

    // STATIC PROPERTIES
    public static string BadSearchMessage = "Error: Search did not yield any result.";
    public static string BadSlotNumberMessage = "Error: No slot with number ";
    public static string SlotsFullMessage = "Error: no slots available.";

    // METHODS
    public void GenerateSlots()
    {
        for (int i = 1; i <= this.Capacity; i++)
        {
            this.VehicleList.Add(i, null);
        }
    }

    public void AddVehicle(Vehicle vehicle)
    {
        foreach (KeyValuePair<int, Vehicle> slot in this.VehicleList)
        {
            if (slot.Value == null)
            {
                this.VehicleList[slot.Key] = vehicle;
                this.SlotsAvailable--;
                return;
            }
        }
        throw new IndexOutOfRangeException(SlotsFullMessage);
    }

    public void RemoveVehicle(string licence)
    {
        try
        {
            int slot = this.VehicleList.First(v => v.Value.Licence == licence).Key;
            this.SlotsAvailable++;
            this.VehicleList[slot] = null;
        }
        catch
        {
            throw new NullReferenceException(BadSearchMessage);
        }
    }

    public bool RemoveVehicle(int slotNumber)
    {
        if (slotNumber > this.Capacity)
        {
            throw new ArgumentException("slot number is greater than capacity, it doesn't exist");
        }else if(slotNumber <= 0)
        {
            throw new ArgumentException("slot number cannot be negative or zero, it doesn't exist");
        }
        if(this.VehicleList[slotNumber] != null) 
        {
            this.VehicleList[slotNumber] = null;
            this.SlotsAvailable++;
        }
        else //no vehicle is in this slotNumber
        {
            throw new NullReferenceException(BadSearchMessage);
        }
        return true;
    }

    public List<Vehicle> ParkedPassholders()
    {
        List<Vehicle> passHolders = new List<Vehicle>();
        foreach(KeyValuePair<int, Vehicle> slot in this.VehicleList)
        {
            if(slot.Value != null)
            {
                if(slot.Value.Pass == true)
                {
                    passHolders.Add(slot.Value);
                }
            }
        }
        //passHolders = this.VehicleList.Where(slot => slot.Value.Pass).Select(slot => slot.Value).ToList();
        //passHolders.Add(this.VehicleList.FirstOrDefault(v => v.Value.Pass).Value);
        return passHolders;
    }

    public double PassholderPercentage()
    {
        double passHolders = ParkedPassholders().Count();
        double totalParkedVehicles = this.Capacity - this.SlotsAvailable;
        double percentage = (passHolders / totalParkedVehicles) * 100;
        return percentage;
    }
}

/*
When initialized, a VehicleTracker object should have empty 
slots [{SlotNumber, Vehicle}] from 1 - Capacity in VehicleTracker.VehicleList 
(ie. { {1, null}, {2, null}, {3,null}, //etc}

If the AddVehicle method is called, it should add the vehicle to the first slot in VehicleList that is not full.
If there are no open slots, it should throw a generic exception with the VehicleTracker.AllSlotsFull message.

RemoveVehicle should accept either a licence or slot number, and set that slot’s vehicle to “null”.

RemoveVehicle should throw an exception if 
the licence passed to RemoveVehicle() is not found, 
if the slot number is invalid (greater than capacity or negative), 
or the slot is not filled.

VehicleTracker should track the proper number of slots available at all times with VehicleTracker.SlotsAvailable.

The VehicleTracker.ParkedPassholders() method should return a list of all parked vehicles that have a pass.

VehicleTracker.PassholderPercentage() method should return the percentage of vehicles that are parked which have passes. 
Note that this method uses the ParkedPassholders() method to get a count of passholders.

 */
