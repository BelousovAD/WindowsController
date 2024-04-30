namespace WindowsController.Feature.WindowManagement
{
    using UnityEngine;
    using global::WindowsController.Feature.CommandHistory;

    /// <summary>
    /// Компонент вызова команд закрытия окон.
    /// </summary>
    public class ButtonCloseWindow : MonoBehaviour
    {
        [SerializeField]
        private WindowsController _windowsController = null;
        private CommandHistory<BaseWindowCommand> _commandHistory = null;

        /// <summary>
        /// Устанавливает историю команд.
        /// </summary>
        private void Start() => _commandHistory = _windowsController.GetCommandHistory();

        /// <summary>
        /// Добавляет последнее открытое окно в очередь на закрытие.
        /// </summary>
        public void CloseWindow() => _commandHistory.AddToCancellationQueue();
    }
}
