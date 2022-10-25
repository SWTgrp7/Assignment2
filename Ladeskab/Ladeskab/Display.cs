using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class Display : IDisplay
    {
        public string connectphone = "Please Connect your phone";
        public string disconnectphone = "Please Disconnect your phone";
        public string connectionerror = "Connection Error - try connecting your phone again";
        public string chargecomplete = "Charge Complete - disconnect your phone";
        public string charging = "Charging";
        public string chargingcurrent = "Charging - Current: ";
        public string RFIDerror = "RFID Error - try again";
        public string scanRFID = "Please scan your RFID tag";
        public string occupied = "Cabinet Occupied - try again later";

        public void ConnectPhone()
        {
            Console.WriteLine(connectphone);
        }

        public void DisconnectPhone()
        {
            Console.WriteLine(disconnectphone);
        }

        public void ConnectionError()
        {
            Console.WriteLine(connectionerror);
        }

        public void ChargeComplete()
        {
            Console.WriteLine(chargecomplete);
        }

        public void Charging()
        {
            Console.WriteLine(charging);
        }

        public void Charging(double current)
        {
           Console.WriteLine(chargingcurrent + current);
        }

        public void RFIDError()
        {
            Console.WriteLine(RFIDerror);
        }

        public void LoadRFID()
        {
            Console.WriteLine(scanRFID);
        }

        public void Occuppied()
        {
            Console.WriteLine(occupied);
        }
    }
}