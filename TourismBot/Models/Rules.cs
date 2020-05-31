using System.Collections.Generic;

namespace TourismBot.Models
{
    public static class Rules
    {
        public static List<string> Food { get; } = InitializeFoodRule();

        private static List<string> InitializeFoodRule()
            => new List<string>
            {
                "еда", "еды", "еде", "еду", "едой", "едой", "о еде",
                "питание", "питания", "питаний", "питанию", "питанием", "о питании", "в питании"
            };
    }
}