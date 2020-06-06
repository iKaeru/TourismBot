using System.Collections.Generic;
using System.Linq;
using Cyriller;
using Cyriller.Model;

namespace TourismBot.Models
{
    public static class NounCollection
    {
        private static CyrNounCollection _cyrNounCollection = new CyrNounCollection();
        
        private static List<string> GetNounDeclension(string word)
        {
            CyrNoun noun = _cyrNounCollection.Get(word, out CasesEnum @case, out NumbersEnum number);
            var singular = noun.Decline().ToArray();
            var plural = noun.DeclinePlural().ToArray();
            var result = new List<string>();
            result.AddRange(singular.Where(singularWord => !string.IsNullOrEmpty(singularWord)).Distinct());
            result.AddRange(plural.Where(singularWord => !string.IsNullOrEmpty(singularWord)).Distinct());
            return result;
        }
    }
}