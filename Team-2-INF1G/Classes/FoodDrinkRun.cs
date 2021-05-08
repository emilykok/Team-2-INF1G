using System;
using System.Collections.Generic;
using System.Text;
using Eten_Class;
using Drinken_Class;

namespace Food_Drink_Run
{
    class FoodDrinkRun
    {
        public void Run(bool permission)
        { // functie callen met een bool of er admin permissions zijn
            Eten eten = new Eten();
            Drinken drinken = new Drinken();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welkom bij het eten en drinken menu.\nTyp het nummer wat je wil doen en klik op enter:\n");
                Console.WriteLine("1. Eten menu\n2. Eten filteren\n3. Drinken menu\n4. Drinken filteren");
                if (permission == true)
                { //menu drinken eten voor admin
                    Console.WriteLine("5. Clicks bekijken\n6. Eten Clicks verwijderen\n7. Drinken Clicks verwijderen");
                    string nummer = Console.ReadLine();
                    // etenmenu
                    if (nummer == "1")
                    {
                        Console.Clear();
                        eten.EtenMenu();
                    }
                    // eten filer
                    if (nummer == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("Kies 1 voor filteren of 2 om allergenen uit te filteren\nTyp hier je nummer:");
                        string fNum = Console.ReadLine();
                        Console.WriteLine("Typ hier je zoekterm:");
                        string strInp = Console.ReadLine();
                        if (fNum == "1") eten.EtenFilter(strInp);
                        if (fNum == "2") eten.EtenAllergieFilter(strInp);
                    }
                    // drinken menu
                    if (nummer == "3")
                    {
                        Console.Clear();
                        drinken.DrinkenMenu();
                    }

                    // drinken filter
                    if (nummer == "4")
                    {
                        Console.Clear();
                        Console.WriteLine("Kies 1 voor filteren of 2 om allergenen uit te filteren:");
                        string fNum = Console.ReadLine();
                        Console.WriteLine("Typ hier je zoekterm:");
                        string strInp = Console.ReadLine();
                        if (fNum == "1") drinken.DrinkenFilter(strInp);
                        if (fNum == "2") drinken.DrinkenAllergieFilter(strInp);
                    }
                    //Clicks bekijken
                    if (nummer == "5")
                    {
                        Console.Clear();
                        Console.WriteLine("Van welke item wil je de clicks bekijken?\n1. Popcorn zoet\n2. Popcorn zout\n3. Popcorn karamel\n4. M&M's pinda\n5. M&M's chocola\n6. Chips naturel\n7. Chips paprika\n8. Doritos nacho cheese\n9. Haribo goudberen\n10. Skittles fruits\n11. Cola\n12. Pepsi\n13. Dr.Pepper\n14. Fanta Orange\n15. Spa rood\n16. Spa blauw\n17. Appelsap\n18. Rode wijn\n19. Witte wijn\n20. Heineken\n\nTyp het nummer van het product:");
                        string inp = Console.ReadLine();
                        try
                        {
                            if (Convert.ToInt32(inp) < 11) eten.ViewClicks(Convert.ToInt32(inp));
                            else drinken.ViewClicks(Convert.ToInt32(inp) - 10);
                        }
                        catch { Console.WriteLine("Je input is niet correct, weet je zeker dat je een nummer uit de lijst hebt getoetst?"); }
                    }

                    // clicks deleten eten
                    if (nummer == "6")
                    {
                        Console.Clear();
                        Console.WriteLine("Typ 1 voor op een index verwijderen en 2 voor alles te verwijderen");
                        string num = Console.ReadLine();
                        // deleten op index
                        if (num == "1")
                        {
                            Console.WriteLine("voer hier je index in");
                            int n = Convert.ToInt32(Console.ReadLine());
                            eten.ClearClicks(n);
                            Console.WriteLine($"Alle clicks op index {n} zijn verwijderd.");
                        }
                        // alles deleten
                        if (num == "2")
                        {
                            eten.ClearAllClicks();
                            Console.WriteLine("Alle clicks van het eten zijn verwijderd.");
                        }
                    }

                    // clicksdeleten drinken
                    if (nummer == "7")
                    {
                        Console.Clear();
                        Console.WriteLine("Typ 1 voor op een index verwijderen en 2 voor alles te verwijderen");
                        string numm = Console.ReadLine();
                        // deleten op index
                        if (numm == "1")
                        {
                            Console.WriteLine("voer hier je index in");
                            int n = Convert.ToInt32(Console.ReadLine());
                            drinken.ClearClicks(n);
                            Console.WriteLine($"Alle clicks op index {n} zijn verwijderd.");
                        }
                        // alles deleten
                        if (numm == "2")
                        {
                            drinken.ClearAllClicks();
                            Console.WriteLine("Alle clicks van het drinken zijn verwijderd.");
                        }
                    }
                }

                else
                {
                    string nummer = Console.ReadLine();
                    if (nummer == "1")
                    {
                        Console.Clear();
                        eten.EtenMenu();
                    }
                    // eten filer
                    if (nummer == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("Kies 1 voor filteren of 2 om allergenen uit te filteren\nTyp hier je nummer:");
                        string fNum = Console.ReadLine();
                        Console.WriteLine("Typ hier je zoekterm:");
                        string strInp = Console.ReadLine();
                        if (fNum == "1") eten.EtenFilter(strInp);
                        if (fNum == "2") eten.EtenAllergieFilter(strInp);
                    }
                    // drinken menu
                    if (nummer == "3")
                    {
                        Console.Clear();
                        drinken.DrinkenMenu();
                    }

                    // drinken filter
                    if (nummer == "4")
                    {
                        Console.Clear();
                        Console.WriteLine("Kies 1 voor filteren of 2 om allergenen uit te filteren:");
                        string fNum = Console.ReadLine();
                        Console.WriteLine("Typ hier je zoekterm:");
                        string strInp = Console.ReadLine();
                        if (fNum == "1") drinken.DrinkenFilter(strInp);
                        if (fNum == "2") drinken.DrinkenAllergieFilter(strInp); ;
                    }
                }
            }
        }
    }
}


