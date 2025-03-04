using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Cryptography;
using Client.Pomocne_metode;

namespace Client
{
    public class Client
    {
        static void Main(string[] args)
        {
            Socket clientSocket;
            IPEndPoint destEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65000);

            Console.WriteLine("Aplikacija je pokrenuta.\n");
            Console.WriteLine("Izaberite koji protokol zelite da koristite (UDP ili TCP): ");
            string? protokol = Console.ReadLine();

            if (protokol?.ToLower() == "tcp")
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    clientSocket.Connect(destEP);
                    Console.WriteLine("Klijent je uspesno povezan sa serverom!");
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Greska pri povezivanju sa serverom: {ex.SocketErrorCode}");
                    return;
                }
            }
            else if (protokol?.ToLower() == "udp")
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            else
            {
                Console.WriteLine("Za tip protokola niste uneli validnu vrednost. Aplikacija prestaje sa radom.\n");
                return;
            }

            Console.WriteLine("Izaberite koji algoritam sifrovanja zelite da koristite (DES ili AES): ");
            string? algoritam = Console.ReadLine();

            if (algoritam?.ToLower() != "des" && algoritam?.ToLower() != "aes")
            {
                Console.WriteLine("Za algoritam sifrovanja niste uneli validnu vrednost. Aplikacija prestaje sa radom.\n");
                clientSocket.Close();
                return;
            }
            
            /*
            Hesiranje hes = new Hesiranje();
            string proba = "dijana";
            Console.WriteLine(hes.Hesiraj(proba));
            */
        }
    }
}
