using System.Collections.Generic;

namespace TourismBot.Helpers
{
    public static class Vocabulary
    {
        public static List<string> Numbers { get; } = new List<string>
        {
            "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять", "десять"
        };

        public static List<string> GetNumbersWithNoun(string noun)
        {
            var result = new List<string>();
            var ending = "";
            for (var i = 0; i < Numbers.Count; i++)
            {
                if (i == 0)
                    ending = "";
                else if (i >= 1 && i <= 3)
                    ending = "а";
                else
                    ending = "ов";

                result.Add($"{Numbers[i]} {noun}{ending}");
            }

            return result;
        }
    }
}