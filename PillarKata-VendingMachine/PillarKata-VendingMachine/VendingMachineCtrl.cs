using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    /// <summary>
    /// The main Vending Machine Control Class
    /// This object represents the main control for the Vending Machine
    /// </summary>
    public class VendingMachineCtrl
    {
        /// <summary>
        /// Delay time constant for displaying multiple items on display
        /// </summary>
        public const Int32 DELAY_TIME = 1000;

        /// <summary>
        /// Vending Machine Event Type
        /// </summary>
        public enum VendingMachineStatus
        {
            /// <summary>
            /// The Vending machine is requesting to update the display
            /// </summary>
            DISPLAY_UPDATE 
        }

        UInt32 m_amountInserted = 0;
        UInt32 m_lastCoinValue = 0;

        //The Vending Machine will have one Coin Changer
        CoinChanger m_coinChanger = new CoinChanger();

        //The Vending Machine will have one Product Manager
        ProductManager m_productManager = new ProductManager();

        //Initialize the lastEvent
        VendingMachineCtrlEventArgs m_lastEventArgs = new VendingMachineCtrlEventArgs();

        private event EventHandler _VendingMachineStatusNotify;
       
        /// <summary>
        /// The Operational Status of the Vending Machine, will callback immediately on subscribe
        /// </summary>
        public event EventHandler VendingMachineStatusNotify
        {
            add
            {
                _VendingMachineStatusNotify += value;
                value(this, m_lastEventArgs);
            }
            remove
            {
                _VendingMachineStatusNotify -= value;
            }
        }
        /// <summary>
        /// Product Manager Event Callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProductManagerEvent(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Helper function for displaying currencies with no prefix.
        /// </summary>
        /// <param name="currency"></param>
        void DisplayCurrencyAmount(double currency)
        {
            DisplayCurrencyAmount("", currency);
        }

        /// <summary>
        /// Display the amount inserted thus far
        /// </summary>
        /// <param name="prefix">A string to prefix to the currency </param>
        /// <param name="currency">The amount of currency to sho</param>
        void DisplayCurrencyAmount(string prefix, double currency)
        {
            if (currency != 0.0)
            {
                m_lastEventArgs.Status = VendingMachineStatus.DISPLAY_UPDATE;
                m_lastEventArgs.DisplayData = String.Format(prefix + "{0:C2}", currency);
                _VendingMachineStatusNotify(this, m_lastEventArgs);
            }
            else
            {
                DisplayInsertCoin();
            }
        }
        /// <summary>
        /// Notify the customer to Insert a Coin
        /// </summary>
        void DisplayInsertCoin()
        {
            m_lastEventArgs.Status = VendingMachineStatus.DISPLAY_UPDATE;
            m_lastEventArgs.DisplayData = "Insert Coin";
            _VendingMachineStatusNotify(this, m_lastEventArgs);
        }

        /// <summary>
        /// Wait a short delay, and then revert back to the previous display state
        /// </summary>
        void WaitAndDisplayCurrentState()
        {
            Thread t = new Thread(() =>
            {
                //Delay
                Thread.Sleep(DELAY_TIME);
                
                //Show the amount inserted thus far
                DisplayCurrencyAmount(m_amountInserted / 100.0);
            });
            t.Start();
        }

        /// <summary>
        /// Helper function to display "Exact Change Only".  
        /// After displaying "Exact Change Only", will wait a specified delay
        /// and then show the amount inserted thus far
        /// </summary>
        void DisplayExactChangeOnly()
        {
            // Update the Display to show Exact Change Only,
            // wait a delay time and then show the amount currently inserted
            m_lastEventArgs.Status = VendingMachineStatus.DISPLAY_UPDATE;
            m_lastEventArgs.DisplayData = "Exact Change Only";
            _VendingMachineStatusNotify(this, m_lastEventArgs);
            WaitAndDisplayCurrentState();
        }

        /// <summary>
        /// Helper function to display "Sold Out"
        /// After displaying "Sold Out", wait a specified delay time and then
        /// Show the amount inserted or insert coin
        /// </summary>
        void DisplaySoldOut()
        {
            m_lastEventArgs.Status = VendingMachineStatus.DISPLAY_UPDATE;
            m_lastEventArgs.DisplayData = "Sold Out";
            _VendingMachineStatusNotify(this, m_lastEventArgs);
            //Wait a predetermined amount of time and then display 
            //the amount inserted
            WaitAndDisplayCurrentState();
        }

        /// <summary>
        /// Helper function to display "Thank You"
        /// </summary>
        void DisplayThankYou()
        {
            m_lastEventArgs.Status = VendingMachineStatus.DISPLAY_UPDATE;
            m_lastEventArgs.DisplayData = "Thank You";
            _VendingMachineStatusNotify(this, m_lastEventArgs);
            WaitAndDisplayCurrentState();
        }

        /// <summary>
        /// Display the price of a product, wait a predetermined amount of 
        /// time then show the amount inserted. 
        /// </summary>
        /// <param name="amount"></param>
        void DisplayProductPrice(double amount)
        {
            DisplayCurrencyAmount("Cost:", amount);
            WaitAndDisplayCurrentState();
        }

        /// <summary>
        /// CoinChanger event callback. 
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Additional Parameters for the CoinChanger event</param>
        void CoinChangerEvent(object sender, EventArgs e)
        {
            CoinChangerEventArgs args = (CoinChangerEventArgs)e;
            switch (args.EventType)
            {
                //A coin was inserted into the Coin Changer
                case CoinChangerEventOp.COIN_INSERTED:
                {
                    m_lastCoinValue = args.Value;
                    m_amountInserted += m_lastCoinValue;
                    DisplayCurrencyAmount(m_amountInserted / 100.0);
                    break;
                }

                //The Customer pressed the refund button
                case CoinChangerEventOp.ISSUE_REFUND:
                {
                    CoinChanger changer = (CoinChanger)sender;
                    changer.DispenseChange(m_amountInserted);
                    m_amountInserted = 0;
                    break;
                }
                //A coin was inserted into the Coin Changer
                case CoinChangerEventOp.MAKE_CHANGE:
                {
                    m_lastCoinValue = 0;
                    m_amountInserted = 0;
                    DisplayCurrencyAmount(0);
                   
                    break;
                }
            }
        }

        /// <summary>
        /// Stock the machine with a particular product
        /// </summary>
        /// <param name="count">The number of products to stock</param>
        /// <param name="product">The product to stock</param>
        /// <returns></returns>
        public bool StockProduct(UInt32 count, String product)
        {
            return m_productManager.StockProduct(count, product);
        }

        /// <summary>
        /// Set the product category price
        /// </summary>
        /// <param name="product">The product group to set price for</param>
        /// <param name="Cost">The cost of the product in cents</param>
        /// <returns>true if the price is succesfully set, otherwise false</returns>
        public bool SetProductPrice(String product, UInt32 Cost)
        {
            return m_productManager.SetProductPrice(product,Cost);
        }

        /// <summary>
        /// Select the specified product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool SelectProduct(String product)
        {
            bool retVal = false;

            ProductData data = null;
            retVal = m_productManager.GetProductData(product, out data);

            if (retVal)
            {
                retVal = false;
                // Make sure a product cost has been set  
                if (data.ProductCost != 0)
                {
                    // Determine if enough money is inserted
                    if (data.ProductCost <= m_amountInserted)
                    {
                        // Verify that the product is in inventory 
                        if (data.ProductCount > 0)
                        {
                            //Determine if there if it is possible to give change based
                            //upon the coins in the coin vault.
                            if (m_coinChanger.DispenseChange(m_amountInserted - data.ProductCost))
                            {
                                //Attempt to dispense the product
                                if(m_productManager.DispenseProduct(product))
                                {
                                    //Product dispensing was succesful, display "Thank You"
                                    DisplayThankYou();
                                    retVal = true;
                                }
                            }
                            else
                            {
                                //The coins stored in the coind vault can not give the proper change
                                DisplayExactChangeOnly();
                            }
                        }
                        else
                        {
                            //Product is not in inventory, display Sold Out
                            DisplaySoldOut();
                        }
                    }
                    else
                    {
                        //Not enough money inserted, show the product price. 
                        DisplayProductPrice(data.ProductCost / 100.0);
                    }

                }
            }
            return retVal;
        }

        /// <summary>
        /// <c>VendingMachineCtrl</c> test
        /// </summary>
        public VendingMachineCtrl()
        {
            m_coinChanger.CoinChangerEvent += CoinChangerEvent;
            m_productManager.ProductManagerEvent += ProductManagerEvent;
            m_lastEventArgs.Status = VendingMachineStatus.DISPLAY_UPDATE;
            m_lastEventArgs.DisplayData = "Insert Coin";
        }

        /// <summary>
        /// The Accept Coin Function / Use Case
        /// </summary>
        /// <param name="coin">A Coin Literal Name
        /// Valid values are : "Nickel, Dime Quarter, Half Dollar, and Dollar
        /// Or a String value representing the coin value in cents (i.e. 5, 10 ,25, 50 or 100)</param>
        /// <returns>true if a valid coin value was accepted</returns>
        public bool AcceptCoin(string coin)
        {
            return m_coinChanger.InsertCoin(coin);
        }
        
        /// <summary>
        /// Issues a Refund
        /// </summary>
        /// <returns></returns>
        public bool IssueRefund()
        {
            return m_coinChanger.IssueRefund();
        }
    }
}
