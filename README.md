# Pillar Technologies - Vending Machine Kata Interview Submission

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

[Visual Studio 2017 - Community Edition](https://www.visualstudio.com/downloads/) - Build IDE

[Git for Windows](https://git-scm.com/download/win) - Git

### Building 

*  Download and Install Visual Studio 2017 Community Edition
	* Select .NET desktop environment in install options 
	* Install 
* Download and Install Github for Windows
* Retrive a copy of the project source code from GitHub at  https://github.com/ebrady1/PillarKata-VendingMachine.git 
* From Visual Studio, load PillarKata-VendingMachine.sln
* Select  **Build->Rebuild Solution**.   All projects should rebuild with no errors and 1 Warning

## Architecture Notes 

High Level class diagram can be found in the docs directory.  This shows simple class diagrams and provides a broad overview 
of code architecture.

This software was designed to be Event Driven, to minimize impact on CPU and memory resources by the host system.   

## Running the tests

To run unit tests, select **Test -> Run -> All Tests**.   All Tests will take 10+ seconds to execute 
depending upon the speed of your machine.   All unit tests should run cleanly with no  errors

## Running the Vending Machine Simulator
Along with individual test cases, this build contains a Vending Machine Simulator GUI to demonstrate the functionality of the Vending Machine
Use Cases. 

To Run, select Start and begin using the GUI.   The Vending Machine starts with no money "inside of it", so initially, Exact Change
only will be required for certain product selections.  As money is added to the machine, and certain products selected, then 
making change becomes possible.   To view this functionality, use some of the higher coin denominations and select either Candy
or Chips for the product selection.

As products are dispensed, their inventory will decrease which ultimately allows for a **"Sold Out"** condition. 

The simulator will correctly display all of the defined use cases when used.

## Vending Machine Class Tests
Included in the build are 10 different functionality tests.  These tests start by testing the functionality of the lower level classes, and
gradually work up to the full blown test cases.  This method was chosen to allow development of the architecture per **ClassDiagram.pdf** located
in the docs directory. 

### CoinChangerTests
These tests are specific to the functionality of the low level CoinChanger class, which is responsible for accepting money, making change and 
refunding money.

**--- InsertCoins ---**

This unit test validate the ability of the CoinChanger class to accept a coin.  Various combinations of "Valid" and "Invalid" coins are
attempted

**--- GiveRefund ---**

This unit test validate the ability of the CoinChanger class to give a refund appropriately.  The tests inserts different combinations of 
money into the Coin Changer and issues a refund in different scenarios.

### ProductManagerTests 
These tests are specific to the functionality of the low level ProductManager class, which is responsible for tracking product inventory, prices and
dispensing the product when requested.

**--- StockProducts ---**

While not a published use case, this test validates the ability of the ProductManager class to take stocking information from the Vendor. 
"Vendors" can add products to the machine and set their associated price levels. 

**--- DispenseProducts ---**

This unit test validates the ability of the ProductManager class to dispense products, maintain correct inventory levels and notify the
rest of the system if dispensing of the product was succesful or not. 

### VendingMachineCtrlTests 
These tests are VendingMachine system tests that implement testing functions related to the defined Kata use cases. 

**--- AcceptCoins ---**

Validate the ability of the VendingMachine to accept coins and track the amount of money inserted into the machine since the
last refund was issued. A combination of "Valid" and "InValid" coins are tested. 

**--- ExactChangeOnly ---**

Validate the ability of the VendingMachine to know when it is not possible make the correct change based upon the customer
product selection and the amount of money already in the machine.

**--- MakeChange ---**

Validate the ability of the VendingMachine to make the correct amount of change based upon the amount of money contained in
the vending machine.  

**--- ReturnCoins ---**

Validate the ability of the VendingMachine to return the proper amount of money when the customer presses the Refund Button.  

**--- SelectProduct ---**

Validate the ability of the Vending Machine to allow the customer to properly select a product.  This test evaluates the amount of money
inserted, the availability of the product, and if the Vending Machine can make the proper amount of change. i

**--- SoldOut ---**

Validate the ability of the Vending Machine to prevent the selection of a product if it is Sold Out. 

## Authors

* **Ed Brady** - [Kata Submission](https://github.com/ebrady1/PillarKata-VendingMachine)

