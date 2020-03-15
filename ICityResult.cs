using System;
using System.Collections.Generic;
using System.Text;

namespace City_Search
{
    interface ICityResult
    {
        ICollection<string> NextLetters { get; set; }
        ICollection<string> NextCities { get; set; }
    }
}
