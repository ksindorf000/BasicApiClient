using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Helpers
{
    class Extras
    {

        public static HttpClient client = new HttpClient();

        /*******************************
         * SetUpClient()
         *******************************/
        internal static void SetUpClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("http://pokeapi.co/api/v2/");
        }

        /*******************************
         * Menu()
         *******************************/
        public static void Menu()
        {
            bool valid = false;

            while (!valid)
            {
                Console.Clear();

                string uInput = WriteRead(
                    "What would you like to do: \n\n"
                    + "1) Look at a Pokemon's Details, \n"
                    + "2) Look at a list of all Pokemon, \n\n"

                    + "3) Look at a Game/Generation's Details, \n"
                    + "4) Look at a list of all Games/Generations, \n\n"

                    + "5) Look at an Item's Details, \n"
                    + "6) Look at a list of Items? \n\n"
                    + "7) EXIT \n"
                    );

                switch (uInput)
                {
                    case "1": //Single Pokemon
                        PokemonHelp.GetSinglePokemon(client);
                        break;
                    case "2": //List Pokemon
                        PokemonHelp.CatchEmAll(client);
                        break;
                    case "3": //Single Game
                        GameHelp.GetSingleGame(client);
                        break;
                    case "4": //List Games
                        GameHelp.GetGamesList(client);
                        break;
                    case "5": //Single Item
                        ItemHelp.GetSingleItem(client);
                        break;
                    case "6": //List Items
                        ItemHelp.GetAllItems(client);
                        break;
                    case "7": //Exit
                        valid = false;
                        break;
                    default:
                        WriteRead("You missed! Get another PokeBall and try again.");
                        valid = false;
                        break;
                }
            }
        }
        
        /*******************************
         * WriteRead()
         *******************************/
        public static string WriteRead(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }
    }
}
