using Ladeskab.Interfaces;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    public class TestLogger
    {
        private Logger uut;
        StreamReader reader;

        // SETUP
        [SetUp]
        public void Setup()
        {
            uut = new Logger();
            //reader = new StreamReader(logfile);
        }

        // TESTS
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(256)]
        [TestCase(-20)]
        public void Test_LoggingOnDoorLocked(int id)
        {

        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(256)]
        [TestCase(-20)]
        public void Test_LoggingOnDoorUnlocked(int id)
        {

        }
    }
}