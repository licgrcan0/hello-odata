using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers;

public class StoreController
{
    private static Store store;
    
    static StoreController()
    {
        store = new Store
        {
            Name = "FizzBuzz Inc.",
            Website = "https://fizzbuzz.com"
        };
    }

    public ActionResult<Store> Get()
    {
        return store;
    }
}