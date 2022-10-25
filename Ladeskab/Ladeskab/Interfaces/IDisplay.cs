using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Interfaces
{
    public interface IDisplay
    {
        public void ConnectPhone();
        public void DisconnectPhone();
        public void ConnectionError();
        public void ChargeComplete();
        public void Charging();
        public void Charging(double current);
        public void RFIDError();
        public void LoadRFID();
        public void Occuppied();
        public void DoorLocked();
        public void RemovePhone();

        public void WrongRFID();



    }
}
