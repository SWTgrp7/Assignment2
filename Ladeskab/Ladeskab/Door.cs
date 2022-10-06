using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    internal class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorEvent;

        public void LockDoor()
        {
            DoorEvent?.Invoke(this, new DoorEventArgs() { DoorOpen = false });
        }

        public void UnlockDoor()
        {
            DoorEvent?.Invoke(this, new DoorEventArgs() { DoorOpen = true });
        }
    }
}
