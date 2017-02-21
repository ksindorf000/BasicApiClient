using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Models
{
    /*********SECRET WEAPONS***********
     * http://stackoverflow.com/questions/38749730/json-net-error-reading-string-unexpected-token-startobject-path-responsedata
     * http://json2csharp.com/ 
     *********************************/

    public class VersionGroup
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Language
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Name
    {
        public string name { get; set; }
        public Language language { get; set; }
    }

    public class PokemonSpecy
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class gMove
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class MainRegion
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class gType
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Game
    {
        public List<object> abilities { get; set; }
        public string name { get; set; }
        public List<VersionGroup> version_groups { get; set; }
        public int id { get; set; }
        public List<Name> names { get; set; }
        public List<PokemonSpecy> pokemon_species { get; set; }
        public List<gMove> moves { get; set; }
        public MainRegion main_region { get; set; }
        public List<gType> types { get; set; }
    }



}
