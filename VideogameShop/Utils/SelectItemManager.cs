using Microsoft.AspNetCore.Mvc.Rendering;
using VideogameShop.Database;
using VideogameShop.Models;

namespace VideogameShop.Utils
{
    public static class SelectItemManager
    {
        public static List<SelectListItem> ConverterListConsole()
        {
            using VideogameContext db = new();
            List<SelectListItem> list = new();
            foreach(Models.Console console in db.Consoles)
            {
                SelectListItem Item = new() { Text = console.Name, Value = console.Id.ToString() };
                list.Add(Item);
            }
            return list;
        }
    }
}
