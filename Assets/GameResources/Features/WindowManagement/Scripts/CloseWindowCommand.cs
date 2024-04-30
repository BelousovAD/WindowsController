namespace WindowsController.Feature.WindowManagement
{
    /// <summary>
    /// Команда закрытия окна.
    /// </summary>
    public class CloseWindowCommand : BaseWindowCommand
    {
        /// <summary>
        /// Создаёт экземпляр команды.
        /// </summary>
        /// <param name="windowsController">Контроллер окон.</param>
        /// <param name="windowId">Идентификатор следующего окна.</param>
        public CloseWindowCommand(WindowsController windowsController, string windowId)
            : base(windowsController, windowId) { }

        /// <summary>
        /// Закрывает окно.
        /// </summary>
        public override void Execute() => _windowsController.CloseWindow(_windowId);
    }
}
