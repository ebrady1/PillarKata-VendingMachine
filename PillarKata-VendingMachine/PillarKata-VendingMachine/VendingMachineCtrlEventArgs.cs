using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata_VendingMachine
{
    public class VendingMachineCtrlEventArgs : EventArgs
    {
        public VendingMachineCtrl.VendingMachineStatus Status;
    }
}
