﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

using Account_Class;
using Eten_Class;
using MovieDetail_Class;
using Ticket_Class;
using Hoofdmenu;

namespace Team_2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // BJORN GEDEELTE //
            Account acc = new Account();
            bool retry = true;
            int user = -1;
            while (retry == true)
            {
                if (user == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Om in te loggen, enter 1.\nOm een account te creeeren, enter 2.\nOm terug te gaan, enter 'X'");
                    string choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        user = acc.TextLogin();
                    }
                    else if (choose == "2")
                    {
                        acc.TextCreateUser();
                    }
                    else
                    {
                        retry = false;
                    }
                }
                else if (user > 0 && acc.accountDataList[user].Permission == true)
                {
                    Console.Clear();
                    Console.WriteLine("Om eigen account te zien, enter 1.\nOm admin menu in te gaan, enter 2.\nOm uit te loggen, enter 3.\nOm terug te gaan, enter 'X'");
                    string choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        acc.AccountView(user);
                    }
                    else if (choose == "2")
                    {
                        acc.AdminAccountViewer();
                    }
                    else if (choose == "3")
                    {
                        user = -1;
                    }
                    else
                    {
                        retry = false;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Om eigen account te zien, enter 1.\nOm uit te loggen, enter 2.\nOm terug te gaan, enter 'X'");
                    string choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        acc.AccountView(user);
                    }
                    else if (choose == "2")
                    {
                        user = -1;
                    }
                    else
                    {
                        retry = false;
                    }
                }
            }

            // Creates user in JSON
            // acc.TextCreateUser();

            // Login with username and password
            //int print_this = acc.TextLogin();
            //Console.WriteLine(print_this);


            // MELISSA GEDEELTE //
            //Eten eten = new Eten();
            //eten.EtenMenu();

            // DAVID GEDEELTE //
            //MovieDetail.CodeActivate();

            // NOAH GEDEELTE //
            //reservering resv = new reservering();
            //resv.RunTickets();

            // JAMIE GEDEELTE //
            //Applicatie myApllicatie = new Applicatie();
            //myApllicatie.Start();
        }
    }
}
