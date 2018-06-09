using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public UInt16 Value { get; set; } 
        public String CoinName { get; set; }
    };

    /// <summary>
    /// <c>CoinChangerEventArgs</c> class.
    /// EventArgument for Coin Inserted Event 
    /// </summary>
    /// 
    public class CoinChangerEventArgs : EventArgs
    {
        public UInt16 Value { get; set; }
    };


    /// <summary>
    /// The main <c>CoinChanger</c> class.
    /// Contains all methods for performing coin related functions.
    /// </summary>
    public class CoinChanger
    {
        //The current amount of change in the coin changer.
        UInt16 m_Nickels        = 0;
        UInt16 m_Dimes          = 0;
        UInt16 m_Quarters       = 0;
        UInt16 m_HalfDollars    = 0;
        UInt16 m_Dollars        = 0;

        //Known Coin types
        Dictionary<string, Coin> COIN_VALUES = 
            new Dictionary<string, Coin> 
        {
            { "NICKEL",         new Coin("NICKEL",      5) },
            { "DIME",           new Coin("Dime",        10) },
            { "QUARTER",        new Coin("Quarter",     25) },
            { "HALF DOLLAR",    new Coin("Half Dollar", 50) },
            { "DOLLAR",         new Coin("Dollar",      100) }
        };

        //The number of coins currently in the coin changer
        static Dictionary<Coin, UInt16> m_coinVault;

        //Declare a callback for others to know when a coin has been inserted
        public delegate void CoinEvent(UInt16 value);

        public event EventHandler CoinInserted;

        /// <summary>
        /// Class Constructor
        /// </summary>
        public CoinChanger()
        {
            COIN_VALUES.Add("5", COIN_VALUES["NICKEL"]);
            COIN_VALUES.Add("10", COIN_VALUES["DIME"]);
            COIN_VALUES.Add("25", COIN_VALUES["QUARTER"]);
            COIN_VALUES.Add("50", COIN_VALUES["HALF DOLLAR"]);
            COIN_VALUES.Add("100", COIN_VALUES["DOLLAR"]);

            //The number of coins contained in the Coin Changer Vault
            m_coinVault = 
                new Dictionary<Coin, UInt16>
            {
                { COIN_VALUES["NICKEL"],       m_Nickels },
                { COIN_VALUES["DIME"],         m_Dimes },
                { COIN_VALUES["QUARTER"],      m_Quarters},
                { COIN_VALUES["HALF DOLLAR"],   m_HalfDollars},
                { COIN_VALUES["DOLLAR"],       m_Dollars}
            };
        }
        
        /// <summary>
        /// Inserts a coin into the coin changer and test if it is a valid denomination. 
        /// </summary>
        /// <returns>
        /// true if the coin is valid, otherwise false/
        /// </returns>
        /// <param name="coin">A string representing a valid coin denomination"</param>
        public bool InsertCoin(String coin)
        {
            bool retVal = false;
            UInt16 vaultItemCount; 
            Coin value;

            if ((!String.IsNullOrWhiteSpace(coin)) && 
                (!String.IsNullOrEmpty(coin)))
            {
                //Remove any Whitespace
                Regex.Replace(coin, @"\s+", "");

                //Go UpperCase
                coin = coin.ToUpper();

                // We expect the parameter to be in the form of a string
                if (COIN_VALUES.TryGetValue(coin, out value))
                {
                    if (m_coinVault.TryGetValue(value, out vaultItemCount))
                    {
                        //Increment the value in the vault
                        vaultItemCount++;

                        CoinChangerEventArgs e = new CoinChangerEventArgs();
                        e.Value = value.Value;

                        //Throw an event here
                        CoinInserted(this, e);

                        //Report success
                        retVal = true;
                    }
                }
            }
            return retVal;
        }
        

        /// <summary>
        /// Dispense change after a valid transaction or refund. 
        /// </summary>
        /// <returns>
        /// true if correct change was ispensed.  
        /// <seealso cref="CanMakeChange(int)"/>
        /// </returns>
        /// <param name="amount">The amount of change to dispense
        public bool DispenseChange(int amount)
        {
            return false;
        }

        /// <summary>
        /// Ask if the CoinChanger contains enough change to refund the indicated amount 
        /// </summary>
        /// <returns>
        /// true if correct change can be Dispensed. "/>
        /// </returns>
        /// <param name="amount">The amount of change to dispense
        public bool CanMakeChange(int amounts)
        {
            return false;
        }
    }
}
