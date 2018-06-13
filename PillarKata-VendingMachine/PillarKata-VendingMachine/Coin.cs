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
        public Coin()
        {
            Value = 0;
            CoinName = "";
        }

        public Coin(String name, UInt16 value)
        {
            Value = value;
            CoinName = name;
        }

        public UInt32 Value { get; set; }
        public String CoinName { get; set; }
    };

}
