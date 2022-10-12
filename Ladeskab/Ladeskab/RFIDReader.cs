using Ladeskab.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class RFIDReader : IRFIDReader
    {
        public event EventHandler<RFIDReaderEventArgs> RFIDEvent;

        public void OnRfidRead(int rfid)
        {
            RFIDEvent?.Invoke(this, new RFIDReaderEventArgs() { RFID = rfid });
        }
    }
}
