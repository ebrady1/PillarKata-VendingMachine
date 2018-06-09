using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{

    /// <summary>
    /// The main <c>CoinChanger</c> class.
    /// Contains all methods for performing coin related functions.
    /// </summary>
    public class CoinChanger
    {
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

        //Coin Changer Event 
        public event EventHandler CoinChangerEvent;

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

            m_coinVault = new Dictionary<Coin, UInt16>
            {
                { COIN_VALUES["NICKEL"],       0},
                { COIN_VALUES["DIME"],         0},
                { COIN_VALUES["QUARTER"],      0},
                { COIN_VALUES["HALF DOLLAR"],  0},
                { COIN_VALUES["DOLLAR"],       0}
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
                        //Increment the coin vault
                        m_coinVault[value]++;
                       
                        CoinChangerEventArgs e = new CoinChangerEventArgs();
                        e.EventType = CoinChangerEventOp.COIN_INSERTED;
                        e.Value = value.Value;

                        //Throw an event here
                        CoinChangerEvent(this, e);

                        //Report success
                        retVal = true;
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Triggers the CoinChanger to issue a refund
        /// </summary>
        /// <returns>
        /// true if the operation can succeed , otherwise false/
        /// </returns>
        public bool IssueRefund()
        {
            bool retVal = false;

            CoinChangerEventArgs e = new CoinChangerEventArgs();
            e.EventType = CoinChangerEventOp.ISSUE_REFUND;

            //Throw an event here
            CoinChangerEvent(this, e);

            retVal = ((m_coinVault[(COIN_VALUES["NICKEL"])] != 0) &&
                (m_coinVault[(COIN_VALUES["DIME"])] != 0) &&
                (m_coinVault[(COIN_VALUES["QUARTER"])] != 0) &&
                (m_coinVault[(COIN_VALUES["HALF DOLLAR"])] != 0) &&
                (m_coinVault[(COIN_VALUES["DOLLAR"])] != 0));

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
