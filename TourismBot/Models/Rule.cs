using System.Collections.Generic;

namespace TourismBot.Models
{
    public class Rule
    {
        public List<string> AssociatedPhrases { get; }
        public List<(int, float)> RatingValue { get; }

        public Rule(List<string> associatedPhrases, List<(int,float)> ratingValue)
        {
            AssociatedPhrases = associatedPhrases;
            RatingValue = ratingValue;
        }
    }
}