using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Hoofdmenu
{
    class Applicatie
    {
        public void Start()
        {
            Title = "Cinematrix";
            RunHoofdmenu();
        }

        private void RunHoofdmenu()
        {
            string prompt = "\nWelkom bij Cinematrix. Kies waar je naartoe wilt.\n";
            string[] options = { "Films", "Eten en Drinken aanbod", "Informatie", "Afsluiten" };
            Menu Hoofdmenu = new Menu(prompt, options);
            int selectedIndex = Hoofdmenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    ExitApp();
                    break;
               default:
                    break;
            }
        }
        
        private void ExitApp()
        {
            WriteLine("\nDruk op een toets om de App te sluiten..");
            ReadKey(true);
            Environment.Exit(0);
        }
    }   
}
