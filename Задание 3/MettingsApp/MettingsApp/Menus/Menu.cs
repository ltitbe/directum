using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus
{
    public abstract class Menu
    {
        //Общий класс для всех Меню.
        //Для очистки консоли в конструкторе и объявления общих для всех меню полей
        public Menu()
        {
            Console.Clear();
            Items = new List<string>();
        }

        //Заголовок меню
        public abstract string Title { get; }

        //Пункты меню и информация
        public virtual List<string> Items { get; set; }

        //Обработка ввода пользователя
        public abstract Menu HandleInput(string input);
    }
}
