using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus
{
    public abstract class SubMenu : Menu
    {
        protected Menu FromMenu { get; }

        public SubMenu(Menu fromMenu) : base()
        {
            FromMenu = fromMenu;
        }
    }
}
