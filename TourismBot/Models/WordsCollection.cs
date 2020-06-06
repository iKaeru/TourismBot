using System.Collections.Generic;
using System.Linq;
using Cyriller;
using Cyriller.Model;

namespace TourismBot.Models
{
    public static class WordsCollection
    {
        private static CyrNounCollection _cyrNounCollection = new CyrNounCollection();
        private static CyrAdjectiveCollection _cyrAdjectiveCollection = new CyrAdjectiveCollection();
        private static CyrPhrase _cyrPhraseCollection = new CyrPhrase(_cyrNounCollection, _cyrAdjectiveCollection);

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
        
        public static List<string> GetPhraseDeclension(string phrase)
        {
            var singular = _cyrPhraseCollection.Decline(phrase, GetConditionsEnum.Strict).ToArray();
            var plural = _cyrPhraseCollection.DeclinePlural(phrase, GetConditionsEnum.Strict).ToArray();
            var result = new List<string>();
            result.AddRange(singular.Where(singularWord => !string.IsNullOrEmpty(singularWord)));
            result.AddRange(plural.Where(singularWord => !string.IsNullOrEmpty(singularWord)));
            return result.Distinct().ToList();
        }
    }
}