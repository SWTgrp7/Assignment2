using Ladeskab.Interfaces;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    public class TestRFIDReader
    {
        private RFIDReader uut;
        private RFIDReaderEventArgs receivedRFIDEvent;

        // SETUP
        [SetUp]
        public void Setup()
        {
            uut = new RFIDReader();
            receivedRFIDEvent = null;
            
            uut.RFIDEvent += (o, args)
                => receivedRFIDEvent = args;
        }

        // TESTS
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-7)]
        [TestCase(1234)]
        public void Test_ReadRFIDEvent(int rfid)
        {
            uut.OnRfidRead(rfid);
            Assert.That(receivedRFIDEvent, Is.Not.Null);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-7)]
        [TestCase(1234)]
        public void Test_ReadRFIDEventProperty(int rfid)
        {
            uut.OnRfidRead(rfid);
            Assert.That(receivedRFIDEvent.RFID, Is.EqualTo(rfid));
        }
    }
}