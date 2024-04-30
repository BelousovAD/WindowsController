namespace WindowsController.Feature.WindowManagement
{
    /// <summary>
    /// Команда открытия следующего окна.
    /// </summary>
    public class OpenWindowCommand : BaseWindowCommand
    {
        /// <summary>
        /// Создаёт экземпляр команды.
        /// </summary>
        /// <param name="windowsController">Контроллер окон.</param>
        /// <param name="windowId">Идентификатор следующего окна.</param>
        public OpenWindowCommand(WindowsController windowsController, string windowId)
            : base(windowsController, windowId) { }

        /// <summary>
        /// Открывает окно.
        /// </summary>
        public override void Execute() => _windowsController.OpenWindow(_windowId);
    }
}
