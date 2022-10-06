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
        public void ConnectPhone()
        {
            Console.WriteLine("Please Connect your phone");
        }

        public void DisconnectPhone()
        {
            Console.WriteLine("Please Disconnect your phone NOW!!!");
        }

        public void ConnectionError()
        {
            Console.WriteLine("Connection Error - connect your phone");
        }

        public void ChargeComplete()
        {
            Console.WriteLine("Charge Complete - extract your phone");
        }

        public void Charging()
        {
            Console.WriteLine("Charging");
        }
        public void Charging(double current)
        {
           Console.WriteLine("Charging - Current: " + current);
            
        }
        public void RFIDError()
        {
            Console.WriteLine("RFID Error - try again");
        }

        public void LoadRFID()
        {
            Console.WriteLine("add your RFID tag");
        }

        public void Occuppied()
        {
            Console.WriteLine("Cappinet Occupied - try again later");
        }
    }
}
