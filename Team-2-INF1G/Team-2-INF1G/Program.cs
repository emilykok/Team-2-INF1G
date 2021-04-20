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
        //// struct is a data type used for the serialization and deserialization of the JSON file.
        // It will be put in a list, that can be index with []
        // To call the value's in it, use the name and then the value name (accountDataList[0].Age)
        // It is the same as a Tuple, but it works like a class as reference (acc.accountDataList[0].Age)
        public struct AccountData
        {
            public string Name;
            public string Password;
            public int Age;
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
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this.accountDataList, Formatting.Indented);
        }

        // Method that can be called to create a user.
        public void CreateUser(string name, string password, int age)
        {
            
            AccountData newAccountData = new AccountData();
            newAccountData.Name        = name;
            newAccountData.Password    = password;
            newAccountData.Age         = age;

            // add to the list with the added data
            accountDataList.Add(newAccountData);

            // write to the JSON file (updates the file)
            System.IO.File.WriteAllText(this.path, ToJSON());
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            // Create account
            //   Input name
            //   Input password
            Account acc = new Account();

            acc.CreateUser("Bjorn", "abcd", 12);
            acc.CreateUser("koos", "broer", 16);
            acc.CreateUser("boos", "ttee", 23);

            //foreach (var accountData in a.accountDataList)
            //{
            //Console.WriteLine("--------------------");
            //Console.WriteLine(accountData.Age);
            //Console.WriteLine(accountData.Name);
            //Console.WriteLine(accountData.Password);
            //}


            Console.WriteLine(acc.accountDataList[0].Age);
            Console.WriteLine(acc.accountDataList[0].Name);
            Console.WriteLine(acc.accountDataList[0].Password);

            // Update account
            //   Input name
            //   Input password

            // Remove account



        }
    }
}
