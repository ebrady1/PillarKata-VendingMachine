using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    public class ProductData
    {
        private ProductData() { }
        public ProductData(String productName, UInt32 cost, UInt32 count)
        {
            ProductName = productName;
            ProductCost = cost;
            ProductCount = count;
        }

        public String ProductName { get; }
        public UInt32 ProductCost { get; }
        public UInt32 ProductCount { get; }
    }
}
