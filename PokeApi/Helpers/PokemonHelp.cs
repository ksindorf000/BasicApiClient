using PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Helpers
{
    class PokemonHelp
    {
        /*******************************
         * GetSinglePokemon()
         *******************************/
        public static void GetSinglePokemon(HttpClient client)
        {
            Console.WriteLine("Which Pokemon would you like to see Details for?");
            string pId = Console.ReadLine();

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

            foreach (var move in pokemonInst.moves)
            {
                Console.WriteLine($"\t {move.move.name}");
            }

            Console.ReadLine();

            Extras.Menu();
        }

        /*******************************
         * CatchEmAll()
         *******************************/
        public static void CatchEmAll(HttpClient client)
        {
            HttpResponseMessage allPokemonResp = client.GetAsync("pokemon").Result;
            PokemonCollection catchEmAll = allPokemonResp.Content.ReadAsAsync<PokemonCollection>().Result;
            bool keepCatching = true;

            while (keepCatching)
            {
                Console.Clear();

                foreach (var pokemon in catchEmAll.Results)
                {
                    Console.WriteLine(pokemon.name.ToUpper());
                }

                var pageMovement = Extras.WriteRead(
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
                        GetSinglePokemon(client);
                        keepCatching = false;
                        break;
                    default:
                        Extras.Menu();
                        keepCatching = false;
                        break;
                }
            }
        }

    }
}
