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

        Coin m_Nickel;
        Coin m_Dime;
        Coin m_Quarter;
        Coin m_HalfDollar;
        Coin m_Dollar;

        //Declare a callback for others to know when a coin has been inserted
        public delegate void CoinEvent(UInt16 value);

        //Coin Changer Event 
        public event EventHandler CoinChangerEvent;

        /// <summary>
        /// Class Constructor
        /// </summary>
        public CoinChanger()
        {
            m_Nickel = COIN_VALUES["NICKEL"];
            m_Dime = COIN_VALUES["DIME"];
            m_Quarter = COIN_VALUES["QUARTER"];
            m_HalfDollar = COIN_VALUES["HALF DOLLAR"];
            m_Dollar = COIN_VALUES["DOLLAR"];

            COIN_VALUES.Add("5",    m_Nickel); 
            COIN_VALUES.Add("10",   m_Dime);
            COIN_VALUES.Add("25",   m_Quarter);
            COIN_VALUES.Add("50",   m_HalfDollar);
            COIN_VALUES.Add("100",  m_Dollar); 

            m_coinVault = new Dictionary<Coin, UInt16>
            {
                { m_Nickel,     0},
                { m_Dime,       0},
                { m_Quarter,    0},
                { m_HalfDollar, 0},
                { m_Dollar,     0}
            };
        }
        /// <summary>
        /// Returns the amount of money stored in the coin vault
        /// </summary>
        /// <returns>
        /// The amount of money, in cents, stored in the vault 
        /// </returns>
        int MoneyInVault()
        {
            return ((m_coinVault[m_Dollar] * 100) + 
                (m_coinVault[m_HalfDollar] * 50) +
                (m_coinVault[m_Quarter] * 25) +
                (m_coinVault[m_Dime] * 10) + 
                (m_coinVault[m_Nickel] * 5));
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
        public bool IssueRefund()
        {
            bool retVal = false;
            int moneyInVault = this.MoneyInVault();

            if (0 != moneyInVault)
            {
                CoinChangerEventArgs e = new CoinChangerEventArgs();
                e.EventType = CoinChangerEventOp.ISSUE_REFUND;
                e.CoinVault = m_coinVault;

                //Throw an event here
                CoinChangerEvent(this, e);

                retVal = (moneyInVault > this.MoneyInVault());
            }

            return retVal;
        }

        /// <summary>
        /// Dispense change after a valid transaction or refund. 
        /// </summary>
        /// <returns>
        /// true if correct change was ispensed.  
        /// <seealso cref="CanMakeChange(uint)"/>
        /// </returns>
        /// <param name="amount">The amount of change to dispense
        public bool DispenseChange(int amount)
        {
            bool inLoop = false;
            bool retVal = false;
            int tempAmount = amount;
            UInt16 nickelCnt = m_coinVault[m_Nickel];
            UInt16 dimeCnt = m_coinVault[m_Dime];
            UInt16 quarterCnt = m_coinVault[m_Quarter];
            UInt16 halfDollarCnt = m_coinVault[m_HalfDollar];
            UInt16 dollarCnt = m_coinVault[m_Dollar];

            //Look at the amount requested and determine if possible to 
            //come up with a solution using a greedy algorithm
            while(tempAmount > 0)
            {
                inLoop = true;
                if ((dollarCnt > 0) && ((tempAmount / 100) >= 1))
                {
                    tempAmount -= 100;
                    dollarCnt--;
                    continue;
                }
                else if ((halfDollarCnt > 0) && ((tempAmount / 50) >= 1))
                {
                    tempAmount -= 50;
                    halfDollarCnt--;
                    continue;
                }
                else if ((quarterCnt > 0) && ((tempAmount / 25) >= 1))
                {
                    tempAmount -= 25;
                    quarterCnt--;
                    continue;
                }
                else if ((dimeCnt > 0) && ((tempAmount / 10) >= 1))
                {
                    tempAmount -= 10;
                    dimeCnt--;
                    continue;
                }
                else if ((nickelCnt > 0) && ((tempAmount / 5) >= 1))
                {
                    tempAmount -= 5;
                    nickelCnt--;
                    continue;
                }
                else
                {
                    break;
                }
            }

            if ((inLoop) && (0 == tempAmount))
            {
                m_coinVault[m_Nickel] = nickelCnt;
                m_coinVault[m_Dime] = dimeCnt;
                m_coinVault[m_Quarter] = quarterCnt;
                m_coinVault[m_HalfDollar] = halfDollarCnt;
                m_coinVault[m_Dollar] = dollarCnt;
                retVal = true;
            }

            return retVal;
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
