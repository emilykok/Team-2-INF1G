using System;
using System.Collections.Generic;
using System.Text;

namespace Team_2_INF1G.Classes
{
    class Datum
    {
        public string GetDate(string filmNaam) {
            Console.Clear();
            string s = "";
            filmNaam = filmNaam.ToLower();
            bool add;
            try
            {   // loopt door alle items in de json
                /*for (int i = 0; i < etenDataList.Count; i++)
                {
                    add = false;
                    // zoekt in titel van item
                    if (etenDataList[i].naam.ToLower().Contains(toFilter)) add = true;
                    // zoekt in tags
                    for (int j = 0; j < etenDataList[i].tags.Length; j++)
                    {
                        if (etenDataList[i].tags[j].Contains(toFilter)) add = true;
                    }
                    // zoekt in allergenen
                    for (int j = 0; j < etenDataList[i].allergenen.Length; j++)
                    {
                        if (etenDataList[i].allergenen[j].Contains(toFilter)) add = true;
                    }
                    if (add == true) s += etenDataList[i].naam + "\n";
                }
                return $"Gevonden eten met zoekterm '{toFilter}':\n\n{s}"; ;
            }

            catch (Exception)
            {
                return "je input was onjuist";
            } 

            
        }*/
            }
}
