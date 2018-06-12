using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    public class VendingMachineCtrl
    {
        public enum VendingMachineStatus
        {
           DISPLAY_UPDATE 
        }

        Int32 m_amountInserted = 0;
        Int32 m_lastCoinValue = 0;

        //The Vending Machine will have one Coin Changer
        CoinChanger m_coinChanger = new CoinChanger();

        //The Vending Machine will have one Product Manager
        ProductManager m_productManager = new ProductManager();

        //Initialize the lastEvent
        VendingMachineCtrlEventArgs m_lastEventArgs = new VendingMachineCtrlEventArgs();

        private event EventHandler _VendingMachineStatusNotify;
        
        //The Operational Status of the Vending Machine, will callback immediately on subscribe
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
        /// Display the amount inserted thus far
        /// </summary>
        /// <param name="currency"></param>
        void DisplayCurrencyAmount(double currency)
        {
            m_lastEventArgs.Status = VendingMachineStatus.DISPLAY_UPDATE;
            m_lastEventArgs.DisplayData = String.Format("{0:C2}", currency);
            _VendingMachineStatusNotify(this, m_lastEventArgs);

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
                    DisplayInsertCoin();
                    break;
                }
            }
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

        public bool IssueRefund()
        {
            return false;
        }
    }
}
