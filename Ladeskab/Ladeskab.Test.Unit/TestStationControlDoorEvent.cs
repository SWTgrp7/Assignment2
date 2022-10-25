using Ladeskab.Interfaces;
using NSubstitute;
using System.Net.Sockets;

namespace Ladeskab.Test.Unit
{
    public class TestStationControlDoorEvent
    {
        private StationControl _uut;

        private IDisplay _fakeDisplay;
        private IDoor _fakeDoor;
        private IRFIDReader _fakeIRFIDReader;
        private ILogger _fakeLogger;
        private IChargeControl _fakeChargeControl;

       

        [SetUp]
        public void Setup()
        {
            // Creating substitutes for all fakes using NSubstitute

            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeChargeControl = Substitute.For<IChargeControl>();
            _fakeDoor = Substitute.For<IDoor>();
            _fakeIRFIDReader = Substitute.For<IRFIDReader>();
            _fakeLogger = Substitute.For<ILogger>();

            _uut = new StationControl(_fakeChargeControl, _fakeDisplay, _fakeDoor, _fakeIRFIDReader, _fakeLogger);
        }


        // Testing when the door is open

        [Test]
        public void DoorDetected_DoorIsOpen_ConnectPhoneCalled()
        {
            // Setup stub with desired response
            
            // Act
            _fakeDoor.DoorEvent += Raise.EventWith(new DoorEventArgs { DoorOpen = true });
            // Assert
            _fakeDisplay.Received(1).ConnectPhone();
        }

        [Test]
        public void DoorDetected_DoorIsOpen_LoadRfidNotCalled()
        {
            // Setup stub with desired response

            // Act
            _fakeDoor.DoorEvent += Raise.EventWith(new DoorEventArgs { DoorOpen = true });
            // Assert
            _fakeDisplay.DidNotReceive().LoadRFID();
        }

        // Testing when the door is closed

        [Test]
        public void DoorDetected_DoorIsClosed_LoadRfidCalled()
        {
            // Setup stub with desired response

            // Act
            _fakeDoor.DoorEvent += Raise.EventWith(new DoorEventArgs { DoorOpen = false });
            // Assert
            _fakeDisplay.Received(1).LoadRFID();
        }

        [Test]
        public void DoorDetected_DoorIsClosed_ConnectPhoneNotCalled()
        {
            // Setup stub with desired response

            // Act
            _fakeDoor.DoorEvent += Raise.EventWith(new DoorEventArgs { DoorOpen = false });
            // Assert
            _fakeDisplay.Received(0).ConnectPhone();
        }

    }
}