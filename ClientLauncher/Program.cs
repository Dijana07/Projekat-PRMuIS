using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLauncher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Unesite broj klijenata za pokretanje: ");
            int brojKlijenata = int.Parse(Console.ReadLine());

            PokreniKlijente(brojKlijenata);

            Console.WriteLine("Svi klijenti su pokrenuti.");
            Console.ReadLine(); // Sprečava zatvaranje konzole odmah
        }

        static void PokreniKlijente(int brojKlijenata)
        {
            for (int i = 0; i < brojKlijenata; i++)
            {
                // Putanja do kompajliranog klijentskog izvršnog fajla
                string clientPath = @"C:\Users\PC\Desktop\3. godina\mreze\projekat\Projekat\Projekat\bin\Debug\net8.0\Client.exe";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c start \"Klijent {i + 1}\" \"{clientPath}\" {i + 1}",
                    WindowStyle = ProcessWindowStyle.Normal
                };
                
                Process.Start(psi);

                Console.WriteLine($"Pokrenut klijent #{i + 1}");
            }
        }
    }
}
