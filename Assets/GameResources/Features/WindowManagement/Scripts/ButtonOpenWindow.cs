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
        private WindowsPresenter _windowsPresenter = null;

        /// <summary>
        /// Устанавливает получателя команд.
        /// </summary>
        /// <param name="windowsPresenter">Контроллер окон.</param>
        public void Initialize(WindowsPresenter windowsPresenter)
        {
            _windowsPresenter = windowsPresenter;
            _command = new OpenWindowCommand(_windowsPresenter, _nextWindowId);
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
