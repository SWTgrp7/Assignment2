using System;
using System.Security.Cryptography.X509Certificates;

public interface Ilogger
{
    public void LogDoorLocked(int id);
    public void LogDoorUnlocked(int id);
    public void PrintFile();
}
