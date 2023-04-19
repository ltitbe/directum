using MettingsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus
{
    //класс работает с разными меню программы
    public static class MenuHandler
    {
        private static string baseTitle => $"Программа для управления личными встречами\nТекущее время: {DateTime.Now:f}";

        //точка входа в программу
        public static void Start()
        {
            Show(new MainMenu());
        }

        public static void Show(Menu menu)
        {
            while (true) 
            {
                Console.WriteLine($"{baseTitle}\n\n{menu.Title}\n");

                //отображение пунктов меню
                foreach(var item in menu.Items)
                    Console.WriteLine(item);

                Console.Write("\nОжидание ввода пользователя: ");
                try
                {
                    var input = Console.ReadLine()?.Trim() ?? "";
                    if (input != "")
                        menu = menu.HandleInput(input);
                    else throw new Exception("Введена пустая строка");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка! {ex.Message}. Попробуйте ещё раз\n\n");
                }
            }
        }
    }
}
