using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PillarKata_VendingMachine
{
    /// <summary>
    /// The Vending Machine Simulator main form
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The Main Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            VendingMachineCtrl vm = new VendingMachineCtrl();
        }
    }
}
