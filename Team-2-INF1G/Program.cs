
using System;

namespace Project
{
    public class Eten
    {
        // menu //
        public int etemMenu()
        {
            Console.WriteLine("Typ het nummer van wat je wilt bekijken en klink op enter\n1. Popcorn zoet - va 2,99\n2. Popcorn zout - va 2,49\n3. Popcorn karamel - va 2,49\n4. M&M's pinda - 3,99\n5. M&M's chocola - 4,49\n6. Chips naturel\n7. Chips paprika\n8. Doritos nacho cheese\n9. Haribo goudberen\n10. Skittles fruits");
            int num = Convert.ToInt32(Console.ReadLine());
            return num;
        }

        // food info //
        //public void etenInfo(){
        //Console.WriteLine(Foods.json[num-1][naam]);
        //Console.WriteLine(Foods.json[num-1][inhoud]);
        //Console.WriteLine(Foods.json[num-1][prijs]);
        //Console.WriteLine(Foods.json[num-1][voedingswaarde]);
        //Console.WriteLine(Foods.json[num-1][allergenen]);
        //Console.WriteLine(Foods.json[num-1][tags]);
        //Console.WriteLine("Toets x om terug te gaan");
        //if (Console.ReadLine() == 'x') call menu()
        //}
    }
}   
  }    

/*
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Team_2
{

    public class Account
    {
        public string User_Name { get; set; }
        public string Password { get; set; }

        [JsonIgnore] // Dit ignored de value voor als je naar JSON ombouwt
        public string path;
        [JsonIgnore]
        public string json_path;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Account FromJSON(string json_path) // FromJSON returned een Account format, dus de field
        {
            return JsonConvert.DeserializeObject<Account>(json_path); ;
        }

        public Account()
        {
            // Start van path vinden //
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Test\Accounts.json"));
            this.json_path = File.ReadAllText(this.path);
            // End van path vinden //
        }


        public void Create()
        {
            bool retry = true;

            // User input username and password, retry while statement if user wants to retry //
            while (retry == true)
            {
                Console.WriteLine("\nVoer een gebruikersnaam in: ");
                this.User_Name = Console.ReadLine();
                Console.WriteLine("\nVoer een wachtwoord in: ");
                this.Password = Console.ReadLine();
                Console.WriteLine($"\nGeselecteerde gebruikersnaam: {User_Name} | Geselecteerde wachtwoord: {Password} \nOm te confirmeren toets ENTER, anders toets 'X'");

                // Checked if user wants to retry or confirm username //
                string confirm = Console.ReadLine();
                if (confirm == "X" || confirm == "x" || confirm == "'X'" || confirm == "'x'")
                {
                    retry = true;
                }
                else
                {
                    retry = false;
                }
            }
            // Write naar JSON file
            System.IO.File.WriteAllText(this.path, ToJSON());
            Console.WriteLine($"Account gecreeerd | Gebruikersnaam: {User_Name} | Wachtwoord: {Password}");
        }
    }
    class Program
    {


        static void Main(string[] args)
        {

            new Account().Create();

        }
    }
} */
