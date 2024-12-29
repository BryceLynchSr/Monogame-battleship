// Douglas Netzel - Projekt 3, sänka Skepp, Utökningar för en meny samt säkerställa att giltiga x- och y-koordinater valts

// Skapa en slump-variabel av typen Random.
Random slump = new Random();

// Deklarera variabler av typen heltal för spelkartans bredd och höjd och tilldela värden.
int kartBredd = 6;
int kartHöjd = 4;

// Deklarera variabel för menyval samt senaste vinnaren av typen sträng och ge dessa neutrala värden.
string val = "";
string senasteVinnaren = ("Ingen vinnare än.");

// Skapa arrays för spelarens resp datorns karta och tilldela dessa bredden och höjden som vi angav innan.
string[,] spelarensKarta = new string[kartBredd, kartHöjd];
string[,] datornsKarta = new string[kartBredd, kartHöjd];

// Skapa arrays för spelarens resp datorns avfyrade skott av typen boolean och tilldela dessa samma 
// värden som våra kart-arrays.
bool[,] spelarensSkott = new bool[kartBredd, kartHöjd];
bool[,] datornsSkott = new bool[kartBredd, kartHöjd];

// En while-sats styr vår meny och visar följande alternativ tills dess att spelaren avslutar spelet.
while (val != "3")
{
    Console.WriteLine("VÄLKOMMEN TILL SÄNKA FARTYG!");
    Console.WriteLine("Välj något av följande:");
    Console.WriteLine("1. Spela Sänka fartyg.");
    Console.WriteLine("2. Se senaste vinnaren.");
    Console.WriteLine("3. Avsluta spelet.");
    val = Console.ReadLine();

// En switch-sats styr vårt menyval.
    switch (val)
    {
        // Vid val 1 startar spelet och vi anropar metoder för att skapa kartor, placera ut skepp och 
        // sedan påbörja spelomgången.
        case "1":

            SkapaKartorna();
            PlaceraUtSkepp();
            SpelaSänkaFartyg();

            // Metod för att skapa kartorna för spelare och dator och att med en loop ge de avfyrade skotten värdet
            // false, som nu innebär att alla skott har missat.
            void SkapaKartorna()
            {

                for (int y = 0; y < kartHöjd; y++)
                {
                    for (int x = 0; x < kartBredd; x++)
                    {
                        spelarensKarta[x, y] = "O";
                        datornsKarta[x, y] = "O";
                        spelarensSkott[x, y] = false;
                        datornsSkott[x, y] = false;
                    }
                }

            }
            // Metod för att placera ut 3 st X på spelarens resp datorns karta.
            void PlaceraUtSkepp()
            {

                spelarensKarta[2, 3] = "X";
                spelarensKarta[2, 1] = "X";
                spelarensKarta[3, 1] = "X";
                datornsKarta[2, 2] = "X";
                datornsKarta[5, 3] = "X";
                datornsKarta[4, 1] = "X";

            }

            // Metoden för att spela en omgång av Sänka fartyg.
            void SpelaSänkaFartyg()

            { 
                // En variabel av typen boolean för att hålla reda på när spelet är slut deklareras. 
                bool speletÄrSlut = false;

                // En while-sats håller reda på när någon ännu inte har vunnit och som håller spelet igång.
                while (speletÄrSlut == false)
                {
                    // Vi börjar med att rita ut våra kartor genom att anropa metoden för detta. Sedan får spelaren börja
                    // med att välja x-koordinaten för sitt skott.
                    RitaKarta();
                    Console.WriteLine();
                    Console.WriteLine("Ange en x-koordinat mellan 1-6 för ditt skott.");
                    int skottXSpelare = LäsInt();

                    // En while-sats kontrollerar att spelaren har angivit ett korrekt värde för x. Även metoden LäsInt
                    // anropas som säkerställer att ett giltigt heltal har skrivits in.
                    while (skottXSpelare < 1 || skottXSpelare > 6)
                    {
                        Console.WriteLine("Du har angivit ett tal som är för högt eller lågt. Försök igen.");
                        skottXSpelare = LäsInt();

                    }
                    // Spelaren får ange y-värdet för sitt skott. Även här kontrollerar sedan programmet 
                    // att det inskrivna värdet är korrekt. Metoden LäsInt anropas även här som 
                    // säkerställer att ett giltigt heltal har skrivits in.
                    Console.WriteLine("Ange en y-koordinat mellan 1-4 för ditt skott.");
                    int skottYSpelare = LäsInt();

                    while (skottYSpelare < 1 || skottYSpelare > 4)
                    {
                        Console.WriteLine("Du har angivit ett tal som är för högt eller lågt. Försök igen.");
                        skottYSpelare = LäsInt();

                    }

                    // Spelarens skott (x- och y-värdet) placeras ut på datorns karta med vår bool-array.
                    spelarensSkott[skottXSpelare - 1, skottYSpelare - 1] = true;

                    // Datorns x- och y-värde för sitt skott slumpas fram med variabeln för slump (Random) och placeras
                    // ut på spelarens karta.
                    int skottXdator = slump.Next(6);
                    int skottYdator = slump.Next(4);

                    datornsSkott[skottXdator, skottYdator] = true;

                    // Programmet kontrollerar om spelarens skott har träffat något av datorns fartyg genom att anropa
                    // denna metod. Om spelaren har vunnit får denne skriva in sitt namn och spelet avslutas. Spelarens 
                    // namn sparas till variabeln för senaste vinnaren.
                    SpelarenVinner();
                    if (SpelarenVinner())
                    {
                        Console.Clear();
                        RitaKarta();
                        Console.WriteLine("DU HAR VUNNIT!");
                        Console.WriteLine("Skriv in ditt namn.");
                        string namn = Console.ReadLine();
                        senasteVinnaren = namn;
                        Console.WriteLine();
                        speletÄrSlut = true;

                    }
                    // Programmet kontrollerar om datorns skott har träffat något av spelarens fartyg genom att anropa
                    // denna metod. Om datorn har vunnit sparas datorn i variabeln för den senaste vinnaren och sedan
                    // avslutas spelet.
                    DatornVinner();
                    if (DatornVinner())
                    {
                        Console.Clear();
                        RitaKarta();
                        Console.WriteLine("DATORN HAR VUNNIT. BÄTTRE LYCKA NÄSTA GÅNG!");
                        senasteVinnaren = "Datorn";
                        Console.WriteLine();
                        speletÄrSlut = true;
                    }
                }

            }
            // Metod för att rita ut våra respektive kartor. Till en början syns spelarens hela karta där bokstaven O
            // innebär att inget skepp finns och där X innebär att där finns ett skepp. Datorns karta betsår här endast av streck.
            // När ett skott har träffat någon ruta blir den röd och då syns denna ruta även på datorns karta.
            void RitaKarta()
            {
                Console.WriteLine("SPELARENS KARTA");

                for (int y = 0; y < kartHöjd; y++)
                {
                    for (int x = 0; x < kartBredd; x++)
                    {
                        if (datornsSkott[x, y] == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                        }

                        Console.Write(spelarensKarta[x, y]);

                        Console.ForegroundColor = ConsoleColor.Gray;

                    }
                    Console.WriteLine();
                }
                Console.WriteLine();


                Console.WriteLine("DATORNS KARTA");

                for (int y = 0; y < kartHöjd; y++)
                {
                    for (int x = 0; x < kartBredd; x++)
                    {
                        if (spelarensSkott[x, y] == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(datornsKarta[x, y]);
                        }
                        else
                        {

                            Console.Write("-");
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.WriteLine();
                }


            }
            // Metod som kontrollerar om spelaren har vunnit och som sedan returnerar ett bool-värde för detta. Har skottet
            // träffat en ruta märkt med X har spelaren vunnit och värdet true returneras.
            bool SpelarenVinner()
            {
                for (int y = 0; y < kartHöjd; y++)
                {
                    for (int x = 0; x < kartBredd; x++)
                    {
                        if (datornsKarta[x, y] == "X" && spelarensSkott[x, y] == true)
                        {
                            return true; ;
                        }
                    }
                }

                return false; ;
            }

            // Metod som kontrollerar om datorn har vunnit och som sedan returnerar ett bool-värde för detta. Har skottet
            // träffat en ruta märkt med X har datorn vunnit och värdet true returneras.
            bool DatornVinner()
            {
                for (int y = 0; y < kartHöjd; y++)
                {
                    for (int x = 0; x < kartBredd; x++)
                    {
                        if (spelarensKarta[x, y] == "X" && datornsSkott[x, y] == true)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            // Metod som säkerställer att ett giltigt heltal har skrivits in för x och y. När detta stämmer returneras 
            // heltalet.
            int LäsInt()
            {
                int skottSpelare;

                while (int.TryParse(Console.ReadLine(), out skottSpelare) == false)
                {
                    Console.WriteLine("Du angav inget heltal. Försök igen.");
                }

                return skottSpelare;

            }
            break;

            // Case 2 i vår switch-sats visar den senaste vinnaren. 
        case "2":

            Console.WriteLine("Den senaste vinnaren är: " +senasteVinnaren);
            Console.WriteLine();

            break;

            // Case 3 avslutar spelet. 
        case "3":

            Console.WriteLine("Tack för att du har spelat Sänka fartyg!");
            break;

        default:

            Console.WriteLine("Du har inte valt ett korrekt alternativ.");
            Console.WriteLine();
            break;


    }
}