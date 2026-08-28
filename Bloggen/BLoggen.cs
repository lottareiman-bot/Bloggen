using System;
using System.Collections.Generic;

namespace Bloggen
{
    // ------------------------------------
    //   ********* KLASS INLÄGG *********
    // ------------------------------------
    public class Inlägg // Klass som beskriver ett inlägg
    {
        // beskriver vad varje inlägg består av
        public string Datum;
        public string Titel;
        public string Text;

        // konstruktor som tar emot en strängvektor med tre värden
        public Inlägg(string[] data) 
        {
            Datum = data[0];
            Titel = data[1];
            Text = data[2];
        }
    }
     
    internal class BLoggen
    {
        // -------------------------------------------
        //   ************** MAIN ******************
        // -------------------------------------------
        static void Main(string[] args)
        {
            // Ny lista; som innehåller strängvektorer (datum, titel & inlägg)
            List<string[]> vektorInläggLista = new List<string[]>();
            bool isRunning = true;

            // ****** MENY ******
            while (isRunning) 
            {
                Console.Clear();
                // anropar metoden som visar det senaste inlägget innan menyn visas
                VisaSenasteInlägg(vektorInläggLista); 
                Console.WriteLine
                    ("Välkommen till din blogg!\n " +
                    "\n\t[1] Skriv ett inlägg" +
                    "\n\t[2] Visa dina inlägg" +
                    "\n\t[3] Sök bland dina inlägg" +
                    "\n\t[4] Redigera ett inlägg" +
                    "\n\t[5] Ta bort ett inlägg" +
                    "\n\t[6] Avsluta programmet");
                Console.Write("\nVälj ett alternativ: ");
                int.TryParse(Console.ReadLine(), out int menyVal); // felhantering

                switch (menyVal)
                {
                    // ***** MENYVAL 1 *******
                    // Skriv ett inlägg
                    case 1:
                        //skapar en vektor med tre element
                        string[] inlägg = new string[3];
                        DateTime datum;
                        while (true)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue; // blå färg för inlägg
                            Console.WriteLine("\n***********************************"); // mer användarvänlig utskrift
                            Console.WriteLine("       SKAPA ETT NYTT INLÄGG");
                            Console.WriteLine("      Ange Datum, Titel, Inlägg");
                            Console.WriteLine("***********************************");
                            Console.ResetColor();
                            Console.Write("\nSkriv datum för inlägget (åååå-mm-dd): ");
                            string input = Console.ReadLine();
                            // Försöker konvertera det inmatade datumet till ett DateTime-objekt

                            if (DateTime.TryParse(input, out datum))
                            {
                                // Om datumet är giltigt, sparas det i inläggsvektorn
                                inlägg[0] = datum.ToString("yyyy-MM-dd");
                                break; // Avslutar loopen när ett giltigt datum har matats in
                            }
                            else
                            {
                                Console.WriteLine("\tFelaktigt datumformat, försök igen.");
                                Console.ReadKey();
                            }
                        }
                        Console.Write("\nSkriv en titel:  "); // tilldelar resterande element i vektorn
                        inlägg[1] = Console.ReadLine();
                        Console.Write("\nSkriv ditt inlägg:\n- ");
                        inlägg[2] = Console.ReadLine();
                        vektorInläggLista.Add(inlägg);
                        Console.ForegroundColor= ConsoleColor.DarkYellow;
                        Console.WriteLine("\nInlägget har sparats!");
                        återMeny();
                        break;

                    // ***** MENYVAL 2 *******
                    // Visa dina inlägg
                    case 2:
                        if (vektorInläggLista.Count == 0)       // om inga inläggs finns
                        {
                            Console.WriteLine("\nDet finns inga sparade inlägg. ");
                            återMeny();
                        }
                        else
                        { 
                            BubbleSortDatum(vektorInläggLista); // anropa metod sortera efter datum innan visning
                            VisaInlägg(vektorInläggLista);      // anropa metod visa alla inlägg                            
                            återMeny();                         // anropa metoden åter
                        }
                        break;

                    // ***** MENYVAL 3 *******
                    // Sök bland dina inlägg
                    case 3:
                        if (vektorInläggLista.Count == 0)   // om inga inlägg finns
                        {
                            Console.WriteLine("\nDet finns inga sparade inlägg. ");
                            återMeny();
                        }

                        else
                        {
                            Console.Clear(); // rensar text för renare utskrift
                            Console.WriteLine("\n***********************************"); // mer användarvänlig utskrift
                            Console.WriteLine("       SÖK BLAND DINA INLÄGG");
                            Console.WriteLine("***********************************\n");
                            // Anropar metod 3 sorterar inläggsvektorn
                            // i titelordning innan sökningen påbörjas
                            BubbleSortTitel(vektorInläggLista);                        
                            Console.Write("Sök på en titel: ");                        
                            string sökOrd = Console.ReadLine();                        
                            // anropar metod 4 som utför en binär sökning i den sorterade listan                        
                            BinärSökning(vektorInläggLista, sökOrd);
                        }
                        break;

                    // ***** MENYVAL 4 *******
                    // Redigera ett inlägg
                    case 4: 
                        if (vektorInläggLista.Count == 0) // om inga inlägg finns
                        {
                            Console.WriteLine("\nDet finns inga sparade inlägg.");
                            återMeny();
                            break;
                        }
                        Console.Clear(); // rensar text för renare utskrift
                        BubbleSortDatum(vektorInläggLista); // anropa sortera efter datum-metoden
                        VisaInlägg(vektorInläggLista); // anropa metoden visa alla inlägg innan nedan fråga
                        
                        Console.WriteLine("\n***********************************");
                        Console.WriteLine("       REDIGERA ETT INLÄGG");
                        Console.WriteLine("***********************************\n");
                        
                        Console.Write("\nVilket inlägg vill du redigera? (ange # nummer): ");                        
                        int.TryParse(Console.ReadLine(), out int redigeraVal); // felhantering

                        bool hittat = false; 

                        for (int i = 0; i < vektorInläggLista.Count; i++) // loop som går igenom listan
                        {
                            // Jämför det inmatade numret med indexet för varje inlägg i listan
                            // (i + 1) används eftersom indexet börjar på 0,
                            // om det hittas får användaren möjlighet att redigera datum, titel och inlägg                           
                            if (redigeraVal == i + 1) 
                            {
                                hittat = true;
                                string input;                                
                                while (true) 
                                {                                    
                                    Console.Write("\nSkriv ett nytt datum för inlägget (åååå-mm-dd):  ");
                                    input = Console.ReadLine();

                                    if (DateTime.TryParse(input, out DateTime datumRedigera)) // felhantering
                                    {
                                        vektorInläggLista[i][0] = input;
                                        break; // ersätter vardera element med nytt värde 
                                    }
                                     // korrekt datum, avsluta loopen

                                    else
                                    {
                                        Console.WriteLine("\nFelaktigt datumformat, försök igen.");
                                        Console.ReadKey();
                                    }
                                }                                
                                Console.Write("\nSkriv en ny titel:  ");    
                                vektorInläggLista[i][1] = Console.ReadLine(); 
                                Console.Write("\nSkriv ett nytt inlägg:\n- ");
                                vektorInläggLista[i][2] = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.DarkYellow; // gul färg för lyckad redigering
                                Console.WriteLine("\nInlägget har redigerats!");
                                återMeny(); // anropar metod
                                break;
                            }
                        }
                        if (!hittat) // inlägget hittas inte
                        {
                            Console.WriteLine("\nInlägget kunde inte hittas.");
                            återMeny();                            
                        }
                        break;
                    
                    // ***** MENYVAL 5 *******
                    // Ta bort ett inlägg    
                    case 5:
                        if (vektorInläggLista.Count == 0) // om inga inlägg finns
                        {
                            Console.WriteLine("\nDet finns inga sparade inlägg.");
                            återMeny(); // anropar metod
                            break;
                        }
                        Console.Clear(); // rensar text för renare utskrift
                        BubbleSortDatum(vektorInläggLista); // anropa sortera efter datum-metoden
                        VisaInlägg(vektorInläggLista); // anropa metoden visa alla inlägg innan nedan fråga
                        Console.WriteLine("\n***********************************");
                        Console.WriteLine("       TA BORT ETT INLÄGG");
                        Console.WriteLine("***********************************\n");
                        while (true) // loop som fortsätter tills ett giltigt nummer matas in
                        {
                            Console.Write("\nAnge vilket # nummer på det inlägget du vill ta bort: ");
                            string input = Console.ReadLine();

                            // Kontrollerar om konverteringen lyckades
                            if (!int.TryParse(input, out int taBort))
                            {
                                Console.WriteLine("\nDu måste skriva en siffra, försök igen.");
                                Console.ReadKey();
                                continue;
                            }

                            // Kontrollerar att värdet är inom listans gränser
                            // om input är mindre än 1 eller större än listans längd
                            // = fel, försök igen
                            if (taBort < 1 || taBort > vektorInläggLista.Count)
                            {
                                Console.WriteLine("\nDet numret finns inte, försök igen.");
                                Console.ReadKey(); 
                                continue;
                            }
                            // giltigt nummer, ta bort inlägget och avsluta loopen
                            int index = taBort - 1;                                
                            vektorInläggLista.RemoveAt(index);
                            Console.ForegroundColor = ConsoleColor.DarkYellow; // gul text för lyckad borttagning
                            Console.WriteLine($"\nInlägg #{taBort} har tagits bort.");
                            återMeny();                             
                            break;
                            }
                           
                        break;

                    // ***** MENYVAL 6 *******
                    // Avsluta programmet
                    case 6: 
                        isRunning = false;
                        Console.WriteLine("\nTryck ENTER för att AVSLUTA...");
                        Console.ReadKey();
                        break;
                        
                    default: // Felaktigt val
                        Console.WriteLine("\nFelaktigt val, försök igen.");
                        återMeny();
                        break;
                    }
                
            }
        } // Nedan finns skapde metoder i programmet

        // -------------------------------
        //  ******** METOD (1) **********
        //  *** SORTERA EFTER DATUM ****
        // -------------------------------
        static void BubbleSortDatum(List<string[]> vektorInlägg)
        {
            // yttre loop som går igenom alla element i listan
            for (int i = 0; i < vektorInlägg.Count - 1; i++)
            {
                // inre loop som jämför varje inlägg med det nästa inlägget - 1 - i
                for (int j = 0; j < vektorInlägg.Count - 1 - i; j++)
                {
                    // konverterar till DateTime-objekt för att kunna jämföra
                    // datumen i två inlägg i taget
                    DateTime.TryParse(vektorInlägg[j][0], out DateTime d1);
                    DateTime.TryParse(vektorInlägg[j + 1][0], out DateTime d2);

                    if (d1 > d2) // Om första datumet är senare än det andra --> byt
                    {                        
                        var temp = vektorInlägg[j]; // skapar var temp för temporär lagring 
                        vektorInlägg[j] = vektorInlägg[j + 1];
                        vektorInlägg[j + 1] = temp;
                    }
                }
            }
        }
        // ---------------------------------
        //  ******** METOD (2) *********
        //  *** VISA SENASTE INLÄGGET ***
        // ---------------------------------
        static void VisaSenasteInlägg(List<string[]> vektorInläggLista)
        {
            if (vektorInläggLista.Count == 0) // OM inga sparade inlägg finns
            {
                Console.ForegroundColor = ConsoleColor.Blue; // blå färg på inlägg
                Console.WriteLine("Det finns inga sparade inlägg.");
                Console.WriteLine("-------------------------------\n");
                Console.ResetColor();
                return;
            }
            // Hämtar det senaste inkägget i listan genom Count - 1
            // och skapar ett nytt objekt där datan sparas innan visning.
            Inlägg Inlägg = new Inlägg(vektorInläggLista[vektorInläggLista.Count - 1]);

            Console.ForegroundColor = ConsoleColor.Blue; // blå färg för alla inlägg
            Console.WriteLine("-------------------------------"); // enklare utskrift genom klassen
            Console.WriteLine($"Senaste inlägg #{vektorInläggLista.Count}:");
            Console.WriteLine($"Publicering: {Inlägg.Datum}");
            Console.WriteLine($"Titel: {Inlägg.Titel}");
            Console.WriteLine($"Inlägg: {Inlägg.Text}");
            Console.WriteLine("-------------------------------");
            Console.ResetColor();
        }

        // ------------------------------
        //  ******* METOD (3) *********
        //  ** SORTERA TITELORDNING **
        // ------------------------------
        static void BubbleSortTitel(List<string[]> vektorInläggLista)
        {
            // yttre loop: beroende på listans längd, lika många varv
            // efter varje varv hamnar det alfabetiskt största längst bak
            for (int i = 0; i < vektorInläggLista.Count - 1; i++)
            {
                // inre loop som jämför två intilliggande inlägg åt gången
                // - 1 då det 'största' inlägget redan hamnat sist
                for (int j = 0; j < vektorInläggLista.Count - 1 - i; j++)
                {
                    // hämtar titlar för jämförelse genom CompareTo
                    string titel1 = vektorInläggLista[j][1];
                    string titel2 = vektorInläggLista[j + 1][1];

                    // CompareTo returnerar 0 om titel1 kommer efter titel2
                    // OM titel1 är alfabetiskt större än titel2 byter de plats
                    if (titel1.CompareTo(titel2) > 0)
                    {
                        // byter plats i listan
                        string[] temp = vektorInläggLista[j];
                        vektorInläggLista[j] = vektorInläggLista[j + 1];
                        vektorInläggLista[j + 1] = temp;
                    }
                }
            }
        }
        // ------------------------------
        // ******** METOD (4) *********
        // **** VISAR ALLA INLÄGG *****
        // ------------------------------
        static void VisaInlägg(List<string[]> vektorInläggLista)
        {
            Console.Clear(); // rensar text för renare utskrift
            Console.WriteLine("\n***********************************");
            Console.WriteLine("       DINA SPARADE INLÄGG");
            Console.WriteLine("***********************************\n");
            // for loop som går igenom alla inlägg i vektorn
            for (int i = 0; i < vektorInläggLista.Count; i++)
            {
                Inlägg Inlägg = new Inlägg(vektorInläggLista[i]); // Skapar objekt från klassen Inlägg

                Console.ForegroundColor = ConsoleColor.Blue; // blå färg för inlägg
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"Inlägg #{i + 1}:");    // tydligare utskrift med klassen Inlägg
                Console.WriteLine($"Publicering:{Inlägg.Datum}");
                Console.WriteLine($"Titel:{Inlägg.Titel}");
                Console.WriteLine($"Inlägg:{Inlägg.Text}");
                Console.ResetColor();
            }
        }
        // ------------------------------
        // ******** METOD (5) *********
        // ****** BINÄR SÖKNING *******
        // ------------------------------
        static void BinärSökning(List<string[]> vektorInläggLista, string sökOrd)
        {
            // *** metoden BubbleSortTitel har anropats i Main. ***

            int start = 0; // startvärde för sökning
            int slut = vektorInläggLista.Count - 1; // slutindex för sökning
            // Variabel som används för att avgöra om inlägget har hittats eller inte
            bool hittad = false;
            while (start <= slut) // SÅ LÄNGE start är mindre eller lika med slut
            {
                int mitt = start + (slut - start) / 2;  // variabel för att hålla koll på mitten
                string titel = vektorInläggLista[mitt][1]; // hämtar titel på inlägg i mitten

                if (titel.ToUpper() == sökOrd.ToUpper()) // OM sökordet hittas oberoende av versaler/gemener
                {
                    hittad = true; // träff
                    Console.WriteLine("\nDin sökning matchar med följande inlägg: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n-------------------------------"); // utskrift av det hittade inlägget
                    Console.WriteLine($"Ditt inlägg #{mitt + 1}: ");
                    Console.WriteLine($"Publicering: {vektorInläggLista[mitt][0]}");
                    Console.WriteLine($"Titel: {vektorInläggLista[mitt][1]}");
                    Console.WriteLine($"Inlägg: {vektorInläggLista[mitt][2]}");
                    återMeny(); // åter meny metod
                    break;
                }
                
                // Om titel är alfabetiskt mindre än sökOrd
                // fortsätter sökning i högra halvan (oberoende av versal/gemen)
                else if (titel.ToUpper().CompareTo(sökOrd.ToUpper()) < 0)
                {                   
                    start = mitt + 1;
                }
                 // ANNARS är titel > sökOrd fortsätter sökning i vänstra halvan
                else
                {              
                    slut = mitt - 1;
                }
            }
            if (!hittad) // OM sökordet inte hittas
            {
                Console.WriteLine("\nInlägget kunde inte hittas.");
                återMeny();
            }            
        }
        // ------------------------------
        // ******** METOD (6) *********
        // ***** ÅTER TILL MENY ******
        // ------------------------------
        static void återMeny()      // undviker upprepning
        {
            Console.ResetColor();   // alla eventuella färger tas bort innan utskrift
            Console.WriteLine("\nTryck ENTER för att återgå till menyn...");
            Console.ReadKey();
        }

    }
}
