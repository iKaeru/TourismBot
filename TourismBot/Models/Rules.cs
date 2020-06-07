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
        public static Rule Alcohol { get; } = new Rule(InitializeAlcoholRule(), InitializeCustomRating(4.7f));
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
            var cheapest = WordsCollection.GetPhraseDeclension("самый дешёвый");
            result.AddRange(WordsCollection.GetPhraseDeclension("самый недорогой"));
            result.AddRange(WordsCollection.GetPhraseDeclension("самый малостоящий"));
            result.AddRange(cheapest);
            result.AddRange(cheapest.Select(phrase =>
                phrase.Replace('ё', 'е'))); // include typos
            result.AddRange(WordsCollection.GetPhraseDeclension("самый бюджетный"));
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
            result.AddRange(WordsCollection.GetPhraseDeclension("экономичный класс"));
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
            result.AddRange(WordsCollection.GetNounDeclension("пища"));
            result.AddRange(WordsCollection.GetNounDeclension("питание"));
            result.AddRange(WordsCollection.GetNounDeclension("блюдо"));
            result.AddRange(WordsCollection.GetNounDeclension("перекус"));
            result.AddRange(WordsCollection.GetNounDeclension("закуска"));
            result.AddRange(WordsCollection.GetNounDeclension("завтрак"));
            result.AddRange(WordsCollection.GetNounDeclension("обед"));
            result.AddRange(WordsCollection.GetNounDeclension("ужин"));
            result.AddRange(WordsCollection.GetNounDeclension("рацион"));
            result.AddRange(new List<string> {"поесть", "перекусить", "закусить",});
            return result;
        }

        private static List<string> InitializeAlcoholRule()
        {
            var result = new List<string>();
            result.AddRange(WordsCollection.GetNounDeclension("бокал"));
            result.AddRange(WordsCollection.GetNounDeclension("бутылка"));
            result.AddRange(WordsCollection.GetNounDeclension("бутылочка"));
            result.AddRange(WordsCollection.GetNounDeclension("бокальчик"));
            result.AddRange(WordsCollection.GetNounDeclension("вино"));
            result.AddRange(WordsCollection.GetNounDeclension("шампанское"));
            result.AddRange(WordsCollection.GetNounDeclension("алкоголь"));
            result.AddRange(WordsCollection.GetNounDeclension("выпивка"));
            result.AddRange(WordsCollection.GetNounDeclension("напиток"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("алкогольный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("спиртной"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("крепкий"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("горячительный"));
            result.AddRange(new List<string> {"выпить", "выпивать"});
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