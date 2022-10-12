﻿using Ladeskab;
using Ladeskab.Interfaces;

class Program
    {
        static void Main(string[] args)
        {
            IOpenDoor door = new Door();
            IRFIDReader rfidReader = new RFIDReader();
            StationControl SC = new StationControl();
            

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;
                    case 'P':
                        SC._logger.PrintFile();
                        break;

                default:
                        break;
                }

            } while (!finish);
        }
    }

