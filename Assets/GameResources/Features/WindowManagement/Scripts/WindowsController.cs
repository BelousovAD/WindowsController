namespace WindowsController.Feature.WindowManagement
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using global::WindowsController.Feature.Window;
    using global::WindowsController.Feature.CommandHistory;

    /// <summary>
    /// Контроллер окон.
    /// </summary>
    public sealed class WindowsController : MonoBehaviour
    {
        /// <summary>
        /// Открылось окно.
        /// </summary>
        public event Action<Window> onWindowOpened = delegate { };

        /// <summary>
        /// Закрылось окно.
        /// </summary>
        public event Action<Window> onWindowClosed = delegate { };

        [SerializeField]
        private Transform _parent = null;
        [SerializeField]
        private List<WindowInfo> _windowInfos = new List<WindowInfo>();
        [SerializeField]
        private WindowInfo _firstWindow = null;

        private CommandHistory<BaseWindowCommand> _commandHistory = new CommandHistory<BaseWindowCommand>();

        /// <summary>
        /// Возвращает историю команд.
        /// </summary>
        /// <returns>История команд.</returns>
        public CommandHistory<BaseWindowCommand> GetCommandHistory() => _commandHistory;

        private Window _currentWindow = null;

        private void SetCurrentWindow(Window value)
        {
            if (_currentWindow != value)
            {
                if (_currentWindow != null)
                {
                    _currentWindow.IsInFocus = false;
                }

                _currentWindow = value;
            }
        }

        private Window _nextWindow = null;
        private Window NextWindow
        {
            get => _nextWindow;
            set
            {
                if (_nextWindow != value)
                {
                    _nextWindow = value;

                    if (_nextWindow != null)
                    {
                        _nextWindow.IsInFocus = true;
                    }
                }
            }
        }

        /// <summary>
        /// Открывает окно с заданным идентификатором.
        /// </summary>
        /// <param name="windowId">Идентификатор окна.</param>
        public void OpenWindow(string windowId)
        {
            WindowInfo windowInfo = FindWindowInfo(windowId);

            if (windowInfo != null)
            {
                GameObject nextWindowInstance = Instantiate(windowInfo.GetWindowPrefab(), _parent);
                nextWindowInstance.GetComponent<ButtonOpenWindow>().Initialize(this);
                NextWindow = nextWindowInstance.GetComponent<Window>();
            }
            else
            {
                Debug.LogError($"Cannot open window: " +
                    $"WindowInfo with ID \"{windowId}\" does not exist.");

                return;
            }

            SetCurrentWindow(NextWindow);
        }

        /// <summary>
        /// Закрывает окно, которое находится в фокусе.
        /// </summary>
        public void CloseWindow(string windowId)
        {
            Transform window = _parent.Find(windowId);

            if (window != null)
            {
                window.gameObject.SetActive(false);

                if (!_commandHistory.IsHistoryEmpty())
                {
                    BaseWindowCommand lastExecutedCommand = _commandHistory.GetLastExecutedCommand();
                    Transform nextWindowTransform = _parent.Find(lastExecutedCommand.GetWindowId());

                    if (nextWindowTransform != null)
                    {
                        NextWindow = nextWindowTransform.GetComponent<Window>();
                    }
                }
                else
                {
                    Debug.LogError($"Cannot close window: " +
                        $"There are no open windows");

                    return;
                }
            }
            else
            {
                Debug.LogError($"Cannot close window: " +
                    $"WindowInfo with ID \"{windowId}\" does not opened.");

                return;
            }

            SetCurrentWindow(NextWindow);
        }

        private void Start() => OpenWindow(_firstWindow.GetId());

        private void OnEnable()
        {
            _commandHistory.onExecuteCommand += HandleCommand;
            _commandHistory.onCancelCommand += HandleCommand;
        }
        
        private void OnDisable()
        {
            _commandHistory.onExecuteCommand -= HandleCommand;
            _commandHistory.onCancelCommand -= HandleCommand;
        }

        private void HandleCommand()
        {
            while (!_commandHistory.IsExecutionQueueEmpty())
            {
                BaseWindowCommand tempBaseCommand = _commandHistory.GetCommandToExecute();
                OpenWindowCommand tempCommand = new (tempBaseCommand.GetWindowsController(), tempBaseCommand.GetWindowId());
                tempCommand.Execute();
                _commandHistory.MoveCommandToHistory();
            }

            while (!_commandHistory.IsCancellationQueueEmpty())
            {
                BaseWindowCommand tempBaseCommand = _commandHistory.GetCommandToExecute();
                CloseWindowCommand tempCommand = new(tempBaseCommand.GetWindowsController(), tempBaseCommand.GetWindowId());
                tempCommand.Execute();
            }
        }

        private WindowInfo FindWindowInfo(string windowId)
        {
            foreach (WindowInfo windowInfo in _windowInfos)
            {
                if (windowInfo.GetId().Equals(windowId))
                {
                    return windowInfo;
                }
            }

            return null;
        }
    }
}
