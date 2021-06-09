
using System;

using Account_Class;
using Food_Drink_Run;
using MovieDetail_Class;
using Reservatie_Class;
using Kalender_Class;
using Console_Buffer;
using System.Diagnostics.CodeAnalysis;

namespace Team_2
{
    class Program
    {
        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            
            Account acc = new Account();
            Kalender kalender = new Kalender();

            bool retry = true;
            int user = -1;
            while (retry == true)
            {
                if (user == -1) // If there is no login (no user selected)
                {
                    Console_Reset.clear();
                    Console.WriteLine("Welkom bij Cinematrix\nHoofd menu:\n---------------------------------------------------\n");
                    Console.WriteLine("1. Inloggen.\n2. Account creeeren.\n3. Bekijk kalender\n4. Bekijk catalogus.\n5. Eten / Drinken menu.\nOm programma te verlaten, enter 'X'");
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
                        kalender.Navigation();
                    }
                    else if (choose == "4")
                    {
                        MovieDetail.Navigation();
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
                else if (user > -1 && acc.accountDataList[user].Permission == true) // If there is logged in (admin)
                {
                    Console_Reset.clear();

                    // Print the username with welcome text
                    string username = acc.ReturnUsername(user);
                    Console.WriteLine("Administrator: " + username + "\n");

                    Console.WriteLine("1. Account overzicht.\n2. Admin menu.\n3. Bekijk kalender\n4. Bekijk catalogus.\n5. Mijn reserveringen.\n6. Eten / Drinken menu.\n7. Uitloggen.\nOm programma te verlaten, enter 'X'");
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
                        kalender.Navigation();
                    }
                    else if (choose == "4")
                    {
                        MovieDetail.Navigation(user);
                    }
                    else if (choose == "5")
                    {
                        Reservering reservering = new Reservering();
                        reservering.ReservationUserPrint(user);
                    }
                    else if (choose == "6")
                    {
                        FoodDrinkRun.Run(true);
                    }
                    else if (choose == "7")
                    {
                        user = -1;
                    }
                    else if (choose == "x" || choose == "X")
                    {
                        retry = false;
                    }
                }
                else // If there is logged in (user)
                {
                    Console_Reset.clear();

                    // Print the username with welcome text
                    string username = acc.ReturnUsername(user);
                    Console.WriteLine("Welkom " + username + "\n");

                    Console.WriteLine("1. Account overzicht.\n2. Bekijk catalogus.\n3. Bekijk kalender\n4. Mijn reserveringen.\n5. Eten / Drinken menu.\n6. Uitloggen.\nOm programma te verlaten, enter 'X'");
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
                        MovieDetail.Navigation(user);
                    }
                    else if (choose == "3")
                    {
                        kalender.Navigation();
                    }
                    else if (choose == "4")
                    {
                        Reservering reservering = new Reservering();
                        reservering.ReservationUserPrint(user);
                    }
                    else if (choose == "5")
                    {
                        FoodDrinkRun.Run();
                    }
                    else if (choose == "6")
                    {
                        user = -1;
                    }
                    else if (choose == "x" || choose == "X")
                        retry = false;
                    }
                }
            }
        }
    }
