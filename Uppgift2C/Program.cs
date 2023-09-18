using System;
using System.Collections.Generic;
using System.IO;

namespace Uppgift2C
{
    internal class Program
    {
        // Metod som delar in säljarna i kategorier baserat på sålda artiklar
        // (static) Kan användas direkt, Objekt behöver ej skapas
        // Returnerar ett string värde basear på antal sålda artiklar
        // Använder if, if else och else för att dela in i olika nivåer
        static string BeräknaKategori(int såldaArtiklar)
        {
            if (såldaArtiklar < 50)
                return "Nivå 1: Under 50 artiklar.";
            else if (såldaArtiklar < 100)
                return "Nivå 2: 50-99 artiklar.";
            else if (såldaArtiklar < 200)
                return "Nivå 3: 100-199 sålda artiklar.";
            else
                return "Nivå 4: Över 199 sålda artiklar.";
        }

        // Main metod för att köra programmet
        static void Main(string[] args )
        {
            // Create a dictionary to store the count of sellers for each level
            Dictionary<string, int> sellersByLevelCount = new Dictionary<string, int>();

            // Lista för att lagra säljare
            // lagrar information om säljare
            List<Säljare> säljare = new List<Säljare>();

            // Fråga användaren hur många säljare de vill registrera
            // int.Parse används för att konvertera en string till en heltalsvariabel
            Console.Write("Hur många säljare vill du registrera?:  ");
            int antalSäljare = int.Parse(Console.ReadLine());
            Console.WriteLine();

            // Loopa och Läs in säljarens information
            // En ny instans av klassen Säljare skapas för varje säljare
            for (int i = 0; i < antalSäljare; i++)
            {
                Säljare säljareInfo = new Säljare();

                Console.WriteLine();
                Console.WriteLine("Säljare " + (i + 1));

                // Fråga och läs in namn
                Console.Write("Namn: ");
                säljareInfo.Namn = Console.ReadLine();

                // Fråga och läs in personnummer
                Console.Write("Personnummer: ");
                säljareInfo.Personnummer = Console.ReadLine();

                // Fråga och läs in distrikt
                Console.Write("Distrikt: ");
                säljareInfo.Distrikt = Console.ReadLine();

                // Fråga och läs in antalet sålda artiklar
                Console.Write("Sålda artiklar: ");
                säljareInfo.SåldaArtiklar = int.Parse(Console.ReadLine());

                // Lägg till säljaren i listan
                säljare.Add(säljareInfo);
            }

            // Kategorisera säljarna baserat på antal sålda artiklar
            Dictionary<int, List<Säljare>> sellersBySåldaArtiklar = new Dictionary<int, List<Säljare>>();

            // foreach-loop används för att iterera element som finns i en samling som tex array
            // Om det ej finns en nyckel, skapa en ny och lägg till säljare
            foreach (var säljaren in säljare)
            {
                int såldaArtiklar = säljaren.SåldaArtiklar;

                if (!sellersBySåldaArtiklar.ContainsKey(såldaArtiklar))
                {
                    sellersBySåldaArtiklar[såldaArtiklar] = new List<Säljare>();
                }

                sellersBySåldaArtiklar[såldaArtiklar].Add(säljaren);
            }    

            // Skapa en StreamWriter för att skriva resultat till filen "resultat.txt"
            // SteamWriter är en klass i C# som används för att skriva ut information till en fil
            // filen heter resultat.txt
            // using används för att säkerställa att filen stängs ordentligt när den ej används
            using (StreamWriter skrivare = new StreamWriter("resultat.txt"))
            {
                // foreach-loop för att iterera element som finns i kategori av säljare 
                foreach (var relation in sellersBySåldaArtiklar)
                {
                    int såldaArtiklar = relation.Key;
                    List<Säljare> säljareKategori= relation.Value;

                    string kategori = BeräknaKategori(såldaArtiklar);

                    // Skriv ut kategorin med antalet säljare till konsolen
                    Console.WriteLine();
                    Console.WriteLine($"{säljareKategori.Count} säljare har nått {kategori}");
                   
                   
                    // Skriv ut informationen för varje säljare i kategorin
                    foreach (var seller in säljareKategori)
                    {
                        // Skriv ut säljarnas informationen till fil
                        // $ används för att inkludera var
                        skrivare.WriteLine($"Namn: {seller.Namn}");
                        skrivare.WriteLine($"Personnummer: {seller.Personnummer}");
                        skrivare.WriteLine($"Distrikt: {seller.Distrikt}");
                        skrivare.WriteLine($"Sålda artiklar: {seller.SåldaArtiklar}");
                        skrivare.WriteLine();
                        skrivare.WriteLine($"{säljareKategori.Count} säljare har nått {kategori}");  
                    }
                }

                // Stäng StreamWriter
                skrivare.Close();
            }

            // Avslutningsmeddelande
            Console.WriteLine("Tryck Enter för att avsluta...");
            Console.ReadLine();
        }
    }
}
