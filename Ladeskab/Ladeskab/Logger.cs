using System;
using System.IO;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class Logger : ILogger
    {

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        private StreamWriter writer;

        public Logger()
        {
            //writer = new StreamWriter(logFile);
        }

        public void LogDoorLocked(int id)
        {
            using var writer = File.AppendText(logFile);
            writer.WriteLine(DateTime.Now + $": Cabinet locked with RFID: {id}");
        }

        public void LogDoorUnlocked(int id)
        {
            using var writer = File.AppendText(logFile);
            writer.WriteLine(DateTime.Now + $": Cabinet unlocked with RFID: {id}");
        }
    }
}