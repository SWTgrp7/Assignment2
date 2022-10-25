using Ladeskab.Interfaces;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    public class TestDisplay
    {
        private Display uut;
        StringWriter sw;

        // SETUP
        [SetUp]
        public void Setup()
        {
            uut = new Display();
            sw = new StringWriter();
            Console.SetOut(sw);
            Console.SetError(sw);
        }

        // TESTS
        [Test]
        public void Test_ConnectPhone()
        {
            uut.ConnectPhone();
            Assert.That(sw.ToString(), Is.EqualTo(uut.connectphone + "\r\n"));
        }

        [Test]
        public void Test_DisconnectPhone()
        {
            uut.DisconnectPhone();
            Assert.That(sw.ToString(), Is.EqualTo(uut.disconnectphone + "\r\n"));
        }

        [Test]
        public void Test_ConnectionError()
        {
            uut.ConnectionError();
            Assert.That(sw.ToString(), Is.EqualTo(uut.connectionerror + "\r\n"));
        }

        [Test]
        public void Test_ChargeComplete()
        {
            uut.ChargeComplete();
            Assert.That(sw.ToString(), Is.EqualTo(uut.chargecomplete + "\r\n"));
        }

        [Test]
        public void Test_Charging()
        {
            uut.Charging();
            Assert.That(sw.ToString(), Is.EqualTo(uut.charging + "\r\n"));
        }

        [TestCase(100)]
        [TestCase(12.7)]
        [TestCase(-20)]
        [TestCase(0)]
        public void Test_ChargingCurrent(double current)
        {
            uut.Charging(current);
            Assert.That(sw.ToString(), Is.EqualTo(uut.chargingcurrent + current + "\r\n"));
        }

        [Test]
        public void Test_RFIDError()
        {
            uut.RFIDError();
            Assert.That(sw.ToString(), Is.EqualTo(uut.RFIDerror + "\r\n"));
        }

        [Test]
        public void Test_LoadRFID()
        {
            uut.LoadRFID();
            Assert.That(sw.ToString(), Is.EqualTo(uut.scanRFID + "\r\n"));
        }

        [Test]
        public void Test_Occupied()
        {
            uut.Occuppied();
            Assert.That(sw.ToString(), Is.EqualTo(uut.occupied + "\r\n"));
        }

        [Test]
        public void Test_DoorLocked()
        {
            uut.DoorLocked();
            Assert.That(sw.ToString(), Is.EqualTo(uut.doorlocked + "\r\n"));
        }

        [Test]
        public void Test_RemovePhone()
        {
            uut.RemovePhone();
            Assert.That(sw.ToString(), Is.EqualTo(uut.removephone + "\r\n"));
        }

        [Test]
        public void Test_WrongRFID()
        {
            uut.WrongRFID();
            Assert.That(sw.ToString(), Is.EqualTo(uut.wrongRFID + "\r\n"));
        }
    }
}