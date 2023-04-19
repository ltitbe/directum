namespace MettingsApp.Menus
{
    public abstract class SubMenu : Menu
    {
        //Общий класс для подменю. Всегда есть куда возвращаться
        protected Menu FromMenu { get; }

        public SubMenu(Menu fromMenu) : base()
        {
            FromMenu = fromMenu;
        }
    }
}
