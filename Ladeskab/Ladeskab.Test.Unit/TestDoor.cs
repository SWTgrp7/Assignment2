using Ladeskab.Interfaces;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    public class TestDoor
    {
        private Door uut;

        // SETUP
        [SetUp]
        public void Setup()
        {
            uut = new Door();
        }

        // TESTS
        [Test]
        public void Test_DoorIsLocked()
        {
            uut.LockDoor();
            Assert.That(uut.IsLocked, Is.True);
        }

        [Test]
        public void Test_DoorIsUnlocked()
        {
            uut.UnlockDoor();
            Assert.That(uut.IsLocked, Is.False);
        }

        [Test]
        public void Test_DoorIsOpened()
        {
            uut.OnDoorOpen();
            Assert.That(uut.IsDoorOpen, Is.True);

            // Event arg test?
            //_uut.DoorEvent += Raise.Event();
            //Assert.That(uut., Is.);
        }

        [Test]
        public void Test_DoorIsClosed()
        {
            uut.OnDoorClose();
            Assert.That(uut.IsDoorOpen, Is.False);

            // Event arg test?
        }
    }
}