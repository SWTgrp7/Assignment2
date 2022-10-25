using Ladeskab.Interfaces;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    public class TestRFIDReader
    {
        private RFIDReader uut;

        // SETUP
        [SetUp]
        public void Setup()
        {
            uut = new RFIDReader();
            //uut = Substitute.For<RFIDReader>();
        }

        // TESTS
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-7)]
        [TestCase(1234)]
        public void Test_ReadRFID(int rfid)
        {
            //uut.RFIDEvent += Raise.EventWith(new RFIDReaderEventArgs()
            //    { RFID = rfid });

            // Manual way of testing
            bool eventRaised = false;

            uut.RFIDEvent += delegate (object sender, RFIDReaderEventArgs e)
                { eventRaised = true; };
            
            uut.OnRfidRead(rfid);
            
            // Test if the boolean is set to true
            // in the delegate call.
            Assert.That(eventRaised, Is.EqualTo(true));
        }
    }
}