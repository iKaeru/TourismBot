using System.Collections.Generic;
using System.Linq;

namespace TourismBot.Models
{
    public static class Rules
    {
        public static Rule TheCheapest { get; } = new Rule(InitializeTheCheapestRule(), InitializeCustomRating(0.0f));
        public static Rule Budgetary { get; } = new Rule(InitializeBudgetaryRule(), InitializeCustomRating(3.5f));
        public static Rule Picky { get; } = new Rule(InitializePickyRule(), InitializeCustomRating(4.5f));
        public static Rule Food { get; } = new Rule(InitializeFoodRule(), InitializeFoodRating());
        public static Rule Alcohol { get; } = new Rule(new List<string>(), InitializeCustomRating(4.7f));
        public static Rule Quality { get; } = new Rule(new List<string>(), InitializeCustomRating(4.3f));
        public static Rule RestWithKids { get; } = new Rule(new List<string>(), InitializeCustomRating(4.2f));
        public static Rule BeachBar { get; } = new Rule(new List<string>(), InitializeCustomRating(4.4f));
        public static Rule ConstructionDate { get; } = new Rule(new List<string>(), InitializeCustomRating(4.4f));
        public static Rule BabyStrollers { get; } = new Rule(new List<string>(), InitializeCustomRating(4.3f));
        public static Rule Blender { get; } = new Rule(new List<string>(), InitializeCustomRating(4.3f));
        public static Rule HeatedPool { get; } = new Rule(new List<string>(), InitializeCustomRating(4.4f));
        public static Rule PoolsQuantity { get; } = new Rule(new List<string>(), InitializeCustomRating(4.3f));
        public static Rule BarsQuantity { get; } = new Rule(new List<string>(), InitializeCustomRating(4.4f));
        public static Rule KidsClub { get; } = new Rule(new List<string>(), InitializeCustomRating(4.4f));
        public static Rule KidsPot { get; } = new Rule(new List<string>(), InitializeCustomRating(4.2f));
        public static Rule AnimationShow { get; } = new Rule(new List<string>(), InitializeCustomRating(4.3f));
        public static Rule PeopleWithDisabilities { get; } = new Rule(new List<string>(), InitializeCustomRating(4.4f));
        public static Rule UltraAllInclusive { get; } = new Rule(new List<string>(), InitializeCustomRating(4.4f));
        public static Rule KidsFood { get; } = new Rule(new List<string>(), InitializeCustomRating(4.6f));
        public static Rule BikeRental { get; } = new Rule(new List<string>(), InitializeCustomRating(4.2f));
        public static Rule SportAndTraining { get; } = new Rule(new List<string>(), InitializeCustomRating(4.23f));
        public static Rule Rooms { get; } = new Rule(new List<string>(), InitializeCustomRating(4.4f));

        #region vocabulary

        private static List<string> InitializeTheCheapestRule()
        {
            var result = new List<string>();
            result.AddRange(WordsCollection.GetPhraseDeclension("самый недорогой"));
            result.AddRange(WordsCollection.GetPhraseDeclension("самый дешевый"));
            result.AddRange(WordsCollection.GetPhraseDeclension("самый малостоящий"));
            return result;
        }

        private static List<string> InitializeBudgetaryRule()
        {
            var result = new List<string>();
            var budgetary = WordsCollection.GetAdjectiveDeclension("бюджетный");
            result.AddRange(budgetary);
            result.AddRange(budgetary.Select(word => $"мало{word}")); // малобюджетный
            result.AddRange(budgetary.Select(word => $"низко{word}")); // низкобюджетный
            result.AddRange(WordsCollection.GetAdjectiveDeclension("экономичный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("ценовой")
                .Select(word => $"низко{word}")); // низкоценовой
            var economyClass = WordsCollection.GetNounDeclension("класс");
            result.AddRange(economyClass.Select(word => $"эконом{word}"));
            result.AddRange(economyClass.Select(word => $"эконом {word}")); // include typos
            result.AddRange(economyClass.Select(word => $"эконом-{word}")); // include typos
            return result;
        }

        private static List<string> InitializePickyRule()
        {
            var result = new List<string>();
            result.AddRange(WordsCollection.GetAdjectiveDeclension("непривередливый"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("привередливый")
                .Select(word => $"не {word}")); // include typos
            result.AddRange(WordsCollection.GetAdjectiveDeclension("неприхотливый"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("прихотливый")
                .Select(word => $"не {word}")); // include typos
            result.AddRange(WordsCollection.GetAdjectiveDeclension("нетребовательный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("требовательный")
                .Select(word => $"не {word}")); // include typos
            result.AddRange(WordsCollection.GetAdjectiveDeclension("некапризный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("капризный")
                .Select(word => $"не {word}")); // include typos
            result.AddRange(WordsCollection.GetAdjectiveDeclension("неразборчивый"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("разборчивый")
                .Select(word => $"не {word}")); // include typos
            result.AddRange(WordsCollection.GetAdjectiveDeclension("непритязательный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("притязательный")
                .Select(word => $"не {word}")); // include typos
            result.AddRange(WordsCollection.GetAdjectiveDeclension("беспритязательный"));
            var temp = new List<string>
            {
                "предъявляющий", "предъявляющего", "предъявляющему", "предъявляющим", "предъявляющем",
                "предъявляющие", "предъявляющих", "предъявляющими"
            };
            result.AddRange(temp.Select(word => $"не {word} больших требований"));
            return result;
        }

        private static List<string> InitializeFoodRule()
        {
            var result = new List<string>();
            result.AddRange(WordsCollection.GetNounDeclension("еда"));
            result.AddRange(WordsCollection.GetNounDeclension("питание"));
            result.AddRange(new List<string> {"поесть", "поем", "поешь", "поест", "поедим", "поедите", "поедят"});
            return result;
        }

        #endregion vocabulary

        #region rating

        private static List<(int, float)> InitializeFoodRating()
            => new List<(int, float)> {(1, 4.5f), (3, 4.7f),};

        private static List<(int, float)> InitializeCustomRating(float value)
            => new List<(int, float)> {(1, value)};

        #endregion rating
    }
}