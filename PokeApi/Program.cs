using Newtonsoft.Json;
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
                    "What would you like to do: \n"
                    + "1) Look at a Pokemon's Details, \n"
                    + "2) Look at a list of all Pokemon, \n"
                    + "3) Look at a Game's Details, \n"
                    + "4) Look at a list of Games, \n"
                    + "5) Look at an Item's Details, \n"
                    + "6) Look at a list of Items? \n"
                    + "7) EXIT \n"
                    );

                switch (uInput)
                {
                    case "1": //Single Pokemon
                        GetSinglePokemon();
                        break;
                    case "2": //List Pokemon
                        CatchEmAll();
                        break;
                    case "3": //Single Game
                        GetSingleGame();
                        break;
                    case "4": //List Games
                        GetGamesList();
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        Menu();
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
            List<string> movesList = new List<string>();

            var response = client.GetAsync($"pokemon/{pId}").Result;
            Pokemon pokemonInst = response.Content.ReadAsAsync<Pokemon>().Result;

            Console.WriteLine(
                  "\n NAME: " + pokemonInst.name
                + "\n BASE EXP: " + pokemonInst.base_experience
                + "\n HEIGHT: " + pokemonInst.height
                + "\n WEIGHT: " + pokemonInst.weight
                + "\n ORDER: " + pokemonInst.order
                + "\n DEFAULT: " + pokemonInst.is_default
                + "\n MOVES: "
                );

            foreach(var move in pokemonInst.moves)
            {
                Console.WriteLine($"\t {move.move.name}");
            }

            Console.ReadLine();

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
                    Console.WriteLine(pokemon.name.ToUpper());
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


        /***********************************
         * GetSingleGame()
         *  Prints single Game detail 
         ***********************************/
        private static void GetSingleGame()
        {            
            Console.WriteLine("Which Game/Generation would you like to see Details for?");
            string gId = Console.ReadLine();

            var response = client.GetAsync($"generation/{gId}").Result;
            Game gameInst = response.Content.ReadAsAsync<Game>().Result;

            Console.WriteLine(gameInst.name);

            string JSON = JsonConvert.SerializeObject(gameInst);
            Game obj = JsonConvert.DeserializeObject<Game>(JSON);

            foreach (Name name in obj.names)
            {
                Console.WriteLine($"{name.name} : {name.language.name}");
            }

            Console.ReadLine();

        }

        /***********************************
         * GetGamesList()
         *  Prints list of games 
         ***********************************/
        private static void GetGamesList()
        {
            var allGames = client.GetAsync("generation").Result;
            //GameCollection gameList = allGames.Content.ReadAsAsync<GameCollection>().Result;

            //foreach (var game in gameList.Results)
            //{
            //    Console.WriteLine(game.Name);
            //}
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
