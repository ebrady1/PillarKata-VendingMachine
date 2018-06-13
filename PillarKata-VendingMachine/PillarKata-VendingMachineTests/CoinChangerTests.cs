using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PillarKata_VendingMachine;

namespace PillarKata_VendingMachineTests
{    
    /// <summary>
    /// The main <c>CoinChangerTests</c> class.
    /// Contains all methods for performing coin test related functions.
    /// </summary>
    [TestClass]
    public class CoinChangerTests
    {
        Int32 lastCoinValue;
        Int32 amountInserted;

        /// <summary>
        /// The <c>CoinChanger</c> test Class
        /// </summary>
        public CoinChangerTests()
        {
            lastCoinValue = 0;
            amountInserted = 0;
        }

        void CoinChangerEvent(object sender, EventArgs e)
        {
            CoinChangerEventArgs args = (CoinChangerEventArgs)e;
            switch(args.EventType)
            {
                case CoinChangerEventOp.COIN_INSERTED:
                {
                        lastCoinValue = (Int32)args.Value;
                        amountInserted += (Int32)lastCoinValue;
                        break;
                }

                case CoinChangerEventOp.ISSUE_REFUND:
                {
                    CoinChanger changer = (CoinChanger)sender;
                    changer.DispenseChange((UInt32)amountInserted);
                    amountInserted = 0;
                    break;
                }
            }
        }

        /// <summary>
        /// Tests variations of valid/invalid denominations for the CoinChanger 
        /// </summary>
        [TestMethod]
        public void InsertCoins()
        {
            //Create a new CoinChanger object
            CoinChanger coinChanger = new CoinChanger();
            coinChanger.CoinChangerEvent += CoinChangerEvent;

            //Try various "valid" coing methods
            Assert.AreEqual(true, coinChanger.InsertCoin("Nickel"), "Nickel not detected correctly");
            Assert.AreEqual(5, lastCoinValue, "Nickel coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("Dime"), "Dime not detected correctly");
            Assert.AreEqual(10, lastCoinValue, "Dime coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual(25, lastCoinValue, "Quarter coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("Half Dollar"), "Half Dollar not detected correctly");
            Assert.AreEqual(50, lastCoinValue, "Half Dollar coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("Dollar"), "Dollar not detected correctly");
            Assert.AreEqual(100, lastCoinValue, "Dollar coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("NiCkEl"), "Nickel not detected correctly");
            Assert.AreEqual(5, lastCoinValue, "Nickel coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("dIMe"), "dIMe not detected correctly");
            Assert.AreEqual(10, lastCoinValue, "Dime coin value incorrect");
            Assert.AreEqual(true, coinChanger.InsertCoin("quarteR"), "Quarter not detected correctly");
            Assert.AreEqual(25, lastCoinValue, "Quarter coin value incorrect");

            //Try various "Alternate" Coin Insertions that are valid also
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

            Assert.AreEqual(false, coinChanger.InsertCoin("Penny"), "Invalid coin detected as good");
            Assert.AreEqual(false, coinChanger.InsertCoin("1"), "Invalid coin detected as good");

            Assert.AreEqual(false, coinChanger.InsertCoin(""), "Invalid coin detecte as good");
            Assert.AreEqual(false, coinChanger.InsertCoin("5 Centavo"), "Invalid coin detecte as good");
            Assert.AreEqual(false, coinChanger.InsertCoin("10 Kopecks"), "Invalid coin detecte as good");
            Assert.AreEqual(false, coinChanger.InsertCoin("Rouble"), "Invalid coin detecte as good");
            Assert.AreEqual(false, coinChanger.InsertCoin("5 Roubles"), "Invalid coin detecte as good");
            Assert.AreEqual(false, coinChanger.InsertCoin("Talon"), "Invalid coin detecte as good");
            Assert.AreEqual(false, coinChanger.InsertCoin("Good Looks"), "Invalid coin detecte as good");

        }

        /// <summary>
        /// Issue a Full Refund Test case
        /// </summary>
        [TestMethod]
        public void GiveRefund()
        {
            //Create a new CoinChanger object
            CoinChanger coinChanger = new CoinChanger();
            coinChanger.CoinChangerEvent += CoinChangerEvent;

            //Try various "valid" coing methods
            coinChanger.InsertCoin("Nickel");
            coinChanger.InsertCoin("Dime");
            coinChanger.InsertCoin("Half Dollar");
            coinChanger.InsertCoin("Dollar");
            coinChanger.InsertCoin("Dollar");

            // At this point, we should have $2.65 in the coin changer, issue the refund
            Assert.AreEqual(true, coinChanger.IssueRefund());

            //At this point, we should have no money in the coin changer, fail
            Assert.AreEqual(false, coinChanger.IssueRefund());

            coinChanger.InsertCoin("5");
            coinChanger.InsertCoin("10");
            coinChanger.InsertCoin("25");
            coinChanger.InsertCoin("50");
            coinChanger.InsertCoin("100");
            
            // At this point, we should have $1.90 in the coin changer, issue the refund
            Assert.AreEqual(true, coinChanger.IssueRefund());

            //At this point, we should have no money in the coin changer, fail
            Assert.AreEqual(false, coinChanger.IssueRefund());

        }
    }
}
