using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using static System.Console;

namespace Hoofdmenu
{
    [ExcludeFromCodeCoverage]
    class Menu
    {
        public int SelectedIndex;
        public string[] Options;
        public string Prompt;
        public string finalText;
        public int whiteLine;

        public Menu(string prompt, string[] options, string final = "", int line = 0)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
            finalText = final;
            whiteLine = line;
        }

        private void DisplayOptions()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                if (whiteLine != 0 && whiteLine == i)
                {
                    WriteLine("\n");
                }
                string currentOption = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;

                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;


                }

                WriteLine($"-> {currentOption} ");
            }
            ResetColor();
            WriteLine(finalText);
        }

        public int Run()
        {
            bool retry = true;
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
                else if (keyPressed == ConsoleKey.Enter)
                {
                    retry = false;
                }

            } while (retry);

            return SelectedIndex;
        }
    }
}

