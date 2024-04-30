namespace WindowsController.Feature.WindowManagement
{
    /// <summary>
    /// Базовый класс команды для контроллера окон.
    /// </summary>
    public class BaseWindowCommand : ICommand
    {
        protected WindowsController _windowsController = null;
        protected string _windowId = string.Empty;

        /// <summary>
        /// Создаёт экземпляр команды.
        /// </summary>
        /// <param name="windowsController">Контроллер окон.</param>
        /// <param name="windowId">Идентификатор окна.</param>
        public BaseWindowCommand(WindowsController windowsController, string windowId)
        {
            _windowsController = windowsController;
            _windowId = windowId;
        }

        /// <summary>
        /// Возвращает идентификатор окна.
        /// </summary>
        /// <returns>Идентификатор окна.</returns>
        public string GetWindowId() => _windowId;

        /// <summary>
        /// Возвращает ссылку на контроллера окон.
        /// </summary>
        /// <returns>Ссылка на контроллера окон.</returns>
        public WindowsController GetWindowsController() => _windowsController;

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        public virtual void Execute() { }
    }
}
