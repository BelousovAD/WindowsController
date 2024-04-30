namespace WindowsController.Feature.CommandHistory
{
    using System.Collections.Generic;
    using WindowsController.Feature.WindowManagement;

    /// <summary>
    /// История команд.
    /// </summary>
    /// <typeparam name="TCommand">Тип команды.</typeparam>
    public class CommandHistory<TCommand> where TCommand : ICommand
    {
        private Stack<TCommand> _history = new Stack<TCommand>();

        /// <summary>
        /// Выполняет команду и добавляет её в историю.
        /// </summary>
        /// <param name="command">Команда.</param>
        public void PushCommand(TCommand command)
        {
            command.Execute();
            _history.Push(command);
        }

        /// <summary>
        /// Отменяет команду и удаляет её из истории.
        /// </summary>
        public void PopCommand()
        {
            if (_history.Count > 0)
            {
                TCommand command = _history.Pop();
                command?.Undo();
            }
        }
    }
}
