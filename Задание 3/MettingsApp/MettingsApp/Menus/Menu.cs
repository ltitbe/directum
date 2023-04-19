namespace MettingsApp.Menus
{
    public abstract class Menu
    {
        //Заголовок меню
        public abstract string Title { get; }

        //Пункты меню и информация
        public virtual List<string> Items { get; set; }

        //Общий класс для всех Меню.
        //Для очистки консоли в конструкторе и объявления общих для всех меню полей
        public Menu()
        {
            Console.Clear();
            Items = new List<string>();
        }               

        //Обработка ввода пользователя
        public abstract Menu HandleInput(string input);
    }
}
