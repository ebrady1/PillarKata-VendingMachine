using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PillarKata_VendingMachine;

namespace PillarKata_VendingMachineTests
{
    [TestClass]
    public class CoinChangerTests
    {
        int lastCoinValue;

        public CoinChangerTests()
        {
            lastCoinValue = 0;
        }

        /// <summary>
        /// Tests variations of valid/invalid denominations for the CoinChanger 
        /// </summary>
        [TestMethod]
        public void InsertValidCoin()
        {
            //Create a new CoinChanger object
            CoinChanger coinChanger = new CoinChanger();

            //Try various "valid" coing methods
            Assert.AreEqual(true, coinChanger.InsertCoin("Nickel"), "Nickel not detected correctly");
            Assert.AreEqual(5, lastCoinValue, "Nickel coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("Dime"), "Dime not detected correctly");
            Assert.AreEqual(10, lastCoinValue, "Dime coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual(25, lastCoinValue, "Quarter coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("Half Dollar"), "Half Dollarnot detected correctly");
            Assert.AreEqual(50, lastCoinValue, "Half Dollar coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("Dollar"), "Dollar not detected correctly");
            Assert.AreEqual(100, lastCoinValue, "Dollar coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("NiCkEl"), "Nickel not detected correctly");
            Assert.AreEqual(5, lastCoinValue, "Nickel coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("dIMe"), "dIMe not detected correctly");
            Assert.AreEqual(10, lastCoinValue, "Dime coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("quarteR"), "Quarter not detected correctly");
            Assert.AreEqual(25, lastCoinValue, "Quarter coin value incorrect");

            Assert.AreEqual(true, coinChanger.InsertCoin("5"), "Nickel not detected correctly");
            Assert.AreEqual(5, lastCoinValue, "Nickel coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("10"), "Dime not detected correctly");
            Assert.AreEqual(10, lastCoinValue, "Dime coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("25"), "Quarter not detected correctly");
            Assert.AreEqual(25, lastCoinValue, "Quarter coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("50"), "Half Dollarnot detected correctly");
            Assert.AreEqual(50, lastCoinValue, "Half Dollar coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("100"), "Dollar not detected correctly");
            Assert.AreEqual(100, lastCoinValue, "Dollar coin value incorrect");

            Assert.AreEqual(true, coinChanger.InsertCoin(5), "Nickel not detected correctly");
            Assert.AreEqual(5, lastCoinValue, "Nickel coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin(10), "Dime not detected correctly");
            Assert.AreEqual(10, lastCoinValue, "Dime coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin(25), "Quarter not detected correctly");
            Assert.AreEqual(25, lastCoinValue, "Quarter coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin(50), "Half Dollarnot detected correctly");
            Assert.AreEqual(50, lastCoinValue, "Half Dollar coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin(100), "Dollar not detected correctly");
            Assert.AreEqual(100, lastCoinValue, "Dollar coin value incorrect");


            Assert.AreEqual(false, coinChanger.InsertCoin("Penny"), "Invalid coin detected as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");
            Assert.AreEqual(false, coinChanger.InsertCoin("1"), "Invalid coin detected as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");
            Assert.AreEqual(false, coinChanger.InsertCoin(1), "Invalid coin detected as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");

            Assert.AreEqual(false, coinChanger.InsertCoin(""), "Invalid coin detecte as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");
            Assert.AreEqual(false, coinChanger.InsertCoin("5 Centavo"), "Invalid coin detecte as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");
            Assert.AreEqual(false, coinChanger.InsertCoin("10 Kopecks"), "Invalid coin detecte as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");
            Assert.AreEqual(false, coinChanger.InsertCoin("Rouble"), "Invalid coin detecte as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");
            Assert.AreEqual(false, coinChanger.InsertCoin("5 Roubles"), "Invalid coin detecte as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");
            Assert.AreEqual(false, coinChanger.InsertCoin("Talon"), "Invalid coin detecte as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");
            Assert.AreEqual(false, coinChanger.InsertCoin("Good Looks"), "Invalid coin detecte as good");
            Assert.AreEqual(-1, lastCoinValue, "Invalid coin registered a value");


        }

    }
}
