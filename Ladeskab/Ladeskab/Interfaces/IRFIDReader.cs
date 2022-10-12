using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Interfaces
{
    public class RFIDReaderEventArgs : EventArgs
    {
        public int RFID { get; set; }
    }

    public interface IRFIDReader
    {
        public event EventHandler<RFIDReaderEventArgs> RFIDEvent;
        public void OnRfidRead(int rfid);
    }
}