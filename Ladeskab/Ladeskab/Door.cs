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
        public bool IsLocked { get; private set; }
        public bool IsDoorOpen { get; private set; }
        
        public void OnDoorOpen()
        {
            IsDoorOpen = true;
            DoorEvent?.Invoke(this, new DoorEventArgs() { DoorOpen = true });
        }

        public void OnDoorClose()
        {
            IsDoorOpen = false;
            DoorEvent?.Invoke(this, new DoorEventArgs() { DoorOpen = false });
        }

        public void LockDoor()
        {
            IsLocked = true;
        }

        public void UnlockDoor()
        {
            IsLocked = false;
        }
    }
}