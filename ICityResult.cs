using System.Collections.Generic;

namespace City_Search
{
    public interface ICityResult
    {
        ICollection<string> NextLetters { get; set; }
        ICollection<string> NextCities { get; set; }
        bool CityFound { get; set; }
    }

    public class CityResult : ICityResult
    {
        private ICollection<string> nextCharacters = new List<string>();
        private ICollection<string> nextCities = new List<string>();
        private bool cityFound = false;

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

        public bool CityFound
        {
            get
            {
                return cityFound;
            }
            set
            {
                cityFound = value;
            }
        }
    }
}
