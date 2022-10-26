using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private IDisplay _display;
        private int _oldId;
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private ILogger _logger;


        public int OldId { get { return _oldId; } set { } }

        private LadeskabState State { get => _state; set => _state = value; }
        public IChargeControl Charger { get => _charger; private set => _charger = value; }
        public IDisplay Display { get => _display; private set => _display = value; }
        public int OldId1 { get => _oldId; private set => _oldId = value; }
        public IDoor Door { get => _door; private set => _door = value; }
        public IRFIDReader RfidReader { get => _rfidReader; private set => _rfidReader = value; }
        public ILogger Logger { get => _logger; private set => _logger = value; }



        // private string logFile = "logfile.txt"; // Navnet på systemets log-fil -> Moved to Logger class

        public StationControl()
        {
            _charger = new ChargerControl();
            _state = LadeskabState.Available;
            _display = new Display();
            _door = new Door();
            _rfidReader = new RFIDReader();
            _logger = new Logger();
            
            _door.DoorEvent += HandleDoorEvent;
            _rfidReader.RFIDEvent += HandleRFIDEvent;
        }

        public StationControl(IDoor door, IRFIDReader RFIDReader)
        {
            _charger = new ChargerControl();
            _state = LadeskabState.Available;
            _display = new Display();
            _door = door;
            _rfidReader = RFIDReader;
            _logger = new Logger();

            _door.DoorEvent += HandleDoorEvent;
            _rfidReader.RFIDEvent += HandleRFIDEvent;
        }


        public StationControl(IChargeControl charger, IDisplay display, IDoor door, IRFIDReader RFIDReader,ILogger logger)
        { 
            _charger = charger;
            _state = LadeskabState.Available;
            _display = display;
            _door = door;
            _rfidReader = RFIDReader;
            _logger = logger;
            
            _door.DoorEvent += HandleDoorEvent;
            _rfidReader.RFIDEvent += HandleRFIDEvent;
            
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        _logger.LogDoorLocked(_oldId);
                        _display.DoorLocked();
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.ConnectionError();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logger.LogDoorUnlocked(_oldId);
                        _display.RemovePhone();

                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.WrongRFID();
                    }

                    break;
            }
        }

        private void DoorDetected(bool doorOpen)
        {
            if (doorOpen == true)
            {
                _state = LadeskabState.DoorOpen;
                _display.ConnectPhone();
            }



            if (doorOpen == false)
            {
                _state = LadeskabState.Available;
                _display.LoadRFID();
            }
        }
        private void HandleDoorEvent(object sender, DoorEventArgs e)
        {
            DoorDetected(e.DoorOpen);                
        }

        private void HandleRFIDEvent(object sender, RFIDReaderEventArgs e)
        {
            RfidDetected(e.RFID);
        }


    }
}
    
