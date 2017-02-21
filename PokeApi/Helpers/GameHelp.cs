using Newtonsoft.Json;
using PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Helpers
{
    class GameHelp
    {

        /***********************************
         * GetSingleGame()
         *  Prints single Game detail 
         ***********************************/
        public static void GetSingleGame(HttpClient client)
        {
            Console.WriteLine("Which Game/Generation would you like to see Details for?");
            string gId = Console.ReadLine();

            var response = client.GetAsync($"generation/{gId}").Result;
            Game gameInst = response.Content.ReadAsAsync<Game>().Result;

            Console.WriteLine(gameInst.name);

            string JSON = JsonConvert.SerializeObject(gameInst);
            Game obj = JsonConvert.DeserializeObject<Game>(JSON);

            foreach (gName name in obj.names)
            {
                Console.WriteLine($"{name.name} : {name.language.name}");
            }

            Console.ReadLine();

        }

        /***********************************
         * GetGamesList()
         *  Prints list of games 
         ***********************************/
        public static void GetGamesList(HttpClient client)
        {
            var allGames = client.GetAsync("generation").Result;
            GameCollection gameList = allGames.Content.ReadAsAsync<GameCollection>().Result;

            foreach (var game in gameList.results)
            {
                Console.WriteLine(game.name);
            }
        }
    }
}
