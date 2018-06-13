using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    /// <summary>
    /// EventArgument for Coin Inserted Event 
    /// </summary>
    /// 
    public enum CoinChangerEventOp
    {
        /// <summary>A Coin was inserted into the Coin Changer</summary>
        COIN_INSERTED,    

        /// <summary>A Refund was requested by the Customer</summary>
        ISSUE_REFUND,
        
        /// <summary>A coin was returned to the customer</summary>
        MAKE_CHANGE
    };

    /// <summary>
    /// <c>CoinChangerEventArgs</c> class.
    /// EventArgument for Coin Inserted Event 
    /// </summary>
    /// 
    public class CoinChangerEventArgs : EventArgs
    {
        public CoinChangerEventOp EventType { get; set; }
        public UInt32 Value { get; set; }
        public Dictionary<Coin, UInt32> CoinVault { get; set; }
    }
}
