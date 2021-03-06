﻿using System;
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
        List<PictureBox> m_colaPics = null;
        List<PictureBox> m_chipPics = null;
        List<PictureBox> m_candyPics = null;


        /// <summary>
        /// The Main Constructor
        /// </summary>
        public VendingMachineSim()
        {
            InitializeComponent();
            m_vmCtrl.StockProduct(3, "Cola");
            m_vmCtrl.StockProduct(3, "Candy");
            m_vmCtrl.StockProduct(3, "Chips");

            m_vmCtrl.SetProductPrice("Cola", 100);
            m_vmCtrl.SetProductPrice("Candy", 65);
            m_vmCtrl.SetProductPrice("Chips", 50);

            m_colaPics = new List<PictureBox> { colaPic1, colaPic2, colaPic3 };
            m_chipPics = new List<PictureBox> { chipsPic1, chipsPic2, chipsPic3 };
            m_candyPics = new List<PictureBox> { candyPic1, candyPic2, candyPic3 };

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
                        if (insertCoinTextBox.InvokeRequired)
                        {
                            insertCoinTextBox.Invoke(new MethodInvoker(delegate 
                            {
                                insertCoinTextBox.Text = vmCtrlEventArgs.DisplayData;
                            }));
                        }
                        else
                        {
                            insertCoinTextBox.Text = vmCtrlEventArgs.DisplayData;
                        }
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
            if (m_vmCtrl.SelectProduct("Cola"))
            {
                UpdateVisualProductCount("Cola", m_colaPics);
            }
        }

        private void candyButton_Click(object sender, EventArgs e)
        {
            if (m_vmCtrl.SelectProduct("Candy"))
            {
                UpdateVisualProductCount("Candy", m_candyPics);
            }
        }

        private void chipsButton_Click(object sender, EventArgs e)
        {
            if ( m_vmCtrl.SelectProduct("Chips"))
            {
                UpdateVisualProductCount("Chips", m_chipPics);
            }
        }

        private void UpdateVisualProductCount(string type, List<PictureBox> picList)
        {
            foreach(PictureBox p in picList)
            {
                if (p.Visible)
                {
                    p.Visible = false;
                    break;
                }
            }
        }

    }
}
