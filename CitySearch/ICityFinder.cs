using System;
using System.Collections.Generic;
using System.Text;

namespace CitySearch
{
    public interface ICityFinder
    {
        ICityResult Search(string searchString);
    }
}
