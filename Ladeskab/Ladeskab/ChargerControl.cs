using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;
using UsbSimulator;

namespace Ladeskab
{
    public class ChargerControl : IChargeControl
    {
        IDisplay _display;
        IUsbCharger _USBcharger;
        private bool _charging { get; set; }
        private double CurrentNow { get;  set; }

        public ChargerControl()
        {
            _display = new Display();
            _USBcharger = new UsbChargerSimulator();
            _USBcharger.CurrentValueEvent += HandleCurrentValueChanged;
        }
        public ChargerControl(IDisplay display, IUsbCharger USBcharger)
        {
            _display = display;
            _USBcharger = USBcharger;
        }
        
        public bool Connected {
            
            get { return _USBcharger.Connected; }
            
            private set { } 
        }
        
        public void StartCharge()
        {
            _USBcharger.StartCharge();            
        }

        public void StopCharge()
        {
            _USBcharger.StopCharge();
        }

        private void HandleCurrentValueChanged(object  sender, CurrentEventArgs e)
        {
            CurrentNow = e.Current;
            
            //ingen tilslutning
            if (CurrentNow == 0.0) { if(_charging == false) _display.ConnectPhone(); _charging = false; }

            //tilsluttet færdig
            else if(CurrentNow<=5 && CurrentNow > 0) { _display.ChargeComplete(); _charging = false; }
            
            //opladning igang
            else if(CurrentNow <= 500 && CurrentNow > 5) {
                if(_charging==false)
                    _display.Charging();
                _charging = true;
            }

            //FAIL
            else  { 
                _display.DisconnectPhone();
                StopCharge();
            }


        }
        
    }
    
}
