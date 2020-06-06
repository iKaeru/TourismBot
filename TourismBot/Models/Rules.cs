using System.Collections.Generic;

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

        private static List<string> InitializeTheCheapestRule()
            => new List<string>
            {
                "самый недорогой",
                "самый дешевый"
            };

        private static List<string> InitializeBudgetaryRule()
            => new List<string>
            {
                "бюджетный",
                "эконом класса",
                "экономкласса",
                "эконом-класса",
                "экономичный",
                "малобюджетный",
                "низкоценовой",
                "малостоящий",
                "низкобюджетный",
                "недорогой",
                "дешёвый"
            };

        private static List<string> InitializePickyRule()
            => new List<string>
            {
                "непривередливые", "непривередливый", "непривередливого", "непривередливых", "непривередливому",
                "непривередливым", "непривередливыми",
                "не привередливые", "не привередливый", "не привередливого", "не привередливых", "не привередливому",
                "не привередливым", "не привередливыми",
                "неприхотливый", "неприхотливые", "неприхотливого", "неприхотливых", "неприхотливому", "неприхотливым",
                "неприхотливыми",
                "не прихотливый", "не прихотливые", "не прихотливого", "не прихотливых", "не прихотливому",
                "не прихотливым", "не прихотливыми",
                "нетребовательный", "нетребовательные", "нетребовательного", "нетребовательных", "нетребовательному",
                "нетребовательным", "нетребовательными",
                "не требовательный", "не требовательные", "не требовательного", "не требовательных",
                "не требовательному", "не требовательным", "не требовательными",
                "некапризный", "некапризные", "некапризного", "некапризных", "некапризному", "некапризным",
                "некапризными",
                "не капризный", "не капризные", "не капризного", "не капризных", "не капризному", "не капризным",
                "не капризными",
                "неразборчивый", "неразборчивые", "неразборчивого", "неразборчивых", "неразборчивому", "неразборчивым",
                "неразборчивыми",
                "не разборчивый", "не разборчивые", "не разборчивого", "не разборчивых", "не разборчивому",
                "не разборчивым", "не разборчивыми",
                "беспритязательный", "беспритязательные", "беспритязательного", "беспритязательных",
                "беспритязательному", "беспритязательным", "беспритязательными",
                "непритязательный", "непритязательные", "непритязательного", "непритязательных", "непритязательному",
                "непритязательным", "непритязательными",
                "не притязательный", "не притязательные", "не притязательного", "не притязательных",
                "не притязательному", "не притязательным", "не притязательными",
                "не предъявляющий больших требований",
            };

        private static List<string> InitializeFoodRule()
            => new List<string>
            {
                "еда", "еды", "еде", "еду", "едой", "о еде",
                "питание", "питания", "питаний", "питанию", "питанием", "о питании", "в питании",
                "поесть", "поем", "поешь", "поест", "поедим", "поедите", "поедят"
            };

        private static List<(int, float)> InitializeFoodRating()
            => new List<(int, float)> {(1, 4.5f), (3, 4.7f),};

        private static List<(int, float)> InitializeCustomRating(float value)
            => new List<(int, float)> {(1, value)};
    }
}