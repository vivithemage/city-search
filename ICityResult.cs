using System.Collections.Generic;

namespace City_Search
{
    public interface ICityResult
    {
        ICollection<string> NextLetters { get; set; }
        ICollection<string> NextCities { get; set; }
    }

    public class CityResult : ICityResult
    {
        private ICollection<string> nextCharacters = new List<string>();
        private ICollection<string> nextCities = new List<string>();

        public ICollection<string> NextLetters
        {
            get
            {
                return nextCharacters;
            }
            set
            {
                nextCharacters = value;
            }
                  
        }

        public ICollection<string> NextCities
        {            
            get
            {
                return nextCities;
            }

            set
            {
                nextCities = value;
            }
        }
    }
}
