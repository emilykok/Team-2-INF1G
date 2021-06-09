using System;

using Eten_Class;
using Drinken_Class;
using Console_Buffer;
using System.Diagnostics.CodeAnalysis;

namespace Food_Drink_Run
{
    [ExcludeFromCodeCoverage]
    class FoodDrinkRun
    {
        public static void Run(bool permission = false)
        { // functie callen met een bool of er admin permissions zijn
            Eten eten = new Eten();
            Drinken drinken = new Drinken();
            bool whileLoop = true;
            

            while (whileLoop == true)
            {
                Console_Reset.clear();
                Console.WriteLine("Welkom bij het eten en drinken menu.\n---------------------------------------------------\n\nTyp het nummer wat je wil doen en klik op enter:\n");
                Console.WriteLine("1. Eten menu\n2. Eten filteren\n3. Drinken menu\n4. Drinken filteren\n");
                if (permission == true)
                { //menu drinken eten voor admin
                    Console.WriteLine("5. Clicks bekijken\n6. Eten Clicks verwijderen\n7. Drinken Clicks verwijderen\n\n8. Terug naar vorige pagina");
                    string nummer = Console.ReadLine();
                    // etenmenu
                    if (nummer == "1")
                    {
                        Console_Reset.clear();
                        eten.EtenMenu();
                    }
                    // eten filter
                    if (nummer == "2")
                    {
                        Console_Reset.clear();
                        Console.WriteLine("Eten filteren:\n---------------------------------------------------\nTyp het nummer van wat je wilt doen:\n1. Op zoekterm filteren\n2. Allergie uitfilteren\n3. Terug naar vorige pagina");
                        string fNum = Console.ReadLine();

                        if (fNum == "1")
                        {
                            Console.WriteLine("Typ hier je zoekterm:");
                            string strInp = Console.ReadLine();
                            Console.WriteLine(eten.EtenFilter(strInp));
                            Console.WriteLine("Toets enter om terug te gaan");
                            string inp = Console.ReadLine();
                            if (inp == "") FoodDrinkRun.Run();
                        }

                        else if (fNum == "2")
                        {
                            Console.WriteLine("U kunt filteren uit [lactose, soja, pinda, amandel, hazelnoot, noten, gluten, tarwe]");
                            Console.WriteLine("Typ hier je zoekterm:");
                            string strInp = Console.ReadLine();
                            Console.WriteLine(eten.EtenAllergieFilter(strInp));
                            Console.WriteLine("Toets enter om terug te gaan");
                            string inp = Console.ReadLine();
                            if (inp == "") FoodDrinkRun.Run();
                        }

                        else if (fNum == "3") FoodDrinkRun.Run();
                    }
                    // drinken menu
                    if (nummer == "3")
                    {
                        Console_Reset.clear();
                        drinken.DrinkenMenu();
                    }

                    // drinken filter
                    if (nummer == "4")
                    {
                        Console_Reset.clear();
                        Console.WriteLine("Drinken filteren:\n---------------------------------------------------\nTyp het nummer van wat je wilt doen:\n1.Op zoekterm filteren\n2.Allergie uitfilteren\n3.Terug naar vorige pagina");
                        string fNum = Console.ReadLine();
                        if (fNum == "1")
                        {
                            Console.WriteLine("Typ hier je zoekterm:");
                            string strInp = Console.ReadLine();
                            Console.WriteLine(drinken.DrinkenFilter(strInp));
                            Console.WriteLine("Toets enter om terug te gaan");
                            string inp = Console.ReadLine();
                            if (inp == "") FoodDrinkRun.Run();
                        }
                        else if (fNum == "2")
                        {
                            Console.WriteLine("U kunt filteren uit [lactose, soja, pinda, amandel, hazelnoot, noten, gluten, tarwe]");
                            Console.WriteLine("Typ hier je zoekterm:");
                            string strInp = Console.ReadLine();
                            Console.WriteLine(drinken.DrinkenAllergieFilter(strInp));
                            Console.WriteLine("Toets enter om terug te gaan");
                            string inp = Console.ReadLine();
                            if (inp == "") FoodDrinkRun.Run();
                        }
                        else if (fNum == "3") FoodDrinkRun.Run();
                    }

                    //Clicks bekijken
                    if (nummer == "5")
                    {
                        Console_Reset.clear();
                        Console.WriteLine("Van welke item wil je de clicks bekijken?\n---------------------------------------------------\n1. Popcorn zoet\n2. Popcorn zout\n3. Popcorn karamel\n4. M&M's pinda\n5. M&M's chocola\n6. Chips naturel\n7. Chips paprika\n8. Doritos nacho cheese\n9. Haribo goudberen\n10. Skittles fruits\n\n11. Cola\n12. Pepsi\n13. Dr.Pepper\n14. Fanta Orange\n15. Spa rood\n16. Spa blauw\n17. Appelsap\n18. Rode wijn\n19. Witte wijn\n20. Heineken\n\nTyp het nummer van het product:");
                        string inp = Console.ReadLine();
                        try
                        {
                            if (Convert.ToInt32(inp) < 11) {
                                eten.ViewClicks(Convert.ToInt32(inp));
                                Console.WriteLine("Toets enter om terug te gaan");
                                string inn = Console.ReadLine();
                                if (inn == "") FoodDrinkRun.Run();
                            } 
                            else drinken.ViewClicks(Convert.ToInt32(inp) - 10);
                        }
                        catch { Console.WriteLine("Je input is niet correct, weet je zeker dat je een nummer uit de lijst hebt getoetst?"); }
                    }

                    // clicks deleten eten
                    if (nummer == "6")
                    {
                        Console_Reset.clear();
                        Console.WriteLine("Clicks verwijderen:\n---------------------------------------------------\nTyp het nummer van wat je wilt doen:\n1. Op index verwijderen\n2. Alle clicks van eten verwijderen\n3. Terug naar vorige pagina\n");
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
                        if (num == "3") FoodDrinkRun.Run();
                    }

                    // clicksdeleten drinken
                    if (nummer == "7")
                    {
                        Console_Reset.clear();
                        Console.WriteLine("Clicks verwijderen:\n---------------------------------------------------\nTyp het nummer van wat je wilt doen:\n1. Op index verwijderen\n2. Alle clicks van eten verwijderen\n3. Terug naar vorige pagina\n");
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
                        if (numm == "3") FoodDrinkRun.Run();
                    }
                    if (nummer == "8")
                    {
                        whileLoop = false;
                    }
                }

                // non admin opties
                else
                {
                    Console.WriteLine("\n5. Terug naar vorige pagina");
                    string nummer = Console.ReadLine();
                    // eten menu
                    if (nummer == "1")
                    {
                        Console_Reset.clear();
                        eten.EtenMenu();
                    }
                    // eten filter
                    if (nummer == "2")
                    {
                        Console_Reset.clear();
                        Console.WriteLine("Eten filteren:\n---------------------------------------------------\nTyp het nummer van wat je wilt doen:\n1. Op zoekterm filteren\n2. Allergie uitfilteren\n3. Terug naar vorige pagina");
                        string fNum = Console.ReadLine();

                        if (fNum == "1") {
                            Console.WriteLine("Typ hier je zoekterm:");
                            string strInp = Console.ReadLine();
                            Console.WriteLine(eten.EtenFilter(strInp));
                            Console.WriteLine("Toets enter om terug te gaan");
                            string inp = Console.ReadLine();
                            if (inp == "") FoodDrinkRun.Run();
                        }
                        else if (fNum == "2") {
                            Console.WriteLine("U kunt filteren uit [lactose, soja, pinda, amandel, hazelnoot, noten, gluten, tarwe]");
                            Console.WriteLine("Typ hier je zoekterm:");
                            string strInp = Console.ReadLine();
                            Console.WriteLine(eten.EtenAllergieFilter(strInp));
                            Console.WriteLine("Toets enter om terug te gaan");
                            string inp = Console.ReadLine();
                            if (inp == "") FoodDrinkRun.Run();
                        } 
                        else if (fNum == "3") FoodDrinkRun.Run(); 
                    }

                    // drinken menu
                    if (nummer == "3")
                    {
                        Console_Reset.clear();
                        drinken.DrinkenMenu();
                    }

                    // drinken filter
                    if (nummer == "4")
                    {
                        Console_Reset.clear();
                        Console.WriteLine("Drinken filteren:\n---------------------------------------------------\nTyp het nummer van wat je wilt doen:\n1.Op zoekterm filteren\n2.Allergie uitfilteren\n3.Terug naar vorige pagina");
                        string fNum = Console.ReadLine();

                        if (fNum == "1")
                        {
                            Console.WriteLine("Typ hier je zoekterm:");
                            string strInp = Console.ReadLine();
                            Console.WriteLine(drinken.DrinkenFilter(strInp));
                            Console.WriteLine("Toets enter om terug te gaan");
                            string inp = Console.ReadLine();
                            if (inp == "") FoodDrinkRun.Run();
                        }
                        else if (fNum == "2")
                        {
                            Console.WriteLine("U kunt filteren uit [lactose, soja, pinda, amandel, hazelnoot, noten, gluten, tarwe]");
                            Console.WriteLine("Typ hier je zoekterm:");
                            string strInp = Console.ReadLine();
                            Console.WriteLine(drinken.DrinkenAllergieFilter(strInp));
                            Console.WriteLine("Toets enter om terug te gaan");
                            string inp = Console.ReadLine();
                            if (inp == "") FoodDrinkRun.Run();
                        }
                        else if (fNum == "3") FoodDrinkRun.Run();
                    }
                    if (nummer == "5") break; 
                }
            }
        }
    }
}


