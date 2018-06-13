using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    /// <summary>
    /// Arguments for a <c>VendingMachineCtrl</c> Ctrl Event
    /// </summary>
    public class VendingMachineCtrlEventArgs : EventArgs
    {
        /// <summary>
        /// The status change of the Vending Machine
        /// </summary>
        public VendingMachineCtrl.VendingMachineStatus Status;

        /// <summary>
        /// A string argument, containing data to place on the Vending 
        /// Machine Display
        /// </summary>
        public String DisplayData; 
    }
}
