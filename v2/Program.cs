using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

namespace Uppgift2C
{
    internal class Program
    {
        // En metod för att beräkna säljarnas nivå baserat på antalet sålda artiklar.
        static string BeräknaKategori(int såldaArtiklar)
        {
            if (såldaArtiklar < 50)
                return "Nivå 1: Under 50 artiklar";
            else if (såldaArtiklar < 100)
                return "Nivå 2: 50-99 artiklar";
            else if (såldaArtiklar < 200)
                return "Nivå 3: 100-199 sålda artiklar";
            else
                return "Nivå 4: Över 199 sålda artiklar";
        }

        static void Main(string[] args)
        {
            // Skapa en tom lista för att lagra säljarnas information.
            List<Säljare> säljare = new List<Säljare>();

            // Fråga användaren om antal säljare de vill registrera.
            Console.Write("Hur många säljare vill du registrera?: ");
            int antalSäljare = int.Parse(Console.ReadLine());
            Console.WriteLine();

            // Loopa för att läsa in säljarnas information.
            for (int i = 0; i < antalSäljare; i++)
            {
                // Skapa en ny säljare och fyll i deras information.
                Säljare säljareInfo = new Säljare();

                Console.WriteLine();
                Console.WriteLine("Säljare " + (i + 1));

                // Fråga användaren om namn och spara det i säljareInfo.Namn.
                Console.Write("Namn: ");
                säljareInfo.Namn = Console.ReadLine();

                // Fråga användaren om personnummer och spara det i säljareInfo.Personnummer.
                Console.Write("Personnummer: ");
                säljareInfo.Personnummer = Console.ReadLine();

                // Fråga användaren om distrikt och spara det i säljareInfo.Distrikt.
                Console.Write("Distrikt: ");
                säljareInfo.Distrikt = Console.ReadLine();

                // Fråga användaren om antalet sålda artiklar och spara det i säljareInfo.SåldaArtiklar.
                Console.Write("Sålda artiklar: ");
                säljareInfo.SåldaArtiklar = int.Parse(Console.ReadLine());

                // Lägg till säljaren i listan.
                säljare.Add(säljareInfo);
            }

            // Sortera säljarna efter antalet sålda artiklar i fallande ordning.
            säljare = säljare.OrderBy(seller => seller.SåldaArtiklar).ToList();

            // Skapa en dictionary för att gruppera säljarna efter deras nivåer.
            Dictionary<string, List<Säljare>> sellersByLevel = new Dictionary<string, List<Säljare>>();

            // Loopa igenom säljarna och placera dem i rätt kategori baserat på antalet sålda artiklar.
            foreach (var seller in säljare)
            {
                string kategori = BeräknaKategori(seller.SåldaArtiklar);

                // Om kategorin inte redan finns i dictionary, skapa en ny tom lista för den.
                if (!sellersByLevel.ContainsKey(kategori))
                {
                    sellersByLevel[kategori] = new List<Säljare>();
                }

                // Lägg till säljaren i listan som motsvarar deras kategori.
                sellersByLevel[kategori].Add(seller);
            }

            // Öppna en textfil för att skriva resultatet.
            using (StreamWriter skrivare = new StreamWriter("resultat.text"))
            {

                // Loopa igenom varje kategori av säljare.
                foreach (var kvp in sellersByLevel)
                {
                    string level = kvp.Key;
                    List<Säljare> sellersInLevel = kvp.Value;

                    // Skriv ut kategorin och antalet säljare som har nått den.
                    Console.WriteLine();
                    skrivare.WriteLine();
                    Console.WriteLine("Namn".PadRight(20) + "Persnr".PadRight(20) + "Distrikt".PadRight(20) + "Antal");
                    skrivare.WriteLine("Namn".PadRight(20) + "Persnr".PadRight(20) + "Distrikt".PadRight(20) + "Antal");

                    // Loopa igenom säljarna i kategorin och skriv ut deras information.
                    foreach (var seller in sellersInLevel)
                    {
                        Console.WriteLine($"{seller.Namn.PadRight(20)}{seller.Personnummer.PadRight(20)}{seller.Distrikt.PadRight(20)}{seller.SåldaArtiklar}");
                        skrivare.WriteLine($"{seller.Namn.PadRight(20)}{seller.Personnummer.PadRight(20)}{seller.Distrikt.PadRight(20)}{seller.SåldaArtiklar}");
                    }

                    // Skriv ut antalet säljare som har nått nivån.
                    Console.WriteLine($"{sellersInLevel.Count} säljare har nått {level}");
                    skrivare.WriteLine($"{sellersInLevel.Count} säljare har nått {level}");
                }

                // Stäng textfilen.
                skrivare.Close();
            }

            // Avslutningsmeddelande.         
            Console.WriteLine();
            Console.WriteLine("Tryck Enter för att avsluta...");
            Console.ReadLine();
        }
    }
}
