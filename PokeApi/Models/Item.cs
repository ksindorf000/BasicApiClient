using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Models
{
    class Item
    {
        public Category category { get; set; }
        public string name { get; set; }
        public object fling_effect { get; set; }
        public List<EffectEntry> effect_entries { get; set; }
        public List<object> held_by_pokemon { get; set; }
        public Sprites sprites { get; set; }
        public List<GameIndice> game_indices { get; set; }
        public object baby_trigger_for { get; set; }
        public int cost { get; set; }
        public List<Name> names { get; set; }
        public List<Attribute> attributes { get; set; }
        public List<FlavorTextEntry> flavor_text_entries { get; set; }
        public int id { get; set; }
        public List<object> machines { get; set; }
        public object fling_power { get; set; }
    }

    class ItemCollection
    {
            public int count { get; set; }
            public Uri previous { get; set; }
            public List<Item> results { get; set; }
            public Uri next { get; set; }

        private ItemCollection GetPage(HttpClient client, Uri page)
        {
            if (page != null)
            {
                string pageNumber = page.Query;
                var allPokemonResp = client.GetAsync($"item/{pageNumber}").Result;
                return allPokemonResp.Content.ReadAsAsync<ItemCollection>().Result;
            }
            else
            {
                Console.WriteLine("Oops! You've reached the end. Try going back the other way.");
                Console.ReadLine();
            }

            return this;
        }

        public ItemCollection GetPrevious(HttpClient client)
        {
            return GetPage(client, previous);
        }

        public ItemCollection GetNext(HttpClient client)
        {
            return GetPage(client, next);

        }


    }

    public class Category
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Language
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class EffectEntry
    {
        public string short_effect { get; set; }
        public string effect { get; set; }
        public Language language { get; set; }
    }

    public class Sprites
    {
        public string @default { get; set; }
    }

    public class Generation
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class GameIndice
    {
        public Generation generation { get; set; }
        public int game_index { get; set; }
    }

    public class Language2
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Name
    {
        public string name { get; set; }
        public Language2 language { get; set; }
    }

    public class Attribute
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class VersionGroup
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Language3
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class FlavorTextEntry
    {
        public string text { get; set; }
        public VersionGroup version_group { get; set; }
        public Language3 language { get; set; }
    }

}
