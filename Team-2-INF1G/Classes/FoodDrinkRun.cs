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
                Console.WriteLine("Welkom bij het eten en drinken menu.\nTyp het nummer wat je wil doen en klik op enter:\n");
                Console.WriteLine("1. Eten menu\n2. Eten filteren\n3. Drinken menu\n4. Drinken filteren");
                if (permission == true)
                { //permissions admin
                    Console.WriteLine("5. Eten Clicks bekijken\n6. Eten Clicks verwijderen\n7. Drinken Clicks bekijken\n8. Drinken Clicks verwijderen");
                    string nummer = Console.ReadLine();
                    // etenmenu
                    if (nummer == "1") eten.EtenMenu();
                    // eten filer
                    if (nummer == "2")
                    {
                        Console.WriteLine("Kies 1 voor filteren of 2 om allergenen uit te filteren:");
                        string fNum = Console.ReadLine();
                        Console.WriteLine("Typ hier je zoekterm:");
                        string strInp = Console.ReadLine();
                        if (fNum == "1") eten.EtenFilter(strInp);
                        if (fNum == "2") eten.EtenAllergieFilter(strInp);
                    }
                    // drinken menu
                    if (nummer == "3") drinken.DrinkenMenu();
                    // drinken filter
                    if (nummer == "4")
                    {
                        Console.WriteLine("Kies 1 voor filteren of 2 om allergenen uit te filteren:");
                        string fNum = Console.ReadLine();
                        Console.WriteLine("Typ hier je zoekterm:");
                        string strInp = Console.ReadLine();
                        if (fNum == "1") drinken.DrinkenFilter(strInp);
                        if (fNum == "2") drinken.DrinkenAllergieFilter(strInp);
                    }
                    /* Clicks bekijken
                    if (nummer == "5") {
                        Console.WriteLine();
                    }*/

                    // clicks deleten
                    if (nummer == "6")
                    {
                        Console.WriteLine("Typ 1 voor op een index verwijderen en 2 voor alles te verwijderen");
                        string num = Console.ReadLine();
                        if (num == "1")
                        {
                            Console.WriteLine("voer hier je index in");
                            int n = Convert.ToInt32(Console.ReadLine());
                            eten.ClearClicks(n);
                            if (num == "2") eten.ClearAllClicks();

                        }
                        /* Clicks bekijken
                        if (nummer == "5") {
                            Console.WriteLine();
                        }*/
                        if (nummer == "8")
                        {
                            Console.WriteLine("Typ 1 voor op een index verwijderen en 2 voor alles te verwijderen");
                            string numm = Console.ReadLine();
                            if (numm == "1")
                            {
                                Console.WriteLine("voer hier je index in");
                                int n = Convert.ToInt32(Console.ReadLine());
                                drinken.ClearClicks(n);
                                if (numm == "2") drinken.ClearAllClicks();
                            }
                        }
                        else
                        {
                            string n = Console.ReadLine();
                            // etenmenu
                            if (n == "1") eten.EtenMenu();
                            // eten filer
                            if (n == "3")
                            {
                                Console.WriteLine("Kies 1 voor filteren of 2 om allergenen uit te filteren:");
                                string fNum = Console.ReadLine();
                                Console.WriteLine("Typ hier je zoekterm:");
                                string strInp = Console.ReadLine();
                                if (fNum == "1") eten.EtenFilter(strInp);
                                if (fNum == "2") eten.EtenAllergieFilter(strInp);
                            }
                            // drinken menu
                            if (n == "2") drinken.DrinkenMenu();
                            // drinken filter
                            if (n == "3")
                            {
                                Console.WriteLine("Kies 1 voor filteren of 2 om allergenen uit te filteren:");
                                string fNum = Console.ReadLine();
                                Console.WriteLine("Typ hier je zoekterm:");
                                string strInp = Console.ReadLine();
                                if (fNum == "1") drinken.DrinkenFilter(strInp);
                                if (fNum == "2") drinken.DrinkenAllergieFilter(strInp);
                            }
                        }
                    }
                }
            }
        }
    }
}

