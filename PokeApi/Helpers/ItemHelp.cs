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
    class ItemHelp
    {

        /*******************************
         * GetSingleItem()
         *******************************/
        public static void GetSingleItem(HttpClient client)
        {
            Console.WriteLine("Which Item would you like to see Details for?");
            string iId = Console.ReadLine();

            var response = client.GetAsync($"item/{iId}").Result;
            Item itemInst = response.Content.ReadAsAsync<Item>().Result;

            Console.WriteLine(
                  "\n NAME: " + itemInst.name
                + "\n COST: " + itemInst.cost
                + "\n ATTRIBUTES: "
                );

            foreach (var attr in itemInst.attributes)
            {
                Console.WriteLine($"\t {attr.name}");
            }

            Console.ReadLine();

            Extras.Menu();
        }

        /*******************************
         * GetAllItems()
         *******************************/
        public static void GetAllItems(HttpClient client)
        {
            var allItemsResp = client.GetAsync("item").Result;
            ItemCollection allItems = allItemsResp.Content.ReadAsAsync<ItemCollection>().Result;
            bool keepShopping = true;

            while (keepShopping)
            {
                Console.Clear();

                foreach (var item in allItems.results)
                {
                    Console.WriteLine(item.name.ToUpper());
                }

                var pageMovement = Extras.WriteRead(
                    "\n (N)ext page |"
                    + "(P)revious page |"
                    + "(G)et Details |"
                    + "(E)xit: ").ToUpper();

                switch (pageMovement)
                {
                    case "N":
                        allItems = allItems.GetNext(client);
                        break;
                    case "P":
                        allItems = allItems.GetPrevious(client);
                        break;
                    case "G":
                        GetSingleItem(client);
                        keepShopping = false;
                        break;
                    default:
                        Extras.Menu();
                        keepShopping = false;
                        break;
                }
            }
        }

    }
}
