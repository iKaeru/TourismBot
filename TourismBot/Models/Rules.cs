using System.Collections.Generic;
using System.Linq;
using TourismBot.Helpers;

namespace TourismBot.Models
{
    public static class Rules
    {
        public static Rule TheCheapest { get; } = new Rule(InitializeTheCheapestRule(), InitializeCustomRating(0.0f));
        public static Rule Budgetary { get; } = new Rule(InitializeBudgetaryRule(), InitializeCustomRating(3.5f));
        public static Rule Picky { get; } = new Rule(InitializePickyRule(), InitializeCustomRating(4.5f));
        public static Rule Food { get; } = new Rule(InitializeFoodRule(), InitializeFoodRating());
        public static Rule Alcohol { get; } = new Rule(InitializeAlcoholRule(), InitializeCustomRating(4.7f));
        public static Rule Quality { get; } = new Rule(InitializeQualityRule(), InitializeCustomRating(4.3f));
        public static Rule RestWithKids { get; } = new Rule(InitializeRestWithKidsRule(), InitializeCustomRating(4.2f));
        public static Rule BeachBar { get; } = new Rule(InitializeBeachBarRule(), InitializeCustomRating(4.4f));

        public static Rule ConstructionDate { get; } =
            new Rule(InitializeConstructionDateRule(), InitializeCustomRating(4.4f));

        public static Rule BabyStrollers { get; } =
            new Rule(InitializeBabyStrollersRule(), InitializeCustomRating(4.3f));

        public static Rule Blender { get; } = new Rule(InitializeBlenderRule(), InitializeCustomRating(4.3f));
        public static Rule HeatedPool { get; } = new Rule(InitializeHeatedPoolRule(), InitializeCustomRating(4.4f));

        public static Rule PoolsQuantity { get; } =
            new Rule(InitializePoolsQuantityRule(), InitializeCustomRating(4.3f));

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
                "предъявляющие", "предъявляющих", "предъявляющими",
                "минимальные требования", "минимальных требований", "минимальным требованиям",
                "минимальными требованиями", "минимальных требованиях"
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

        private static List<string> InitializeQualityRule()
        {
            var result = new List<string>();
            result.AddRange(WordsCollection.GetNounDeclension("сервис"));
            result.AddRange(WordsCollection.GetNounDeclension("качество"));
            result.AddRange(WordsCollection.GetNounDeclension("услуга"));
            result.AddRange(WordsCollection.GetNounDeclension("обслуживание"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("качественный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("высококачественный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("первоклассный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("хороший"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("отличный"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("удобный"));
            return result;
        }

        private static List<string> InitializeRestWithKidsRule()
        {
            var result = new List<string>();
            var kid = WordsCollection.GetNounDeclension("ребёнок");
            result.AddRange(kid);
            result.AddRange(kid.Select(word =>
                word.Replace('ё', 'е'))); // include typos
            kid = WordsCollection.GetNounDeclension("ребёночек");
            result.AddRange(kid);
            result.AddRange(kid.Select(word =>
                word.Replace('ё', 'е'))); // include typos
            result.AddRange(WordsCollection.GetNounDeclension("мальчик"));
            result.AddRange(WordsCollection.GetNounDeclension("мальчишка"));
            result.AddRange(WordsCollection.GetNounDeclension("девочка"));
            var girl = WordsCollection.GetNounDeclension("девчёнка");
            result.AddRange(girl);
            result.AddRange(girl.Select(word =>
                word.Replace('ё', 'е'))); // include typos
            result.AddRange(WordsCollection.GetNounDeclension("сын"));
            result.AddRange(WordsCollection.GetNounDeclension("сынок"));
            result.AddRange(WordsCollection.GetNounDeclension("сыночек"));
            result.AddRange(WordsCollection.GetNounDeclension("сынишка"));
            result.AddRange(WordsCollection.GetNounDeclension("дочь"));
            result.AddRange(WordsCollection.GetNounDeclension("дочка"));
            result.AddRange(WordsCollection.GetNounDeclension("доченька"));
            result.AddRange(WordsCollection.GetNounDeclension("дочурка"));
            result.AddRange(WordsCollection.GetNounDeclension("дитя"));
            result.AddRange(WordsCollection.GetNounDeclension("малыш"));
            result.AddRange(WordsCollection.GetNounDeclension("младенец"));
            result.AddRange(WordsCollection.GetNounDeclension("отпрыск"));
            result.AddRange(WordsCollection.GetAdjectiveDeclension("детский"));
            return result;
        }

        private static List<string> InitializeConstructionDateRule()
        {
            var result = new List<string>();
            result.AddRange(WordsCollection.GetNounDeclension("постройка"));
            result.AddRange(WordsCollection.GetNounDeclension("строительство"));
            result.AddRange(WordsCollection.GetNounDeclension("эксплуатация"));
            result.AddRange(WordsCollection.GetNounDeclension("возведение"));
            result.AddRange(WordsCollection.GetNounDeclension("воздвиженье"));
            result.AddRange(WordsCollection.GetNounDeclension("реновация"));
            result.AddRange(WordsCollection.GetNounDeclension("обновление"));
            result.AddRange(WordsCollection.GetNounDeclension("ремонт"));
            result.AddRange(WordsCollection.GetNounDeclension("ремонтирование"));
            result.AddRange(new List<string>
            {
                "построен", "воздвижен", "воздвигнут", "отремотирован", "ремонтирован",
                "обновлён", "обновлен", "отсроен", "воздвигнут",
                "построенный", "построенные", "построенного", "построенных",
                "построенному", "построенным", "построенными", "построенном",
                "воздвиженный", "воздвиженные", "воздвиженного", "воздвиженных",
                "воздвиженному", "воздвиженным", "воздвиженными", "воздвиженном",
                "отремотированный", "отремотированные", "отремотированного", "отремотированных",
                "отремотированному", "отремотированным", "отремотированными", "отремотированном",
                "эксплуатируемый", "эксплуатируемые", "эксплуатируемого", "эксплуатируемых",
                "эксплуатируемому", "эксплуатируемым", "эксплуатируемыми", "эксплуатируемом",
            });
            return result;
        }

        private static List<string> InitializeBeachBarRule()
        {
            var result = new List<string>();
            var bar = WordsCollection.GetNounDeclension("бар");
            result.AddRange(bar.Select(word => word.Insert(word.Length, " на пляже")));
            result.AddRange(new List<string> {"бар есть на пляже", "бар был на пляже", "бар будет на пляже"});
            return result;
        }

        private static List<string> InitializeBabyStrollersRule()
        {
            var result = new List<string>();
            result.AddRange(WordsCollection.GetPhraseDeclension("детская коляска"));
            var rent = WordsCollection.GetNounDeclension("аренда");
            result.AddRange(rent.Select(word => word.Insert(word.Length, " детской коляски")));
            result.AddRange(rent.Select(word => word.Insert(word.Length, " детских колясок")));
            rent = WordsCollection.GetNounDeclension("прокат");
            result.AddRange(rent.Select(word => word.Insert(word.Length, " детской коляски")));
            result.AddRange(rent.Select(word => word.Insert(word.Length, " детских колясок")));
            return result;
        }

        private static List<string> InitializeBlenderRule()
        {
            var result = new List<string>();
            result.AddRange(WordsCollection.GetNounDeclension("блендер"));
            result.AddRange(WordsCollection.GetNounDeclension("миксер"));
            return result;
        }

        private static List<string> InitializeHeatedPoolRule()
        {
            var result = new List<string>();
            var pool = WordsCollection.GetNounDeclension("бассейн");
            result.AddRange(pool.Select(word => word.Insert(word.Length, " с подогревом")));
            result.AddRange(new List<string>
            {
                "подогреваемый бассейн", "подогреваемые бассейны",
                "подогреваемого бассейна", "подогреваемых бассейнов",
                "подогреваемому бассейну", "подогреваемым бассейном",
                "подогреваемыми бассейнами", "подогреваемом бассейне",
            });

            return result;
        }

        private static List<string> InitializePoolsQuantityRule()
        {
            var result = new List<string>();
            var amount = WordsCollection.GetNounDeclension("количество");
            result.AddRange(amount.Select(word => word.Insert(word.Length, " бассейнов")));
            amount = WordsCollection.GetNounDeclension("число");
            result.AddRange(amount.Select(word => word.Insert(word.Length, " бассейнов")));
            result.AddRange(Vocabulary.GetNumbersWithNoun("бассейн"));
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