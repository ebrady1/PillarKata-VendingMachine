using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    /// <summary>
    /// The <c>Coin</c> class.
    /// Contains all methods to describe a coin
    /// </summary>
    /// 
    public class Coin
    {
        /// <summary>
        /// Constructor for <c>Coin</c>, initializes the object with 0 value and no Name
        /// </summary>
        public Coin()
        {
            Value = 0;
            CoinName = "";
        }

        /// <summary>
        /// Initialization constructor for <c>Coin</c>, initializes the object with
        /// the value and human readable name of the Coin
        /// </summary>
        /// <param name="name">The accepted name of the coin</param>
        /// <param name="value">The value of the coin in cents</param>
        public Coin(String name, UInt16 value)
        {
            Value = value;
            CoinName = name;
        }
        /// <summary>
        /// Return the Value of the coin as set by the <c>CoinChanger</c>
        /// </summary>
        public UInt32 Value { get; set; }

        /// <summary>
        /// Set the name of the coin as set by the <c>CoinChanger</c>
        /// </summary>
        public String CoinName { get; set; }
    };

}
