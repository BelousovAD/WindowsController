namespace WindowsController.Feature.WindowManagement
{
    /// <summary>
    /// Интерфейс команды.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Выполняет команду.
        /// </summary>
        public void Execute();

        /// <summary>
        /// Отменяет команду.
        /// </summary>
        public void Undo();
    }
}
