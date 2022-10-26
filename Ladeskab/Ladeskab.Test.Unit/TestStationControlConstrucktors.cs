using Ladeskab.Interfaces;
using NSubstitute;
using System.Net.Sockets;

namespace Ladeskab.Test.Unit
{
    public class TestStationControlConstructors
    {
        private StationControl _uutc1, _uutc2,_uutc3;

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
            _uutc1 = new StationControl();
            _uutc2 = new StationControl(_fakeDoor, _fakeIRFIDReader);
            _uutc3 = new StationControl(_fakeChargeControl, _fakeDisplay, _fakeDoor, _fakeIRFIDReader, _fakeLogger);
        }
        #region test construktors
        [Test]
        public void emptyConstruktor()
        {
            Assert.That(_uutc1.Door, Is.Not.Null);
            Assert.That(_uutc1.Charger, Is.Not.Null);
            Assert.That(_uutc1.RfidReader, Is.Not.Null);
            Assert.That(_uutc1.Logger, Is.Not.Null);
            Assert.That(_uutc1.Display, Is.Not.Null);

        }
        [Test]
        public void semiEmptyConstruktor()
        {
            {
                Assert.That(_uutc2.Door, Is.Not.Null);
                Assert.That(_uutc2.Charger, Is.Not.Null);
                Assert.That(_uutc2.RfidReader, Is.Not.Null);
                Assert.That(_uutc2.Logger, Is.Not.Null);
                Assert.That(_uutc2.Display, Is.Not.Null);
            }
        }
        [Test]
        public void FullConstruktor()
        {
            Assert.That(_uutc3.Door, Is.Not.Null);
            Assert.That(_uutc3.Charger, Is.Not.Null);
            Assert.That(_uutc3.RfidReader, Is.Not.Null);
            Assert.That(_uutc3.Logger, Is.Not.Null);
            Assert.That(_uutc3.Display, Is.Not.Null);

        }

        #endregion


    }
}