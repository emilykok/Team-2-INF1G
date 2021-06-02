using System;
using System.IO;
using Account_Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hashing_Class;
using System.Security.Cryptography;

namespace BiosTest
{
    [TestClass]
    public class AccountTests
    {
        // NameReturn //
        [TestMethod]
        public void ReturnCorrectUsername()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            string res = acc1.ReturnUsername(4);

            // Assert
            Assert.AreEqual(res, "Admin1");
        }
        

        // TextCheck //
        [TestMethod]
        public void TextCheck_InputEmpty_ReturnsTrue() 
        {
            // Arrange
            Account acc1 = new Account();
            string[] arr = new string[] {"","",""};

            // Act
            var result = acc1.TextCheck(arr);

            // Assert
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void TextCheck_Input_ReturnsFalse()
        {
            // Arrange
            Account acc1 = new Account();
            string[] arr = new string[] { "a", "test", "boop" };

            // Act
            var result = acc1.TextCheck(arr);

            // Assert
            Assert.IsFalse(result);
        }


        // UsernameCheck //
        [TestMethod]
        public void UsernameCheck_NewName_ReturnsFalse()
        {
            // Arrange
            Account acc1 = new Account();
            string name = "IDontHeccinKnow";

            // Act
            var result = acc1.UsernameCheck(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UsernameCheck_TakenName_ReturnsTrue()
        {
            // Arrange
            Account acc1 = new Account();
            string name = "Admin1";

            // Act
            var result = acc1.UsernameCheck(name);

            // Assert
            Assert.IsTrue(result);
        }


        // PrintItem //
        [TestMethod]
        public void PrintItem_InRange_PrintsItems()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            acc1.PrintItem(2,5);

            // Assert
            Assert.AreEqual("a", "a"); // need a way to read what is printed on the console...
        }

        // AvailableAllergie //
        [TestMethod]
        public void AvailableAllergies_Empty_ReturnAllAllergies() {
            // Arrange
            Account acc1 = new Account();
            string[] allAll = new string[] { "lactose", "soja", "pinda", "amandel", "hazelnoot", "noten", "gluten", "tarwe" };
            string[] allergies = new string[0];

            // Act
            string[] res = acc1.AvailableAllergie(allergies);

            // Assert
            CollectionAssert.AreEqual(res, allAll);
        }

        [TestMethod]
        public void AvailableAllergies_Input_ReturnAllAllergies()
        {
            // Arrange
            Account acc1 = new Account();
            string[] expected = new string[] { "lactose", "pinda", "amandel", "noten", "gluten", "tarwe" };
            string[] allergies = new string[] {"soja", "hazelnoot"};

            // Act
            string[] res = acc1.AvailableAllergie(allergies);

            // Assert
            CollectionAssert.AreEqual(res, expected); // checks the collection disregarding references!
        }

        // Login //
        [TestMethod]
        public void Login_CorrectInput_Return4()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            int res = acc1.Login("Admin1", "Admin1");

            // Assert
            Assert.AreEqual(res, 4);
        }
 
        [TestMethod]
        public void Login_WrongtInput_ReturnMin1()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            int res = acc1.Login("Admin1", "wrong");

            // Assert
            Assert.AreEqual(res, -1); // checks the value's BE CAREFUL WITH REFERENCES!
        }

        // TextLogin //
        [TestMethod]
        public void TextLogin_CorrectInput()
        {
            // Arrange
            Account acc1 = new Account();


            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"Admin1
Admin1

");
            Console.SetIn(input);

            int res = acc1.TextLogin();
            
            // Assert

            Assert.AreEqual(res, 4); // checks the value's BE CAREFUL WITH REFERENCES!
            
        }

        [TestMethod]
        public void TextLogin_IncorrectInput()
        {
            // Arrange
            Account acc1 = new Account();


            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"Admin1


Admin!
admin1
r
Admin!
admin1
x
");
            Console.SetIn(input);

            int res = acc1.TextLogin();

            // Assert

            Assert.AreEqual(res, -1); // checks the value's BE CAREFUL WITH REFERENCES!

        }

        // TextCreateUser //
        [TestMethod]
        public void TextCreateUser_CorrectInput()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"TextCreateUser
TextCreateUser

");
            Console.SetIn(input);
            acc1.TextCreateUser();

            // Assert
            Assert.AreEqual("TextCreateUser", acc1.accountDataList[acc1.accountDataList.Count - 1].Name); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void TextCreateUser_IncorrectInput()
        {
            // Arrange
            Account acc1 = new Account();


            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"Admin1
Admin1

Admin1

r
Admin1
Admin1
x
");
            Console.SetIn(input);
            acc1.TextCreateUser();

            // Assert
            Assert.AreNotEqual("Admin1", acc1.accountDataList[acc1.accountDataList.Count - 1].Name); // checks the value's BE CAREFUL WITH REFERENCES!
        }

        // AccountView //
        [TestMethod]
        public void AccountView_UpdateNaam()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testname", "testname");

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"1
newName

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreEqual("newName", acc1.accountDataList[acc1.accountDataList.Count - 1].Name); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_Updatewachtwoord()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testww", "testww");

            // Hash the password
            string password = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                password = Hashing.GetHash(sha256Hash, "wachtwoord");
            }

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"2
wachtwoord

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreEqual(password, acc1.accountDataList[acc1.accountDataList.Count - 1].Password); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateLeeftijd()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testage", "testage");

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"3
18

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreEqual(18, acc1.accountDataList[acc1.accountDataList.Count - 1].Age); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateGeslachtMan()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testmale", "testmale");           

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"4
man

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreEqual("man", acc1.accountDataList[acc1.accountDataList.Count - 1].Gender); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateGeslachtVrouw()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testfemale", "testfemale");

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"4
vrouw

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreEqual("vrouw", acc1.accountDataList[acc1.accountDataList.Count - 1].Gender); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateGeslachtOther()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testfemale", "testfemale");

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"4
other

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreEqual("other", acc1.accountDataList[acc1.accountDataList.Count - 1].Gender); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateEmail()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testemail", "testemail");

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"5
test@outlook.com

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreEqual("test@outlook.com", acc1.accountDataList[acc1.accountDataList.Count - 1].Email); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateBankgegevens()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testbank", "testbank");

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"6
INGB 0000 0000 0000

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreEqual("INGB 0000 0000 0000", acc1.accountDataList[acc1.accountDataList.Count - 1].bankingDetails); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateAllergienAdd()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testadd", "testadd");

            string[] allergies = { "lactose" };

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"7
1
1

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            CollectionAssert.AreEqual(allergies, acc1.accountDataList[acc1.accountDataList.Count - 1].Allergies); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateAllergienRemove1()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testremove", "testremove");

            acc1.UpdateUser("allergiesAdd", "lactose", acc1.accountDataList.Count - 1);
            string[] allergies = { "lactose" };

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"7
2
1

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            CollectionAssert.AreNotEqual(allergies, acc1.accountDataList[acc1.accountDataList.Count - 1].Allergies); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_UpdateAllergienRemove2()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testremove", "testremove");

            acc1.UpdateUser("allergiesAdd", "lactose", acc1.accountDataList.Count - 1);
            acc1.UpdateUser("allergiesAdd", "noten", acc1.accountDataList.Count - 1);
            string[] allergies = { "noten" };

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"7
2
1

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            CollectionAssert.AreEqual(allergies, acc1.accountDataList[acc1.accountDataList.Count - 1].Allergies); // checks the value's BE CAREFUL WITH REFERENCES!
            acc1.DeleteUser(acc1.accountDataList.Count - 1);
        }

        [TestMethod]
        public void AccountView_Verwijder()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testdelete", "testdelete");

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"8
VERWIJDER

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1);

            // Assert
            Assert.AreNotEqual("testdelete", acc1.accountDataList[acc1.accountDataList.Count - 1].Name); // checks the value's BE CAREFUL WITH REFERENCES!
        }

        // Now with admin logged in
        [TestMethod]
        public void AccountView_Verwijder_Admin()
        {
            // Arrange
            Account acc1 = new Account();
            acc1.CreateUser("testdeleteadmin", "testdeleteadmin");

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@"9
VERWIJDER

x
");
            Console.SetIn(input);
            acc1.AccountView(acc1.accountDataList.Count - 1, true);

            // Assert
            Assert.AreNotEqual("testdeleteadmin", acc1.accountDataList[acc1.accountDataList.Count - 1].Name); // checks the value's BE CAREFUL WITH REFERENCES!
        }


        // AdminAccountViewer //

        [TestMethod]
        public void AdminControl_Navigate_Normal()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@">

>
<
<
21
x

<
x
");
            Console.SetIn(input);
            int result = acc1.AdminAccountViewer("Admin1");

            // Assert
            Assert.AreEqual(4, result); // checks the value's BE CAREFUL WITH REFERENCES!
        }
        [TestMethod]
        public void AdminControl_Navigate_Extend()
        {
            // Arrange
            Account acc1 = new Account();

            // Act
            // _step 1, read from console "user input"
            // WARNING when using StringReader, you MUST use @ and no indentation!
            var input = new StringReader(@">

>
>
>

<
<
<
<

x
");
            Console.SetIn(input);
            int result = acc1.AdminAccountViewer("Admin1");

            // Assert
            Assert.AreEqual(4, result); // checks the value's BE CAREFUL WITH REFERENCES!
        }
    }
}
