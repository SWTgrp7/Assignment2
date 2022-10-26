using Ladeskab.Interfaces;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    public class TestDoor
    {
        private Door uut;
        private DoorEventArgs receivedDoorEvent;

        // SETUP
        [SetUp]
        public void Setup()
        {
            uut = new Door();
            receivedDoorEvent = null;

            uut.DoorEvent += (o, args)
                => receivedDoorEvent = args;
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
        }

        [Test]
        public void Test_DoorOpenedEvent()
        {
            uut.OnDoorOpen();
            Assert.That(receivedDoorEvent, Is.Not.Null);
        }

        [Test]
        public void Test_DoorOpenedEventPropertyIsTrue()
        {
            uut.OnDoorOpen();
            Assert.That(receivedDoorEvent.DoorOpen, Is.True);
        }

        [Test]
        public void Test_DoorIsClosed()
        {
            uut.OnDoorClose();
            Assert.That(uut.IsDoorOpen, Is.False);
        }

        [Test]
        public void Test_DoorClosedEvent()
        {
            uut.OnDoorClose();
            Assert.That(receivedDoorEvent, Is.Not.Null);
        }

        [Test]
        public void Test_DoorClosedEventPropertyIsTrue()
        {
            uut.OnDoorClose();
            Assert.That(receivedDoorEvent.DoorOpen, Is.False);
        }
    }
}