## Abstraction
Abstraction for me is like a dish. The only part most of us get is the delicious serving. This abstracts the complexity of the cooking process, leaving us to only judge the output of the process.

## The Scenario
In the following scenario, I would mostly use layers of abstractions on the frontend portion of the application. An example of that would be if the Admin wants to make a new Checking Account, he/she would only need to push buttons and fill out fields **while my methods or controllers (which are abstracted away from them) are handling their request giving out proper responses.**
```
[HttpGet]
OpenANewAccountForCustomer(int customerId, AccountType accountType) 
{
  //send this info to Post 
}
[HttpPost]
OpenANewAccountForCustomer() 
{
  //make new account and save to database
}
```

## Inheritance
I think the best example for understanding inheritance for me is a **family tree**. You inherit genetic traits from your parents, they inherit their genetic traits from their parents and so on. Similarly in Programming, a `childClass` can inherit properties and methods from a `parentClass`.

## The Scenario
In the bank application, I would make use Inheritance when making Accounts. Even if there are multiple different types of accounts fundamentally they all track a balance for their customer. So I would make a base class of `Account` and have other account classes inherit from it.

```
class Account  
{
  //all the properties needed for an account to exist
}

class Checking : Account
{
  //all the properties needed for an account to exist(Inherited by Account)
  public double OverdrawFees {get; set}
}

class TFSA : Account
{
  //all the properties needed for an account to exist(Inherited by Account)
}
```




## Encapsulation
I think a good example for understanding encapsulation in Programming is **understanding the basics of how a car works**. A car needs an engine, ignition, tires, seats and etc. All of those different parts make up the `Car`.  The `Car` encapsulates those different parts and is able to fully function. In Programming the `Car` would be consider an object, a class or a method that encapsulates related pieces of data in it. This allows your code to be separated and less prone to errors.

## The Scenario
In the bank application, I would use encapsulation where I want a group of related data to stay. Much like when making a class or a method. For example the `Customer` class encapsulates all the methods and properties needed for a Customer. This would keep my Customer properties and methods separate from my other code to avoid any possible errors. 

```
class Customer  
{
  public int Id {get; set;}
  public string Name {get; set;}
  public List<Account> {get; set;}

  public List<Account> GetCustomerAccounts() 
  {
    //return all accounts of customers
  }
  public void Withdraw(double amount, int accountId)
  {
    //withdraw money from their account
  }
}
```
## Polymorphism
The only real way I understood polymorphism is by seeing it in through code. Polymorphism allows a `ChildClass(Derived Class)` to be treated the same way as it's `ParentClass(BaseClass)`. Like for example here I could add the instance of `Car` and `Bicycle` in the same List because both of those classes have the same base class; `Vehicle`.

```
List<Vehicle> vehicles = new List<Vehicle>() 
{
  new Car(),
  new Bicycle(),
}

class Vehicle
{
  public int Tires {get; set;}
  public virtual void Ride() 
  {
    Console.WriteLine("Ride method invoked on the base class Vehicle");
  }
}
class Car : Vehicle
{
  public int Tires {get; set;}
  public override void Ride() 
  {
    Console.WriteLine("Riding a Car");
  }
}

class Bicycle : Vehicle
{
  public int Tires {get; set;}
  public override void Ride() 
  {
    Console.WriteLine("Riding a Bicycle");
  }
}
```
Another thing that Polymorphism allows us to do is override methods or properties of the `ParentClass(BaseClass)` in the  `ChildClass(Derived Class)` to do something that is only unique to that `ChildClass(Derived Class)` and then on runtime that method or property will be executed differently for that instance. In the previous example I have overridden the Ride() method on both the derived classes, which made them execute different outputs for the same method.

```
List<Vehicle> vehicles = new List<Vehicle>() 
{
  new Car(),
  new Bicycle(),
}

foreach(Vehicle vehicle in vehicles) 
{
  vehicle.Ride();
}
/*
Output:
"Riding a Car"
"Riding a Bicycle"
*/
```
## This Scenario
In the bank application, I think a good place to utilize polymorphism is when I want to see all accounts regardless of what type they are. If all derived classes of different accounts inherit from one base class `Account` then it would be easier to keep track of them.