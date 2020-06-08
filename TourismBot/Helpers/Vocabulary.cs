using System.Collections.Generic;
using System.Linq;
using TourismBot.Models;

namespace TourismBot.Helpers
{
    public static class Vocabulary
    {
        public static List<string> Numbers { get; } = new List<string>
        {
            "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять", "десять"
        };

        public static List<Cases> NumbersDeclension { get; } = new List<Cases>
        {
            new Cases
            {
                NominativeCase = "один", GenitiveCase = "одного", DativeCase = "одному",
                AccusativeCase = "один", InstrumentalCase = "одним", PrepositionalCase = "одном"
            },
            new Cases
            {
                NominativeCase = "два", GenitiveCase = "двух", DativeCase = "двум",
                AccusativeCase = "два", InstrumentalCase = "двумя", PrepositionalCase = "двух"
            },
            new Cases
            {
                NominativeCase = "три", GenitiveCase = "трёх", DativeCase = "трём",
                AccusativeCase = "три", InstrumentalCase = "тремя", PrepositionalCase = "трёх"
            },
            new Cases
            {
                NominativeCase = "четыре", GenitiveCase = "четырёх", DativeCase = "четырём",
                AccusativeCase = "четыре", InstrumentalCase = "четырьмя", PrepositionalCase = "четырёх"
            },
            new Cases
            {
                NominativeCase = "пять", GenitiveCase = "пяти", DativeCase = "пяти",
                AccusativeCase = "пять", InstrumentalCase = "пятью", PrepositionalCase = "пяти"
            },
            new Cases
            {
                NominativeCase = "шесть", GenitiveCase = "шести", DativeCase = "шести",
                AccusativeCase = "шесть", InstrumentalCase = "шестью", PrepositionalCase = "шести"
            },
            new Cases
            {
                NominativeCase = "семь", GenitiveCase = "семи", DativeCase = "семи",
                AccusativeCase = "семь", InstrumentalCase = "семью", PrepositionalCase = "семи"
            },
            new Cases
            {
                NominativeCase = "восемь", GenitiveCase = "восьми", DativeCase = "восьми",
                AccusativeCase = "восемь", InstrumentalCase = "восьмью", PrepositionalCase = "восьми"
            },
            new Cases
            {
                NominativeCase = "девять", GenitiveCase = "девяти", DativeCase = "девяти",
                AccusativeCase = "девять", InstrumentalCase = "девятью", PrepositionalCase = "девяти"
            },
            new Cases
            {
                NominativeCase = "десять", GenitiveCase = "десяти", DativeCase = "десяти",
                AccusativeCase = "десять", InstrumentalCase = "десятью", PrepositionalCase = "десяти"
            }
        };

        public static List<string> GetNumbersWithNounDeclension(string noun)
        {
            var result = new List<string>();
            var nounCases = WordsCollection.GetNounCases(noun);
            for (var i = 0; i < NumbersDeclension.Count; i++)
            {
                var ending = GetEnding(i);
                result.Add($"{NumbersDeclension[i].NominativeCase} {noun}{ending}");
                result.Add($"{NumbersDeclension[i].GenitiveCase} {nounCases.GenitiveCase}");
                result.Add($"{NumbersDeclension[i].DativeCase} {nounCases.DativeCase}");
                result.Add($"{NumbersDeclension[i].AccusativeCase} {noun}{ending}");
                result.Add($"{NumbersDeclension[i].InstrumentalCase} {nounCases.InstrumentalCase}");
                result.Add($"{NumbersDeclension[i].PrepositionalCase} {nounCases.PrepositionalCase}");
            }

            return result.Distinct().ToList();
        }

        public static List<string> GetNumbersWithNoun(string noun)
        {
            var result = new List<string>();
            for (var i = 0; i < Numbers.Count; i++)
                result.Add($"{Numbers[i]} {noun}{GetEnding(i)}");

            return result;
        }

        private static string GetEnding(int index)
        {
            if (index == 0)
                return "";
            if (index >= 1 && index <= 3)
                return "а";

            return "ов";
        }
    }
}