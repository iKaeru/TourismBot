using System;
using System.Collections.Generic;
using System.Linq;
using Cyriller;
using Cyriller.Model;

namespace TourismBot.Models
{
    public static class WordsCollection
    {
        private static CyrNounCollection _cyrNounCollection;
        private static CyrAdjectiveCollection _cyrAdjectiveCollection;
        private static CyrPhrase _cyrPhraseCollection;

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

        public static Cases GetNounCases(string word)
        {
            var noun = _cyrNounCollection.Get(word, out CasesEnum @case, out NumbersEnum number);
            var singular = noun.Decline();
            return new Cases
            {
                NominativeCase = singular.Nominative,
                GenitiveCase = singular.Genitive,
                DativeCase = singular.Dative,
                AccusativeCase = singular.Accusative,
                InstrumentalCase = singular.Instrumental,
                PrepositionalCase = singular.Prepositional
            };
        }

        private static void InitializeNouns()
        {
            Console.WriteLine("Initializing nouns collection");
            var result = new CyrNounCollection();
            Console.WriteLine("Complete nouns collection");
            _cyrNounCollection = result;
        }

        private static void InitializeAdjectives()
        {
            Console.WriteLine("Initializing adjectives collection");
            var result = new CyrAdjectiveCollection();
            Console.WriteLine("Complete adjectives collection");
            _cyrAdjectiveCollection = result;
        }

        private static void InitializePhrases()
        {
            Console.WriteLine("Initializing phrases collection");
            var result = new CyrPhrase(_cyrNounCollection, _cyrAdjectiveCollection);
            Console.WriteLine("Complete phrases collection");
            _cyrPhraseCollection = result;
        }

        public static void InitializeWordsCollection()
        {
            InitializeNouns();
            InitializeAdjectives();
            InitializePhrases();
        }
    }
}