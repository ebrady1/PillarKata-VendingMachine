using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PillarKata_VendingMachine;

namespace PillarKata_VendingMachineTests
{
    [TestClass]
    public class VendingMachineCtrlTests
    {
        int m_lastCoinValue = 0;
        int m_amountInserted = 0;
        string m_displayString = "";

        public void StatusNotify(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(VendingMachineCtrl))
            {
                VendingMachineCtrl vmCtrl = (VendingMachineCtrl)sender;
                VendingMachineCtrlEventArgs vmCtrlEventArgs = (VendingMachineCtrlEventArgs)e;

                switch (vmCtrlEventArgs.Status)
                {
                    case VendingMachineCtrl.VendingMachineStatus.DISPLAY_UPDATE:
                    {
                        m_displayString = vmCtrlEventArgs.DisplayData; 
                        break;
                    }
                }
            }

        }
        /// <summary>
        /// Display Related Tests for the AcceptCoins Use Case
        /// </summary>
        [TestMethod]
        public void AcceptCoins()
        {
            VendingMachineCtrl vmCtrl = new VendingMachineCtrl();
            vmCtrl.VendingMachineStatusNotify += StatusNotify;

            Assert.AreEqual("Insert Coin", m_displayString);

            //Try various "valid" coin methods
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Nickel"), "Nickel not detected correctly");
            Assert.AreEqual("$0.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dime"), "Dime not detected correctly");
            Assert.AreEqual("$0.15", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual("$0.40", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual("$0.65", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Half Dollar"), "Half Dollar not detected correctly");
            Assert.AreEqual("$1.15", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dollar"), "Dollar not detected correctly");
            Assert.AreEqual("$2.15", m_displayString, "Display Incorrect");

            Assert.AreEqual(true, vmCtrl.AcceptCoin("5"), "Nickel not detected correctly");
            Assert.AreEqual("$2.20", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("10"), "Dime not detected correctly");
            Assert.AreEqual("$2.30", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("25"), "Quarter not detected correctly");
            Assert.AreEqual("$2.55", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("50"), "Half Dollarnot detected correctly");
            Assert.AreEqual("$3.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("100"), "Dollar not detected correctly");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");

            //Try "invalid" coins
            Assert.AreEqual(false, vmCtrl.AcceptCoin("Penny"), "Invalid coin detected as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(false, vmCtrl.AcceptCoin("1"), "Invalid coin detected as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");

            Assert.AreEqual(false, vmCtrl.AcceptCoin(""), "Invalid coin detecte as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(false, vmCtrl.AcceptCoin("5 Centavo"), "Invalid coin detecte as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(false, vmCtrl.AcceptCoin("10 Kopecks"), "Invalid coin detecte as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(false, vmCtrl.AcceptCoin("Rouble"), "Invalid coin detecte as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(false, vmCtrl.AcceptCoin("5 Roubles"), "Invalid coin detecte as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(false, vmCtrl.AcceptCoin("Talon"), "Invalid coin detecte as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(false, vmCtrl.AcceptCoin("Good Looks"), "Invalid coin detecte as good");
            Assert.AreEqual("$4.05", m_displayString, "Display Incorrect");
        }

        /// <summary>
        /// Display Related Tests for the 
        /// </summary>
        [TestMethod]
        public void ReturnCoins()
        {
            VendingMachineCtrl vmCtrl = new VendingMachineCtrl();
            vmCtrl.VendingMachineStatusNotify += StatusNotify;

            Assert.AreEqual("Insert Coin", m_displayString);
            
            //Try various "valid" coin methods
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Nickel"), "Nickel not detected correctly");
            Assert.AreEqual("$0.05", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dime"), "Dime not detected correctly");
            Assert.AreEqual("$0.15", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual("$0.40", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual("$0.65", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Half Dollar"), "Half Dollar not detected correctly");
            Assert.AreEqual("$1.15", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dollar"), "Dollar not detected correctly");
            Assert.AreEqual("$2.15", m_displayString, "Display Incorrect");

            //Issue a refund
            Assert.AreEqual(true, vmCtrl.IssueRefund());

            //At this point, the Display should indicae that no money is inserted.
            Assert.AreEqual("Insert Coin", m_displayString);

            Assert.AreEqual(true, vmCtrl.AcceptCoin("Nickel"), "Nickel not detected correctly");
            Assert.AreEqual("$0.05", m_displayString, "Display Incorrect");
            
            //Issue a refund again
            Assert.AreEqual(true, vmCtrl.IssueRefund());
            
            //At this point, the Display should indicae that no money is inserted.
            Assert.AreEqual("Insert Coin", m_displayString);


        }

        /// <summary>
        /// Test Cases for the Select Product Use Case
        /// </summary>
        [TestMethod]
        public void SelectProduct()
        {
            VendingMachineCtrl vmCtrl = new VendingMachineCtrl();
            vmCtrl.VendingMachineStatusNotify += StatusNotify;

            Assert.AreEqual(true, vmCtrl.StockProduct("Cola"));
            Assert.AreEqual(true, vmCtrl.StockProduct("Chips"));
            Assert.AreEqual(true, vmCtrl.StockProduct("Candy"));

            Assert.AreEqual(true, vmCtrl.SetProductPrice("Cola", 100));
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Chips", 50));
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Cola", 65));

            //At this point, the Display should indicae that no money is inserted.
            Assert.AreEqual("Insert Coin", m_displayString);

            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dollar"), "Dollar not detected correctly");
            Assert.AreEqual("$1.00", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.SelectProduct("Cola"), "Select Product Failed");
            Assert.AreEqual("Thank You", m_displayString);
            Thread.Sleep(5000);
            Assert.AreEqual("Insert Coin", m_displayString);

            Assert.AreEqual(true, vmCtrl.AcceptCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual("$0.25", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual("$0.50", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.SelectProduct("Chips"), "Select Product Failed");
            Assert.AreEqual("Thank You", m_displayString);
            Thread.Sleep(5000);
            Assert.AreEqual("Insert Coin", m_displayString);

            Assert.AreEqual(true, vmCtrl.AcceptCoin("Half Dollar"), "Half Dollar not detected correctly");
            Assert.AreEqual("$0.50", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dime"), "Dime not detected correctly");
            Assert.AreEqual("$0.60", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Nickel"), "Nickel not detected correctly");
            Assert.AreEqual("$0.65", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.SelectProduct("Candy"), "Select Product Failed");
            Assert.AreEqual("Thank You", m_displayString);
            Thread.Sleep(5000);
            Assert.AreEqual("Insert Coin", m_displayString);

        }

        /// <summary>
        /// Test Cases for the Select Product Use Case
        /// </summary>
        [TestMethod]
        public void MakeChange()
        {
            VendingMachineCtrl vmCtrl = new VendingMachineCtrl();
            vmCtrl.VendingMachineStatusNotify += StatusNotify;

            /// Stock the machine with products (Vendor Specific function) 
            Assert.AreEqual(true, vmCtrl.StockProduct("Cola"));
            Assert.AreEqual(true, vmCtrl.StockProduct("Chips"));
            Assert.AreEqual(true, vmCtrl.StockProduct("Candy"));

            /// Set Prices for each product type (Vendor Specific Function)
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Cola", 100));
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Chips", 50));
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Candy", 65));

            //At this point, the Display should indicae that no money is inserted.
            Assert.AreEqual("Insert Coin", m_displayString);

            //First, buy a product to add currency to the vending machine, so that it will
            //have something to return in the test case. 
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Quarter"), "Quarter not detected correctly");
            Assert.AreEqual("$0.25", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dime"), "Dime not detected correctly");
            Assert.AreEqual("$0.35", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dime"), "Dime not detected correctly");
            Assert.AreEqual("$0.45", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Nickel"), "Dime not detected correctly");
            Assert.AreEqual("$0.50", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.SelectProduct("Chips"), "Select Product Failed");
            Assert.AreEqual("Thank You", m_prevDisplayString);
            Assert.AreEqual("Insert Coin", m_displayString);

            //Insert a dollar, but buy a $0.65 item
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dollar"), "Half Dollar not detected correctly");
            Assert.AreEqual("$1.00", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.SelectProduct("Candy"), "Select Product Failed");
            Assert.AreEqual("Thank You", m_prevDisplayString);
            Assert.AreEqual("Insert Coin", m_displayString);
        }

        /// <summary>
        /// Test Cases for the Exact Change Only Use Case
        /// </summary>
        [TestMethod]
        public void ExactChangeOnly()
        {
            VendingMachineCtrl vmCtrl = new VendingMachineCtrl();
            vmCtrl.VendingMachineStatusNotify += StatusNotify;

            /// Stock the machine with products (Vendor Specific function) 
            Assert.AreEqual(true, vmCtrl.StockProduct("Cola"));
            Assert.AreEqual(true, vmCtrl.StockProduct("Chips"));
            Assert.AreEqual(true, vmCtrl.StockProduct("Candy"));

            /// Set Prices for each product type (Vendor Specific Function)
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Cola", 100));
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Chips", 50));
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Candy", 65));

            //At this point, the Display should indicae that no money is inserted.
            Assert.AreEqual("Insert Coin", m_displayString);

            //Insert a dollar, but buy a $0.65 item
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dollar"), "Half Dollar not detected correctly");
            Assert.AreEqual("$1.00", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.SelectProduct("Candy"), "Select Product Failed");
            Assert.AreEqual("Exact Change Only", m_prevDisplayString);
            Assert.AreEqual("$1.00", m_displayString);
        }

        /// <summary>
        /// Test Cases for the Exact Change Only Use Case
        /// </summary>
        [TestMethod]
        public void SoldOut()
        {
            VendingMachineCtrl vmCtrl = new VendingMachineCtrl();
            vmCtrl.VendingMachineStatusNotify += StatusNotify;

            /// Set Prices for each product type (Vendor Specific Function)
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Cola", 100));
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Chips", 50));
            Assert.AreEqual(true, vmCtrl.SetProductPrice("Candy", 65));

            /// Stock the machine with products (Vendor Specific function) 
            Assert.AreEqual(true, vmCtrl.StockProduct("Chips"));
            Assert.AreEqual(true, vmCtrl.StockProduct("Candy"));

            //At this point, the Display should indicae that no money is inserted.
            Assert.AreEqual("Insert Coin", m_displayString);

            //Insert a dollar, but buy a $0.65 item
            Assert.AreEqual(true, vmCtrl.AcceptCoin("Dollar"), "Half Dollar not detected correctly");
            Assert.AreEqual("$1.00", m_displayString, "Display Incorrect");
            Assert.AreEqual(true, vmCtrl.SelectProduct("Cola"), "Select Product Failed");
            Assert.AreEqual("Sold Out", m_prevDisplayString);
            Assert.AreEqual("$1.00", m_displayString);
        }
    }
}
