using System;
using System.Collections.Generic;
using System.Text;

namespace City_Search
{
    public interface ICityFinder
    {
        ICityResult Search(string searchString);
    }
}
