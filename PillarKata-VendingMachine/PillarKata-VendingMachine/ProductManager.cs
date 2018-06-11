using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{    
    /// <summary>
    /// The main <c>ProductManager</c> class.
    /// Contains all operations to manage the Vending Machine's product inventory 
    /// </summary>
    public class ProductManager
    {
        Dictionary<UInt32, UInt32> m_productInventory = new Dictionary<UInt32, UInt32>();

        //Coin Changer Event 
        public event EventHandler ProductManagerEvent;

        /// <summary>
        /// Stocks a particular product into inventory.  Used by the vendor
        /// </summary>
        /// <param name="ID">A Unique ProductID</param>
        /// <returns>true if the product was stocked, otherwise false</returns>
        public  bool StockProduct(UInt32 ID)
        {
            UInt32 value = 0;
            
            if (!m_productInventory.TryGetValue(ID, out value))
            {
                m_productInventory[ID] = 0;
            }

            m_productInventory[ID]++;
            return true;
        }

        /// <summary>
        /// Returns the Inventory Count for indicated product
        /// </summary>
        /// <param name="ID">The Product ID code</param>
        /// <returns>The amount of the indicated product</returns>
        public UInt32 ProductInventory(UInt32 ID)
        {
            UInt32 product = 0;
            if (m_productInventory.TryGetValue(ID, out product))
            {
                product = m_productInventory[ID];
            }
            return product; 
        }
        
        /// <summary>
        /// Dispenses a particular product from inventory. 
        /// </summary>
        /// <param name="ID">A Unique ProductID</param>
        /// <returns>true if the product was dispensed, otherwise false</returns>
        public  bool DispenseProduct(UInt32 ID)
        {
            UInt32 value;
            bool retVal = false;

            if (m_productInventory.TryGetValue(ID, out value))
            {
                if (m_productInventory[ID] != 0)
                {
                    m_productInventory[ID]--;
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}
