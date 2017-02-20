using PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


/*
                                  , \
    _.----.        ____         ,'  _\   ___    ___     ____
_,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.
\      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |
 \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |
   \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |
    \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |
     \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |
      \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |
       \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |
        \_.-'       |__|    `-._ |              '-.|     '-.| |   |
                                `'                            '-._|
*/

namespace PokeApi
{
    class Program
    {
        public static HttpClient client = new HttpClient();

        /*******************************
         * SetUpClient()
         *******************************/
        private static void SetUpClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("http://pokeapi.co/api/v2/");
        }

        /*******************************
         * Main()
         *******************************/
        static void Main(string[] args)
        {
            SetUpClient();
            int selection = Menu();            
        }

        /*******************************
         * Menu()
         *******************************/
        static int Menu()
        {
            int selection = 0;
            bool valid = false;

            while (!valid)
            {
                Console.Clear();

                string uInput = WriteRead(
                    "Would you like to: \n"
                    + "1) Look at a Pokemon's Details, \n"
                    + "2) Look at a list of all Pokemon, \n"
                    + "3) Look at a Game's Details, \n"
                    + "4) Look at a list of Games, \n"
                    + "5) Look at an Item's Details, \n"
                    + "6) Look at a list of Items? \n"
                    );

                switch (uInput)
                {
                    case "1":
                        GetSinglePokemon();
                        valid = true;
                        break;
                    case "2":
                        CatchEmAll();
                        valid = true;
                        break;
                    case "3":
                        valid = true;
                        break;
                    case "4":
                        valid = true;
                        break;
                    case "5":
                        valid = true;
                        break;
                    case "6":
                        valid = true;
                        break;
                    default:
                        WriteRead("You missed! Get another PokeBall and try again.");
                        valid = false;
                        break;
                }
                
            }

            return selection;
        }

        /*******************************
         * GetSinglePokemon()
         *******************************/
        private static void GetSinglePokemon()
        {
            Console.WriteLine("Which Pokemon would you like to see Details for?");
            string pId = Console.ReadLine();

            var response = client.GetAsync($"pokemon/{pId}").Result;
            var pokemonInst = response.Content.ReadAsAsync<Pokemon>().Result;

            WriteRead(
                  "\n NAME: " + pokemonInst.Name
                + "\n BASE EXP: "  + pokemonInst.Base_Experience
                + "\n HEIGHT: " + pokemonInst.Height
                + "\n WEIGHT: " + pokemonInst.Weight
                + "\n ORDER: " + pokemonInst.Order
                + "\n DEFAULT: " + pokemonInst.Is_Default
                );

            Menu();
        }

        /*******************************
         * CatchEmAll()
         *******************************/
        private static void CatchEmAll()
        {
            var allPokemonResp = client.GetAsync("pokemon").Result;
            PokemonCollection catchEmAll = allPokemonResp.Content.ReadAsAsync<PokemonCollection>().Result;
            bool keepCatching = true;

            while (keepCatching)
            {
                Console.Clear();

                foreach (var pokemon in catchEmAll.Results)
                {
                    Console.WriteLine(pokemon.Name.ToUpper());
                }

                var pageMovement = WriteRead(
                    "\n (N)ext page |"
                    + "(P)revious page |"
                    + "(G)et Details |"
                    + "(E)xit: ").ToUpper();

                switch (pageMovement)
                {
                    case "N":
                        catchEmAll = catchEmAll.GetNext(client);
                        break;
                    case "P":
                        catchEmAll = catchEmAll.GetPrevious(client);
                        break;
                    case "G":
                        GetSinglePokemon();
                        keepCatching = false;
                        break;
                    default:
                        Menu();
                        keepCatching = false;
                        break;
                }
            }
        }

        /*******************************
         * WriteRead()
         *******************************/
        private static string WriteRead(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }
    }
}
