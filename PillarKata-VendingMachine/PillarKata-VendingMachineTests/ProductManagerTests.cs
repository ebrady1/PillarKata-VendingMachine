﻿using System;
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
            pm.StockProduct(0);
            pm.StockProduct(1);
            pm.StockProduct(1);
            pm.StockProduct(1);
            pm.StockProduct(2);
            pm.StockProduct(2);
            pm.StockProduct(3);

            /// Should only have 1 of Product 0
            Assert.AreEqual(1, pm.ProductInventory(0));
            /// Should only have 3 of Product 1
            Assert.AreEqual(3, pm.ProductInventory(1));
            /// Should only have 2 of Product 2
            Assert.AreEqual(2, pm.ProductInventory(2));
            /// Should only have 1 of Product 3
            Assert.AreEqual(1, pm.ProductInventory(3));

        }
    }
}