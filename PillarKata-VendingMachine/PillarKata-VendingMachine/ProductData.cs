using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    /// <summary>
    /// Product Information object
    /// </summary>
    public class ProductData
    {
        private ProductData() { }

        /// <summary>
        /// Initialize a ProductData object with required information
        /// </summary>
        /// <param name="productName">The name of the product</param>
        /// <param name="cost">The cost of the product</param>
        /// <param name="count">The number of products in inventory</param>
        public ProductData(String productName, UInt32 cost, UInt32 count)
        {
            ProductName = productName;
            ProductCost = cost;
            ProductCount = count;
        }

        /// <summary>
        /// Returns the Human Readable name of the product
        /// </summary>
        public String ProductName { get; }

        /// <summary>
        /// Returns the Cost of the Product in cents
        /// </summary>
        public UInt32 ProductCost { get; }

        /// <summary>
        /// Returns the number of products currently in inventory
        /// </summary>
        public UInt32 ProductCount { get; }
    }
}
