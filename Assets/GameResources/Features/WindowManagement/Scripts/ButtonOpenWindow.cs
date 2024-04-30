namespace WindowsController.Feature.WindowManagement
{
    using UnityEngine;
    using global::WindowsController.Feature.CommandHistory;

    /// <summary>
    /// Компонент вызова команд перехода между окнами.
    /// </summary>
    public class ButtonOpenWindow : MonoBehaviour
    {
        [SerializeField]
        private string _nextWindowId = string.Empty;

        private OpenWindowCommand _command = null;
        private CommandHistory<OpenWindowCommand> _commandHistory = null;

        /// <summary>
        /// Устанавливает получателя команд.
        /// </summary>
        /// <param name="windowsController">Контроллер окон.</param>
        public void Initialize(WindowsController windowsController)
        {
            _command = new OpenWindowCommand(windowsController, _nextWindowId);
            _commandHistory = windowsController.GetCommandHistory();
        }

        /// <summary>
        /// Добавляет в историю команду открытия следующего окна.
        /// </summary>
        public void OpenNextWindow() => _commandHistory.PushCommand(_command);
    }
}
