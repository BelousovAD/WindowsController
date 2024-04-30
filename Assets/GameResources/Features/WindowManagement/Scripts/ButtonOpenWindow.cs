namespace WindowsController.Feature.WindowManagement
{
    using UnityEngine;
    using global::WindowsController.Feature.CommandHistory;

    /// <summary>
    /// Компонент вызова команд открытия окон.
    /// </summary>
    public class ButtonOpenWindow : MonoBehaviour
    {
        [SerializeField]
        private string _nextWindowId = string.Empty;

        private BaseWindowCommand _command = null;
        private CommandHistory<BaseWindowCommand> _commandHistory = null;

        /// <summary>
        /// Устанавливает получателя команд.
        /// </summary>
        /// <param name="windowsController">Контроллер окон.</param>
        public void Initialize(WindowsController windowsController)
        {
            _command = new BaseWindowCommand(windowsController, _nextWindowId);
            _commandHistory = windowsController.GetCommandHistory();
        }

        /// <summary>
        /// Добавляет в историю команду открытия следующего окна.
        /// </summary>
        public void OpenNextWindow() => _commandHistory.AddToExecutionQueue(_command);
    }
}
