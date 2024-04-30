namespace WindowsController.Feature.WindowManagement
{
    using UnityEngine;

    /// <summary>
    /// Компонент вызова команд перехода между окнами.
    /// </summary>
    public class ButtonOpenWindow : MonoBehaviour
    {
        [SerializeField]
        private string _nextWindowId = string.Empty;

        private OpenWindowCommand _command = null;
        private WindowsController _windowsController = null;

        /// <summary>
        /// Устанавливает получателя команд.
        /// </summary>
        /// <param name="windowsController">Контроллер окон.</param>
        public void Initialize(WindowsController windowsController)
        {
            _windowsController = windowsController;
            _command = new OpenWindowCommand(_windowsController, _nextWindowId);
        }

        /// <summary>
        /// Вызывает команду открытия следующего окна.
        /// </summary>
        public void OpenNextWindow() => _command.Execute();

        /// <summary>
        /// Вызывает команду закрытия текущего окна.
        /// </summary>
        public void CloseCurrentWindow() => _command.Undo();
    }
}
