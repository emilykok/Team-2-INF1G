
using System;

namespace HelloWorld
{
  class Program {
    public static void Main() {
        Console.WriteLine("Typ het nummer van wat je wilt bekijken en klink op enter\n1. Popcorn zoet\n2. Popcorn zout\n3. Popcorn karamel\n4. M&M's pinda\n5. M&M's chocola\n6. Chips naturel\n7. Chips paprika\n8. Doritos nacho cheese\n9. Haribo goudberen\n10. Skittles fruits");
        int foodItem = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(foodItem);
    }
  }
}   