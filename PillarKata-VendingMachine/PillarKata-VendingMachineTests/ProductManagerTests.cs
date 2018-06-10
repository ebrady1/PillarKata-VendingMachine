using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PillarKata_VendingMachine;

namespace PillarKata_VendingMachineTests
{
    [TestClass]
    public class ProductManagerTests
    { 
        /// <summary>
        /// Test the ability of the product manager to properly stock and 
        /// maintain inventory of products.
        /// </summary>
        [TestMethod]
        public void StockProducts()
        {
            ProductManager pm = new ProductManager();
            Assert.AreEqual(true, pm.StockProduct(0));
            Assert.AreEqual(true, pm.StockProduct(1));
            Assert.AreEqual(true, pm.StockProduct(1));
            Assert.AreEqual(true, pm.StockProduct(1));
            Assert.AreEqual(true, pm.StockProduct(2));
            Assert.AreEqual(true, pm.StockProduct(2));
            Assert.AreEqual(true, pm.StockProduct(3));

            /// Should only have 1 of Product 0
            Assert.AreEqual<UInt32>(1, pm.ProductInventory(0));
            /// Should only have 3 of Product 1
            Assert.AreEqual<UInt32>(3, pm.ProductInventory(1));
            /// Should only have 2 of Product 2
            Assert.AreEqual<UInt32>(2, pm.ProductInventory(2));
            /// Should only have 1 of Product 3
            Assert.AreEqual<UInt32>(1, pm.ProductInventory(3));
            /// Should only have 0 of Product 4
            Assert.AreEqual<UInt32>(0, pm.ProductInventory(4));

        }
    }
}
