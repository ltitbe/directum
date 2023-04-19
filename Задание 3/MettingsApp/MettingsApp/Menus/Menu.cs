using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus
{
    public abstract class Menu
    {
        public Menu()
        {
            Console.Clear();
            Items = new List<string>();
        }

        public abstract string Title { get; }

        public virtual List<string> Items { get; set; }

        public abstract Menu HandleInput(string input);
    }
}
