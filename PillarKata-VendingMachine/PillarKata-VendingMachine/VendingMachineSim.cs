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
    public partial class VendingMachineSim : Form
    {

        VendingMachineCtrl m_vmCtrl = new VendingMachineCtrl();

        /// <summary>
        /// The Main Constructor
        /// </summary>
        public VendingMachineSim()
        {
            InitializeComponent();
            m_vmCtrl.StockProduct("Cola");
            m_vmCtrl.StockProduct("Cola");
            m_vmCtrl.StockProduct("Cola");

            m_vmCtrl.StockProduct("Candy");
            m_vmCtrl.StockProduct("Candy");
            m_vmCtrl.StockProduct("Candy");

            m_vmCtrl.StockProduct("Chips");
            m_vmCtrl.StockProduct("Chips");
            m_vmCtrl.StockProduct("Chips");

            m_vmCtrl.SetProductPrice("Cola", 100);
            m_vmCtrl.SetProductPrice("Candy", 65);
            m_vmCtrl.SetProductPrice("Chips", 50);

            m_vmCtrl.VendingMachineStatusNotify += vmCtrl_Notify;
        }

        private void vmCtrl_Notify(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(VendingMachineCtrl))
            {
                VendingMachineCtrl vmCtrl = (VendingMachineCtrl)sender;
                VendingMachineCtrlEventArgs vmCtrlEventArgs = (VendingMachineCtrlEventArgs)e;

                switch (vmCtrlEventArgs.Status)
                {
                    case VendingMachineCtrl.VendingMachineStatus.DISPLAY_UPDATE:
                    {
                        insertCoinTextBox.Text = vmCtrlEventArgs.DisplayData;
                        Invalidate();
                        break;
                    }
                }
            }
        }

        private void VendingMachineSim_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nickelButton_Click(object sender, EventArgs e)
        {
            m_vmCtrl.AcceptCoin("Nickel");
        }

        private void dimeButton_Click(object sender, EventArgs e)
        {
            m_vmCtrl.AcceptCoin("Dime");
        }

        private void quarterButton_Click(object sender, EventArgs e)
        {
            m_vmCtrl.AcceptCoin("Quarter");
        }

        private void halfDollar_Click(object sender, EventArgs e)
        {
            m_vmCtrl.AcceptCoin("Half Dollar");
        }

        private void dollarButton_Click(object sender, EventArgs e)
        {
            m_vmCtrl.AcceptCoin("Dollar");
        }

        private void refundButton_Click(object sender, EventArgs e)
        {
            m_vmCtrl.IssueRefund();
        }

        private void colaButton_Click(object sender, EventArgs e)
        {
            m_vmCtrl.SelectProduct("Cola");
        }

        private void candyButton_Click(object sender, EventArgs e)
        {
            m_vmCtrl.SelectProduct("Candy");
        }

        private void chipsButton_Click(object sender, EventArgs e)
        {
            m_vmCtrl.SelectProduct("Chips");
        }

    }
}
