# City Search

City search is a tool that takes a search string and returns a list all cities that start with the search string along with all valid next characters for each matched city. 

## Getting Started

The first step is to download a copy of Microsoft visual studio and once installed, open the `CitySearch.sln` file.
This should allow you to then build and run the program.

Once the program is loaded in the console, type the first few letters of a city you know (e.g. 'york') and press enter.
You should then find a list of available cities that start with the search term along with potential next characters displayed.
You will also be notified if the string matches an existing city.

## 3rd party use

Running the search itself is quite simple and consists of the three key lines:

```
Trie Cities = new Trie();
Cities.Init();
ICityResult result = Cities.Search("york");
```

The search works by first loading a json file into a trie structure which is done in the 'Init' method. 
This initial load will depend on the size of the number of cities in the json file but once loaded the searches should be very quick.

### Interface used

This uses the following two interfaces:

```
namespace CitySearch
{
    using System.Collections.Generic;

    public interface ICityResult
    {
        ICollection<string> NextLetters { get; set; }

        ICollection<string> NextCities { get; set; }
		
        bool CityFound { get; set; }
    }
}

namespace CitySearch
{
    public interface ICityFinder
    {
        ICityResult Search(string searchString);
    }
}
```

## Diacritics

Currently the application makes an attempt to remove diacritics and replace them with something suitable before creating the trie structure.
If the resulting city does not contain the caracters a - z, space or a dash then it is not inserted into the search trie.

## Running the tests

### Prerequisites

In order to run test cases NUnit will need to be installed. Please refer to the [latest documentation](https://nunit.org/documentation/) on how to install it on your system.

The tests cover the three main areas which are:

 * Loading
 * Validating next characters
 * Validating next cities


## Future improvements

 * Adding a REST api so the application can be ran as a standalone microservice
 * Identify and add a complete list of cities and towns (the current list is incomplete)
 * Allow for handling of characters other than a to z, space and -
 
## Questions 

If you have any questions, please create a ticket and I'll respond as soon as possible. 

## Contributions

Any contributions are welcome so please feel free to get in touch or make pull requests and they will be reviewed and merged as soon as possible.

