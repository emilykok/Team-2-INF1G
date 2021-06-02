using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Formatting = Newtonsoft.Json.Formatting;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using Hashing_Class;
using System.Linq;


using Console_Buffer;
using Reservatie_Class;
using System.Diagnostics.CodeAnalysis;

namespace Account_Class
{
    public class Account
    {
        //// struct is a data type used for the serialization and deserialization of the JSON file.
        // It will be put in a list, that can be index with []
        // To call the value's in it, use the name and then the value name (accountDataList[0].Age)
        // It is the same as a Tuple, but it works like a class as reference (acc.accountDataList[0].Age)
        // You can also use a foreach to loop through all the items in the list.
            //foreach (var accountData in accountDataList)
            //{
            //Console.WriteLine(accountData.Password);
            //}

        public struct AccountData
        {
            public string Name;
            public string Password;
            
            public int Age;
            public string Gender;
            public string Email;
            public string bankingDetails;
            public string[] Allergies;
            public bool Permission;
        }

        //// Field
        // Creates a list, a data type that can be added upon, with the "AccountData" type
        public List<AccountData> accountDataList = new List<AccountData>();

        [JsonIgnore]
        public string jsonPath;
        [JsonIgnore]
        public string path; 

        // Constructor
        public Account()
        {
            // this.path is used in serializing the json data.
            this.path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Accounts.json"));
            // this.jsonPath is used in deserializing the json data.
            this.jsonPath = File.ReadAllText(path);
            // the constructor loads the json file first, so it can be modified later in the file.
            this.accountDataList = JsonConvert.DeserializeObject<List<AccountData>>(jsonPath);

        }

        // Method that is used to write data to the JSON file.
        [ExcludeFromCodeCoverage]
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.accountDataList, Formatting.Indented);
        }

        //// Miscellaneous methods
        // Method that checks if someone has entered anything
        public bool TextCheck(string[] textArr)
        {
            for (int i = 0; i < textArr.Length; i++)
            {
                if (textArr[i] == "")
                {
                    return true;
                }
            }
            return false;
        }

        // Method to check if username is already taken, returns bool (true if taken, false if not taken)
        public bool UsernameCheck(string username)
        {
            foreach (var accountData in accountDataList)
            {
                if(username == accountData.Name)
                {
                    return true;
                }
            }
            return false;
        }

        // Method that prints items with start and stop input (hard coded for Accounts.json!)
        public void PrintItem(int start, int stop)
        {
            for (int i = start; i < stop; i++)
            {
                try
                {
                    Console.WriteLine($"[{i + 1}] {accountDataList[i].Name}");
                }
                catch
                {
                    break;
                }
            }
        }

        // Method that goes through all the available allergies, and returns string array with the ones user has not chosen yet
        public string[] AvailableAllergie(string[] userAllergies)
        {

            string[] allAllergies = {"lactose", "soja", "pinda", "amandel", "hazelnoot", "noten", "gluten", "tarwe"};

            // To get the length of the return array
            if (userAllergies.Length == 0)
            {
                return allAllergies;
            }
            else
            {
                string[] difference = allAllergies.Except(userAllergies).ToArray();
                return difference;
            }
        }

        // Method that returns the username of specific indexed user
        public string ReturnUsername(int index)
        {
            return accountDataList[index].Name;
        }



        // Method to login, returns int (the index of the list which corresponds to user selected) if user is found, else returns -1
        public int Login(string name, string password)
        {
            for (int i = 0; i < accountDataList.Count; i++)
            {
                // compares hashes
                using (SHA256 sha256Hash = SHA256.Create())
                    if (name == accountDataList[i].Name && Hashing.VerifyHash(sha256Hash, password, accountDataList[i].Password))
                {
                    return i;
                }
            }
            return -1;
        }

        // Method that returns user account index, based on input => calls login method.
        public int TextLogin()
        {
            // retry bool for if the user wants to try again
            bool retry = true;
            bool failed = false;

            while (retry == true)
            {
                // Clears the console for typing;
                Console_Reset.clear();

                // Header
                Console.WriteLine("*Login Pagina*");

                // Checks if there was a previous fail
                if (failed == true)
                {
                    Console.WriteLine("!!!Gebruikersnaam of wachtwoord was incorrect, probeer het opnieuw!!!\n");
                }

                // Ask for user input
                Console.WriteLine("Voer een gebruikersnaam in: ");
                string User_Name = Console.ReadLine();
                Console.WriteLine("\nVoer een wachtwoord in: ");
                string Password = Console.ReadLine();

                // Check if there was input
                string[] callValue = new string[]{ User_Name, Password };
                bool inputCheck = TextCheck(callValue);

                if (inputCheck == true)
                {
                    Console.WriteLine("\nBeide velden moeten een input hebben.\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");
                }
                else
                {
                    Console.WriteLine($"\nGeselecteerde gebruikersnaam: {User_Name} | Geselecteerde wachtwoord: {Password} \nOm in te loggen toets ENTER\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");
                }

                // Checked if user wants to retry or confirm username //
                string confirm = Console.ReadLine();
                if (confirm == "R" || confirm == "r" || confirm == "'R'" || confirm == "'r'" && inputCheck == false)
                {
                    retry = true;
                }

                else if (confirm == "X" || confirm == "x" || confirm == "'X'" || confirm == "'x'")
                {
                    retry = false;
                }
                else
                {
                    retry = false;
                    int login_Result = this.Login(User_Name, Password);
                    if (login_Result == -1)
                    {
                        failed = true;
                        retry = true;
                    }
                    else
                    {
                        return login_Result;
                    }
                }
            }
            return -1;
        }

        // Method that can be called to create a user.
        public void CreateUser(string name, string password)
        {
            // Hash the password
            using (SHA256 sha256Hash = SHA256.Create())
            {
                password = Hashing.GetHash(sha256Hash, password);
            }

            AccountData newAccountData = new AccountData();
            newAccountData.Name        = name;
            newAccountData.Password    = password;
            
            // placeHolders
            newAccountData.Age              = -1;
            newAccountData.Gender           = null;
            newAccountData.Email            = null;
            newAccountData.bankingDetails   = null;
            newAccountData.Allergies        = new string[] { };
            newAccountData.Permission       = false;

            // add to the list with the added data
            accountDataList.Add(newAccountData);

            // write to the JSON file (updates the file)
            System.IO.File.WriteAllText(this.path, ToJSON());
        }
        
        // Method that creates user, based on input => calls CreateUser method.
        public void TextCreateUser()
        {
            // retry bool for if the user wants to try again
            bool retry = true;
            bool failed = false;

            while (retry == true){
                // Clears the console for typing;
                Console_Reset.clear();

                // Ask for user input
                Console.WriteLine("*Registreren*");

                if (failed == true) // checks if there was a previous failed account creation
                {
                    Console.WriteLine("!!! Gekozen gebruikersnaam bestaat al, probeer opnieuw !!!");
                }

                Console.WriteLine("\nVoer een gebruikersnaam in: ");
                string User_Name = Console.ReadLine();

                Console.WriteLine("\nVoer een wachtwoord in: ");
                string Password = Console.ReadLine();

                // Check if there was input
                string[] callValue = new string[] { User_Name, Password };
                bool inputCheck = TextCheck(callValue);

                if (inputCheck == true)
                {
                    Console.WriteLine("\nBeide velden moeten een input hebben.\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");
                }
                else
                {
                    Console.WriteLine($"\nGeselecteerde gebruikersnaam: {User_Name} | Geselecteerde wachtwoord: {Password} \nOm account te creeeren toets ENTER\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");
                }
             
                // Checked if user wants to retry or confirm username //
                string confirm = Console.ReadLine();
                if (confirm == "R" || confirm == "r" || confirm == "'R'" || confirm == "'r'")
                {
                    retry = true;
                }
                else if (confirm == "X" || confirm == "x" || confirm == "'X'" || confirm == "'x'")
                {
                    retry = false;
                }
                else
                {
                    if (inputCheck == true)
                    {
                        retry = true;
                    }
                    else
                    {
                        if (UsernameCheck(User_Name) == true)
                        {
                            retry = true;
                            failed = true;
                        }
                        else
                        {
                            retry = false;
                            this.CreateUser(User_Name, Password);
                        }
                    } 
                }
            }
        }



        // Method that deletes entry at certain index
        public void DeleteUser(int index)
        {
            // Makes a list of all the reservations
            Reservering deleteReservations = new Reservering();
            List<int> deleteList = deleteReservations.ReservationList(index);

            // Goes through the list, deleting every entry
            foreach (int itemIndex in deleteList)
            {
                deleteReservations.DeleteTicket(itemIndex);
            }
            
            // Removes account in the JSON file
            accountDataList.RemoveAt(index);

            System.IO.File.WriteAllText(this.path, ToJSON());
        }

        // Method that updates selective user, requires to be logged in before or index!
        // Also requires string (name of the variable being changed) and the value in string
        public bool UpdateUser(string item, string value, int index)
        {
            // unloading the struct item at given index
            string name = accountDataList[index].Name;
            string password = accountDataList[index].Password;
            int age = accountDataList[index].Age;
            string gender = accountDataList[index].Gender;
            string email = accountDataList[index].Email;
            string bankingdetails = accountDataList[index].bankingDetails;
            string[] allergies = accountDataList[index].Allergies;
            bool permission = accountDataList[index].Permission;

            // Checks what needs to be changed, and assigns the value
            bool returnValue = true;

            if (item == "name")
            {
                try
                {
                    bool check = UsernameCheck(value);
                    if (check == false)
                    {
                        name = value;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
                catch
                {
                    returnValue = false;
                }
            }

            else if (item == "password")
            {
                try
                {
                    // Hash the password
                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        password = Hashing.GetHash(sha256Hash, value);
                    }
                }
                catch
                {
                    returnValue = false;
                }
            }
            else if (item == "age")
            {
                try
                {
                    int temp = Convert.ToInt32(value);
                    if (temp > 0)
                    {
                        age = temp;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
                catch
                {
                    returnValue = false;
                }
            }
            else if (item == "gender")
            {
                try
                {
                    if (value == "Male" || value == "male" || value == "Man" || value == "man")
                    {
                        gender = "man";
                    }
                    else if (value == "Female" || value == "female" || value == "Vrouw" || value == "vrouw")
                    {
                        gender = "vrouw";
                    }
                    else
                    {
                        gender = "other";
                    }
                }
                catch
                {
                    returnValue = false;
                }
            }
            else if (item == "email")
            {
                try
                {
                    email = value;
                }
                catch
                {
                    returnValue = false;
                }
            }
            else if (item == "bankingdetails")
            {
                try
                {
                    bankingdetails = value;
                }
                catch
                {
                    returnValue = false;
                }
            }
            else if (item == "allergiesAdd")
            {
                try
                {
                    bool check = false;
                    for (int i = 0; i < allergies.Length; i++)
                    {
                        if (value == allergies[i])
                        {
                            check = true;
                            break;
                        }
                    }
                    if (check == false)
                    {
                        string[] temp = new string[allergies.Length + 1];
                        for (int i = 0; i < allergies.Length; i++)
                        {
                            temp[i] = allergies[i];
                        }
                        temp[allergies.Length] = value;
                        allergies = temp;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
                catch
                {
                    returnValue = false;
                }
            }
            else if (item == "allergiesRemove")
            {
                try
                {
                    string[] temp = new string[allergies.Length - 1];
                    int count = 0;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (value == allergies[count])
                        {
                            count += 1;
                        }
                        temp[i] = allergies[count];
                        count += 1;
                    }
                    allergies = temp;
                }
                catch
                {
                    returnValue = false;
                }
            }
            else if (item == "permission")
            {
                try
                {
                    if (value == "true" || value == "True")
                    {
                        permission = true;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
                catch
                {
                    returnValue = false;
                }
            }

            // Creating the struct item
            AccountData newAccountData = new AccountData();

            newAccountData.Name = name;
            newAccountData.Password = password;
            newAccountData.Age = age;
            newAccountData.Gender = gender;
            newAccountData.Email = email;
            newAccountData.bankingDetails = bankingdetails;
            newAccountData.Allergies = allergies;
            newAccountData.Permission = permission;

            // deletes entry (as struct is immutable)
            DeleteUser(index);

            // add to the list with the added data [indexed]!
            accountDataList.Insert(index, newAccountData);

            // write to the JSON file (updates the file)
            System.IO.File.WriteAllText(this.path, ToJSON());

            if (item == "accountRemove" && value == "VERWIJDER")
            {
                DeleteUser(index);
            }
            else if (item == "accountRemove" && value != "VERWIJDER")
            {
                returnValue = false;
            }

            return returnValue;
        }

        // Prints all the users based on user input given in method, returns integer with current account location (because it changes)
        public int AdminAccountViewer(string activeAdmin)
        {
            string state = " ";
            int start = 0;
            int stop = 0;
            bool loop = true;

            while (loop == true)
            {
                bool executeRun = true;
                
                // runs code when page is at 0, no increase in value
                if (start == 0 && (state != ">" && state != "<"))
                {
                    if (start + 10 > accountDataList.Count)
                    {
                        if (start == accountDataList.Count)
                        {
                            executeRun = false;
                        }
                        stop = accountDataList.Count;
                    }
                    else
                    {
                        stop = start + 10;
                    }
                }

                // runs code with 10 increment, stores value
                else if (state == ">")
                {
                    if ((start + 10) >= accountDataList.Count)
                    {
                        executeRun = false;
                    }
                    else
                    {
                        start += 10;
                        stop = start + 10;
                    }
                }

                // Runs code with 10 decrement, stores value
                else if (state == "<")
                {
                    if ((start - 10) < 0)
                    {
                        executeRun = false;
                    }
                    else
                    {
                        start -= 10;
                        stop = start + 10;
                    }
                }

                // Runs the code
                if (executeRun == true)
                {
                    // Header
                    Console_Reset.clear();
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("Vul < of > in om te navigeren tussen de bladzijden. \nVoer het corresponderende nummer in om de naar de gebruiker te gaan\nOm te stoppen toets X.\n");

                    // Print the users
                    PrintItem(start, stop);

                    // current page indicator
                    int pageCounterCurrent = (start / 10) + 1;
                    int pageCounterAll = (accountDataList.Count / 10) + 1;
                    Console.WriteLine($"\nPage {pageCounterCurrent}/{pageCounterAll}");
                }

                // Process user input
                string userInput = Console.ReadLine();
                try
                {
                    int convert = Convert.ToInt32(userInput);
                    convert -= 1;
                    AccountView(convert, true);
                    state = " ";
                }
                catch
                {
                    if (userInput == ">" || userInput == "<")
                    {
                        state = userInput;
                    }
                    else if (userInput == "x" || userInput == "X")
                    {
                        loop = false;
                        break;
                    }
                    else
                    {
                        state = " ";
                    }
                }
            }

            for (int i = 0; i < accountDataList.Count; i++)
            {
                if (accountDataList[i].Name == activeAdmin)
                {
                    return i;
                }
            }
            return -1;
        }

        // Method that display's account of specific person, returns bool if account still exists
        public bool AccountView(int index, bool perm = false)
        {

            bool retry = true;
            bool returnValue = true;

            while (retry == true)
            {
                Console_Reset.clear();

                if(returnValue == false)
                {
                    Console.WriteLine("Gegeven waarde kan niet aangepast worden of toegevoegd, probeer het opnieuw\n");
                }

                string name = accountDataList[index].Name;
                string password = accountDataList[index].Password;
                int age = accountDataList[index].Age;
                string gender = accountDataList[index].Gender;
                string email = accountDataList[index].Email;
                string bankingdetails = accountDataList[index].bankingDetails;
                string[] allergies = accountDataList[index].Allergies;
                bool permission = accountDataList[index].Permission;

                string userInputPrint = "";

                // compile a string with all the allergies
                string allergiesStringPrint = "";
                for (int i = 0; i < allergies.Length; i++)
                {
                    if (i < allergies.Length - 1)
                    {
                        allergiesStringPrint += $"{allergies[i]}, ";
                    }
                    else
                    {
                        allergiesStringPrint += $"{allergies[i]}";
                    }
                }

                // prints all the account data
                Console.WriteLine($"\n[1] Naam: {name}");
                Console.WriteLine($"\n[2] Wachtwoord: ********");
                if (age == -1)
                {
                    Console.WriteLine($"\n[3] Leeftijd: ");
                }
                else
                {
                    Console.WriteLine($"\n[3] Leeftijd: {age}");
                }
                Console.WriteLine($"\n[4] Geslacht: {gender}");
                Console.WriteLine($"\n[5] Email: {email}");
                Console.WriteLine($"\n[6] Bank gegevens: {bankingdetails}");
                Console.WriteLine($"\n[7] Allergien: " + allergiesStringPrint);

                if (perm == true) // only gives option to change permission value if permission entered is true, for admin reasons
                {
                    Console.WriteLine($"\n[8] Permission: {permission}");
                    Console.WriteLine($"\n[9] Verwijder account");
                }
                else
                {
                    Console.WriteLine($"\n[8] Verwijder account");
                }
       
                Console.WriteLine($"\nVoer nummer in om geselecteerde veld te wijzigen of toe te voegen, of druk X in om terug te gaan: ");

                string userInputItem = Console.ReadLine();

                string userInputValue = ""; // prepare variable for if statement

                if (userInputItem == "X" || userInputItem == "x") // checks for "x" to stop program
                {
                    break;
                }
                
                else if (userInputItem == "7") // checks for "allergie" add or remove [array]
                {
                    Console_Reset.clear();

                    Console.WriteLine("\nKies uit de volgende opties:\n[1] om een allergie toe te voegen\n[2] om een allergie te verwijderen\n'X' om te stoppen");
                    string choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        userInputItem = "7.1";
                        bool innerRetry = true;
                        while (innerRetry == true)
                        {
                            Console_Reset.clear();

                            string[] availableArr = this.AvailableAllergie(allergies);
                            Console.WriteLine("\nKies uit de volgende allergenen: ");
                            for (int i = 0; i < availableArr.Length; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {availableArr[i]}");
                            }

                            Console.WriteLine("\nVoer hier waarde in om mee te geven: ");
                            userInputValue = Console.ReadLine();

                            try
                            {
                                int convert = Convert.ToInt32(userInputValue);
                                convert -= 1;
                                if (convert < availableArr.Length)
                                {
                                    userInputValue = availableArr[convert];
                                    innerRetry = false;
                                }
                                else
                                {
                                    Console.WriteLine("De input valt buiten de selectie, probeer het opnieuw");
                                }
                            }
                            catch
                            {
                                userInputValue = ""; // to return an empty value
                                innerRetry = false;
                            }
                        }
                    }
                    else if (choose == "2")
                    {
                        userInputItem = "7.2";
                        bool innerRetry = true;

                        if (allergies.Length == 0) // checks if user has allergies to begin with
                        {
                            userInputItem = "";
                            Console.WriteLine("\nU heeft nog geen allergenen");
                        }
                        else
                        {
                            while (innerRetry == true) // to make retry possible
                            {
                                Console_Reset.clear();

                                Console.WriteLine("\nKies uit de volgende allergenen: "); // prints all the users allergies
                                for (int i = 0; i < allergies.Length; i++)
                                {
                                    Console.WriteLine($"[{i + 1}] {allergies[i]}");
                                }

                                Console.WriteLine("\nVoer hier waarde in om mee te geven: ");
                                userInputValue = Console.ReadLine();

                                try // tries to convert value to be used in update method
                                {
                                    int convert = Convert.ToInt32(userInputValue);
                                    convert -= 1;
                                    if (convert < allergies.Length)
                                    {
                                        userInputValue = allergies[convert];
                                        innerRetry = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("De input valt buiten de selectie, probeer het opnieuw");
                                    }
                                }
                                catch
                                {
                                    userInputValue = ""; // to return an empty value
                                    innerRetry = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        userInputItem = ""; // to break the update method
                    }
                } // If 7, goes to add allergies section
                
                else // for every other input
                {
                    if ((userInputItem == "8" && perm == false) || (userInputItem == "9" && perm == true)){
                        Console.WriteLine("\nOm account te verwijderen, typ VERWIJDER");
                    }
                    else
                    {
                        Console.WriteLine("\nVoer hier waarde in om mee te geven: ");
                    }
                    userInputValue = Console.ReadLine();
                }
                

                // Check if there was input and sort input
                string[] callValue = new string[] { userInputItem, userInputValue };
                bool inputCheck = TextCheck(callValue);


                // Convert Item input to use in update method
                if (userInputItem == "1")
                {
                    userInputItem = "name";
                    userInputPrint = "Naam";
                }
                else if (userInputItem == "2")
                {
                    userInputItem = "password";
                    userInputPrint = "Wachtwoord";
                }
                else if (userInputItem == "3")
                {
                    userInputItem = "age";
                    userInputPrint = "Leeftijd";
                }
                else if (userInputItem == "4")
                {
                    userInputItem = "gender";
                    userInputPrint = "Geslacht";
                }
                else if (userInputItem == "5")
                {
                    userInputItem = "email";
                    userInputPrint = "Email";
                }
                else if (userInputItem == "6")
                {
                    userInputItem = "bankingdetails";
                    userInputPrint = "Bank gegevens";
                }
                else if (userInputItem == "7.1")
                {
                    userInputItem = "allergiesAdd";
                    userInputPrint = "Allergie toevoegen";
                }
                else if (userInputItem == "7.2")
                {
                    userInputItem = "allergiesRemove";
                    userInputPrint = "Allergie verwijderen";
                }
                else if (userInputItem == "8" && perm == true) // only use 8 if perms == true
                {
                    userInputItem = "permission";
                    userInputPrint = "Rechten";
                }
                else if (userInputItem == "8" && perm == false) // uses 8 to delete acc if perms == false
                {
                    userInputItem = "accountRemove";
                    userInputPrint = "Account verwijderen";
                }
                else if (userInputItem == "9" && perm == true) // uses 9 to delete acc if perms == true
                {
                    userInputItem = "accountRemove";
                    userInputPrint = "Account verwijderen";
                }
                else
                {
                    returnValue = false;
                }

                if (inputCheck == true)
                {
                    Console.WriteLine("\nBeide velden moeten een input hebben.\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");
                }
                else
                {
                    Console.WriteLine($"\nGeselecteerde Veld: {userInputPrint} | Gegeven Waarde: {userInputValue} \nOm toe te passen toets ENTER\nOm opniew te proberen, toets 'r'\nOm terug te gaan, toets 'x'");
                }

                // Checked if user wants to retry or confirm //
                string confirm = Console.ReadLine();
                if (confirm == "R" || confirm == "r" || confirm == "'R'" || confirm == "'r'")
                {
                    retry = true;
                }
                else if (confirm == "X" || confirm == "x" || confirm == "'X'" || confirm == "'x'")
                {
                    retry = false;
                }
                else
                {
                    if (inputCheck == true)
                    {
                        retry = true;
                    }
                    else
                    {
                        returnValue = UpdateUser(userInputItem ,userInputValue, index);
                    }
                }
                if (userInputItem == "accountRemove" && returnValue == true)
                {
                    retry = false;
                    return true; // account has been removed
                }
            }
            return false; // no account removal
        }
    }
}
