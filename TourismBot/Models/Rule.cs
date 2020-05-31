using System.Collections.Generic;

namespace TourismBot.Models
{
    public class Rule
    {
        public List<string> AssociatedPhrases { get; }
        public float RatingValue { get; }

        public Rule(List<string> associatedPhrases, float ratingValue)
        {
            AssociatedPhrases = associatedPhrases;
            RatingValue = ratingValue;
        }
    }
}