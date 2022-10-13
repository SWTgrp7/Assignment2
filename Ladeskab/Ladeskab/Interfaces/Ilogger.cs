using System;
using System.Security.Cryptography.X509Certificates;

public interface ILogger
{
    public void LogDoorLocked(int id);
    public void LogDoorUnlocked(int id);
    
}
