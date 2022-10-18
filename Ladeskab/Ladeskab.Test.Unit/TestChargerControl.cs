using Ladeskab.Interfaces;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    public class TestsChargerControl
    {
        private ChargerControl _uut;
        private IDisplay _display;
        private IUsbCharger _usbCharger;
        private ChargerControl _testor;
       

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargerControl(_display, _usbCharger);
            _testor = new ChargerControl();
           
        }

        [Test]
        public void Test_StartCharge()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
            _usbCharger.DidNotReceive().StopCharge();


        }

        [Test]
        public void Test_StopCharge()
        {
            _uut.StopCharge();
            _usbCharger.Received(1).StopCharge();
            _usbCharger.DidNotReceive().StartCharge();

        }

        [Test]
        public void Test_Connected()
        {
            _usbCharger.Connected.Returns(true);
            Assert.That(_uut.Connected, Is.EqualTo(true));
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(500)]
        public void Test_HandleCurrentValueChanged_CurrentCheck(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            System.Threading.Thread.Sleep(300);
            Assert.That(_uut.CurrentNow, Is.EqualTo(current));
        }

        [Test]
        public void Test_HandleCurrentValueChanged_0()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 0 });
            _display.Received(1).ConnectPhone();
            _display.DidNotReceive().Charging();
            _display.DidNotReceive().ChargeComplete();
        }
        [TestCase(4.9)]
        [TestCase(1)]
        public void Test_HandleCurrentValueChanged_ChargeComplete(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            _display.Received(1).ChargeComplete();
            _display.DidNotReceive().Charging();
            _display.DidNotReceive().ConnectPhone();
        }
        [TestCase(6)]
        [TestCase(499)]     
        public void Test_HandleCurrentValueChanged_Charging(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            _display.Received(1).Charging();
            _display.DidNotReceive().ChargeComplete();
            _display.DidNotReceive().ConnectPhone();
        }
        
        [Test]
        public void Test_HandleCurrentValueChanged_Overload()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 5000 });
            _display.DidNotReceive().Charging();
            _display.DidNotReceive().ChargeComplete();
            _display.DidNotReceive().ConnectPhone();
            _display.Received(1).DisconnectPhone();
            _usbCharger.Received(1).StopCharge();
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(6)]
        [TestCase(499)]
        [TestCase(501)]
        public void Test_HandleCurrentValueChanged_ChargingBool(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });

            if (current == 0)
            {
                Assert.That(_uut._charging, Is.EqualTo(false));
            }
            else if (current > 0 && current < 5)
            {
                Assert.That(_uut._charging, Is.EqualTo(false));
            }
            else if (current <= 500 && current > 5)
            {
                Assert.That(_uut._charging, Is.EqualTo(true));
            }

            else
            { Assert.Pass(); }
        }  

    }
}