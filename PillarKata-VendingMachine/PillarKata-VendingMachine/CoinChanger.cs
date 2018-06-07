using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    /*
        The CoinChanger class 
        This class implements coin related functions
    */
    /// <summary>
    /// The main <c>CoinChanger</c> class.
    /// Contains all methods for performing coin related functions.
    /// </summary>
    public class CoinChanger
    {
        /// <summary>
        /// Inserts a coin into the coin changer and test if it is a valid denomination. 
        /// </summary>
        /// <returns>
        /// true if the coin is valid, otherwise false/
        /// </returns>
        /// <param name="coin">A string representing a valid coin denomination"</param>
        public bool InsertCoin(String coin)
        {
            return false;
        }
        
        /// <summary>
        /// Inserts a coin into the coin changer and test if it is a valid denomination. 
        /// </summary>
        /// <returns>
        /// true if the coin is valid, otherwise false/
        /// </returns>
        /// <param name="coin">A integer representing a valid coin denomination"</param>
        public bool InsertCoin(int coin)
        {
            return false;
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
