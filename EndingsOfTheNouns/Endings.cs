namespace EndingsOfTheNouns
{
    public class Endings
    {
        public static string GetNewWord(string word, int number)
        {
            if ((number == 1) ||
               (number % 10 == 1) && (number > 20))
            {
                return word;  
            }

            else if ((number >= 2) && (number <= 4) ||
                    (number > 20) && (number % 10 >= 2) && (number % 10 <= 4))
            {
                return word + "а";
            }
            else
            {
                return word + "ев";
            }
        }             
    }
}
