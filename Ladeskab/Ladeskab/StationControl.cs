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
        private ILogFile _logFile;

        // private string logFile = "logfile.txt"; // Navnet på systemets log-fil -> Moved to Logger class

        public StationControl()
        {
            _charger = new ChargerControl();
            _state = LadeskabState.Available;
            _display = new Display();
            _door = new Door();
            _rfidReader = new RFIDReader();
            _logFile = new LogFile(logFile);
            
            _door.DoorEvent += HandleDoorEvent;
        }
        public StationControl(IChargeControl charger, IDisplay display, IDoor door, i)
        {
            _charger = charger;
            _state = LadeskabState.Available;
            _display = display;
            _door = door;
            _rfidReader = 
            
            _door.DoorEvent += HandleDoorEvent;
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
                        /*using (var writer = file.appendtext(logfile))
                        {
                            writer.writeline(datetime.now + ": skab låst med rfid: {0}", id);
                        }*/



                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
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
                        /*using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }*/

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }
        private void HandleDoorEvent(object sender, DoorEventArgs e)
        {
            if (e.DoorOpen == true)
                _display.ConnectPhone();


            if (e.DoorOpen == false)
                _display.LoadRFID();
        }
        
    }
}
    
