namespace WindowsController.Feature.CommandHistory
{
    using System;
    using System.Collections.Generic;
    using WindowsController.Feature.WindowManagement;

    /// <summary>
    /// История команд.
    /// </summary>
    /// <typeparam name="TCommand">Тип команды.</typeparam>
    public class CommandHistory<TCommand> where TCommand : ICommand
    {
        public event Action onExecuteCommand = delegate { };
        public event Action onCancelCommand = delegate { };

        private Stack<TCommand> _history = new Stack<TCommand>();
        private Queue<TCommand> _executionQueue = new Queue<TCommand>();
        private Queue<TCommand> _cancellationQueue = new Queue<TCommand>();

        /// <summary>
        /// Добавляет команду в очередь на исполнение.
        /// </summary>
        /// <param name="command">Команда.</param>
        public void AddToExecutionQueue(TCommand command)
        {
            _executionQueue.Enqueue(command);
            onExecuteCommand();
        }

        /// <summary>
        /// Добавляет последнюю команду из истории в очередь на отмену.
        /// </summary>
        public void AddToCancellationQueue()
        {
            if (IsExecutionQueueEmpty() && _history.Count > 0)
            {
                _cancellationQueue.Enqueue(_history.Pop());
                onCancelCommand();
            }
        }

        /// <summary>
        /// Возвращает команду из очереди на исполнение.
        /// </summary>
        /// <returns>Команда на исполнение.</returns>
        public TCommand GetCommandToExecute() => _executionQueue.Peek();

        /// <summary>
        /// Возвращает команду из очереди на отмену.
        /// </summary>
        /// <returns>Команда на отмену.</returns>
        public TCommand GetCommandToCancel() => _cancellationQueue.Peek();

        /// <summary>
        /// Перемещает команду из очереди на выполнение в историю.
        /// </summary>
        public void MoveCommandToHistory()
        {
            _history.Push(_executionQueue.Dequeue());
        }

        /// <summary>
        /// Возвращает результат проверки на пустоту очереди на исполнение.
        /// </summary>
        /// <returns>Пуста ли очередь на исполнение.</returns>
        public bool IsExecutionQueueEmpty() => _executionQueue.Count == 0;

        /// <summary>
        /// Возвращает результат проверки на пустоту очереди на отмену.
        /// </summary>
        /// <returns>Пуста ли очередь на отмену.</returns>
        public bool IsCancellationQueueEmpty() => _cancellationQueue.Count == 0;

        /// <summary>
        /// Возвращает результат проверки на пустоту истории команд.
        /// </summary>
        /// <returns>Пуста ли история команд.</returns>
        public bool IsHistoryEmpty() => _history.Count == 0;

        /// <summary>
        /// Возвращает последнюю выполненную команду.
        /// </summary>
        /// <returns>Последняя выполненная команда.</returns>
        public TCommand GetLastExecutedCommand() => _history.Peek();
    }
}
