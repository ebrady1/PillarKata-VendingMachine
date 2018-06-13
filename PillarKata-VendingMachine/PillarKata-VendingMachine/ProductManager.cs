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
        Dictionary<String, UInt32> m_productInventory = new Dictionary<String, UInt32>();
        Dictionary<String, UInt32> m_productPriceList = new Dictionary<String, UInt32>();

        /// <summary>
        /// Subscription object for <c>ProductManagerEvents</c>
        /// </summary>
        public event EventHandler ProductManagerEvent;

        /// <summary>
        /// Stocks a particular product into inventory.  Used by the vendor
        /// </summary>
        /// <param name="product">The product ID String</param>
        /// <returns>true if the product was stocked, otherwise false</returns>
        public  bool StockProduct(String product)
        {
            UInt32 value = 0;
            
            if (!m_productInventory.TryGetValue(product, out value))
            {
                m_productInventory[product] = 0;
            }

            m_productInventory[product]++;
            return true;
        }
        
        /// <summary>
        /// Set the product category price
        /// </summary>
        /// <param name="product">The product group to set price for</param>
        /// <param name="cost">The cost of the product in cents</param>
        /// <returns>true if the price is succesfully set, otherwise false</returns>
        public bool SetProductPrice(String product, UInt32 cost)
        {
            bool retVal = false;
            if (cost > 0)
            {
                m_productPriceList[product] = cost;
                retVal = true;
            }
            return retVal;
        }

        /// <summary>
        /// Returns Data about the specific product along with inventory counts and cost
        /// </summary>
        /// <param name="product">A Human readable name for the product</param>
        /// <param name="data">A <c>ProductData</c> object containing infomation about the product</param>
        /// <returns></returns>
        public bool GetProductData(String product, out ProductData data)
        {
            UInt32 cost = 0;
            UInt32 inventory = 0;

            bool retVal = false;
            data = null;

            m_productInventory.TryGetValue(product, out inventory);
            m_productPriceList.TryGetValue(product, out cost);

            if ((inventory != 0) || (cost != 0))
            {
                data = new ProductData(product, cost, inventory);
                retVal = true;
            }

            return retVal;
        }

        /// <summary>
        /// Returns the Inventory Count for indicated product
        /// </summary>
        /// <param name="ID">The Product ID String</param>
        /// <returns>The amount of the indicated product</returns>
        public UInt32 ProductInventory(String ID)
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
        public  bool DispenseProduct(String ID)
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
