using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorEvent;
        private bool islocked { get; set; }
        
        public void OnDoorOpen(){
            DoorEvent?.Invoke(this, new DoorEventArgs() { DoorOpen = true });
        }

        public void OnDoorClose()
        {
            DoorEvent?.Invoke(this, new DoorEventArgs() { DoorOpen = false });
        }
        public void LockDoor()
        {
            islocked = true;
        }

        public void UnlockDoor()
        {
            islocked = false;
        }
    }
}
