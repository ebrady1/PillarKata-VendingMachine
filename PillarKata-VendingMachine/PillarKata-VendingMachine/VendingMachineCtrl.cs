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
           AMOUNT_INSERTED 
        }

        Int32 m_amountInserted = 0;
        Int32 m_lastCoinValue = 0;

        //The Vending Machine will have one Coin Changer
        CoinChanger m_coinChanger = new CoinChanger();
        ProductManager m_productManager = new ProductManager();

        //The Operational Status of the Vending Machine
        public event EventHandler VendingMachineStatusNotify;
        
        /// <summary>
        /// Product Manager Event Callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProductManagerEvent(object sender, EventArgs e)
        {

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
                case CoinChangerEventOp.COIN_INSERTED:
                    {
                        m_lastCoinValue = args.Value;
                        m_amountInserted += m_lastCoinValue;
                        break;
                    }

                case CoinChangerEventOp.ISSUE_REFUND:
                    {
                        CoinChanger changer = (CoinChanger)sender;
                        changer.DispenseChange(m_amountInserted);
                        m_amountInserted = 0;
                        break;
                    }
            }
        }

        public VendingMachineCtrl()
        {
            m_coinChanger.CoinChangerEvent += CoinChangerEvent;
            m_productManager.ProductManagerEvent += ProductManagerEvent;
        }

        public bool AcceptCoin(string coin)
        {
            return false;
        }

    }
}
