using Ladeskab.Interfaces;
using NSubstitute;
using System.Net.Sockets;

namespace Ladeskab.Test.Unit
{
    public class TestStationControlRfidEvent
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

        #region LadeskabState_Available

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ChargerIsConnected_LockDoorCalled(int RfidId)
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(true);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeDoor.Received(1).LockDoor();            
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ChargerIsConnected_UnlockDoorNotCalled(int RfidId)
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(true);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeDoor.Received(0).UnlockDoor();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ChargerIsConnected_SaveCorrectRfid(int RfidId)
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(true);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            Assert.That(_uut.OldId, Is.EqualTo(RfidId));
        }

        [Test]
        public void RfidDetected_ChargerIsConnected_StartChargeCalled()
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(true);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = 1 });
            // Assert
            _fakeChargeControl.Received(1).StartCharge();
        }

        [Test]
        public void RfidDetected_ChargerIsConnected_StopChargeNotCalled()
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(true);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = 1 });
            // Assert
            _fakeChargeControl.Received(0).StopCharge();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ChargerIsConnected_LogDoorLockedCalled(int RfidId)
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(true);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeLogger.Received(1).LogDoorLocked(RfidId);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ChargerIsConnected_LogDoorUnlockedCalled(int RfidId)
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(true);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeLogger.Received(0).LogDoorUnlocked(RfidId);
        }

        // Skal også teste for display -> Først refactor stationControl kode i eventhandler!

        [Test]
        public void RfidDetected_ChargerIsConnected_DisplayDoorLockedCalled()
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(true);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = 1 });
            // Assert
            _fakeDisplay.Received(1).DoorLocked();
        }

        // Do we have to check no other display method was called? Seemes redundant..


        // *** Phone not connectet to charger! *** //
        [Test]
        public void RfidDetected_ChargerNOTConnected_DisplayConnectionErrorCalled()
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(false);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = 1 });
            // Assert
            _fakeDisplay.Received(1).ConnectionError();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ChargerNOTConnected_LockDoorNotCalled(int RfidId)
        {
            // Setup stub with desired response
            _fakeChargeControl.Connected.Returns(false);
            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeDoor.Received(0).LockDoor();
        }


        #endregion

        #region LadeskabState_DoorOpen

        [Test]
        public void RfidDetected_SimulateDoorIsOpen_NoAction()
        {
            // Setup stub with desired response
            // First a DoorDetected event is registered - sets the state to DoorOpen
            
            _fakeDoor.DoorEvent += Raise.EventWith(new DoorEventArgs { DoorOpen = true });


            // Act
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = 1 });
            // Assert that no calls were received
            _fakeDoor.Received(0).LockDoor();
            _fakeDisplay.DidNotReceive().ConnectionError();
        }

        // Do we add more tests to determine no methods were called here?

        #endregion

        #region LadeskabState_Locked

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ValidateOldId_StopChargeCalled(int RfidId)
        {
            // Setup stub with desired response
            // The first RFID event, when the phone is connected, stores the rfid and locks the door - State = Locked

            _fakeChargeControl.Connected.Returns(true);
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });

            // Act
            // The second rfid event - if the ID is correct, will unlock the door, and stop charging
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeChargeControl.Received(1).StopCharge();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ValidateOldId_UnlockDoorCalled(int RfidId)
        {
            // Setup stub with desired response
            // The first RFID event, when the phone is connected, stores the rfid and locks the door - State = Locked

            _fakeChargeControl.Connected.Returns(true);
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });

            // Act
            // The second rfid event - if the ID is correct, will unlock the door, and stop charging
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeDoor.Received(1).UnlockDoor();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ValidateOldId_LogDoorUnlockedCalled(int RfidId)
        {
            // Setup stub with desired response
            // The first RFID event, when the phone is connected, stores the rfid and locks the door - State = Locked

            _fakeChargeControl.Connected.Returns(true);
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });

            // Act
            // The second rfid event - if the ID is correct, will unlock the door, and stop charging
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeLogger.Received(1).LogDoorUnlocked(RfidId);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RfidDetected_ValidateOldId_RemovePhoneCalled(int RfidId)
        {
            // Setup stub with desired response
            // The first RFID event, when the phone is connected, stores the rfid and locks the door - State = Locked

            _fakeChargeControl.Connected.Returns(true);
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });

            // Act
            // The second rfid event - if the ID is correct, will unlock the door, and stop charging
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });
            // Assert
            _fakeDisplay.Received(1).RemovePhone();
        }

        [TestCase(1, 5)]
        [TestCase(2, 5)]
        [TestCase(3, 5)]
        public void RfidDetected_ValidateOldId_WRONG_WrongRFIDCalled(int RfidId, int wrongRfidId)
        {
            // Setup stub with desired response
            // The first RFID event, when the phone is connected, stores the rfid and locks the door - State = Locked

            _fakeChargeControl.Connected.Returns(true);
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = RfidId });

            // Act
            // The second rfid event - if the ID is correct, will unlock the door, and stop charging
            _fakeIRFIDReader.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs { RFID = wrongRfidId });
            // Assert
            _fakeDisplay.Received(1).WrongRFID();
        }

        #endregion



    }
}