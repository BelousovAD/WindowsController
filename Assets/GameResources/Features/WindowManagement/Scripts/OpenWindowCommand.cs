namespace WindowsController.Feature.WindowManagement
{
    /// <summary>
    /// Команда перехода между окнами.
    /// </summary>
    public class OpenWindowCommand : ICommand
    {
        private WindowsPresenter _windowsPresenter = null;
        private string _windowId = string.Empty;

        /// <summary>
        /// Создаёт экземпляр команды.
        /// </summary>
        /// <param name="windowsPresenter">Контроллер окон.</param>
        /// <param name="windowId">Идентификатор следующего окна.</param>
        public OpenWindowCommand(WindowsPresenter windowsPresenter, string windowId)
        {
            _windowsPresenter = windowsPresenter;
            _windowId = windowId;
        }

        /// <summary>
        /// Открывает следующее окно.
        /// </summary>
        public void Execute() => _windowsPresenter.OpenWindow(_windowId);

        /// <summary>
        /// Закрывает текущее окно.
        /// </summary>
        public void Undo() => _windowsPresenter.CloseWindow();
    }
}
