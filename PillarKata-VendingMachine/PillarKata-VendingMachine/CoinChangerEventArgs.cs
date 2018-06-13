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
        /// <summary>
        /// The Event Type
        /// </summary>
        public CoinChangerEventOp EventType { get; set; }

        /// <summary>
        /// The Value of the coin Inserted or amount associated with a "Make Change" event
        /// </summary>
        public UInt32 Value { get; set; }

        /// <summary>
        /// The catalog of coins in the Coin Vault
        /// </summary>
        public Dictionary<Coin, UInt32> CoinVault { get; set; }
    }
}
