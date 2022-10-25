using Ladeskab.Interfaces;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    public class TestDoor
    {
        private Door _uut;

        // SETUP
        [SetUp]
        public void Setup()
        {
            _uut = new Door();
        }

        // TESTS
        [Test]
        public void Test_DoorIsLocked()
        {
            _uut.LockDoor();
            Assert.That(_uut.IsLocked, Is.True);
        }

        [Test]
        public void Test_DoorIsUnlocked()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.IsLocked, Is.False);
        }

        [Test]
        public void Test_DoorIsOpened()
        {
            _uut.OnDoorOpen();
            Assert.That(_uut.IsDoorOpen, Is.True);

            // Event arg test?
            //_uut.DoorEvent += Raise.Event();
            //Assert.That(_uut., Is.);
        }

        [Test]
        public void Test_DoorIsClosed()
        {
            _uut.OnDoorClose();
            Assert.That(_uut.IsDoorOpen, Is.False);

            // Event arg test?
        }
    }
}