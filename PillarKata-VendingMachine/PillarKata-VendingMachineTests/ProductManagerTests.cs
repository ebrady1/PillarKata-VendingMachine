using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PillarKata_VendingMachine;

namespace PillarKata_VendingMachineTests
{
    /// <summary>
    /// ProductManager related tests
    /// </summary>
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
            Assert.AreEqual(true, pm.StockProduct("Cola"));
            Assert.AreEqual(true, pm.StockProduct("Chips"));
            Assert.AreEqual(true, pm.StockProduct("Chips"));
            Assert.AreEqual(true, pm.StockProduct("Chips"));
            Assert.AreEqual(true, pm.StockProduct("Candy"));
            Assert.AreEqual(true, pm.StockProduct("Candy"));

            // Should only have 1 Cola 
            Assert.AreEqual<UInt32>(1, pm.ProductInventory("Cola"));
            // Should only have 3 Chips
            Assert.AreEqual<UInt32>(3, pm.ProductInventory("Chips"));
            // Should only have 2 Candy 
            Assert.AreEqual<UInt32>(2, pm.ProductInventory("Candy"));
            // Should have no Beer 
            Assert.AreEqual<UInt32>(0, pm.ProductInventory("Beer"));

        }

        /// <summary>
        /// Test the ability of the product manager to properly stock and 
        /// maintain inventory of products.
        /// </summary>
        [TestMethod]
        public void DispenseProducts()
        {
            // Add a fake inventory
            ProductManager pm = new ProductManager();
            Assert.AreEqual(true, pm.StockProduct("Cola"));
            Assert.AreEqual(true, pm.StockProduct("Chips"));
            Assert.AreEqual(true, pm.StockProduct("Chips"));
            Assert.AreEqual(true, pm.StockProduct("Chips"));
            Assert.AreEqual(true, pm.StockProduct("Candy"));
            Assert.AreEqual(true, pm.StockProduct("Candy"));

            // Try to dispense the products
            Assert.AreEqual(true, pm.DispenseProduct("Chips"));
            Assert.AreEqual(true, pm.DispenseProduct("Chips"));
            Assert.AreEqual(false, pm.DispenseProduct("Beer"));
            Assert.AreEqual(true, pm.DispenseProduct("Cola"));
            Assert.AreEqual(false, pm.DispenseProduct("Cola"));

        }
    }
}
