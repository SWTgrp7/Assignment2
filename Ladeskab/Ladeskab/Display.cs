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
        public string connectphone = "Please Connect your phone\r\n";
        public string disconnectphone = "Please Disconnect your phone\r\n";
        public string connectionerror = "Connection Error - try connecting your phone again\r\n";
        public string chargecomplete = "Charge Complete - disconnect your phone\r\n";
        public string charging = "Charging\r\n";
        public string chargingcurrent = "Charging - Current: ";
        public string RFIDerror = "RFID Error - try again\r\n";
        public string scanRFID = "Please scan your RFID tag\r\n";
        public string occupied = "Cabinet Occupied - try again later\r\n";
        public string doorlocked = "The door is now locked and your phone is charging. Use your RFID to unlock the door\r\n";
        public string removephone = "Please remove your phone and close the door\r\n";
        public string wrongRFID = "Wrong RFID tag\r\n";

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

        public void DoorLocked()
        {
            Console.WriteLine(doorlocked);
        }

        public void Occuppied()
        {
            Console.WriteLine(occupied);
        }

        public void RemovePhone()
        {
            Console.WriteLine(removephone);
        }

        public void WrongRFID()
        {
            Console.WriteLine(wrongRFID);
        }
    }
}