
using System;


using Account_Class;
using Food_Drink_Run;
using MovieDetail_Class;
using Hoofdmenu;
using Reservatie_Class;

namespace Team_2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Account acc = new Account();
            //Reservering reservering = new Reservering();
            bool retry = true;
            int user = -1;
            while (retry == true)
            {
                if (user == -1) // If there is no login (no user selected)
                {
                    Console.Clear();
                    Console.WriteLine("Welkom bij Cinematrix\nHoofd menu:\n---------------------------------------------------\n");
                    Console.WriteLine("1. Inloggen.\n2. Account creeeren.\n3. Bekijk catalogus.\n4. Eten / Drinken menu.\nOm programma te verlaten, enter 'X'");
                    string choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        user = acc.TextLogin();
                    }
                    else if (choose == "2")
                    {
                        acc.TextCreateUser();
                    }
                    else if (choose == "3")
                    {
                        MovieDetail.Navigation();
                    }
                    else if (choose == "4")
                    {
                        FoodDrinkRun.Run();
                    }
                    else
                    {
                        retry = false;
                    }
                }
                else if (user > 0 && acc.accountDataList[user].Permission == true) // If there is logged in (admin)
                {
                    Console.Clear();

                    // Print the username with welcome text
                    string username = acc.ReturnUsername(user);
                    Console.WriteLine("Administrator: " + username + "\n");

                    Console.WriteLine("1. Account overzicht.\n2. Admin menu.\n3. Uitloggen.\n4. Bekijk catalogus.\n5. Reserveren.\n6. Eten / Drinken menu.\nOm programma te verlaten, enter 'X'");
                    string choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        bool returnBool = acc.AccountView(user);
                        if (returnBool == true)
                        {
                            user = -1;
                        }
                    }
                    else if (choose == "2")
                    {
                        user = acc.AdminAccountViewer(acc.ReturnUsername(user));
                    }
                    else if (choose == "3")
                    {
                        user = -1;
                    }
                    else if (choose == "4")
                    {
                        MovieDetail.Navigation();
                    }
                    else if (choose == "5")
                    {
                        //reservering.RunTickets();
                    }
                    else if (choose == "6")
                    {
                        FoodDrinkRun.Run(true);
                    }
                    else
                    {
                        retry = false;
                    }
                }
                else // If there is logged in (user)
                {
                    Console.Clear();

                    // Print the username with welcome text
                    string username = acc.ReturnUsername(user);
                    Console.WriteLine("Welkom " + username + "\n");

                    Console.WriteLine("1. Account overzicht.\n2. Uitloggen.\n3. Bekijk catalogus.\n4. Reserveren.\n5. Eten / Drinken menu.\nOm programma te verlaten, enter 'X'");
                    string choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        bool returnBool = acc.AccountView(user);
                        if (returnBool == true)
                        {
                            user = -1;
                        }
                    }
                    else if (choose == "2")
                    {
                        user = -1;
                    }
                    else if (choose == "3")
                    {
                        MovieDetail.Navigation();
                    }
                    else if (choose == "4")
                    {
                        //reservering.RunTickets();
                    }
                    else if (choose == "5")
                    {
                        FoodDrinkRun.Run();
                    }
                    else
                    {
                        retry = false;
                    }
                }
            }
        }
    }
}
