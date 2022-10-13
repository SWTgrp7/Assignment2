using System;
using System.IO;
using Ladeskab.Interfaces;

public class Logger	: Ilogger
{

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        private StreamWriter writer;
        private StreamReader reader;

        public Logger()
        {
            writer = new StreamWriter(logFile);
            reader = new StreamReader(logFile);
        }

        public void LogDoorLocked(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
            }
        }

        public void LogDoorUnlocked(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            }
        }

    public void PrintFile()
        { }

    
}

