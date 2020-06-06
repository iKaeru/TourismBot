using System.Collections.Generic;
using System.Linq;
using Cyriller;
using Cyriller.Model;

namespace TourismBot.Models
{
    public static class NounAndAdjectveCollection
    {
        private static CyrNounCollection _cyrNounCollection = new CyrNounCollection();
        private static CyrAdjectiveCollection _cyrAdjectiveCollection = new CyrAdjectiveCollection();

        public static List<string> GetNounDeclension(string word)
        {
            var noun = _cyrNounCollection.Get(word, out CasesEnum @case, out NumbersEnum number);
            var singular = noun.Decline().ToArray();
            var plural = noun.DeclinePlural().ToArray();
            var result = new List<string>();
            result.AddRange(singular.Where(singularWord => !string.IsNullOrEmpty(singularWord)));
            result.AddRange(plural.Where(singularWord => !string.IsNullOrEmpty(singularWord)));
            return result.Distinct().ToList();
        }

        public static List<string> GetAdjectiveDeclension(string word)
        {
            var adjective = _cyrAdjectiveCollection.Get(word, out GendersEnum gender, out CasesEnum @case,
                out NumbersEnum number, out AnimatesEnum animate);
            var singular = adjective.Decline(gender, animate).ToArray();
            var plural = adjective.DeclinePlural(animate).ToArray();
            var result = new List<string>();
            result.AddRange(singular.Where(singularWord => !string.IsNullOrEmpty(singularWord)));
            result.AddRange(plural.Where(singularWord => !string.IsNullOrEmpty(singularWord)));
            return result.Distinct().ToList();
        }
    }
}