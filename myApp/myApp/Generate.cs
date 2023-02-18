using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApp
{
    public class Generate
    {
        public string AllWhatWeNeed()
        {
            double percent = 0;
            string result = "INSERT INTO [Table_Pesons] ([SurName], [Name], [Patronymic], [DateBirthday], [Sex]) VALUES ";
            Console.WriteLine("Строка создается");
            for (int i = 0; i < 1000000; i++)
            {
                
                percent = (i * 100) / 1000000;
                result += Combine();
                
            }
            Console.WriteLine($"Строка создана, началась запись");
            return result[..^1];
            
        }
        private string Combine()
        {
            string person = "(";
            for(int i = 0; i < 3; i++)
            {
                person += "'";
                person += GenerateWords();
                person += "',";
            }
            person += "'";
            person += DateBirthday();
            person += "',";
            person += "'";
            person += ManOrWonen() + "'),";
            return person;
        }
        private string DateBirthday()
        {
            Random r = new Random();

            string date = r.Next(1902, 2023).ToString() + "-";

            int month = r.Next(1, 12);
            if(month < 10)
                date += "0" + month + "-";
            else
                date += month + "-";

            int day;
            if(month == 2)
                day = r.Next(1, 28);
            else
                day = r.Next(1, 30);

            if(day < 10)
                date += "0" + day;
            else
                date += day;

            return date;
        }
        private string ManOrWonen()
        {
            if (new Random().Next() > (Int32.MaxValue / 2))
                return "man";
            else
                return "woman";
        }
        private string GenerateWords()
        {
            Random r = new Random();
            int len = r.Next(4, 10);
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; 
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


        }
    }
}
